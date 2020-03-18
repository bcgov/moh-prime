using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateStatusReasons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "Name",
                value: "Automatically Adjudicated");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "Name",
                value: "Manually Adjudicated");

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

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "Name",
                value: "Name discrepancy in PharmaNet practitioner table");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 6,
                column: "Name",
                value: "Birthdate discrepancy in PharmaNet practitioner table");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 7,
                column: "Name",
                value: "Listed as Non-Practicing in PharmaNet practitioner table");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 9,
                column: "Name",
                value: "Licence Class requires manual adjudication");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 12,
                column: "Name",
                value: "Admin has flagged the applicant for manual adjudication");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "Name",
                value: "Automatic");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "Name",
                value: "Manual");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 3,
                column: "Name",
                value: "PharmaNet Error, Licence could not be Validated");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "Name",
                value: "College Licence not in PharmaNet");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "Name",
                value: "Name Discrepancy with PharmaNet College Licence");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 6,
                column: "Name",
                value: "Birthdate Discrepancy with PharmaNet College Licence");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 7,
                column: "Name",
                value: "Listed as Non-Practicing on PharmaNet College Licence");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 9,
                column: "Name",
                value: "Licence Class");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 12,
                column: "Name",
                value: "Always Manual Rule Set");
        }
    }
}
