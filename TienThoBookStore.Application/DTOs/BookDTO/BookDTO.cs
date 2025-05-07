using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Application.DTOs.BookDTO
{
    public class BookDTO
    {
        // ID gốc
        public Guid BookId { get; set; }

        // Thông tin cơ bản
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }

        // Ngày phát hành
        public DateTime PublishedDate { get; set; }

        // Danh mục
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";

        // Ảnh + File PDF
        public string CoverImage { get; set; } = "";
        public string ContentFull { get; set; } = "";
        public string ContentSample { get; set; } = "";
    }
}
