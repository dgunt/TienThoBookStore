using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using System.Security.Claims;
using TienThoBookStore.Application.Services.Interfaces;
using TienThoBookStore.Domain.Entities;
using TienThoBookStore.Infrastructure.Repositories;
using TienThoBookStore.Infrastructure.Repositories.Interfaces;

namespace TienThoBookStore.WebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IGenericRepository<Book> _bookRepo;
        public CartController(IOrderRepository orderRepo, IGenericRepository<Book> bookRepo)
        {
            _orderRepo = orderRepo;
            _bookRepo = bookRepo;
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid bookId)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier),
                       out var userId))
                return Unauthorized();

            var book = await _bookRepo.GetByIdAsync(bookId);
            if (book is null)
                return NotFound(new { message = "Sách không tồn tại." });

            await _orderRepo.AddItemToCartAsync(userId, book);
            int count = await _orderRepo.GetCartItemCountAsync(userId);

            // Nếu AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, count });
            }

            // Truy cập trực tiếp thì Redirect như cũ
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cartOrder = _orderRepo.GetCartByUser(userId);
            return View(cartOrder);
        }
        [HttpPost]
        public async Task<IActionResult> BuyNow(Guid bookId)
        {
            // 1) Bắt user chưa đăng nhập → Unauthorized (AJAX nhận 401)
            if (!Guid.TryParse(
                  User.FindFirstValue(ClaimTypes.NameIdentifier),
                  out var userId))
                return Unauthorized();

            // 2) Lấy Book từ repo
            var book = await _bookRepo.GetByIdAsync(bookId);
            if (book is null)
                return NotFound(new { message = "Sách không tồn tại." });

            // 3) Thêm vào giỏ nếu chưa có
            await _orderRepo.AddItemToCartAsync(userId, book);

            // 4) Nếu AJAX (header X-Requested-With), trả JSON
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // Chuyển tới trang giỏ hàng hoặc trang nào bạn muốn
                var redirectUrl = Url.Action("Index", "Cart");
                return Json(new { success = true, redirectUrl });
            }

            // 5) Nếu không AJAX, redirect như bình thường
            return RedirectToAction("Index");
        }
    }
}
