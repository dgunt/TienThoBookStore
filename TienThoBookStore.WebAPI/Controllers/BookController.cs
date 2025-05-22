using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TienThoBookStore.Application.DTOs.BookDTO;
using TienThoBookStore.Application.Services.Interfaces;
using TienThoBookStore.Domain.Entities;
using TienThoBookStore.Infrastructure.UnitOfWork;

using TienThoBookStore.WebAPI.Models;
using System.Net;
using PdfSharpCore.Pdf.IO;

namespace TienThoBookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<BookController> _logger;
        private readonly IWebHostEnvironment _env;   
        private readonly Application.Services.Implementations.PdfService _pdfService;
        public BookController(IBookService bookService, IUnitOfWork uow
            , ILogger<BookController> logger,
            IWebHostEnvironment env,Application.Services.Implementations.PdfService pdfService)
        {
            _bookService = bookService;
            _uow = uow;
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
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] BookCreateModel model)
        {
            // 1) Validate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }    

            var bookId = Guid.NewGuid();

            // 3) Save ảnh bìa
            var imgDir = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(imgDir);
            var imgExt = Path.GetExtension(model.CoverImageFile.FileName);
            var imgName = $"{bookId}{imgExt}";
            var imgPath = Path.Combine(imgDir, imgName);
            using (var fs = System.IO.File.Create(imgPath))
                await model.CoverImageFile.CopyToAsync(fs);

            // 4) Save PDF full
            var pdfDir = Path.Combine(_env.WebRootPath, "files", "pdfs", "full");
            Directory.CreateDirectory(pdfDir);
            var pdfExt = Path.GetExtension(model.ContentFile.FileName);
            var pdfName = $"{bookId}{pdfExt}";
            var pdfPath = Path.Combine(pdfDir, pdfName);
            using (var fs = System.IO.File.Create(pdfPath))
                await model.ContentFile.CopyToAsync(fs);

            //Kiểm tra số trang PDF
            using var pdfDoc = PdfReader.Open(pdfPath, PdfDocumentOpenMode.Import);
            if (pdfDoc.PageCount < 20)
            {
                // Xóa file đã lưu nếu muốn
                System.IO.File.Delete(pdfPath);

                ModelState.AddModelError(nameof(model.ContentFile),
                    $"File PDF phải có ít nhất 20 trang (hiện có {pdfDoc.PageCount}).");
                return BadRequest(ModelState);
            }

            // 5) Tạo DTO cho service
            var dto = new BookCreateDTO
            {
                BookId = bookId,
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                Price = model.Price,
                PublishedDate = model.PublishedDate,
                CategoryId = model.CategoryId,
                CoverImage = imgName,
                ContentFull = pdfName
            };

            // 6) Gọi service để thêm vào database
            var created = await _bookService.CreateAsync(dto);

            // 7) Build URL & trả về
            created.CoverImage = $"{Request.Scheme}://{Request.Host}/images/{imgName}";
            return CreatedAtAction(nameof(GetById),
                                   new { id = created.BookId },
                                   created);
        }

    }
}
