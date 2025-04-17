using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }  // Primary Key
        public string Name { get; set; }

        // Navigation Property: Một Category có nhiều Book
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
