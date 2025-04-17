using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Domain.Entities
{
    public class CartItem
    {
        public int CartItemId { get; set; }    // Primary Key (có thể dùng int auto increment)
        public Guid CartId { get; set; }         // Foreign Key tới Cart
        public Guid BookId { get; set; }         // Foreign Key tới Book
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public Cart Cart { get; set; }
        public Book Book { get; set; }
    }
}
