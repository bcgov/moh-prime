using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class ReorderLicenseClassPart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 19,
                column: "Weight",
                value: 8);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 91,
                column: "Weight",
                value: 9);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 19,
                column: "Weight",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 91,
                column: "Weight",
                value: 8);
        }
    }
}
