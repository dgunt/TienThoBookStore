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
using TienThoBookStore.Infrastructure.UnitOfWork;

namespace TienThoBookStore.Application.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _bookRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public BookService( IGenericRepository<Book> bookRepo, IUnitOfWork uow, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<BookDTO> CreateAsync(BookCreateDTO dto)
        {
            var book = new Book
            {
                BookId = dto.BookId,
                Title = dto.Title,
                Author = dto.Author,
                Description = dto.Description,
                Price = dto.Price,
                PublishedDate = dto.PublishedDate,
                CategoryId = dto.CategoryId,
                CoverImage = dto.CoverImage,
                ContentFull = dto.ContentFull,
                ContentSample = dto.ContentFull
            };
            await _bookRepo.AddAsync(book);
            await _uow.SaveChangeAsync();
            return _mapper.Map<BookDTO>(book);
        }

        public async Task<bool> ExistsAsync(Guid bookId)
        {
            return await _bookRepo.Query().AnyAsync(b => b.BookId == bookId);
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            IQueryable<Book> query = _bookRepo.Query();
            query = query.Include(b => b.Category);

            var books = await query.ToListAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync(int? categoryId = null)
        {
            // 1) Khai báo query với kiểu IQueryable<Book>
            IQueryable<Book> query = _bookRepo.Query();

            // 2) Include Category (IIncludableQueryable<Book,Category> cũng implement IQueryable<Book>)
            query = query.Include(b => b.Category);

            // 3) Áp filter nếu cần
            if (categoryId.HasValue)
                query = query.Where(b => b.CategoryId == categoryId.Value);

            // 4) ToListAsync() rồi map
            var books = await query.ToListAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync(string? keyword = null)
        {
            IQueryable<Book> query = _bookRepo.Query();
            query = query.Include(b => b.Category);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var k = keyword.Trim().ToLower();
                query = query.Where(b =>
                    b.Title.ToLower().Contains(k) ||
                    b.Author.ToLower().Contains(k));
            }

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
