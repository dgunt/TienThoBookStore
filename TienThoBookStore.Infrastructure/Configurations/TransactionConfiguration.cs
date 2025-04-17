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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");
            builder.HasKey(t => t.TransactionId);

            builder.Property(t => t.TotalAmount)
                   .HasColumnName("totalAmount")
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(t => t.PaymentDate)
                   .HasColumnName("paymentDate")
                   .IsRequired();
            builder.Property(t => t.Status)
                   .HasColumnName("status")
                   .HasMaxLength(100)
                   .IsRequired();

            // Quan hệ: Transaction thuộc về AppUser
            builder.HasOne(t => t.User)
                   .WithMany()
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
