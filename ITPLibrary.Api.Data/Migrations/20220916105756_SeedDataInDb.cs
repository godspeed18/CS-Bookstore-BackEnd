using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITPLibrary.Api.Data.Migrations
{
    public partial class SeedDataInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "OrderStatuses",
               columns: new[] { "Id", "Status" },
               values: new object[,]
               {
                    { 1, "New" },
                    { 2, "Processing" },
                    { 3, "Dispatched" },
                    { 4, "Closed" }
               });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Cash" },
                    { 2, "CreditCard" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
