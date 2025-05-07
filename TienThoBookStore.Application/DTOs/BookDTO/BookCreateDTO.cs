using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Application.DTOs.BookDTO
{
    public class BookCreateDTO
    {
        [Required]
        public Guid BookId { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = "";

        [Required, StringLength(100)]
        public string Author { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CoverImage { get; set; } = "";

        [Required]
        public string ContentFull { get; set; } = "";
    }
}
