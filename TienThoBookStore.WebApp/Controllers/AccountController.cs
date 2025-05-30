﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Security.Claims;
using TienThoBookStore.Domain.Entities;
using TienThoBookStore.WebApp.Models;

namespace TienThoBookStore.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(
            IHttpClientFactory httpFactory,
            UserManager<AppUser> userManager)
        {
            _httpFactory = httpFactory;
            _userManager = userManager;
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            // 1) Nếu ModelState không hợp lệ (ví dụ thiếu trường, format sai)
            if (!ModelState.IsValid)
            {
                // Trả về 400 để client-side AJAX vào .fail() và render lại partial
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return PartialView("_RegisterModalContent", vm);
            }

            // 2) Gọi API tạo tài khoản
            var client = _httpFactory.CreateClient("BookApiClient");
            var apiRes = await client.PostAsJsonAsync("api/Account/register", new
            {
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                Password = vm.Password
            });

            // 3) Nếu API trả về lỗi (email trùng, lỗi khác)
            if (!apiRes.IsSuccessStatusCode)
            {
                // Đọc message từ API (anonymous JSON hoặc ErrorResponse)
                var err = await apiRes.Content.ReadFromJsonAsync<ErrorResponse>()
                          ?? new ErrorResponse { Message = "Đăng ký thất bại." };

                // Nếu lỗi do email đã tồn tại, gán vào field Email để hiển thị ngay dưới ô Email
                if (err.Message.Contains("Email đã được đăng ký"))
                    ModelState.AddModelError(nameof(vm.Email), "Email đã được đăng ký.");
                else
                    // Lỗi chung (ví dụ password không đạt yêu cầu)
                    ModelState.AddModelError(string.Empty, err.Message);

                // Trả về PartialView với status 400
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return PartialView("_RegisterModalContent", vm);
            }

            // 4) Thành công → trả JSON 200
              return Json(new
            {
                Success = true,
                Message = "Đăng ký thành công! Vui lòng kiểm tra email để xác thực.",
                RedirectUrl = Url.Action("RegisterConfirmation", "Account")});
        }

        // GET: /Account/RegisterConfirmation
        [HttpGet]
        public IActionResult RegisterConfirmation()
            => View();

        // GET: /Account/ConfirmEmail
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            var client = _httpFactory.CreateClient("BookApiClient");
            var res = await client.GetAsync(
                            $"api/Account/confirm-email?userId={userId}&token={Uri.EscapeDataString(token)}");
            var data = await res.Content.ReadFromJsonAsync<ErrorResponse>();

            var vm = new ConfirmEmailViewModel
            {
                Message = data?.Message ?? "",
                CanResend = data?.CanResend ?? false,
                Email = ""  // user sẽ nhập nếu cần resend
            };
            return View(vm);
        }

        // POST: /Account/ResendConfirmation
        [HttpPost]
        public async Task<IActionResult> ResendConfirmation(ConfirmEmailViewModel vm)
        {
            // 1) Nếu form không hợp lệ → trả về PartialView + 400
            if (!ModelState.IsValid)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return PartialView("_ResendModal", vm);
            }

            // 2) Gọi API để gửi lại link xác thực
            var client = _httpFactory.CreateClient("BookApiClient");
            var apiRes = await client.PostAsJsonAsync("api/Account/resend-confirmation", new
            {
                Email = vm.Email
            });

            // Đọc JSON lỗi hoặc message từ API
            var data = await apiRes.Content.ReadFromJsonAsync<ErrorResponse>();

            // 3) Nếu API trả về lỗi (400)… → JSON { Message } + 400
            if (!apiRes.IsSuccessStatusCode)
            {
                // BadRequestResult tự set status 400
                return BadRequest(new
                {
                    Message = data?.Message ?? "Gửi lại thất bại."
                });
            }

            // 4) Thành công → JSON { Success = true, Message } + 200
            return Ok(new
            {
                Success = true,
                Message = data.Message
            });
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            // 1) Kiểm ModelState
            if (!ModelState.IsValid)
            {
                // Trả lại partial view để render form với error
                return PartialView("_LoginModalContent", vm);
            }

            // 2) Gọi API login
            var client = _httpFactory.CreateClient("BookApiClient");
            var payload = new
            {
                Email = vm.EmailOrUserName,
                Password = vm.Password
            };
            var res = await client.PostAsJsonAsync("api/Account/login", payload);

            // 3) Xử lý khi login thất bại
            if (!res.IsSuccessStatusCode)
            {
                var err = await res.Content.ReadFromJsonAsync<ErrorResponse>()
                          ?? new ErrorResponse { Message = "Đăng nhập thất bại." };

                // Nếu API trả về có thể resend email (ví dụ link hết hạn)
                if (err.CanResend)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = err.Message,
                        CanResend = true
                    });
                }

                return Json(new
                {
                    Success = false,
                    Message = err.Message
                });
            }

            // 4) Đọc LoginResponse (chứa Token, Roles, v.v.)
            var data = await res.Content.ReadFromJsonAsync<LoginResponse>();

            // 5) Tạo claims: bắt buộc có Name, thêm các Role nếu có
            var appUser = await _userManager.FindByEmailAsync(vm.EmailOrUserName);
            if (appUser is null)
                return Json(new { Success = false, Message = "User không tồn tại." });

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new Claim(ClaimTypes.Name, vm.EmailOrUserName)
        };
            if (data?.Roles != null)
            {
                claims.AddRange(data.Roles.Select(r =>
                    new Claim(ClaimTypes.Role, r)));
            }

            // 6) Đăng nhập bằng cookie
            var identity = new ClaimsIdentity(
                claims,
                IdentityConstants.ApplicationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authProps = new AuthenticationProperties
            {
                IsPersistent = vm.RememberMe
            };
            await HttpContext.SignInAsync(
                IdentityConstants.ApplicationScheme,
                principal,
                authProps);

            // 7) Xác định redirect dựa trên role
            var isAdmin = data?.Roles?.Contains("Admin") == true;
            var redirectUrl = isAdmin
                ? Url.Action("Users", "Admin")
                : Url.Action("Index", "Home");

            // 8) Trả về JSON success kèm URL
            return Json(new
            {
                Success = true,
                RedirectUrl = redirectUrl
            });
        }


        // GET: /Account/Login (hiển thị modal)
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_LoginModal", new LoginViewModel());
        }

        // GET: /Account/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
    public class LoginResponse { public string Message { get; set; } = ""; public List<string>? Roles { get; set; } }
    // DTO để parse lỗi từ API
    public class ErrorResponse
    {
        public string Message { get; set; } = "";
        public bool CanResend { get; set; }
    }
}
