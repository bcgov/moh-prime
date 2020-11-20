using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdatePharmacistLicenceFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 29,
                columns: new[] { "Manual", "Validate" },
                values: new object[] { true, false });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 30,
                column: "NamedInImReg",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 31,
                column: "Validate",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 29,
                columns: new[] { "Manual", "Validate" },
                values: new object[] { false, true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 30,
                column: "NamedInImReg",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 31,
                column: "Validate",
                value: true);
        }
    }
}
