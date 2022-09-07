using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITPLibrary.Api.Data.Migrations
{
    public partial class addbookdetailscolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "DetailId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_DetailId",
                table: "Books",
                column: "DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookDetails_DetailId",
                table: "Books",
                column: "DetailId",
                principalTable: "BookDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookDetails_DetailId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookDetails");

            migrationBuilder.DropIndex(
                name: "IX_Books_DetailId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "Books");

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Popular", "Price", "RecentlyAdded", "Thumbnail", "Title" },
                values: new object[] { 1, "Friederich Nietzsche", true, 50, false, null, "Why Am I So Clever" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Popular", "Price", "RecentlyAdded", "Thumbnail", "Title" },
                values: new object[] { 2, "Rick Riordan", false, 35, true, null, "Percy Jackson And The Olympians" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Popular", "Price", "RecentlyAdded", "Thumbnail", "Title" },
                values: new object[] { 3, "R. J. Palacio", false, 42, false, null, "Wonder" });
        }
    }
}
