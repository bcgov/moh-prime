using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class PotentialPaperEnrolmentReturnee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LinkedErolmentId",
                table: "Enrollee",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "StatusReasonLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 19, "PRIME enrolment does not match paper enrollee record" },
                    { 20, "Possible match with paper enrolment" }
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

            migrationBuilder.DropColumn(
                name: "LinkedErolmentId",
                table: "Enrollee");
        }
    }
}
