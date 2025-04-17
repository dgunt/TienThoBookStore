using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienThoBookStore.Domain.Entities
{
    public class Note
    {
        public int NoteId { get; set; }        // Primary Key
        public Guid UserId { get; set; }         // Foreign Key tới AppUser
        public Guid BookId { get; set; }         // Foreign Key tới Book
        public int PageNumber { get; set; }
        public string NoteText { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public AppUser User { get; set; }
        public Book Book { get; set; }
    }
}
