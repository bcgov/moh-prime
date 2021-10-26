using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class ChangedToDeviceProviderIdentifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceProviderNumber",
                table: "Enrollee",
                newName: "DeviceProviderIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceProviderIdentifier",
                table: "Enrollee",
                newName: "DeviceProviderNumber");
        }
    }
}
