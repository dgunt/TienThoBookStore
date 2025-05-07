using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TienThoBookStore.Application.DTOs.CategoryDTO;
using TienThoBookStore.Application.Services.Interfaces;

namespace TienThoBookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _catService;
        public CategoryController(ICategoryService catService)
            => _catService = catService;

        // ──────── PUBLIC ENDPOINTs ────────

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var cats = await _catService.GetAllCategoriesAsync();
            return Ok(cats);
        }
        [HttpGet("{id:int}")]
        [AllowAnonymous]                      // Cho mọi người xem detail
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var dto = await _catService.GetByIdAsync(id);
            if (dto is null)
                return NotFound();
            return Ok(dto);
        }

        // ──────── ADMIN-ONLY ENDPOINTs ────────
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create([FromBody] CategoryDTO dto)
        {
            var created = await _catService.CreateAsync(dto);
            // Trả về 201 với location header pointing to GET /{id}
            return CreatedAtAction(nameof(GetById),
                                   new { id = created.CategoryId },
                                   created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO dto)
        {
            var updated = await _catService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _catService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // ví dụ: còn sách nên không xóa được
                return BadRequest(new { Message = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}
