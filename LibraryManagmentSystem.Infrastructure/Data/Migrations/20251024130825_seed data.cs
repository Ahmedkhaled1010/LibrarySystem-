using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a5f6417-9375-433e-b1dc-6c544c64640b", null, "Admin", "ADMIN" },
                    { "76cfa1c1-cfcd-433d-9d23-7fc8833bd901", null, "PUBLISHER", "PUBLISHER" },
                    { "76cfa1c1-cfcd-433d-9d23-7fc8833bd903", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "AccessFailedCount", "AccessToken", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IsVerified", "JoinDate", "LimitOfBooksCanBorrow", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TotalBorrow", "TotalBuy", "TwoFactorEnabled", "UserName", "fines", "invoice", "resetPasswordToken", "resetPasswordTokenExpires", "verificationToken" },
                values: new object[] { "d94483b9-9e0c-4b78-88d3-109500ba50f9", 0, null, "STATIC-CONCURRENCY-001", "User", "Admin@gmail.com", false, false, new DateTime(2025, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Admin", null, "ADMIN", "AQAAAAIAAYagAAAAEGS0r1Bv5L+muBQt1VSwXefbpHPsL+N2ZXZutR3Zq5OwoH3Qwdmddak8gUIsYpZl7w==", "01118227172", false, "STATIC-SECURITY-001", 0, 0, false, "admin", 0.0, 0m, null, null, null });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1a5f6417-9375-433e-b1dc-6c544c64640b", "d94483b9-9e0c-4b78-88d3-109500ba50f9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "76cfa1c1-cfcd-433d-9d23-7fc8833bd901");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "76cfa1c1-cfcd-433d-9d23-7fc8833bd903");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1a5f6417-9375-433e-b1dc-6c544c64640b", "d94483b9-9e0c-4b78-88d3-109500ba50f9" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1a5f6417-9375-433e-b1dc-6c544c64640b");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: "d94483b9-9e0c-4b78-88d3-109500ba50f9");
        }
    }
}
