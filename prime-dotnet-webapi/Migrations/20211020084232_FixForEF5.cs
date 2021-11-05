using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class FixForEF5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthAuthorityContact_HealthAuthorityOrganization_HealthA~1",
                table: "HealthAuthorityContact");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthAuthorityContact_HealthAuthorityOrganization_HealthA~2",
                table: "HealthAuthorityContact");

            migrationBuilder.DropIndex(
                name: "IX_HealthAuthorityContact_HealthAuthorityOrganizationId1",
                table: "HealthAuthorityContact");

            migrationBuilder.DropIndex(
                name: "IX_HealthAuthorityContact_HealthAuthorityOrganizationId2",
                table: "HealthAuthorityContact");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthorityContact_HealthAuthorityOrganizationId1",
                table: "HealthAuthorityContact",
                column: "HealthAuthorityOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthorityContact_HealthAuthorityOrganizationId2",
                table: "HealthAuthorityContact",
                column: "HealthAuthorityOrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthAuthorityContact_HealthAuthorityOrganization_HealthA~1",
                table: "HealthAuthorityContact",
                column: "HealthAuthorityOrganizationId",
                principalTable: "HealthAuthorityOrganization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthAuthorityContact_HealthAuthorityOrganization_HealthA~2",
                table: "HealthAuthorityContact",
                column: "HealthAuthorityOrganizationId",
                principalTable: "HealthAuthorityOrganization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
