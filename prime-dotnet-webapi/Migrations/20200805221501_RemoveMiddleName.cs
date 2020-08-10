using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class RemoveMiddleName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GivenNames",
                table: "Enrollee",
                nullable: false,
                defaultValue: "");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Enrollee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Enrollee",
                type: "text",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "GivenNames",
                table: "Enrollee");
        }
    }
}
