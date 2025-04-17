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
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Note");
            builder.HasKey(n => n.NoteId);

            builder.Property(n => n.PageNumber)
                   .HasColumnName("pageNumber")
                   .IsRequired();
            builder.Property(n => n.NoteText)
                   .HasColumnName("noteText");
            builder.Property(n => n.CreatedAt)
                   .HasColumnName("createdAt")
                   .HasDefaultValueSql("GETDATE()");

            // Quan hệ: Note thuộc về AppUser
            builder.HasOne(n => n.User)
                   .WithMany()
                   .HasForeignKey(n => n.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            // Quan hệ: Note thuộc về Book
            builder.HasOne(n => n.Book)
                   .WithMany(b => b.Notes)
                   .HasForeignKey(n => n.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
