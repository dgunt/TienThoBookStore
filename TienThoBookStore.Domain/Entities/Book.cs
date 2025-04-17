using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Domain.Entities
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public decimal Price { get; set; }
        public string ContentSample { get; set; }
        public string ContentFull { get; set; }
        public DateTime PublishedDate { get; set; }

        // Foreign Key: Liên kết với Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Navigation Properties cho các tương tác
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
        public ICollection<Highlight> Highlights { get; set; } = new List<Highlight>();
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public ICollection<ReadingProgress> ReadingProgresses { get; set; } = new List<ReadingProgress>();
        public ICollection<TransactionDetail> TransactionDetails { get; set; } = new List<TransactionDetail>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
