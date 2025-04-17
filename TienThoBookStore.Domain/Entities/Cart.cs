using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Domain.Entities
{
    public class Cart
    {
        public Guid CartId { get; set; }       // Primary Key
        public Guid UserId { get; set; }        // Foreign Key tới AppUser
        public string Status { get; set; }      // Ví dụ: Active, Completed, Cancelled
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public AppUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
