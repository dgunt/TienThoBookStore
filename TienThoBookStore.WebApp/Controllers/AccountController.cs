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

        // POST: /Account/Register (từ modal _RegisterModal)
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_RegisterModal", vm);

            var client = _httpFactory.CreateClient("BookApiClient");
            var dto = new
            {
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                Password = vm.Password
            };

            var res = await client.PostAsJsonAsync("api/Account/register", dto);

            if (!res.IsSuccessStatusCode)
            {
                // parse JSON { Message = "..."} hoặc plain text
                string errorMsg;
                var ct = res.Content.Headers.ContentType?.MediaType;
                if (ct == "application/json")
                {
                    var obj = await res.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                    errorMsg = obj != null && obj.TryGetValue("Message", out var m) ? m : "Đăng ký thất bại.";
                }
                else
                {
                    errorMsg = await res.Content.ReadAsStringAsync();
                }
                ModelState.AddModelError("", errorMsg);
                return PartialView("_RegisterModal", vm);
            }

            // thành công: đọc JSON { Message = "..."}
            var data = await res.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            ViewBag.Message = data != null && data.TryGetValue("Message", out var m2) ? m2 : "Đăng ký thành công!";
            return View("RegisterConfirmation");

        }

        // GET: /Account/RegisterConfirmation
        [HttpGet]
        public IActionResult RegisterConfirmation()
            => View();

        // GET: /Account/ConfirmEmail?userId=...&token=...
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            var client = _httpFactory.CreateClient("BookApiClient");
            var url = $"api/Account/confirm-email?userId={userId}&token={Uri.EscapeDataString(token)}";
            var res = await client.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                ViewBag.Message = await res.Content.ReadAsStringAsync();
            }
            else
            {
                var err = await res.Content.ReadFromJsonAsync<ErrorResponse>();
                ViewBag.Message = err?.Message;
                ViewBag.CanResend = err?.CanResend ?? false;
            }

            return View("ConfirmEmail");
        }

        // POST: /Account/ResendConfirmation
        [HttpPost]
        public async Task<IActionResult> ResendConfirmation(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Vui lòng nhập email để gửi lại.");
                return View("ConfirmEmail");
            }

            var client = _httpFactory.CreateClient("BookApiClient");
            var res = await client.PostAsJsonAsync("api/Account/resend-confirmation", new { Email = email });

            ViewBag.Message = await res.Content.ReadAsStringAsync();
            ViewBag.CanResend = false;
            return View("ConfirmEmail");
        }

        // GET: /Account/Login  (dùng modal _LoginModal)
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_LoginModal", new LoginViewModel());
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return PartialView("_LoginModal", vm);

            var client = _httpFactory.CreateClient("BookApiClient");
            var loginDto = new LoginRequestDto
            {
                Email = vm.EmailOrUserName,
                Password = vm.Password
            };

            var res = await client.PostAsJsonAsync("api/Account/login", loginDto);
            if (!res.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Đăng nhập không thành công hoặc chưa xác thực.");
                return PartialView("_LoginModal", vm);
            }

            // Tạo Cookie Authentication
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, vm.EmailOrUserName) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = vm.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                });

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
    #region Helper DTOs
    // Sử dụng khi đọc lỗi trả về từ API
    public class ErrorResponse
    {
        public string Message { get; set; } = "";
        public bool CanResend { get; set; }
    }

    // Gửi lên API /api/Account/login
    public class LoginRequestDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
    #endregion

}
