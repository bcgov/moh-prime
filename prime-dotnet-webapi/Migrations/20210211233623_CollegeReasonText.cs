using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class CollegeReasonText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 3,
                column: "Name",
                value: "PharmaNet Error, License could not be validated");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "Name",
                value: "College License or Practitioner ID not in PharmaNet table");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 3,
                column: "Name",
                value: "PharmaNet Error, Licence could not be validated");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "Name",
                value: "College Licence not in PharmaNet practitioner table");
        }
    }
}
