using LibraryApp.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).IsRequired();
        builder.Property(b => b.ISBN).HasMaxLength(256).IsRequired();
        builder.HasIndex(b => b.ISBN).IsUnique();
        builder.Property(b => b.Title).HasMaxLength(256).IsRequired();
        builder.Property(b => b.Genre).HasMaxLength(256).IsRequired();
        builder.Property(b => b.Description).HasMaxLength(256).IsRequired();
        builder.Property(b => b.AuthorId).IsRequired();

        builder.Property(b => b.TakenAt)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(b => b.TakenAt)
        .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .IsRequired();

        builder
            .HasOne(b => b.User)
            .WithMany(u => u.Books)
            .HasForeignKey(b => b.UserId);

    }
}
