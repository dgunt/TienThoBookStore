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
        // GET /Book?keyword=...
        [HttpGet]
        public async Task<IActionResult> Index(string? keyword)
        {
            var client = _httpFactory.CreateClient("BookApiClient");
            // encode keyword
            var url = string.IsNullOrWhiteSpace(keyword)
              ? "api/Book"
              : $"api/Book/search?keyword={Uri.EscapeDataString(keyword.Trim())}";

            var books = await client.GetFromJsonAsync<IEnumerable<BookDTO>>(url)
                     ?? Enumerable.Empty<BookDTO>();
            // truyền keyword về view để giữ lại ô tìm kiếm
            
            ViewBag.Keyword = keyword;
            return View(books);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var client = _httpFactory.CreateClient("BookApiClient");
            var book = await client.GetFromJsonAsync<BookDetailDTO>($"api/Book/{id}");
            if (book == null) return NotFound();

            var vm = new BookDetailViewModel { Book = book };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Preview(Guid id)
        {
            // 1) Gọi API lấy chi tiết sách (đã bao gồm ContentSample = URL sample PDF)
            var client = _httpFactory.CreateClient("BookApiClient");
            var dto = await client.GetFromJsonAsync<BookDetailDTO>($"api/Book/{id}");
            if (dto == null) return NotFound();

            // 2) Map sang ViewModel
            var vm = new PreviewViewModel
            {
                Id = id,
                Title = dto.Title,
                SampleUrl = dto.ContentSample!, // chính là URL dạng "https://.../sample/{id}.pdf"
                MaxPages = 10
            };

            // 3) Trả view kèm dữ liệu
            return View(vm);
        }

    }
}
