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
    public class ReadingProgressConfiguration : IEntityTypeConfiguration<ReadingProgress>
    {
        public void Configure(EntityTypeBuilder<ReadingProgress> builder)
        {
            builder.ToTable("ReadingProgress");
            // Sử dụng composite key (UserId, BookId)
            builder.HasKey(rp => new { rp.UserId, rp.BookId });

            builder.Property(rp => rp.LastPage)
                   .HasColumnName("lastPage")
                   .IsRequired();
            builder.Property(rp => rp.PercentCompleted)
                   .HasColumnName("percentCompleted")
                   .IsRequired();
            builder.Property(rp => rp.LastReadAt)
                   .HasColumnName("lastReadAt")
                   .IsRequired();

            // Quan hệ: ReadingProgress liên kết với AppUser
            builder.HasOne(rp => rp.User)
                   .WithMany()
                   .HasForeignKey(rp => rp.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            // Quan hệ: ReadingProgress liên kết với Book
            builder.HasOne(rp => rp.Book)
                   .WithMany(b => b.ReadingProgresses)
                   .HasForeignKey(rp => rp.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
