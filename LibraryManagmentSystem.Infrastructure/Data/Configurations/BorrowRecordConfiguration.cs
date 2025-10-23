using LibraryManagmentSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagmentSystem.Infrastructure.Data.Configurations
{
    public class BorrowRecordConfiguration : IEntityTypeConfiguration<BorrowRecord>
    {
        public void Configure(EntityTypeBuilder<BorrowRecord> builder)
        {
            builder.HasOne(br => br.User)
                 .WithMany(m => m.BorrowRecords)
                 .HasForeignKey(br => br.UserId)
                 .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
