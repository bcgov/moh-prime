using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddHealthAuthorityPasscode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Passcode",
                table: "HealthAuthorityLookup",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "HealthAuthorityLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "Passcode",
                value: "HA@PRIME");

            migrationBuilder.UpdateData(
                table: "HealthAuthorityLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "Passcode",
                value: "HA@PRIME");

            migrationBuilder.UpdateData(
                table: "HealthAuthorityLookup",
                keyColumn: "Code",
                keyValue: 3,
                column: "Passcode",
                value: "HA@PRIME");

            migrationBuilder.UpdateData(
                table: "HealthAuthorityLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "Passcode",
                value: "HA@PRIME");

            migrationBuilder.UpdateData(
                table: "HealthAuthorityLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "Passcode",
                value: "HA@PRIME");

            migrationBuilder.UpdateData(
                table: "HealthAuthorityLookup",
                keyColumn: "Code",
                keyValue: 6,
                column: "Passcode",
                value: "HA@PRIME");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passcode",
                table: "HealthAuthorityLookup");
        }
    }
}
