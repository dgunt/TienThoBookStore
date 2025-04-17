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
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(c => c.CartId);

            builder.Property(c => c.Status)
                   .HasColumnName("status")
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(c => c.CreatedAt)
                   .HasColumnName("createdAt")
                   .HasDefaultValueSql("GETDATE()");

            // Quan hệ: Cart thuộc về AppUser
            builder.HasOne(c => c.User)
                   .WithMany()
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
