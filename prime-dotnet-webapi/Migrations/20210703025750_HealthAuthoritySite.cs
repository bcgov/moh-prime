using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class HealthAuthoritySite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthAuthoritySiteId",
                table: "SiteRegistrationReviewDocument",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealthAuthoritySiteId",
                table: "SiteRegistrationNote",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealthAuthoritySiteId",
                table: "SiteAdjudicationDocument",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealthAuthoritySiteId",
                table: "RemoteUser",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealthAuthoritySiteId",
                table: "BusinessDay",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HealthAuthoritySite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    HealthAuthorityOrganizationId = table.Column<int>(nullable: false),
                    VendorCode = table.Column<int>(nullable: false),
                    SiteName = table.Column<string>(nullable: true),
                    SiteId = table.Column<string>(nullable: true),
                    SecurityGroup = table.Column<string>(nullable: true),
                    HealthAuthorityCareTypeId = table.Column<int>(nullable: false),
                    PhysicalAddressId = table.Column<int>(nullable: true),
                    HealthAuthorityPharmanetAdministratorId = table.Column<int>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    SubmittedDate = table.Column<DateTimeOffset>(nullable: true),
                    ApprovedDate = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AdjudicatorId = table.Column<int>(nullable: true),
                    PEC = table.Column<string>(nullable: true),
                    ProvisionerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthAuthoritySite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_HealthAuthorityCareType_HealthAuthority~",
                        column: x => x.HealthAuthorityCareTypeId,
                        principalTable: "HealthAuthorityCareType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_HealthAuthorityOrganization_HealthAutho~",
                        column: x => x.HealthAuthorityOrganizationId,
                        principalTable: "HealthAuthorityOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_HealthAuthorityContact_HealthAuthorityP~",
                        column: x => x.HealthAuthorityPharmanetAdministratorId,
                        principalTable: "HealthAuthorityContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_Address_PhysicalAddressId",
                        column: x => x.PhysicalAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteRegistrationReviewDocument_HealthAuthoritySiteId",
                table: "SiteRegistrationReviewDocument",
                column: "HealthAuthoritySiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteRegistrationNote_HealthAuthoritySiteId",
                table: "SiteRegistrationNote",
                column: "HealthAuthoritySiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAdjudicationDocument_HealthAuthoritySiteId",
                table: "SiteAdjudicationDocument",
                column: "HealthAuthoritySiteId");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteUser_HealthAuthoritySiteId",
                table: "RemoteUser",
                column: "HealthAuthoritySiteId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessDay_HealthAuthoritySiteId",
                table: "BusinessDay",
                column: "HealthAuthoritySiteId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthoritySite_AdjudicatorId",
                table: "HealthAuthoritySite",
                column: "AdjudicatorId");

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
                name: "IX_HealthAuthoritySite_PhysicalAddressId",
                table: "HealthAuthoritySite",
                column: "PhysicalAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessDay_HealthAuthoritySite_HealthAuthoritySiteId",
                table: "BusinessDay",
                column: "HealthAuthoritySiteId",
                principalTable: "HealthAuthoritySite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RemoteUser_HealthAuthoritySite_HealthAuthoritySiteId",
                table: "RemoteUser",
                column: "HealthAuthoritySiteId",
                principalTable: "HealthAuthoritySite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteAdjudicationDocument_HealthAuthoritySite_HealthAuthorit~",
                table: "SiteAdjudicationDocument",
                column: "HealthAuthoritySiteId",
                principalTable: "HealthAuthoritySite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteRegistrationNote_HealthAuthoritySite_HealthAuthoritySit~",
                table: "SiteRegistrationNote",
                column: "HealthAuthoritySiteId",
                principalTable: "HealthAuthoritySite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteRegistrationReviewDocument_HealthAuthoritySite_HealthAu~",
                table: "SiteRegistrationReviewDocument",
                column: "HealthAuthoritySiteId",
                principalTable: "HealthAuthoritySite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessDay_HealthAuthoritySite_HealthAuthoritySiteId",
                table: "BusinessDay");

            migrationBuilder.DropForeignKey(
                name: "FK_RemoteUser_HealthAuthoritySite_HealthAuthoritySiteId",
                table: "RemoteUser");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteAdjudicationDocument_HealthAuthoritySite_HealthAuthorit~",
                table: "SiteAdjudicationDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteRegistrationNote_HealthAuthoritySite_HealthAuthoritySit~",
                table: "SiteRegistrationNote");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteRegistrationReviewDocument_HealthAuthoritySite_HealthAu~",
                table: "SiteRegistrationReviewDocument");

            migrationBuilder.DropTable(
                name: "HealthAuthoritySite");

            migrationBuilder.DropIndex(
                name: "IX_SiteRegistrationReviewDocument_HealthAuthoritySiteId",
                table: "SiteRegistrationReviewDocument");

            migrationBuilder.DropIndex(
                name: "IX_SiteRegistrationNote_HealthAuthoritySiteId",
                table: "SiteRegistrationNote");

            migrationBuilder.DropIndex(
                name: "IX_SiteAdjudicationDocument_HealthAuthoritySiteId",
                table: "SiteAdjudicationDocument");

            migrationBuilder.DropIndex(
                name: "IX_RemoteUser_HealthAuthoritySiteId",
                table: "RemoteUser");

            migrationBuilder.DropIndex(
                name: "IX_BusinessDay_HealthAuthoritySiteId",
                table: "BusinessDay");

            migrationBuilder.DropColumn(
                name: "HealthAuthoritySiteId",
                table: "SiteRegistrationReviewDocument");

            migrationBuilder.DropColumn(
                name: "HealthAuthoritySiteId",
                table: "SiteRegistrationNote");

            migrationBuilder.DropColumn(
                name: "HealthAuthoritySiteId",
                table: "SiteAdjudicationDocument");

            migrationBuilder.DropColumn(
                name: "HealthAuthoritySiteId",
                table: "RemoteUser");

            migrationBuilder.DropColumn(
                name: "HealthAuthoritySiteId",
                table: "BusinessDay");
        }
    }
}
