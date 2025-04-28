using System;

namespace TienThoBookStore.WebApp.Models
{
    public class PreviewViewModel
    {
        public Guid Id { get; set; }    // BookId
        public string Title { get; set; } = "";  // Tên sách
        public string SampleUrl { get; set; } = "";  // URL tới sample PDF do WebAPI trả
        public int MaxPages { get; set; } = 10;  // Số trang đọc thử
    }
}
