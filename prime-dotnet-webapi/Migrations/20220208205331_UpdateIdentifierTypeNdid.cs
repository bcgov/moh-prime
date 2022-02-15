using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateIdentifierTypeNdid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentifierTypeLookup",
                keyColumn: "Code",
                keyValue: "2.16.840.1.113883.4.538",
                column: "Name",
                value: "NDID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentifierTypeLookup",
                keyColumn: "Code",
                keyValue: "2.16.840.1.113883.4.538",
                column: "Name",
                value: "NAPID");
        }
    }
}
