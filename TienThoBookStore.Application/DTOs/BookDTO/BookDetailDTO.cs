using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Application.DTOs.BookDTO
{
    public class BookDetailDTO
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public decimal Price { get; set; }
        public string CoverImage { get; set; } = default!; // URL ảnh
        public string Description { get; set; } = default!; // Mô tả sách
        public string ContentSample { get; set; } = default!; // Đọc thử

        public string ContentFull { get; set; } = default!; // PDF đầy đủ
    }
}
