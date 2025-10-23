using LibraryManagmentSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagmentSystem.Infrastructure.Data.Configurations
{
    public class OrderBookConfiguration : IEntityTypeConfiguration<OrderBook>
    {
        public void Configure(EntityTypeBuilder<OrderBook> builder)
        {
            builder.HasOne(o => o.User)
       .WithMany(u => u.Orders)
       .HasForeignKey(o => o.UserId)
       .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
