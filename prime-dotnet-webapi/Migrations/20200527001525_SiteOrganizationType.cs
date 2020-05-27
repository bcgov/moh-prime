using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class SiteOrganizationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationTypeCode",
                table: "Organization",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationTypeCode",
                table: "Organization",
                column: "OrganizationTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_OrganizationTypeLookup_OrganizationTypeCode",
                table: "Organization",
                column: "OrganizationTypeCode",
                principalTable: "OrganizationTypeLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organization_OrganizationTypeLookup_OrganizationTypeCode",
                table: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Organization_OrganizationTypeCode",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "OrganizationTypeCode",
                table: "Organization");
        }
    }
}
