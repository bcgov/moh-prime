using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class ChangedDeviceProviderNumberToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceProviderNumber",
                table: "Enrollee");

            migrationBuilder.AddColumn<int>(
                name: "DeviceProviderIdentifier",
                table: "Enrollee",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceProviderIdentifier",
                table: "Enrollee");

            migrationBuilder.AddColumn<string>(
                name: "DeviceProviderNumber",
                table: "Enrollee",
                type: "text",
                nullable: true);
        }
    }
}
