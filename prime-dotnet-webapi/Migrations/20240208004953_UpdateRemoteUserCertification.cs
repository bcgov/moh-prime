using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateRemoteUserCertification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LicenseNumber",
                table: "RemoteUserCertification",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "PractitionerId",
                table: "RemoteUserCertification",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PractitionerId",
                table: "RemoteUserCertification");

            migrationBuilder.AlterColumn<string>(
                name: "LicenseNumber",
                table: "RemoteUserCertification",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
