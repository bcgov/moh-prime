using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddedEnrolleeAdjudicator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdjudicatorId",
                table: "Enrollee",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_AdjudicatorId",
                table: "Enrollee",
                column: "AdjudicatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Admin_AdjudicatorId",
                table: "Enrollee",
                column: "AdjudicatorId",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Admin_AdjudicatorId",
                table: "Enrollee");

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_AdjudicatorId",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "AdjudicatorId",
                table: "Enrollee");
        }
    }
}
