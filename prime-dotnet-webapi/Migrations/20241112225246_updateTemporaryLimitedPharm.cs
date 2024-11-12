using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class updateTemporaryLimitedPharm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 370,
                column: "Manual",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 370,
                column: "Manual",
                value: false);
        }
    }
}
