using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class HealthAuthoritySiteV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // -----------------------------------------------
            // Drop FK and Indexes specific to Community Sites
            // 5 on Site + Business Licence and Site Vendor
            // -----------------------------------------------

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessLicence_Site_SiteId",
                table: "BusinessLicence");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Contact_AdministratorPharmaNetId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Contact_PrivacyOfficerId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Contact_TechnicalSupportId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Organization_OrganizationId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Party_ProvisionerId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteVendor_Site_SiteId",
                table: "SiteVendor");

            migrationBuilder.DropIndex(
                name: "IX_Site_AdministratorPharmaNetId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_OrganizationId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_PrivacyOfficerId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_ProvisionerId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_TechnicalSupportId",
                table: "Site");


            // ------------------------------
            // Migrate Site -> Community Site
            // ------------------------------

            migrationBuilder.CreateTable(
                name: "CommunitySite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false),
                    AdministratorPharmaNetId = table.Column<int>(type: "integer", nullable: true),
                    PrivacyOfficerId = table.Column<int>(type: "integer", nullable: true),
                    TechnicalSupportId = table.Column<int>(type: "integer", nullable: true),
                    ProvisionerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunitySite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunitySite_Contact_AdministratorPharmaNetId",
                        column: x => x.AdministratorPharmaNetId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommunitySite_Contact_PrivacyOfficerId",
                        column: x => x.PrivacyOfficerId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommunitySite_Contact_TechnicalSupportId",
                        column: x => x.TechnicalSupportId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommunitySite_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunitySite_Party_ProvisionerId",
                        column: x => x.ProvisionerId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommunitySite_Site_Id",
                        column: x => x.Id,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessLicence_CommunitySite_SiteId",
                table: "BusinessLicence",
                column: "SiteId",
                principalTable: "CommunitySite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteVendor_CommunitySite_SiteId",
                table: "SiteVendor",
                column: "SiteId",
                principalTable: "CommunitySite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


            // ---------------------
            // Drop old Site columns
            // ---------------------

            migrationBuilder.DropColumn(
                name: "AdministratorPharmaNetId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "PrivacyOfficerId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "ProvisionerId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "TechnicalSupportId",
                table: "Site");


            // ------------------------------
            // Remainder HA stuff and indexes
            // ------------------------------

            migrationBuilder.CreateTable(
                name: "HealthAuthoritySite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SiteName = table.Column<string>(type: "text", nullable: true),
                    SecurityGroupCode = table.Column<int>(type: "integer", nullable: false),
                    HealthAuthorityOrganizationId = table.Column<int>(type: "integer", nullable: false),
                    HealthAuthorityVendorId = table.Column<int>(type: "integer", nullable: false),
                    HealthAuthorityCareTypeId = table.Column<int>(type: "integer", nullable: true),
                    HealthAuthorityPharmanetAdministratorId = table.Column<int>(type: "integer", nullable: true),
                    HealthAuthorityTechnicalSupportId = table.Column<int>(type: "integer", nullable: true),
                    AuthorizedUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthAuthoritySite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_AuthorizedUsers_AuthorizedUserId",
                        column: x => x.AuthorizedUserId,
                        principalTable: "AuthorizedUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_HealthAuthorityCareType_HealthAuthority~",
                        column: x => x.HealthAuthorityCareTypeId,
                        principalTable: "HealthAuthorityCareType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_HealthAuthorityContact_HealthAuthorityP~",
                        column: x => x.HealthAuthorityPharmanetAdministratorId,
                        principalTable: "HealthAuthorityContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_HealthAuthorityContact_HealthAuthorityT~",
                        column: x => x.HealthAuthorityTechnicalSupportId,
                        principalTable: "HealthAuthorityContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_HealthAuthorityOrganization_HealthAutho~",
                        column: x => x.HealthAuthorityOrganizationId,
                        principalTable: "HealthAuthorityOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_HealthAuthorityVendor_HealthAuthorityVe~",
                        column: x => x.HealthAuthorityVendorId,
                        principalTable: "HealthAuthorityVendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_Site_Id",
                        column: x => x.Id,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunitySite_AdministratorPharmaNetId",
                table: "CommunitySite",
                column: "AdministratorPharmaNetId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunitySite_OrganizationId",
                table: "CommunitySite",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunitySite_PrivacyOfficerId",
                table: "CommunitySite",
                column: "PrivacyOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunitySite_ProvisionerId",
                table: "CommunitySite",
                column: "ProvisionerId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunitySite_TechnicalSupportId",
                table: "CommunitySite",
                column: "TechnicalSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthoritySite_AuthorizedUserId",
                table: "HealthAuthoritySite",
                column: "AuthorizedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthoritySite_HealthAuthorityCareTypeId",
                table: "HealthAuthoritySite",
                column: "HealthAuthorityCareTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthoritySite_HealthAuthorityOrganizationId",
                table: "HealthAuthoritySite",
                column: "HealthAuthorityOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthoritySite_HealthAuthorityPharmanetAdministratorId",
                table: "HealthAuthoritySite",
                column: "HealthAuthorityPharmanetAdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthoritySite_HealthAuthorityTechnicalSupportId",
                table: "HealthAuthoritySite",
                column: "HealthAuthorityTechnicalSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthoritySite_HealthAuthorityVendorId",
                table: "HealthAuthoritySite",
                column: "HealthAuthorityVendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessLicence_CommunitySite_SiteId",
                table: "BusinessLicence");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteVendor_CommunitySite_SiteId",
                table: "SiteVendor");

            migrationBuilder.DropTable(
                name: "CommunitySite");

            migrationBuilder.DropTable(
                name: "HealthAuthoritySite");

            migrationBuilder.AddColumn<int>(
                name: "AdministratorPharmaNetId",
                table: "Site",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Site",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrivacyOfficerId",
                table: "Site",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvisionerId",
                table: "Site",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TechnicalSupportId",
                table: "Site",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Site_AdministratorPharmaNetId",
                table: "Site",
                column: "AdministratorPharmaNetId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_OrganizationId",
                table: "Site",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_PrivacyOfficerId",
                table: "Site",
                column: "PrivacyOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_ProvisionerId",
                table: "Site",
                column: "ProvisionerId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_TechnicalSupportId",
                table: "Site",
                column: "TechnicalSupportId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessLicence_Site_SiteId",
                table: "BusinessLicence",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Contact_AdministratorPharmaNetId",
                table: "Site",
                column: "AdministratorPharmaNetId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Contact_PrivacyOfficerId",
                table: "Site",
                column: "PrivacyOfficerId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Contact_TechnicalSupportId",
                table: "Site",
                column: "TechnicalSupportId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Organization_OrganizationId",
                table: "Site",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Party_ProvisionerId",
                table: "Site",
                column: "ProvisionerId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteVendor_Site_SiteId",
                table: "SiteVendor",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
