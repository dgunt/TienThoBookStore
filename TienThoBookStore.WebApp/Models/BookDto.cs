namespace TienThoBookStore.WebApp.Models
{
    public class BookDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public string CategoryName { get; set; } = "";
        public int CategoryId { get; set; }
        public string? CoverImage { get; set; }
        public string? ContentFull { get; set; }
    }
}
