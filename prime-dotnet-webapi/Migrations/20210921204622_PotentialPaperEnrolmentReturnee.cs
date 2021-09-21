using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class PotentialPaperEnrolmentReturnee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StatusReasonLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 19, "PRIME enrolment does not match paper enrollee record" },
                    { 20, "Possible match with paper enrolment" },
                    { 21, "Unable to link enrollee to paper enrolment" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 21);
        }
    }
}
