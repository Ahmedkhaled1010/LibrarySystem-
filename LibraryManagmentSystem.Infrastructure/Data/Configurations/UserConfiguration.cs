using LibraryManagmentSystem.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagmentSystem.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {



        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();

            builder.HasData(

                new User
                {
                    Id = "d94483b9-9e0c-4b78-88d3-109500ba50f9",
                    Name = "Admin",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "Admin@gmail.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEGS0r1Bv5L+muBQt1VSwXefbpHPsL+N2ZXZutR3Zq5OwoH3Qwdmddak8gUIsYpZl7w==",
                    PhoneNumber = "01118227172",
                    JoinDate = new DateTime(2025, 10, 24),
                    ConcurrencyStamp = "STATIC-CONCURRENCY-001",
                    SecurityStamp = "STATIC-SECURITY-001"
                });
        }
    }
}
