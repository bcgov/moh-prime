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

            // TODO: Code to go into migration once made for Health Authorities
            // Note we might have to deal with id sequence unless it is based off of site
            // Table then we are all good.
            migrationBuilder.Sql(@"
                    insert into ""CommunitySite""(
                        ""Id"",
                        ""OrganizationId"",
                        ""AdministratorPharmaNetId"",
                        ""PrivacyOfficerId"",
                        ""TechnicalSupportId"",
                        ""ProvisionerId""
                    )
                    select
                        ""Id"",
                        ""OrganizationId"",
                        ""AdministratorPharmaNetId"",
                        ""PrivacyOfficerId"",
                        ""TechnicalSupportId"",
                        ""ProvisionerId""
                    from ""Site"";
            ");
        }
    }
}
