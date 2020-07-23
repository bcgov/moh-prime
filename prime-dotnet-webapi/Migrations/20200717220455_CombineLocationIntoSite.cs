using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class CombineLocationIntoSite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_VendorLookup_VendorCode",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_SiteRegistrationReviewDocument_SiteId",
                table: "SiteRegistrationReviewDocument");

            migrationBuilder.DropIndex(
                name: "IX_Site_VendorCode",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "VendorCode",
                table: "Site");

            migrationBuilder.AddColumn<int>(
                name: "AdministratorPharmaNetId",
                table: "Site",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoingBusinessAs",
                table: "Site",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Site",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhysicalAddressId",
                table: "Site",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrivacyOfficerId",
                table: "Site",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TechnicalSupportId",
                table: "Site",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "BusinessDay",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SiteVendor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    VendorCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteVendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteVendor_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteVendor_VendorLookup_VendorCode",
                        column: x => x.VendorCode,
                        principalTable: "VendorLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteRegistrationReviewDocument_SiteId",
                table: "SiteRegistrationReviewDocument",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_AdministratorPharmaNetId",
                table: "Site",
                column: "AdministratorPharmaNetId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_OrganizationId",
                table: "Site",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_PhysicalAddressId",
                table: "Site",
                column: "PhysicalAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_PrivacyOfficerId",
                table: "Site",
                column: "PrivacyOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_TechnicalSupportId",
                table: "Site",
                column: "TechnicalSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessDay_SiteId",
                table: "BusinessDay",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteVendor_SiteId",
                table: "SiteVendor",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteVendor_VendorCode",
                table: "SiteVendor",
                column: "VendorCode");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessDay_Site_SiteId",
                table: "BusinessDay",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Party_AdministratorPharmaNetId",
                table: "Site",
                column: "AdministratorPharmaNetId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site",
                column: "LocationId",
                principalTable: "Location",
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
                name: "FK_Site_Address_PhysicalAddressId",
                table: "Site",
                column: "PhysicalAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Party_PrivacyOfficerId",
                table: "Site",
                column: "PrivacyOfficerId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Party_TechnicalSupportId",
                table: "Site",
                column: "TechnicalSupportId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessDay_Site_SiteId",
                table: "BusinessDay");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Party_AdministratorPharmaNetId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Organization_OrganizationId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Address_PhysicalAddressId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Party_PrivacyOfficerId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Party_TechnicalSupportId",
                table: "Site");

            migrationBuilder.DropTable(
                name: "SiteVendor");

            migrationBuilder.DropIndex(
                name: "IX_SiteRegistrationReviewDocument_SiteId",
                table: "SiteRegistrationReviewDocument");

            migrationBuilder.DropIndex(
                name: "IX_Site_AdministratorPharmaNetId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_OrganizationId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_PhysicalAddressId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_PrivacyOfficerId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_TechnicalSupportId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_BusinessDay_SiteId",
                table: "BusinessDay");

            migrationBuilder.DropColumn(
                name: "AdministratorPharmaNetId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "DoingBusinessAs",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "PhysicalAddressId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "PrivacyOfficerId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "TechnicalSupportId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "BusinessDay");

            migrationBuilder.AddColumn<int>(
                name: "VendorCode",
                table: "Site",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SiteRegistrationReviewDocument_SiteId",
                table: "SiteRegistrationReviewDocument",
                column: "SiteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Site_VendorCode",
                table: "Site",
                column: "VendorCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_VendorLookup_VendorCode",
                table: "Site",
                column: "VendorCode",
                principalTable: "VendorLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
