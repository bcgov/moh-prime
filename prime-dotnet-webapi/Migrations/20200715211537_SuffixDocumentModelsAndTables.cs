using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class SuffixDocumentModelsAndTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessLicence",
                table: "BusinessLicence");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessLicence_Site_SiteId",
                table: "BusinessLicence");

            migrationBuilder.RenameTable(
                name: "BusinessLicence",
                newName: "BusinessLicenceDocument");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessLicence_SiteId",
                table: "BusinessLicenceDocument",
                newName: "IX_BusinessLicenceDocument_SiteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessLicenceDocument",
                table: "BusinessLicenceDocument",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessLicenceDocument_Site_SiteId",
                table: "BusinessLicenceDocument",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);




            migrationBuilder.DropPrimaryKey(
                name: "PK_SignedAgreement",
                table: "SignedAgreement");

            migrationBuilder.DropForeignKey(
                name: "FK_SignedAgreement_Organization_OrganizationId",
                table: "SignedAgreement");

            migrationBuilder.RenameTable(
                name: "SignedAgreement",
                newName: "SignedAgreementDocument");

            migrationBuilder.RenameIndex(
                name: "IX_SignedAgreement_OrganizationId",
                table: "SignedAgreementDocument",
                newName: "IX_SignedAgreementDocument_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignedAgreementDocument",
                table: "SignedAgreementDocument",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SignedAgreementDocument_Organization_OrganizationId",
                table: "SignedAgreementDocument",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);




            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteRegistrationReview",
                table: "SiteRegistrationReview");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteRegistrationReview_Site_SiteId",
                table: "SiteRegistrationReview");

            migrationBuilder.RenameTable(
                name: "SiteRegistrationReview",
                newName: "SiteRegistrationReviewDocument");

            migrationBuilder.RenameIndex(
                name: "IX_SiteRegistrationReview_SiteId",
                table: "SiteRegistrationReviewDocument",
                newName: "IX_SiteRegistrationReviewDocument_SiteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteRegistrationReviewDocument",
                table: "SiteRegistrationReviewDocument",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteRegistrationReviewDocument_Site_SiteId",
                table: "SiteRegistrationReviewDocument",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BusinessLicenceDocument",
                table: "BusinessLicenceDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessLicenceDocument_Site_SiteId",
                table: "BusinessLicenceDocument");

            migrationBuilder.RenameTable(
                name: "BusinessLicence",
                newName: "BusinessLicence");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessLicenceDocument_SiteId",
                table: "BusinessLicence",
                newName: "IX_BusinessLicence_SiteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BusinessLicence",
                table: "BusinessLicence",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessLicence_Site_SiteId",
                table: "BusinessLicence",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);




            migrationBuilder.DropPrimaryKey(
                name: "PK_SignedAgreementDocument",
                table: "SignedAgreementDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_SignedAgreementDocument_Organization_OrganizationId",
                table: "SignedAgreementDocument");

            migrationBuilder.RenameTable(
                name: "SignedAgreementDocument",
                newName: "SignedAgreement");

            migrationBuilder.RenameIndex(
                name: "IX_SignedAgreementDocument_OrganizationId",
                table: "SignedAgreement",
                newName: "IX_SignedAgreement_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignedAgreement",
                table: "SignedAgreement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SignedAgreement_Organization_OrganizationId",
                table: "SignedAgreement",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);




            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteRegistrationReviewDocument",
                table: "SiteRegistrationReviewDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteRegistrationReviewDocument_Site_SiteId",
                table: "SiteRegistrationReviewDocument");

            migrationBuilder.RenameTable(
                name: "SiteRegistrationReviewDocument",
                newName: "SiteRegistrationReview");

            migrationBuilder.RenameIndex(
                name: "IX_SiteRegistrationReviewDocument_SiteId",
                table: "SiteRegistrationReview",
                newName: "IX_SiteRegistrationReview_SiteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteRegistrationReview",
                table: "SiteRegistrationReview",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteRegistrationReview_Site_SiteId",
                table: "SiteRegistrationReview",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
