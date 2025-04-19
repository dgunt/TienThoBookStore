using System.ComponentModel.DataAnnotations;

namespace TienThoBookStore.WebApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Tên đăng nhập hoặc email")]
        public string EmailOrUserName { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberMe { get; set; }
    }
}
