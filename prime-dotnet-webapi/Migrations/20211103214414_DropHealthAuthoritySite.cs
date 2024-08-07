﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class DropHealthAuthoritySite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthAuthoritySiteId",
                table: "SiteRegistrationReviewDocument",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealthAuthoritySiteId",
                table: "SiteRegistrationNote",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealthAuthoritySiteId",
                table: "SiteAdjudicationDocument",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealthAuthoritySiteId",
                table: "RemoteUser",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealthAuthoritySiteId",
                table: "BusinessDay",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HealthAuthoritySite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdjudicatorId = table.Column<int>(type: "integer", nullable: true),
                    ApprovedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CareType = table.Column<string>(type: "text", nullable: true),
                    Completed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    HealthAuthorityOrganizationId = table.Column<int>(type: "integer", nullable: false),
                    HealthAuthorityPharmanetAdministratorId = table.Column<int>(type: "integer", nullable: true),
                    HealthAuthorityTechnicalSupportId = table.Column<int>(type: "integer", nullable: true),
                    PEC = table.Column<string>(type: "text", nullable: true),
                    PhysicalAddressId = table.Column<int>(type: "integer", nullable: true),
                    ProvisionerId = table.Column<int>(type: "integer", nullable: true),
                    SecurityGroupCode = table.Column<int>(type: "integer", nullable: false),
                    SiteId = table.Column<string>(type: "text", nullable: true),
                    SiteName = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SubmittedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendorCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthAuthoritySite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_Address_PhysicalAddressId",
                        column: x => x.PhysicalAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HealthAuthoritySite_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
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
    }
}
