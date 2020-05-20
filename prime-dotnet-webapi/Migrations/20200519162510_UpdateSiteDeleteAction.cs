using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateSiteDeleteAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Organization_OrganizationId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Organization_OrganizationId",
                table: "Location",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Organization_OrganizationId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Organization_OrganizationId",
                table: "Location",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
