using AutoMapper;
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
    }
}
