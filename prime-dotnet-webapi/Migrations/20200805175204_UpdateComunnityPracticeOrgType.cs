using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateComunnityPracticeOrgType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "Name",
                value: "Private Community Health Practice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "Name",
                value: "Community Practice");
        }
    }
}
