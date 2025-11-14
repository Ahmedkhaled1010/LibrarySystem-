using LibraryManagmentSystem.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Infrastructure.Data.Context
{
    public class LibraryDbContext : IdentityDbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

            builder.Entity<User>().Property(m => m.invoice)
                .HasColumnType("decimal(18,2)");
        }

        public DbSet<Book> books { get; set; }
        public DbSet<BorrowRecord> borrowRecords { get; set; }
        public DbSet<OrderBook> orderBooks { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        public DbSet<BookPurchase> bookPurchases { get; set; }
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
        //        .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        //    {
        //        entry.Entity.DateModified = DateTime.Now;
        //        entry.Entity.ModifiedBy = _userService.UserId;
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.DateCreated = DateTime.Now;
        //            entry.Entity.CreatedBy = _userService.UserId;
        //        }
        //    }

        //    return base.SaveChangesAsync(cancellationToken);
        //}

    }
}
