using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddPrescriberIdMandatorytoLicence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrescriberIdMandatory",
                table: "LicenseLookup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 3,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 6,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 7,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 8,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 9,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 10,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 11,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 12,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 13,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 14,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 15,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 16,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 17,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 18,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 19,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 20,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 21,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 22,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 23,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 24,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 25,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 26,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 27,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 28,
                columns: new[] { "NamedInImReg", "PrescriberIdMandatory", "Validate" },
                values: new object[] { false, 1, false });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 29,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 30,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 31,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 32,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 33,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 34,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 35,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 36,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 37,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 38,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 39,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 40,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 41,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 42,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 43,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 44,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 45,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 46,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 47,
                column: "PrescriberIdMandatory",
                value: 3);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 48,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 49,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 50,
                column: "PrescriberIdMandatory",
                value: 3);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 51,
                column: "PrescriberIdMandatory",
                value: 3);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 52,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 53,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 54,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 55,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 56,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 57,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 58,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 59,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 60,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 61,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 62,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 63,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 64,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 65,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 66,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 67,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 68,
                column: "PrescriberIdMandatory",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescriberIdMandatory",
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
