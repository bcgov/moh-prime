using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class SiteOrganizationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationTypeCode",
                table: "Site",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Site_OrganizationTypeCode",
                table: "Site",
                column: "OrganizationTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_OrganizationTypeLookup_OrganizationTypeCode",
                table: "Site",
                column: "OrganizationTypeCode",
                principalTable: "OrganizationTypeLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_OrganizationTypeLookup_OrganizationTypeCode",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_OrganizationTypeCode",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "OrganizationTypeCode",
                table: "Site");
        }
    }
}
