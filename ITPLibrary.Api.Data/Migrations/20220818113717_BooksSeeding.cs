using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITPLibrary.Api.Data.Migrations
{
    public partial class BooksSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Name", "Price" },
                values: new object[] { 1, "Friederich Nietzsche", "Why Am I So Clever", 50 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Name", "Price" },
                values: new object[] { 2, "Rick Riordan", "Percy Jackson And The Olympians", 35 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Name", "Price" },
                values: new object[] { 3, "R. J. Palacio", "Wonder", 42 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
