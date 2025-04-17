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
    public class TransactionDetailConfiguration : IEntityTypeConfiguration<TransactionDetail>
    {
        public void Configure(EntityTypeBuilder<TransactionDetail> builder)
        {
            builder.ToTable("TransactionDetail");
            builder.HasKey(td => td.TransactionDetailId);

            builder.Property(td => td.Quantity)
                   .HasColumnName("quantity")
                   .IsRequired();
            builder.Property(td => td.UnitPrice)
                   .HasColumnName("unitPrice")
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Quan hệ: TransactionDetail thuộc về Transaction
            builder.HasOne(td => td.Transaction)
                   .WithMany(t => t.TransactionDetails)
                   .HasForeignKey(td => td.TransactionId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ: TransactionDetail thuộc về Book
            builder.HasOne(td => td.Book)
                   .WithMany(b => b.TransactionDetails)
                   .HasForeignKey(td => td.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
