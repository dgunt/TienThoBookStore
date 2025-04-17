using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Domain.Entities
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }   // Primary Key
        public Guid UserId { get; set; }          // Foreign Key tới AppUser
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }        // Ví dụ: Paid, Pending, Failed

        // Nếu muốn lưu thông tin Cart gốc, bạn có thể thêm CartId
        // public Guid CartId { get; set; }

        // Navigation Properties
        public AppUser User { get; set; }
        public ICollection<TransactionDetail> TransactionDetails { get; set; } = new List<TransactionDetail>();
    }
}
