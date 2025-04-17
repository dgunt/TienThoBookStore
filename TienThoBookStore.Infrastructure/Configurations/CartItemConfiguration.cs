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
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItem");
            builder.HasKey(ci => ci.CartItemId);

            builder.Property(ci => ci.Quantity)
                   .HasColumnName("quantity")
                   .IsRequired();
            builder.Property(ci => ci.CreatedAt)
                   .HasColumnName("createdAt")
                   .HasDefaultValueSql("GETDATE()");

            // Quan hệ: CartItem thuộc về Cart
            builder.HasOne(ci => ci.Cart)
                   .WithMany(c => c.CartItems)
                   .HasForeignKey(ci => ci.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ: CartItem thuộc về Book
            builder.HasOne(ci => ci.Book)
                   .WithMany(b => b.CartItems)
                   .HasForeignKey(ci => ci.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
