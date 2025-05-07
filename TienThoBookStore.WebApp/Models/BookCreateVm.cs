using System.ComponentModel.DataAnnotations;

namespace TienThoBookStore.WebApp.Models
{
    public class BookCreateVm : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Tác giả không được để trống")]
        public string Author { get; set; } = "";
        
        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Description { get; set; } = "";

        [Range(1_000, 1_000_000, ErrorMessage = "Giá phải từ 1.000 đến 1.000.000 VNĐ")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Phải chọn ngày phát hành")]
        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }

        [Required(ErrorMessage = "Phải chọn danh mục")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Phải chọn file ảnh bìa")]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public IFormFile? CoverImageFile { get; set; }

        [Required(ErrorMessage = "Phải chọn file PDF nội dung")]
        [AllowedExtensions(new[] { ".pdf" })]
        public IFormFile? ContentFile { get; set; }

        // để hiển thị tên file cũ khi Edit
        public string? CoverImageFileName { get; set; }
        public string? ContentFileName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PublishedDate.Date >= DateTime.Today)
            {
                yield return new ValidationResult(
                    "Ngày phát hành phải trước ngày hiện tại",
                    new[] { nameof(PublishedDate) }
                );
            }
        }
    }
}
