using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Application.DTOs.BookDTO;
using TienThoBookStore.Application.Services.Interfaces;
using TienThoBookStore.Domain.Entities;
using TienThoBookStore.Infrastructure.Repositories;

namespace TienThoBookStore.Application.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _bookRepo;
        private readonly IMapper _mapper;

        public BookService( IGenericRepository<Book> bookRepo, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            var books = await _bookRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync(int? categoryId = null)
        {
            var query = _bookRepo.Query();         // cần Query() trả về IQueryable<Book>
            if (categoryId.HasValue)
                query = query.Where(b => b.CategoryId == categoryId.Value);

            var books = await query.ToListAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<BookDetailDTO?> GetBookByIdAsync(Guid bookId)
        {
            var book = await _bookRepo.GetByIdAsync(bookId); // giả sử repo có GetByIdAsync
            if (book == null) return null;
            return _mapper.Map<BookDetailDTO>(book);
        }
    }
}
