using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Application.DTOs.BookDTO;

namespace TienThoBookStore.Application.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllBooksAsync();
        Task<IEnumerable<BookDTO>> GetAllBooksAsync(int? categoryId = null);
        Task<BookDetailDTO?> GetBookByIdAsync(Guid bookId);
        Task<IEnumerable<BookDTO>> GetAllBooksAsync(string? keyword = null);


    }
}
