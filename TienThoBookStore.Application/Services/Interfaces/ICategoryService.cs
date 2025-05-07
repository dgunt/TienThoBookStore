using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Application.DTOs.CategoryDTO;

namespace TienThoBookStore.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<CategoryDTO> CreateAsync(CategoryDTO dto);
        Task<bool> UpdateAsync(int id, CategoryDTO dto);
        Task DeleteAsync(int id);
    }
}
