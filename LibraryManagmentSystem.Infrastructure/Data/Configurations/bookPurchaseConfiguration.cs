using LibraryManagmentSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagmentSystem.Infrastructure.Data.Configurations
{
    public class bookPurchaseConfiguration : IEntityTypeConfiguration<BookPurchase>
    {
        public void Configure(EntityTypeBuilder<BookPurchase> builder)
        {
            builder
     .HasOne(x => x.User)
     .WithMany(x => x.BookPurchases)
     .HasForeignKey(x => x.UserId)
     .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
