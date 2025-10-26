using LibraryManagmentSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagmentSystem.Infrastructure.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
        .HasOne(b => b.Category)
        .WithMany(c => c.Books)
        .HasForeignKey(b => b.CategoryId)
        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
