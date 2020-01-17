using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class licenseNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LicenseNumber",
                table: "Certification",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LicenseNumber",
                table: "Certification",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
