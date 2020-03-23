using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddedAdjudicatorToNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdjudicatorId",
                table: "AdjudicatorNote",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdjudicatorNote_AdjudicatorId",
                table: "AdjudicatorNote",
                column: "AdjudicatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdjudicatorNote_Admin_AdjudicatorId",
                table: "AdjudicatorNote",
                column: "AdjudicatorId",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdjudicatorNote_Admin_AdjudicatorId",
                table: "AdjudicatorNote");

            migrationBuilder.DropIndex(
                name: "IX_AdjudicatorNote_AdjudicatorId",
                table: "AdjudicatorNote");

            migrationBuilder.DropColumn(
                name: "AdjudicatorId",
                table: "AdjudicatorNote");
        }
    }
}
