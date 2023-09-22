using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddEnrolleeConsent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsentForAutoPull",
                table: "EnrolleeHealthAuthority");

            migrationBuilder.DropColumn(
                name: "ConsentForAutoPull",
                table: "EnrolleeCareSetting");

            migrationBuilder.AddColumn<bool>(
                name: "ConsentForAutoPull",
                table: "Enrollee",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsentForAutoPull",
                table: "Enrollee");

            migrationBuilder.AddColumn<bool>(
                name: "ConsentForAutoPull",
                table: "EnrolleeHealthAuthority",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConsentForAutoPull",
                table: "EnrolleeCareSetting",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
