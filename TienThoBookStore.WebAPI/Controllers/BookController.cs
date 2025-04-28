using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TienThoBookStore.Application.Services.Interfaces;
using TienThoBookStore.WebAPI.Services;

namespace TienThoBookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        private readonly IWebHostEnvironment _env;   
        private readonly PdfService _pdfService;
        public BookController(IBookService bookService,ILogger<BookController> logger,
            IWebHostEnvironment env,PdfService pdfService)
        {
            _bookService = bookService;
            _logger = logger;
            _env = env;                         
            _pdfService = pdfService;          
        }
        // GET api/Book?keyword=...
        [HttpGet("search")]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword)
        {
            var books = await _bookService.GetAllBooksAsync(keyword);
            var baseUrl = $"{Request.Scheme}://{Request.Host}/images/";
            foreach (var b in books)
                b.CoverImage = $"{baseUrl}{b.CoverImage}";
            return Ok(books);
        }

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
            // 1) Lấy thông tin sách
            var b = await _bookService.GetBookByIdAsync(id);
            if (b == null)
                return NotFound();

            // 2) Build URL cho ảnh bìa như trước
            var imageBaseUrl = $"{Request.Scheme}://{Request.Host}/images/";
            b.CoverImage = $"{imageBaseUrl}{b.CoverImage}";

            // 3) Xác định đường dẫn vật lý đến PDF full và sample
            var fullPdfPath = Path.Combine(_env.WebRootPath, "files", "pdfs", "full", b.ContentFull!);
            var samplePdfPath = Path.Combine(_env.WebRootPath, "files", "pdfs", "sample", b.ContentSample!);

            // 4) Nếu sample chưa có, tạo file sample 10 trang đầu
            if (!System.IO.File.Exists(samplePdfPath))
            {
                _pdfService.CreateSample(fullPdfPath, samplePdfPath, pages: 10);
            }

            // 5) Build URL public cho sample PDF
            var sampleBaseUrl = $"{Request.Scheme}://{Request.Host}/files/pdfs/sample/";
            b.ContentSample = $"{sampleBaseUrl}{b.ContentSample}";

            // 6) Trả về đối tượng sách đã được gắn URL ảnh và sample PDF
            return Ok(b);
        }

    }
}
