using System.ComponentModel.DataAnnotations;

namespace TienThoBookStore.WebAPI.Models
{
    public class BookCreateModel
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = "";

        [Required, StringLength(200)]
        public string Author { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        [Range(0, 1_000_000)]
        public decimal Price { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Phải chọn ảnh bìa")]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public IFormFile CoverImageFile { get; set; } = default!;

        [Required(ErrorMessage = "Phải chọn file PDF")]
        [AllowedExtensions(new[] { ".pdf" })]
        public IFormFile ContentFile { get; set; } = default!;
    }
}
