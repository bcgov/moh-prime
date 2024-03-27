using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddNewStatusCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StatusLookup",
                columns: new[] { "Code", "Name" },
                values: new object[] { 6, "Disabled" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: 6);
        }
    }
}
