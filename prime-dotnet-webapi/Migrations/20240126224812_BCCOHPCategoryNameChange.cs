using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class BCCOHPCategoryNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CollegeLicenseGroupingLookup",
                keyColumn: "Code",
                keyValue: 6,
                column: "Name",
                value: "Certified Dental Assistant");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CollegeLicenseGroupingLookup",
                keyColumn: "Code",
                keyValue: 6,
                column: "Name",
                value: "Dental Assistant");
        }
    }
}
