using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateCertificationAccessToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CareSettingCode",
                table: "EnrolmentCertificateAccessToken",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealthAuthorityCode",
                table: "EnrolmentCertificateAccessToken",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CareSettingCode",
                table: "EnrolmentCertificateAccessToken");

            migrationBuilder.DropColumn(
                name: "HealthAuthorityCode",
                table: "EnrolmentCertificateAccessToken");
        }
    }
}
