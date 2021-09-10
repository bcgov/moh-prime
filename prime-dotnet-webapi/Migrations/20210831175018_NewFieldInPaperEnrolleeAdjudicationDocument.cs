using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class NewFieldInPaperEnrolleeAdjudicationDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "EnrolleeAdjudicationDocument",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "EnrolleeAdjudicationDocument");
        }
    }
}
