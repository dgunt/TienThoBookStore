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
