using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdatedRemoteUserCertWithLicCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LicenseCode",
                table: "RemoteUserCertification",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenseCode",
                table: "RemoteUserCertification");
        }
    }
}
