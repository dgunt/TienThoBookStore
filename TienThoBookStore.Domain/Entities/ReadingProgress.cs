using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Domain.Entities
{
    public class ReadingProgress
    {
        // Định nghĩa 2 thuộc tính làm composite key (UserId, BookId)
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }

        public int LastPage { get; set; }
        public float PercentCompleted { get; set; }
        public DateTime LastReadAt { get; set; }

        // Navigation Properties
        public AppUser User { get; set; }
        public Book Book { get; set; }
    }
}
