using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddHealthAuthSiteTechSupportContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthAuthorityTechnicalSupportId",
                table: "HealthAuthoritySite",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthoritySite_HealthAuthorityTechnicalSupportId",
                table: "HealthAuthoritySite",
                column: "HealthAuthorityTechnicalSupportId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthAuthoritySite_HealthAuthorityContact_HealthAuthorityT~",
                table: "HealthAuthoritySite",
                column: "HealthAuthorityTechnicalSupportId",
                principalTable: "HealthAuthorityContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthAuthoritySite_HealthAuthorityContact_HealthAuthorityT~",
                table: "HealthAuthoritySite");

            migrationBuilder.DropIndex(
                name: "IX_HealthAuthoritySite_HealthAuthorityTechnicalSupportId",
                table: "HealthAuthoritySite");

            migrationBuilder.DropColumn(
                name: "HealthAuthorityTechnicalSupportId",
                table: "HealthAuthoritySite");
        }
    }
}
