using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UPDATEBOOK2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PdfUrl",
                table: "books",
               type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
        name: "CoverImageUrl",
        table: "books",
       type: "nvarchar(max)",
        nullable: true);


            migrationBuilder.AddColumn<int>(
                name: "Pages",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "CoverImageUrl",
               table: "books");

            migrationBuilder.DropColumn(
                name: "PdfUrl",
                table: "books");

            migrationBuilder.RenameColumn(
                name: "CoverImageUrl",
                table: "books",
                newName: "CoverImage");
        }
    }
}
