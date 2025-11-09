using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBook2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopiesAvailable",
                table: "books");

            migrationBuilder.DropColumn(
                name: "CopiesForSaleAvailable",
                table: "books");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailableForSale",
                table: "books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "books");

            migrationBuilder.DropColumn(
                name: "IsAvailableForSale",
                table: "books");

            migrationBuilder.AddColumn<int>(
                name: "CopiesAvailable",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CopiesForSaleAvailable",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
