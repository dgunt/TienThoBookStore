using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Domain.Entities;
using TienThoBookStore.Infrastructure.Configurations;
using System.Reflection.Emit;

namespace TienThoBookStore.Infrastructure.Contexts
{
    public class TienThoBookStoreDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public TienThoBookStoreDbContext(DbContextOptions<TienThoBookStoreDbContext> options)
            : base(options)
        {
        }

        // DbSet cho Domain Entities
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Highlight> Highlights { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ReadingProgress> ReadingProgresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Gọi base để Identity cấu hình các bảng mặc định
            base.OnModelCreating(builder);

            // Áp dụng tự động các Configuration đã tạo trong Assembly Configurations
            builder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);

            // --- SEED CATEGORY ---
            builder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Tiểu thuyết" },
                new Category { CategoryId = 2, Name = "Khoa học" },
                new Category { CategoryId = 3, Name = "Kinh tế" },
                new Category { CategoryId = 4, Name = "Lịch sử" }
            );
            // --- SEED BOOK ---
            builder.Entity<Book>().HasData(
                new Book
                {
                    BookId = new Guid("11111111-1111-1111-1111-111111111111"),
                    Title = "Dế Mèn Phiêu Lưu Ký",
                    Author = "Tô Hoài",
                    Description = "",
                    CoverImage = "cover1.jpg",
                    Price = 120000m,
                    ContentSample = "",
                    ContentFull = "",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 1
                },
                new Book
                {
                    BookId = new Guid("22222222-2222-2222-2222-222222222222"),
                    Title = "Chí Phèo",
                    Author = "Nam Cao",
                    Description = "",
                    CoverImage = "cover2.jpg",
                    Price = 150000m,
                    ContentSample = "",
                    ContentFull = "",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 1
                },
                new Book
                {
                    BookId = new Guid("33333333-3333-3333-3333-333333333333"),
                    Title = "Lão Hạc",
                    Author = "Nam Cao",
                    Description = "",
                    CoverImage = "cover3.jpg",
                    Price = 130000m,
                    ContentSample = "",
                    ContentFull = "",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 1
                },
                new Book
                {
                    BookId = new Guid("44444444-4444-4444-4444-444444444444"),
                    Title = "Tắt Đèn",
                    Author = "Ngô Tất Tố",
                    Description = "",
                    CoverImage = "cover4.jpg",
                    Price = 110000m,
                    ContentSample = "",
                    ContentFull = "",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 1
                },
                new Book
                {
                    BookId = new Guid("55555555-5555-5555-5555-555555555555"),
                    Title = "On the Origin of Species",
                    Author = "Charles Darwin",
                    Description = "",
                    CoverImage = "cover5.jpg",
                    Price = 200000m,
                    ContentSample = "",
                    ContentFull = "",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 2
                },
                new Book
                {
                    BookId = new Guid("66666666-6666-6666-6666-666666666666"),
                    Title = "The Science of Mechanics",
                    Author = "Ernst Mach",
                    Description = "",
                    CoverImage = "cover6.jpg",
                    Price = 180000m,
                    ContentSample = "",
                    ContentFull = "",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 2
                },
                new Book
                {
                    BookId = new Guid("77777777-7777-7777-7777-777777777777"),
                    Title = "The Wealth of Nations",
                    Author = "Adam Smith",
                    Description = "",
                    CoverImage = "cover7.jpg",
                    Price = 220000m,
                    ContentSample = "",
                    ContentFull = "",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 3
                },
                new Book
                {
                    BookId = new Guid("88888888-8888-8888-8888-888888888888"),
                    Title = "The Economic Consequences of the Peace",
                    Author = "John Maynard Keynes",
                    Description = "",
                    CoverImage = "cover8.jpg",
                    Price = 210000m,
                    ContentSample = "",
                    ContentFull = "",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 3
                },
                new Book
                {
                    BookId = new Guid("99999999-9999-9999-9999-999999999999"),
                    Title = "An Nam Chí Lược",
                    Author = "Lê Tắc",
                    Description = "",
                    CoverImage = "cover9.jpg",
                    Price = 90000m,
                    ContentSample = "",
                    ContentFull = "",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 4
                },
                new Book
                {
                    BookId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Title = "Việt Nam Sử Lược",
                    Author = "Trần Trọng Kim",
                    Description = "",
                    CoverImage = "cover10.jpg",
                    Price = 95000m,
                    ContentSample = "",
                    ContentFull = "",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 4
                }
            );



            // Cấu hình lại bảng AppUser nếu cần
            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable("User");
                entity.Property(u => u.Id)
                      .HasColumnName("userId")
                      .IsRequired();
                entity.Property(u => u.Name)
                      .HasColumnName("name")
                      .HasMaxLength(256)
                      .IsRequired();
                entity.Property(u => u.Email)
                      .HasColumnName("email")
                      .HasMaxLength(256)
                      .IsRequired();
                entity.Property(u => u.PasswordHash)
                      .HasColumnName("passwordHash");
                entity.Property(u => u.PhoneNumber)
                      .HasColumnName("phone");
                entity.Property(u => u.Role)
                      .HasColumnName("role")
                      .IsRequired(false); 
                entity.Property(u => u.Verified)
                      .HasColumnName("verified")
                      .HasDefaultValue(false);
                entity.Property(u => u.CreatedAt)
                      .HasColumnName("createdAt")
                      .HasDefaultValueSql("GETDATE()");
            });

            // Thay đổi tên các bảng Identity mặc định nếu cần
            builder.Entity<IdentityRole<Guid>>().ToTable("Role");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        }
    }
}
