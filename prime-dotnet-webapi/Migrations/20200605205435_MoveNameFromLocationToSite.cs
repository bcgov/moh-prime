using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class MoveNameFromLocationToSite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Site");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Location",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Location");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Site",
                type: "text",
                nullable: true);
        }
    }
}
