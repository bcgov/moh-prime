using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class NewStatusReasonDuplicateEnrollee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StatusReasonLookup",
                columns: new[] { "Code", "Name" },
                values: new object[] { 23, "Enrollee has possible duplicate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 23);
        }
    }
}
