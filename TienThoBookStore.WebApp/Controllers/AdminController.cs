using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;
using PdfSharpCore.Pdf.IO;
using TienThoBookStore.WebApp.Models;
using TienThoBookStore.Application.DTOs.BookDTO;
using System.Net.Http.Headers;
using System.Text.Json;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IHttpClientFactory _http;
    private readonly IWebHostEnvironment _env;

    public AdminController(IHttpClientFactory http, IWebHostEnvironment env)
    {
        _http = http;
        _env = env;
    }

    // ── 1. Quản lý Người dùng ────────────────────────

    public async Task<IActionResult> Users()
    {
        var list = await _http.CreateClient("BookApiClient")
                     .GetFromJsonAsync<List<UserDto>>("api/Account/pending-users")
                 ?? new List<UserDto>();
        return View(list);
    }

    [HttpPost]
    public async Task<IActionResult> ApproveUser(string id)
    {
        await _http.CreateClient("BookApiClient")
            .PostAsync($"api/Account/approve-user/{id}", null);
        return RedirectToAction(nameof(Users));
    }
    [HttpPost]
    public async Task<IActionResult> DeactivateUser(string id)
    {
        await _http.CreateClient("BookApiClient")
            .PostAsync($"api/Account/deactivate-user/{id}", null);
        return RedirectToAction(nameof(Users));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var client = _http.CreateClient("BookApiClient");
        var res = await client.DeleteAsync($"api/Account/delete-user/{id}");
        if (!res.IsSuccessStatusCode)
        {
            // đọc lỗi
            var err = await res.Content.ReadFromJsonAsync<ErrorResponse>();
            TempData["Error"] = err?.Message;
        }
        return RedirectToAction(nameof(Users));
    }
    // ── 2. Quản lý Sách ──────────────────────────────

    public async Task<IActionResult> Books(string? keyword)
    {
        var client = _http.CreateClient("BookApiClient");
        var url = string.IsNullOrWhiteSpace(keyword)
            ? "api/Book"
            : $"api/Book/search?keyword={Uri.EscapeDataString(keyword)}";
        var books = await client.GetFromJsonAsync<List<BookDto>>(url)
                   ?? new List<BookDto>();
        // Lấy danh mục từ API
        var categories = await GetCategories();
        var dict = categories.ToDictionary(c => c.CategoryId, c => c.Name);

        // Gán CategoryName
        foreach (var b in books)
            b.CategoryName = dict.TryGetValue(b.CategoryId, out var n) ? n : "(Chưa phân loại)";
        ViewBag.Keyword = keyword;
        return View(books);
    }

    public async Task<IActionResult> CreateBook()
    {
        ViewBag.Categories = await GetCategories();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(BookCreateVm vm)
    {
        // 1) Kiểm ModelState
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await GetCategories();
            return View(vm);
        }

        // 2) Tạo client và chuẩn bị form-data
        var client = _http.CreateClient("BookApiClient");
        using var form = new MultipartFormDataContent();

        // 2.1) Các field text
        form.Add(new StringContent(vm.Title), nameof(vm.Title));
        form.Add(new StringContent(vm.Author), nameof(vm.Author));
        form.Add(new StringContent(vm.Description ?? ""), nameof(vm.Description));
        form.Add(new StringContent(vm.Price.ToString()), nameof(vm.Price));
        form.Add(new StringContent(vm.PublishedDate.ToString("yyyy-MM-dd")), nameof(vm.PublishedDate));
        form.Add(new StringContent(vm.CategoryId.ToString()), nameof(vm.CategoryId));

        // 2.2) File ảnh bìa
        if (vm.CoverImageFile != null && vm.CoverImageFile.Length > 0)
        {
            var imgContent = new StreamContent(vm.CoverImageFile.OpenReadStream());
            imgContent.Headers.ContentType = MediaTypeHeaderValue.Parse(vm.CoverImageFile.ContentType);
            // tên field trên form phải giống tên property trong BookCreateModel
            form.Add(imgContent, nameof(vm.CoverImageFile), vm.CoverImageFile.FileName);
        }

        // 2.3) File PDF nội dung
        if (vm.ContentFile != null && vm.ContentFile.Length > 0)
        {
            var pdfContent = new StreamContent(vm.ContentFile.OpenReadStream());
            pdfContent.Headers.ContentType = MediaTypeHeaderValue.Parse(vm.ContentFile.ContentType);
            form.Add(pdfContent, nameof(vm.ContentFile), vm.ContentFile.FileName);
        }

        // 3) Gửi lên WebAPI
        var response = await client.PostAsync("api/Book", form);

        // 4) Xử lý lỗi nếu có
        if (!response.IsSuccessStatusCode)
        {
            // nếu WebAPI trả về BadRequest kèm ModelState lỗi
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                //var problems = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
                //foreach (var kv in problems.Errors)
                //    foreach (var msg in kv.Value)
                //        ModelState.AddModelError(kv.Key, msg);
                var raw = await response.Content.ReadAsStringAsync();
                var dict = JsonSerializer.Deserialize<Dictionary<string, string[]>>(raw,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                           ?? new Dictionary<string, string[]>();
                foreach (var kv in dict)
                    foreach (var msg in kv.Value)
                        ModelState.AddModelError(kv.Key, msg);
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Lỗi server ({response.StatusCode})");
            }
            ViewBag.Categories = await GetCategories();
            return View(vm);
        }

        // 5) Thành công → quay về danh sách sách
        return RedirectToAction("Books");
    }

    [HttpGet]
    public async Task<IActionResult> EditBook(Guid id)
    {
        var dto = await _http.CreateClient("BookApiClient")
                      .GetFromJsonAsync<BookDto>($"api/Book/{id}");
        if (dto == null) return NotFound();
        var vm = new BookCreateVm
        {
            Title = dto.Title,
            Author = dto.Author,
            Description = dto.Description,
            Price = dto.Price,
            PublishedDate = dto.PublishedDate,
            CategoryId = dto.CategoryId,
            CoverImageFileName = dto.CoverImage,
            ContentFileName = dto.ContentFull
        };
        ViewBag.Categories = await GetCategories();
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> EditBook(Guid id, BookCreateVm vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await GetCategories();
            return View(vm);
        }
        var dto = new BookDto
        {
            BookId = id,
            Title = vm.Title,
            Author = vm.Author,
            Description = vm.Description,
            Price = vm.Price,
            PublishedDate = vm.PublishedDate,
            CategoryId = vm.CategoryId
        };
        // (tương tự upload file nếu có vm.CoverImageFile / ContentFile)
        // Upload ảnh
        if (vm.CoverImageFile?.Length > 0)
        {
            var ext = Path.GetExtension(vm.CoverImageFile.FileName);
            var fn = $"{Guid.NewGuid()}{ext}";
            var path = Path.Combine(_env.WebRootPath, "images", fn);
            using var s = System.IO.File.Create(path);
            await vm.CoverImageFile.CopyToAsync(s);
            dto.CoverImage = $"images/{fn}";
        }
        // Upload PDF
        if (vm.ContentFile?.Length > 0)
        {
            var ext = Path.GetExtension(vm.ContentFile.FileName);
            var fn = $"{Guid.NewGuid()}{ext}";
            var path = Path.Combine(_env.WebRootPath, "files/pdfs", fn);
            using var s = System.IO.File.Create(path);
            await vm.ContentFile.CopyToAsync(s);
            dto.ContentFull = $"files/pdfs/{fn}";
        }
        await _http.CreateClient("BookApiClient")
                   .PutAsJsonAsync($"api/Book/{id}", dto);
        return RedirectToAction("Books");
    }
    // GET: /Admin/DeleteBook/{id}
    [HttpGet]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var dto = await _http.CreateClient("BookApiClient")
                    .GetFromJsonAsync<BookDto>($"api/Book/{id}");
        if (dto == null) return NotFound();
        return View("DeleteBook", dto);
    }

    [HttpPost, ActionName("DeleteBook")]
    public async Task<IActionResult> DeleteBookConfirmed(Guid id)
    {
        await _http.CreateClient("BookApiClient")
                   .DeleteAsync($"api/Book/{id}");
        return RedirectToAction("Books");
    }

    // ── 3. Quản lý Danh mục ──────────────────────────

    public async Task<IActionResult> Categories()
    {
        var list = await GetCategories();
        return View(list);
    }

    public IActionResult CreateCategory() => View();

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        await _http.CreateClient("BookApiClient")
                   .PostAsJsonAsync("api/Category", vm);
        return RedirectToAction("Categories");
    }

    public async Task<IActionResult> EditCategory(int id)
    {
        var vm = await _http.CreateClient("BookApiClient")
                       .GetFromJsonAsync<CategoryDto>($"api/Category/{id}");
        if (vm == null) return NotFound();
        return View(new CategoryVm { CategoryId = vm.CategoryId, Name = vm.Name });
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(CategoryVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        await _http.CreateClient("BookApiClient")
                   .PutAsJsonAsync($"api/Category/{vm.CategoryId}", vm);
        return RedirectToAction("Categories");
    }
    // GET: /Admin/DeleteCategory/{id}
    [HttpGet]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var dto = await _http.CreateClient("BookApiClient")
                    .GetFromJsonAsync<CategoryDto>($"api/Category/{id}");
        if (dto == null) return NotFound();
        return View("DeleteCategory", dto);
    }

    [HttpPost, ActionName("DeleteCategory")]
    public async Task<IActionResult> DeleteCategoryConfirmed(int id)
    {
        var res = await _http.CreateClient("BookApiClient")
                   .DeleteAsync($"api/Category/{id}");
        if (!res.IsSuccessStatusCode)
        {
            // đọc lỗi từ API và show TempData
            var err = await res.Content.ReadFromJsonAsync<ErrorResponse>();
            TempData["Error"] = err?.Message;
        }
        return RedirectToAction("Categories");
    }

    private async Task<List<CategoryDto>> GetCategories()
        => await _http.CreateClient("BookApiClient")
             .GetFromJsonAsync<List<CategoryDto>>("api/Category")
          ?? new List<CategoryDto>();
}
