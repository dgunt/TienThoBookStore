using System.ComponentModel.DataAnnotations;

namespace TienThoBookStore.WebAPI.Models
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _exts;
        public AllowedExtensionsAttribute(string[] exts) => _exts = exts;
        protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
        {
            var file = value as IFormFile;
            if (file == null) return ValidationResult.Success;
            var ext = System.IO.Path.GetExtension(file.FileName).ToLower();
            return _exts.Contains(ext)
                ? ValidationResult.Success
                : new ValidationResult($"Chỉ cho phép các định dạng: {string.Join(", ", _exts)}");
        }
    }
}
