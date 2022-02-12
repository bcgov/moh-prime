using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class FixPIdMandatoryFeb1LicenseChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 81,
                column: "PrescriberIdType",
                value: null);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 84,
                column: "PrescriberIdType",
                value: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 81,
                column: "PrescriberIdType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 84,
                column: "PrescriberIdType",
                value: 2);
        }
    }
}
