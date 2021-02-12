using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddPrescriberIdTypetoLicence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrescriberIdType",
                table: "LicenseLookup",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 28,
                columns: new[] { "NamedInImReg", "Validate" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 32,
                column: "PrescriberIdType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 35,
                column: "PrescriberIdType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 38,
                column: "PrescriberIdType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 39,
                column: "PrescriberIdType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 41,
                column: "PrescriberIdType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 44,
                column: "PrescriberIdType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 45,
                column: "PrescriberIdType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 47,
                column: "PrescriberIdType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 50,
                column: "PrescriberIdType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 51,
                column: "PrescriberIdType",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescriberIdType",
                table: "LicenseLookup");

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 28,
                columns: new[] { "NamedInImReg", "Validate" },
                values: new object[] { true, true });
        }
    }
}
