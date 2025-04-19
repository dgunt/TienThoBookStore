using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TienThoBookStore.Application.DTOs.BookDTO;
using TienThoBookStore.Application.DTOs.CategoryDTO;
using TienThoBookStore.WebApp.Models;

namespace TienThoBookStore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpFactory;

        public HomeController(ILogger<HomeController> logger,
                              IHttpClientFactory httpFactory)
        {
            _logger = logger;
            _httpFactory = httpFactory;
        }

        public async Task<IActionResult> Index(int? categoryId = null)
        {
            var client = _httpFactory.CreateClient("BookApiClient");

            // 1. Lấy categories
            var cats = await client.GetFromJsonAsync<List<CategoryDTO>>("api/Category")
                       ?? new List<CategoryDTO>();

            // 2. Lấy books, nếu có categoryId thì API sẽ lọc
            var url = "api/Book";
            if (categoryId.HasValue)
                url += $"?categoryId={categoryId.Value}";
            var books = await client.GetFromJsonAsync<List<BookDTO>>(url)
                       ?? new List<BookDTO>();

            // 3. Build view model
            var vm = new BookCategoryViewModel
            {
                Categories = cats,
                Books = books,
                SelectedCategoryId = categoryId
            };

            return View(vm);
        }



        public IActionResult Privacy()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new Models.ErrorViewModel
            { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
