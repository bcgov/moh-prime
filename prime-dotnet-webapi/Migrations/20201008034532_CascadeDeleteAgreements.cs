using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class CascadeDeleteAgreements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignedAgreementDocument_Agreement_AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Enrollee_EnrolleeId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Organization_OrganizationId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Party_PartyId",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument",
                column: "EnrolleeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Enrollee_EnrolleeId",
                table: "Agreement",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Organization_OrganizationId",
                table: "Agreement",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Party_PartyId",
                table: "Agreement",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SignedAgreementDocument_Agreement_AgreementId",
                table: "SignedAgreementDocument",
                column: "AgreementId",
                principalTable: "Agreement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Enrollee_EnrolleeId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Organization_OrganizationId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Party_PartyId",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument",
                column: "EnrolleeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Enrollee_EnrolleeId",
                table: "Agreement",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Organization_OrganizationId",
                table: "Agreement",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Party_PartyId",
                table: "Agreement",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
