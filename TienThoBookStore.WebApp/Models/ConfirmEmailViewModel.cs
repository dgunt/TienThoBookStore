using System.ComponentModel.DataAnnotations;

namespace TienThoBookStore.WebApp.Models
{
    public class ConfirmEmailViewModel
    {
        public string Message { get; set; } = "";

        public bool CanResend { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}
