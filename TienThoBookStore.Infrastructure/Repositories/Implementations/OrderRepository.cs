using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Domain.Entities;
using TienThoBookStore.Infrastructure.Contexts;
using TienThoBookStore.Infrastructure.Repositories.Interfaces;

namespace TienThoBookStore.Infrastructure.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TienThoBookStoreDbContext _context;
        public OrderRepository(TienThoBookStoreDbContext context)
        {
            _context = context;
        }

        public Order GetCartByUser(Guid userId)
        {
            // Lấy đơn hàng ở trạng thái Pending của user
            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Book)
                .FirstOrDefault(o => o.UserId == userId && o.Status == OrderStatus.Pending);
        }

        public async Task AddItemToCartAsync(Guid userId, Book book)
        {

            bool alreadyPurchased = await _context.Orders
        .Where(o => o.UserId == userId && o.Status == OrderStatus.Paid)
        .AnyAsync(o => o.Items.Any(i => i.BookId == book.BookId));

            if (alreadyPurchased)
                return;                     // <- thoát sớm, không thêm nữa

            // Lấy giỏ hàng (include Items) hoặc tạo mới
            var order = await _context.Orders
                        .Include(o => o.Items)
                        .FirstOrDefaultAsync(o => o.UserId == userId
                                               && o.Status == OrderStatus.Pending);

            if (order is null)
            {
                order = new Order
                {
                    UserId = userId,
                    CreatedDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                    Items = new List<OrderItem>()
                };
                _context.Orders.Add(order);
            }

            // Đã có sách này trong giỏ?
            if (!order.Items.Any(i => i.BookId == book.BookId))
            {
                order.Items.Add(new OrderItem
                {
                    BookId = book.BookId,
                    Price = book.Price
                });
            }

            // Cập nhật tổng tiền
            order.TotalAmount = order.Items.Sum(i => i.Price);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCartItemCountAsync(Guid userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId && o.Status == OrderStatus.Pending)
                .SelectMany(o => o.Items)
                .CountAsync();
        }

        public void MarkOrderAsPaid(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = OrderStatus.Paid;
                order.PaidDate = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
