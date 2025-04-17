using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Domain.Entities;

namespace TienThoBookStore.Infrastructure.Configurations
{
    public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            builder.ToTable("Bookmark");
            builder.HasKey(bm => bm.BookmarkId);

            builder.Property(bm => bm.PageNumber)
                   .HasColumnName("pageNumber")
                   .IsRequired();
            builder.Property(bm => bm.CreatedAt)
                   .HasColumnName("createdAt")
                   .HasDefaultValueSql("GETDATE()");

            // Quan hệ: Bookmark thuộc về AppUser
            builder.HasOne(bm => bm.User)
                   .WithMany()
                   .HasForeignKey(bm => bm.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            // Quan hệ: Bookmark thuộc về Book
            builder.HasOne(bm => bm.Book)
                   .WithMany(b => b.Bookmarks)
                   .HasForeignKey(bm => bm.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
