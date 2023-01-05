using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class FilterVendorsByHACareType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthAuthorityVendor_HealthAuthorityOrganization_HealthAut~",
                table: "HealthAuthorityVendor");

            migrationBuilder.RenameColumn(
                name: "HealthAuthorityOrganizationId",
                table: "HealthAuthorityVendor",
                newName: "HealthAuthorityCareTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthAuthorityVendor_HealthAuthorityOrganizationId",
                table: "HealthAuthorityVendor",
                newName: "IX_HealthAuthorityVendor_HealthAuthorityCareTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthAuthorityVendor_HealthAuthorityCareType_HealthAuthori~",
                table: "HealthAuthorityVendor",
                column: "HealthAuthorityCareTypeId",
                principalTable: "HealthAuthorityCareType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthAuthorityVendor_HealthAuthorityCareType_HealthAuthori~",
                table: "HealthAuthorityVendor");

            migrationBuilder.RenameColumn(
                name: "HealthAuthorityCareTypeId",
                table: "HealthAuthorityVendor",
                newName: "HealthAuthorityOrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthAuthorityVendor_HealthAuthorityCareTypeId",
                table: "HealthAuthorityVendor",
                newName: "IX_HealthAuthorityVendor_HealthAuthorityOrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthAuthorityVendor_HealthAuthorityOrganization_HealthAut~",
                table: "HealthAuthorityVendor",
                column: "HealthAuthorityOrganizationId",
                principalTable: "HealthAuthorityOrganization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
