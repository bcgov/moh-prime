using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateCollegeLicencePrefixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 33,
                column: "Validate",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 35,
                column: "Prefix",
                value: "R9");

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 36,
                column: "Prefix",
                value: "R9");

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 37,
                column: "Prefix",
                value: "R9");

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 39,
                column: "Validate",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 40,
                column: "Prefix",
                value: "R9");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 33,
                column: "Validate",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 35,
                column: "Prefix",
                value: "96");

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 36,
                column: "Prefix",
                value: "96");

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 37,
                column: "Prefix",
                value: "96");

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 39,
                column: "Validate",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 40,
                column: "Prefix",
                value: "96");
        }
    }
}
