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

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();  // bạn có thể vẫn giữ view riêng nếu muốn, nhưng modal sẽ chủ yếu dùng
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(vm);

            var client = _httpFactory.CreateClient("BookApiClient");
            // 1) Chuẩn bị DTO đúng tên property Email/Password
            var loginDto = new LoginRequestDto
            {
                Email = vm.EmailOrUserName,
                Password = vm.Password
            };

            // 2) Gửi lên API
            var response = await client.PostAsJsonAsync("api/Account/login", loginDto);


            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Đăng nhập không thành công. Kiểm tra lại thông tin.");
                return View(vm);
            }

            // Nếu API trả 200 OK, coi như đăng nhập thành công
            // Tạo claims chỉ với email/username (không cần token)
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, vm.EmailOrUserName)
    };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProps = new AuthenticationProperties
            {
                IsPersistent = vm.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProps
            );


            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
