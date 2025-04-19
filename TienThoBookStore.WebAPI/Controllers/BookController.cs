using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TienThoBookStore.Application.Services.Interfaces;

namespace TienThoBookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
            => _bookService = bookService;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? categoryId)
        {
            var books = await _bookService.GetAllBooksAsync(categoryId);
            var baseUrl = $"{Request.Scheme}://{Request.Host}/images/";
            foreach (var b in books)
                b.CoverImage = $"{baseUrl}{b.CoverImage}";
            return Ok(books);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var b = await _bookService.GetBookByIdAsync(id);
            if (b == null) return NotFound();
            // build URL ảnh
            var baseUrl = $"{Request.Scheme}://{Request.Host}/images/";
            b.CoverImage = $"{baseUrl}{b.CoverImage}";
            return Ok(b);
        }

    }
}
