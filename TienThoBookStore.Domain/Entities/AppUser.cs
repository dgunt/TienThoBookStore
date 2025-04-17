using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TienThoBookStore.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        // Các thuộc tính có sẵn từ IdentityUser: Id, UserName, Email, PasswordHash, PhoneNumber, v.v.
        // Các thuộc tính bổ sung theo yêu cầu dự án
        public string Name { get; set; }
        public string? Role { get; set; }       // Cho phép null nếu không cung cấp
        public bool Verified { get; set; }     // Sử dụng cho việc xác thực, ví dụ: OTP
        public DateTime CreatedAt { get; set; }

        // Navigation Properties:
        // Một người dùng có thể có nhiều đánh giá (Reviews)
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        // Một người dùng có thể có nhiều Bookmark
        public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();

        // Một người dùng có thể có nhiều Highlight
        public ICollection<Highlight> Highlights { get; set; } = new List<Highlight>();

        // Một người dùng có thể có nhiều Note
        public ICollection<Note> Notes { get; set; } = new List<Note>();

        // Một người dùng có thể có nhiều bản ghi tiến độ đọc (ReadingProgress)
        public ICollection<ReadingProgress> ReadingProgresses { get; set; } = new List<ReadingProgress>();

        // Một người dùng có thể có nhiều Giỏ hàng (Cart)
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();

        // Một người dùng có thể thực hiện nhiều Giao dịch (Transaction)
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
