using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddedSiteAdjudicator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdjudicatorId",
                table: "Site",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Site_AdjudicatorId",
                table: "Site",
                column: "AdjudicatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Admin_AdjudicatorId",
                table: "Site",
                column: "AdjudicatorId",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_Admin_AdjudicatorId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_AdjudicatorId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "AdjudicatorId",
                table: "Site");
        }
    }
}
