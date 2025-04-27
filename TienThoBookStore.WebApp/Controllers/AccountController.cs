using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Security.Claims;
using TienThoBookStore.WebApp.Models;

namespace TienThoBookStore.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpFactory;
        public AccountController(IHttpClientFactory httpFactory)
            => _httpFactory = httpFactory;

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_RegisterModalContent", vm);

            var client = _httpFactory.CreateClient("BookApiClient");
            var res = await client.PostAsJsonAsync("api/Account/register", new
            {
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                Password = vm.Password
            });

            if (!res.IsSuccessStatusCode)
            {
                var err = await res.Content.ReadFromJsonAsync<ErrorResponse>()
                          ?? new ErrorResponse { Message = "Đăng ký thất bại." };
                ModelState.AddModelError("", err.Message);
                return PartialView("_RegisterModalContent", vm);
            }

            // thành công → trả JSON
            return Json(new { Success = true });
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
            if (!ModelState.IsValid)
                return PartialView("_ResendModal", vm);

            var client = _httpFactory.CreateClient("BookApiClient");
            var res = await client.PostAsJsonAsync("api/Account/resend-confirmation", new
            {
                Email = vm.Email
            });
            var data = await res.Content.ReadFromJsonAsync<ErrorResponse>();
            if (!res.IsSuccessStatusCode)
            {
                return Json(new { Success = false, Message = data?.Message ?? "Gửi lại thất bại." });
            }

            return Json(new { Success = true, Message = data.Message });
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            // 1) Kiểm ModelState
            if (!ModelState.IsValid)
            {
                return PartialView("_LoginModalContent", vm);
            }

            // 2) Gọi API login
            var client = _httpFactory.CreateClient("BookApiClient");
            var res = await client.PostAsJsonAsync("api/Account/login", new
            {
                Email = vm.EmailOrUserName,
                Password = vm.Password
            });

            // nếu 2xx, coi là Success
            if (res.IsSuccessStatusCode)
            {
                // tạo cookie
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, vm.EmailOrUserName) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties { IsPersistent = vm.RememberMe }
                );
                // trả JSON success
                return Json(new { Success = true });
            }

            // đọc JSON lỗi
            var err = await res.Content.ReadFromJsonAsync<ErrorResponse>()
                      ?? new ErrorResponse { Message = "Đăng nhập thất bại." };

            // nếu API báo canResend, ta trả JSON để JS biết phải show modal resend
            if (err.CanResend)
                return Json(new
                {
                    Success = false,
                    Message = err.Message,
                    CanResend = true
                });

            // những lỗi khác (mật khẩu sai / chưa có user / chờ duyệt)
            // trả partial HTML để replace modal body và show validation errors
            ModelState.AddModelError("", err.Message);
            return PartialView("_LoginModalContent", vm);
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
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }

    // DTO để parse lỗi từ API
    public class ErrorResponse
    {
        public string Message { get; set; } = "";
        public bool CanResend { get; set; }
    }
}
