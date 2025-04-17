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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(b => b.BookId);

            builder.Property(b => b.Title)
                   .HasColumnName("title")
                   .HasMaxLength(255)
                   .IsRequired();
            builder.Property(b => b.Author)
                   .HasColumnName("author")
                   .HasMaxLength(255);
            builder.Property(b => b.Description)
                   .HasColumnName("description");
            builder.Property(b => b.CoverImage)
                   .HasColumnName("coverImage");
            builder.Property(b => b.Price)
                   .HasColumnName("price")
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(b => b.ContentSample)
                   .HasColumnName("contentSample");
            builder.Property(b => b.ContentFull)
                   .HasColumnName("contentFull");
            builder.Property(b => b.PublishedDate)
                   .HasColumnName("publishedDate");

            // Quan hệ với Category
            builder.HasOne(b => b.Category)
                   .WithMany(c => c.Books)
                   .HasForeignKey(b => b.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
