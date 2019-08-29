using Microsoft.EntityFrameworkCore.Migrations;

namespace prime.Migrations
{
    public partial class SeedRegistrationNumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "public",
                table: "PharmacistRegistrationNumber",
                columns: new[] { "Id", "Number" },
                values: new object[,]
                {
                    { 1, "A0000" },
                    { 2, "A0001" },
                    { 3, "B0000" },
                    { 4, "B0001" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "PharmacistRegistrationNumber",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "PharmacistRegistrationNumber",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "PharmacistRegistrationNumber",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "PharmacistRegistrationNumber",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
