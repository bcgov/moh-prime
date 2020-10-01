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

            migrationBuilder.DropTable(
                name: "AgreementVersion");

            migrationBuilder.DropIndex(
                name: "IX_SignedAgreementDocument_AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_AgreementVersionId",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_EnrolleeId",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_LimitsConditionsClauseId",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_OrganizationId",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_PartyId",
                table: "Agreement");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Agreement_OnlyOneForeignKey",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropColumn(
                name: "AcceptedDate",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "AgreementVersionId",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "EnrolleeId",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "LimitsConditionsClauseId",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Agreement");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Agreement",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EffectiveDate",
                table: "Agreement",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Agreement",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccessTerm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AcceptedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    AgreementId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnrolleeId = table.Column<int>(type: "integer", nullable: false),
                    ExpiryDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LimitsConditionsClauseId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessTerm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessTerm_Agreement_AgreementId",
                        column: x => x.AgreementId,
                        principalTable: "Agreement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessTerm_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessTerm_LimitsConditionsClause_LimitsConditionsClauseId",
                        column: x => x.LimitsConditionsClauseId,
                        principalTable: "LimitsConditionsClause",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
        }
    }
}
