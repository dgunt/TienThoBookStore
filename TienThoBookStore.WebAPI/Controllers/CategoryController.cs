using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TienThoBookStore.Application.Services.Interfaces;

namespace TienThoBookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _catService;
        public CategoryController(ICategoryService catService)
            => _catService = catService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cats = await _catService.GetAllCategoriesAsync();
            return Ok(cats);
        }
    }
}
