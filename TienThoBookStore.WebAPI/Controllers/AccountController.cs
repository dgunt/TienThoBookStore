using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TienThoBookStore.Application.Services.Interfaces;
using TienThoBookStore.Domain.Entities;

namespace TienThoBookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<AppUser> userManager
            , SignInManager<AppUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        // POST: api/Account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ",
                    ModelState.Values
                              .SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage));
                return BadRequest(new { Message = $"Dữ liệu không hợp lệ: {errors}" });
            }

            // 1) Kiểm trùng email sớm
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return BadRequest(new { Message = "Email đã được đăng ký." });

            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.Phone,
                CreatedAt = DateTime.UtcNow,
                EmailConfirmed = false,
                Verified = false
            };

            // 2) Tạo user
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var msg = string.Join("; ", result.Errors.Select(e => e.Description));
                return BadRequest(new { Message = msg });
            }

            // Sinh token và gửi email xác thực
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(
                nameof(ConfirmEmail), "Account",
                new { userId = user.Id, token },
                Request.Scheme);

            var html = $@"
                <p>Chào {model.Name},</p>
                <p>Click <a href=""{link}"">vào đây</a> để xác thực email. Link có giá trị 5 phút.</p>";

            await _emailSender.SendAsync(model.Email, "Xác thực email Tiến Thọ", html);

            return Ok(new { Message = "Đăng ký thành công! Vui lòng kiểm tra email để xác thực." });
        }

        // GET: api/Account/confirm-email?userId={userId}&token={token}
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return BadRequest(new { Message = "User không tồn tại. " });

            var res = await _userManager.ConfirmEmailAsync(user, token);
            if (res.Succeeded)
            {
                user.Verified = true;                     // ← tự động bật flag
                await _userManager.UpdateAsync(user);
                return Ok(new { Message = "Email đã được xác thực thành công!" });
            }    
               

            if (res.Errors.Any(e => e.Code == "InvalidToken"))
                return BadRequest(new { Message = "Link xác thực đã hết hạn.", CanResend = true });

            return BadRequest(new { Message = "Xác thực email thất bại." });
        }

        // POST: api/Account/resend-confirmation
        [HttpPost("resend-confirmation")]
        public async Task<IActionResult> ResendConfirmation([FromBody] ResendDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest(new { Message = "Email không tồn tại" });
            if (user.EmailConfirmed)
                return BadRequest(new { Message = "Email đã được xác thực" });

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(
                nameof(ConfirmEmail), "Account",
                new { userId = user.Id, token },
                Request.Scheme);

            var html = $@"
                <p>Link mới: <a href=""{link}"">Click vào đây</a>. Có giá trị 5 phút.</p>";

            await _emailSender.SendAsync(dto.Email, "Gửi lại xác thực email Tiến Thọ", html);

            return Ok(new { Message = "Đã gửi lại email xác thực, vui lòng kiểm tra hộp thư." });

        }

        // POST: api/Account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ",
                    ModelState.Values
                              .SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage));
                return BadRequest(new { Message = $"Dữ liệu không hợp lệ: {errors}" });
            }


            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new { Message = "Sai thông tin đăng nhập!" });

            if (!user.EmailConfirmed)
                return BadRequest(new { Message = "Vui lòng xác thực email trước khi đăng nhập.", CanResend = true });
            if (!user.Verified)
                return BadRequest(new { Message = "Tài khoản đang chờ admin phê duyệt." });

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
                return Ok(new { Message = "Đăng nhập thành công!" });

            return Unauthorized(new { Message = "Sai thông tin đăng nhập!" });
        }

    }
    #region DTO Models
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
    public class ResendDto
    {
        public string Email { get; set; } = "";
    }
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    #endregion
}
