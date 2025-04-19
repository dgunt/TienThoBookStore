using System.Collections.Generic;
using TienThoBookStore.Application.DTOs.BookDTO;
using TienThoBookStore.Application.DTOs.CategoryDTO;

namespace TienThoBookStore.WebApp.Models
{
    public class BookCategoryViewModel
    {
        public IEnumerable<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
        public IEnumerable<BookDTO> Books { get; set; } = new List<BookDTO>();
        public int? SelectedCategoryId { get; set; }
    }
}
