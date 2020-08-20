using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class OrgTypetoCareSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_OrganizationTypeLookup_OrganizationTypeCode",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorLookup_OrganizationTypeLookup_OrganizationTypeCode",
                table: "VendorLookup");

            migrationBuilder.RenameTable(
                name: "EnrolleeOrganizationType",
                schema: null,
                newName: "EnrolleeCareSetting");

            migrationBuilder.RenameTable(
                name: "OrganizationTypeLookup",
                schema: null,
                newName: "CareSettingLookup");

            migrationBuilder.DropIndex(
                name: "IX_VendorLookup_OrganizationTypeCode",
                table: "VendorLookup");

            migrationBuilder.DropIndex(
                name: "IX_Site_OrganizationTypeCode",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_EnrolleeOrganizationType_EnrolleeId",
                table: "EnrolleeOrganizationType");

            migrationBuilder.DropIndex(
                name: "IX_EnrolleeOrganizationType_OrganizationTypeCode",
                table: "EnrolleeOrganizationType");

            migrationBuilder.RenameColumn(
                name: "OrganizationTypeCode",
                table: "Site",
                newName: "CareSettingCode");

            migrationBuilder.RenameColumn(
                name: "OrganizationTypeCode",
                table: "EnrolleeCareSetting",
                newName: "CareSettingCode");

            migrationBuilder.RenameColumn(
                name: "OrganizationTypeCode",
                table: "VendorLookup",
                newName: "CareSettingCode");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolleeCareSetting_CareSettingLookup_CareSettingCode",
                table: "EnrolleeCareSetting",
                column: "CareSettingCode",
                principalTable: "CareSettingLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolleeCareSetting_Enrollee_EnrolleeId",
                table: "EnrolleeCareSetting",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateIndex(
                name: "IX_VendorLookup_CareSettingCode",
                table: "VendorLookup",
                column: "CareSettingCode");

            migrationBuilder.CreateIndex(
                name: "IX_Site_CareSettingCode",
                table: "Site",
                column: "CareSettingCode");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeCareSetting_CareSettingCode",
                table: "EnrolleeCareSetting",
                column: "CareSettingCode");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeCareSetting_EnrolleeId",
                table: "EnrolleeCareSetting",
                column: "EnrolleeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_CareSettingLookup_CareSettingCode",
                table: "Site",
                column: "CareSettingCode",
                principalTable: "CareSettingLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorLookup_CareSettingLookup_CareSettingCode",
                table: "VendorLookup",
                column: "CareSettingCode",
                principalTable: "CareSettingLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_CareSettingLookup_CareSettingCode",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorLookup_CareSettingLookup_CareSettingCode",
                table: "VendorLookup");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolleeCareSetting_CareSettingLookup_CareSettingCode",
                table: "EnrolleeCareSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolleeCareSetting_Enrollee_EnrolleeId",
                table: "EnrolleeCareSetting");

            migrationBuilder.RenameTable(
                 name: "EnrolleeCareSetting",
                 schema: null,
                 newName: "EnrolleeOrganizationType");

            migrationBuilder.RenameTable(
                name: "CareSettingLookup",
                schema: null,
                newName: "OrganizationTypeLookup");

            migrationBuilder.DropIndex(
                name: "IX_VendorLookup_CareSettingCode",
                table: "VendorLookup");

            migrationBuilder.DropIndex(
                name: "IX_Site_CareSettingCode",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_EnrolleeCareSetting_CareSettingCode",
                table: "EnrolleeCareSetting");

            migrationBuilder.DropIndex(
                name: "IX_EnrolleeCareSetting_EnrolleeId",
                table: "EnrolleeCareSetting");

            migrationBuilder.RenameColumn(
                name: "CareSettingCode",
                table: "Site",
                newName: "OrganizationTypeCode");

            migrationBuilder.RenameColumn(
                name: "CareSettingCode",
                table: "EnrolleeOrganizationType",
                newName: "OrganizationTypeCode");

            migrationBuilder.RenameColumn(
                name: "CareSettingCode",
                table: "VendorLookup",
                newName: "OrganizationTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_VendorLookup_OrganizationTypeCode",
                table: "VendorLookup",
                column: "OrganizationTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Site_OrganizationTypeCode",
                table: "Site",
                column: "OrganizationTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeOrganizationType_EnrolleeId",
                table: "EnrolleeOrganizationType",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeOrganizationType_OrganizationTypeCode",
                table: "EnrolleeOrganizationType",
                column: "OrganizationTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_OrganizationTypeLookup_OrganizationTypeCode",
                table: "Site",
                column: "OrganizationTypeCode",
                principalTable: "OrganizationTypeLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorLookup_OrganizationTypeLookup_OrganizationTypeCode",
                table: "VendorLookup",
                column: "OrganizationTypeCode",
                principalTable: "OrganizationTypeLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
