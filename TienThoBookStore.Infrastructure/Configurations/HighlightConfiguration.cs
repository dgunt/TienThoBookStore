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
    public class HighlightConfiguration : IEntityTypeConfiguration<Highlight>
    {
        public void Configure(EntityTypeBuilder<Highlight> builder)
        {
            builder.ToTable("Highlight");
            builder.HasKey(h => h.HighlightId);

            builder.Property(h => h.PageNumber)
                   .HasColumnName("pageNumber")
                   .IsRequired();
            builder.Property(h => h.ContentExcerpt)
                   .HasColumnName("contentExcerpt");
            builder.Property(h => h.Color)
                   .HasColumnName("color")
                   .HasMaxLength(50);
            builder.Property(h => h.CreatedAt)
                   .HasColumnName("createdAt")
                   .HasDefaultValueSql("GETDATE()");

            // Quan hệ: Highlight thuộc về AppUser
            builder.HasOne(h => h.User)
                   .WithMany()
                   .HasForeignKey(h => h.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            // Quan hệ: Highlight thuộc về Book
            builder.HasOne(h => h.Book)
                   .WithMany(b => b.Highlights)
                   .HasForeignKey(h => h.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
