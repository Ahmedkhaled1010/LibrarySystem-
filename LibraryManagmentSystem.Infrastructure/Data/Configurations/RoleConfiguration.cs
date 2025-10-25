using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagmentSystem.Infrastructure.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(

                new IdentityRole
                {
                    Id = "1a5f6417-9375-433e-b1dc-6c544c64640b",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "76cfa1c1-cfcd-433d-9d23-7fc8833bd903",
                    Name = "User",
                    NormalizedName = "USER"
                },
                  new IdentityRole
                  {
                      Id = "76cfa1c1-cfcd-433d-9d23-7fc8833bd901",
                      Name = "PUBLISHER",
                      NormalizedName = "PUBLISHER"
                  }


                );
        }
    }
}
