using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITPLibrary.Api.Data.Migrations
{
    public partial class bookdetailsforeignkeyrename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookDetails_DepartmentId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_DepartmentId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "DetailId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.DropIndex(
                name: "IX_Books_DetailId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Books_DepartmentId",
                table: "Books",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookDetails_DepartmentId",
                table: "Books",
                column: "DepartmentId",
                principalTable: "BookDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
