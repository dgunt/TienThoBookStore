namespace TienThoBookStore.WebApp.Models
{
    public class LoginRequestDto
    {
        // Phải khớp EXACT với tên property bên WebAPI LoginModel
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
