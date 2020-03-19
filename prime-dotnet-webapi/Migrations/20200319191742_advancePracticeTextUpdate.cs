using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class advancePracticeTextUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "Name",
                value: "Reproductive Health - Sexually Transmitted Infections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "Name",
                value: "Reproductive Health - STI");
        }
    }
}
