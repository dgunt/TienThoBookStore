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

namespace TienThoBookStore.Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _catRepo;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> catRepo, IMapper mapper)
        {
            _catRepo = catRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var cats = await _catRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(cats);
        }
    }
}
