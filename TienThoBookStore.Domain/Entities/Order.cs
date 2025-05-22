using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }            // ID của người dùng (từ Identity)
        public DateTime CreatedDate { get; set; }
        public DateTime PaidDate { get; set; }  
        public OrderStatus Status { get; set; }       // Trạng thái đơn hàng (Pending, Paid, etc.)
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal TotalAmount { get; set; }
    }
    public enum OrderStatus
    {
        Pending,
        Paid,
        Canceled
    }
}
