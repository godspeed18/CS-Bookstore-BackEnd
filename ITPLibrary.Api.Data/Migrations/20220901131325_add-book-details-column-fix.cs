using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITPLibrary.Api.Data.Migrations
{
    public partial class addbookdetailscolumnfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookDetails_DetailId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "DetailId",
                table: "Books",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_DetailId",
                table: "Books",
                newName: "IX_Books_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookDetails_DepartmentId",
                table: "Books",
                column: "DepartmentId",
                principalTable: "BookDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookDetails_DepartmentId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Books",
                newName: "DetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_DepartmentId",
                table: "Books",
                newName: "IX_Books_DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookDetails_DetailId",
                table: "Books",
                column: "DetailId",
                principalTable: "BookDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
