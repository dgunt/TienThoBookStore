using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Domain.Entities
{
    public class TransactionDetail
    {
        public int TransactionDetailId { get; set; }  // Primary Key (hoặc bạn có thể dùng composite key: TransactionId + BookId)
        public Guid TransactionId { get; set; }         // Foreign Key tới Transaction
        public Guid BookId { get; set; }                // Foreign Key tới Book
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation Properties
        public Transaction Transaction { get; set; }
        public Book Book { get; set; }
    }
}
