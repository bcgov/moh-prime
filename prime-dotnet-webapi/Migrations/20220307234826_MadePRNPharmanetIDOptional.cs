using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class MadePRNPharmanetIDOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 73,
                column: "PrescriberIdType",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 73,
                column: "PrescriberIdType",
                value: 2);
        }
    }
}
