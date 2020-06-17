using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class OrganizationRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Location",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "Name",
                value: "Medinet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Location",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "Name",
                value: "MediNet");
        }
    }
}
