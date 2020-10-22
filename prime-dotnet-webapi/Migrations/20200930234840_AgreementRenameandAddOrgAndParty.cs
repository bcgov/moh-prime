using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AgreementRenameandAddOrgAndParty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTerm_Agreement_AgreementId",
                table: "AccessTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessTerm_Enrollee_EnrolleeId",
                table: "AccessTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessTerm_LimitsConditionsClause_LimitsConditionsClauseId",
                table: "AccessTerm");

            migrationBuilder.DropIndex(
                name: "IX_AccessTerm_AgreementId",
                table: "AccessTerm");

            migrationBuilder.DropIndex(
            name: "IX_AccessTerm_EnrolleeId",
            table: "AccessTerm");

            migrationBuilder.DropIndex(
                name: "IX_AccessTerm_LimitsConditionsClauseId",
                table: "AccessTerm");

            migrationBuilder.RenameTable(
                name: "Agreement",
                newName: "AgreementVersion");

            migrationBuilder.RenameTable(
                name: "AccessTerm",
                newName: "Agreement");

            migrationBuilder.RenameColumn(
                name: "AgreementId",
                table: "Agreement",
                newName: "AgreementVersionId");

            migrationBuilder.AddColumn<int>(
                name: "AgreementId",
                table: "SignedAgreementDocument",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgreementVersionType",
                table: "AgreementVersion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 9,
                column: "AgreementVersionType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 10,
                column: "AgreementVersionType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 1,
                column: "AgreementVersionType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 3,
                column: "AgreementVersionType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 5,
                column: "AgreementVersionType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 7,
                column: "AgreementVersionType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 2,
                column: "AgreementVersionType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 4,
                column: "AgreementVersionType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 6,
                column: "AgreementVersionType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 8,
                column: "AgreementVersionType",
                value: 2);

            migrationBuilder.CreateCheckConstraint(
                name: "CHK_Agreement_OnlyOneForeignKey",
                table: "Agreement",
                sql: @"( CASE WHEN ""EnrolleeId"" IS NULL THEN 0 ELSE 1 END
                     + CASE WHEN ""OrganizationId"" IS NULL THEN 0 ELSE 1 END
                     + CASE WHEN ""PartyId"" IS NULL THEN 0 ELSE 1 END) = 1");

            migrationBuilder.CreateIndex(
                name: "IX_SignedAgreementDocument_AgreementId",
                table: "SignedAgreementDocument",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_AgreementVersionId",
                table: "Agreement",
                column: "AgreementVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_EnrolleeId",
                table: "Agreement",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_LimitsConditionsClauseId",
                table: "Agreement",
                column: "LimitsConditionsClauseId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_OrganizationId",
                table: "Agreement",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_PartyId",
                table: "Agreement",
                column: "PartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_AgreementVersion_AgreementVersionId",
                table: "Agreement",
                column: "AgreementVersionId",
                principalTable: "AgreementVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Enrollee_EnrolleeId",
                table: "Agreement",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_LimitsConditionsClause_LimitsConditionsClauseId",
                table: "Agreement",
                column: "LimitsConditionsClauseId",
                principalTable: "LimitsConditionsClause",
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

            migrationBuilder.AddForeignKey(
                name: "FK_SignedAgreementDocument_Agreement_AgreementId",
                table: "SignedAgreementDocument",
                column: "AgreementId",
                principalTable: "Agreement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Agreement_OnlyOneForeignKey",
                table: "Agreement"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_AgreementVersion_AgreementVersionId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Enrollee_EnrolleeId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_LimitsConditionsClause_LimitsConditionsClauseId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Organization_OrganizationId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Party_PartyId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_SignedAgreementDocument_Agreement_AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropIndex(
                name: "IX_SignedAgreementDocument_AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_PartyId",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_OrganizationId",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Agreement");

            migrationBuilder.RenameColumn(
                name: "AgreementVersionId",
                table: "Agreement",
                newName: "AgreementId");

            migrationBuilder.RenameTable(
                name: "Agreement",
                schema: null,
                newName: "AccessTerm");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_AgreementVersionId",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_EnrolleeId",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_LimitsConditionsClauseId",
                table: "Agreement");

            migrationBuilder.RenameTable(
                name: "AgreementVersion",
                schema: null,
                newName: "Agreement");

            migrationBuilder.CreateIndex(
                name: "IX_AccessTerm_AgreementId",
                table: "AccessTerm",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessTerm_EnrolleeId",
                table: "AccessTerm",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessTerm_LimitsConditionsClauseId",
                table: "AccessTerm",
                column: "LimitsConditionsClauseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTerm_Agreement_AgreementId",
                table: "AccessTerm",
                column: "AgreementId",
                principalTable: "Agreement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTerm_Enrollee_EnrolleeId",
                table: "AccessTerm",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTerm_LimitsConditionsClause_LimitsConditionsClauseId",
                table: "AccessTerm",
                column: "LimitsConditionsClauseId",
                principalTable: "LimitsConditionsClause",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
