using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class manuallyAdjudicate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ManuallyAdjudicate",
                table: "Enrollee",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManuallyAdjudicate",
                table: "Enrollee");
        }
    }
}
