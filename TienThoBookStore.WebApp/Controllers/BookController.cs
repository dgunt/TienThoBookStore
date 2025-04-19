using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using TienThoBookStore.Application.DTOs.BookDTO;
using TienThoBookStore.WebApp.Models;

namespace TienThoBookStore.WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IHttpClientFactory _httpFactory;

        public BookController(IHttpClientFactory httpFactory)
            => _httpFactory = httpFactory;

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var client = _httpFactory.CreateClient("BookApiClient");
            var book = await client.GetFromJsonAsync<BookDetailDTO>($"api/Book/{id}");
            if (book == null) return NotFound();

            var vm = new BookDetailViewModel { Book = book };
            return View(vm);
        }
    }
}
