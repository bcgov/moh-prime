using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class RemovePharmanetStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PharmaNetStatus",
                table: "EnrolmentStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PharmaNetStatus",
                table: "EnrolmentStatus",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
