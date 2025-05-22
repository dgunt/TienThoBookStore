using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Domain.Entities;

namespace TienThoBookStore.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order GetCartByUser(Guid userId);
        Task AddItemToCartAsync(Guid userId, Book book);
        Task<int> GetCartItemCountAsync(Guid userId);
        void MarkOrderAsPaid(int orderId);
        // ... các phương thức khác nếu cần
    }
}
