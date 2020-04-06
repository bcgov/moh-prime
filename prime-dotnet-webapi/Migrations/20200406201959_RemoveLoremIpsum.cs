using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class RemoveLoremIpsum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GlobalClause",
                keyColumn: "Id",
                keyValue: 1,
                column: "Clause",
                value: "");

            migrationBuilder.UpdateData(
                table: "LicenseClassClause",
                keyColumn: "Id",
                keyValue: 1,
                column: "Clause",
                value: "");

            migrationBuilder.UpdateData(
                table: "LicenseClassClause",
                keyColumn: "Id",
                keyValue: 2,
                column: "Clause",
                value: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GlobalClause",
                keyColumn: "Id",
                keyValue: 1,
                column: "Clause",
                value: "Global clause lorem, ipsum dolor sit amet consectetur adipisicing elit. Modi nihil corporis, ex totam, eos sapiente quam, sit ea iure consequatur neque harum architecto debitis adipisci molestiae fuga sed nam vitae.");

            migrationBuilder.UpdateData(
                table: "LicenseClassClause",
                keyColumn: "Id",
                keyValue: 1,
                column: "Clause",
                value: "License class clause 1 Consectetur adipisicing elit. Doloremque sit, rerum assumenda sed facere quam vel soluta suscipit esse neque quod, pariatur ea excepturi atque delectus voluptatum, modi obcaecati aliquid!");

            migrationBuilder.UpdateData(
                table: "LicenseClassClause",
                keyColumn: "Id",
                keyValue: 2,
                column: "Clause",
                value: "License class clause 2 Rerum assumenda sed facere quam vel soluta suscipit esse neque quod, pariatur ea excepturi atque delectus voluptatum, modi obcaecati aliquid!");
        }
    }
}
