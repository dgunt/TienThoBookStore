using System.ComponentModel.DataAnnotations;

namespace TienThoBookStore.WebApp.Models
{
    public class RegisterViewModel
    {
        [Required, EmailAddress] 
        public string Email { get; set; } = "";
        [Required, DataType(DataType.Password)] 
        public string Password { get; set; } = "";

        [Required, DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; } = "";

        [Required] 
        public string Name { get; set; } = "";

        [Required,Phone] 
        public string Phone { get; set; } = "";
    }
}
