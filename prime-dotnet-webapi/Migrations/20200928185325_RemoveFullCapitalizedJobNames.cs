using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class RemoveFullCapitalizedJobNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "Name",
                value: "Medical office assistant");

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "Name",
                value: "Pharmacy assistant");

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: 3,
                column: "Name",
                value: "Registration clerk");

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "Name",
                value: "Ward clerk");

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "Name",
                value: "Nursing unit assistant");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "Name",
                value: "Medical Office Assistant");

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "Name",
                value: "Pharmacy Assistant");

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: 3,
                column: "Name",
                value: "Registration Clerk");

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "Name",
                value: "Ward Clerk");

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "Name",
                value: "Nursing Unit Assistant");
        }
    }
}
