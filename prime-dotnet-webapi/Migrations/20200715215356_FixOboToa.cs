using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class FixOboToa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserClause",
                keyColumn: "Id",
                keyValue: 8,
                column: "EnrolleeClassification",
                value: "RU");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserClause",
                keyColumn: "Id",
                keyValue: 8,
                column: "EnrolleeClassification",
                value: "OBO");
        }
    }
}
