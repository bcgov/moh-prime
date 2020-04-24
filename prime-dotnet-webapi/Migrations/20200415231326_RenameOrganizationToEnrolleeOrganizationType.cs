using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class RenameOrganizationToEnrolleeOrganizationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organization_Enrollee_EnrolleeId",
                table: "Organization");

            migrationBuilder.DropForeignKey(
                name: "FK_Organization_OrganizationTypeLookup_OrganizationTypeCode",
                table: "Organization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organization",
                table: "Organization");

            migrationBuilder.RenameTable(
                name: "Organization",
                newName: "EnrolleeOrganizationType");

            migrationBuilder.RenameIndex(
                name: "IX_Organization_OrganizationTypeCode",
                table: "EnrolleeOrganizationType",
                newName: "IX_EnrolleeOrganizationType_OrganizationTypeCode");

            migrationBuilder.RenameIndex(
                name: "IX_Organization_EnrolleeId",
                table: "EnrolleeOrganizationType",
                newName: "IX_EnrolleeOrganizationType_EnrolleeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolleeOrganizationType",
                table: "EnrolleeOrganizationType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolleeOrganizationType_Enrollee_EnrolleeId",
                table: "EnrolleeOrganizationType",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolleeOrganizationType_OrganizationTypeLookup_Organizatio~",
                table: "EnrolleeOrganizationType",
                column: "OrganizationTypeCode",
                principalTable: "OrganizationTypeLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolleeOrganizationType_Enrollee_EnrolleeId",
                table: "EnrolleeOrganizationType");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolleeOrganizationType_OrganizationTypeLookup_Organizatio~",
                table: "EnrolleeOrganizationType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolleeOrganizationType",
                table: "EnrolleeOrganizationType");

            migrationBuilder.RenameTable(
                name: "EnrolleeOrganizationType",
                newName: "Organization");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolleeOrganizationType_OrganizationTypeCode",
                table: "Organization",
                newName: "IX_Organization_OrganizationTypeCode");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolleeOrganizationType_EnrolleeId",
                table: "Organization",
                newName: "IX_Organization_EnrolleeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organization",
                table: "Organization",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_Enrollee_EnrolleeId",
                table: "Organization",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_OrganizationTypeLookup_OrganizationTypeCode",
                table: "Organization",
                column: "OrganizationTypeCode",
                principalTable: "OrganizationTypeLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
