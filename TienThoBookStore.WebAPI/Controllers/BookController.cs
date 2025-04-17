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
        public async Task<IActionResult> Get()
        {
            var books = await _bookService.GetAllBooksAsync();

            // Chuyển tên file thành URL đầy đủ
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            foreach (var b in books)
                b.CoverImage = $"{baseUrl}/images/{b.CoverImage}";

            return Ok(books);
        }
    }
}
