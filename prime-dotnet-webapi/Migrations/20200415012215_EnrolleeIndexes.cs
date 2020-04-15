using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class EnrolleeIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_GPID",
                table: "Enrollee",
                column: "GPID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_HPDID",
                table: "Enrollee",
                column: "HPDID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enrollee_GPID",
                table: "Enrollee");

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_HPDID",
                table: "Enrollee");
        }
    }
}
