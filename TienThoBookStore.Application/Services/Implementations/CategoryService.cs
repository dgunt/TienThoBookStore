using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Application.DTOs.CategoryDTO;
using TienThoBookStore.Application.Services.Interfaces;
using TienThoBookStore.Domain.Entities;
using TienThoBookStore.Infrastructure.Repositories;
using TienThoBookStore.Infrastructure.UnitOfWork;

namespace TienThoBookStore.Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _catRepo;
        private readonly IGenericRepository<Book> _bookRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public CategoryService(IGenericRepository<Category> catRepo,IGenericRepository<Book> bookRepo,
            IMapper mapper, IUnitOfWork uow)
        {
            _catRepo = catRepo;
            _bookRepo = bookRepo;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<CategoryDTO> CreateAsync(CategoryDTO dto)
        {
            // tạo entity từ dto (chỉ map Name, Id sẽ do DB sinh)
            var entity = new Category { Name = dto.Name };
            await _catRepo.AddAsync(entity);
            await _uow.SaveChangeAsync();

            // map ngược entity (đã có CategoryId) ra DTO để return
            return _mapper.Map<CategoryDTO>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            // 1. Kiểm xem còn sách không
            var books = await _bookRepo.FindAsync(b => b.CategoryId == id);
            if (books.Any())
                throw new InvalidOperationException("Không thể xóa: danh mục vẫn còn sách.");

            // 2. Lấy entity và xóa
            var entity = await _catRepo.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Không tìm thấy danh mục với id {id}");

            await _catRepo.Delete(entity);
            await _uow.SaveChangeAsync();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var cats = await _catRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(cats);
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var cat = await _catRepo.GetByIdAsync(id);
            if (cat == null) return null;
            return _mapper.Map<CategoryDTO>(cat);
        }

        public async Task<bool> UpdateAsync(int id, CategoryDTO dto)
        {
            var entity = await _catRepo.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.Name = dto.Name;
            await _catRepo.Update(entity);
            await _uow.SaveChangeAsync();
            return true;
        }
    }
}
