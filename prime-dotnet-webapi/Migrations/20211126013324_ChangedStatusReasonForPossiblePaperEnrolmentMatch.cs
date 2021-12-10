using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class ChangedStatusReasonForPossiblePaperEnrolmentMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 20,
                column: "Name",
                value: "Possible match with paper enrolment(s)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 20,
                column: "Name",
                value: "Possible match with paper enrolment");
        }
    }
}
