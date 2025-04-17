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
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => r.ReviewId);

            builder.Property(r => r.Rating)
                   .HasColumnName("rating")
                   .IsRequired();
            builder.Property(r => r.ReviewDate)
                   .HasColumnName("reviewDate")
                   .IsRequired();
            builder.Property(r => r.Comment)
                   .HasColumnName("comment");

            // Quan hệ: Review thuộc về AppUser
            builder.HasOne(r => r.User)
                   .WithMany()  // Nếu trong AppUser bạn chưa có navigation property Reviews
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            // Quan hệ: Review thuộc về Book
            builder.HasOne(r => r.Book)
                   .WithMany(b => b.Reviews)
                   .HasForeignKey(r => r.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
