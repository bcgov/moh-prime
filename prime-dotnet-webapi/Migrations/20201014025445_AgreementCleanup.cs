﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AgreementCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drops
            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS public.""NewestAgreements"";
            ");

            migrationBuilder.DropForeignKey(
                name: "FK_SignedAgreementDocument_Agreement_AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_SignedAgreementDocument_Organization_OrganizationId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropIndex(
                name: "IX_SignedAgreementDocument_AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropIndex(
                name: "IX_SignedAgreementDocument_OrganizationId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropColumn(
                name: "AcceptedAgreementDate",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AgreementVersion");


            // Changes
            migrationBuilder.RenameColumn(
                name: "AgreementVersionType",
                table: "AgreementVersion",
                newName: "AgreementType");

            migrationBuilder.AlterColumn<int>(
                name: "AgreementId",
                table: "SignedAgreementDocument",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EnrolleeId",
                table: "Agreement",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: false);

            migrationBuilder.InsertData(
                table: "AgreementVersion",
                columns: new[] { "Id", "AgreementType", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "Text", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 12, 5, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<p class=""text-center"">
  This Agreement is made the {{day}} day of {{month}}, {{year}}
</p>

<h1>---- PLACEHOLDER TEXT ----</h1>
", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.CreateIndex(
                name: "IX_SignedAgreementDocument_AgreementId",
                table: "SignedAgreementDocument",
                column: "AgreementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument",
                column: "EnrolleeId");

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
                name: "FK_SignedAgreementDocument_Agreement_AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropIndex(
                name: "IX_SignedAgreementDocument_AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument");

            migrationBuilder.DeleteData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DropColumn(
                name: "AgreementType",
                table: "AgreementVersion");

            migrationBuilder.AlterColumn<int>(
                name: "AgreementId",
                table: "SignedAgreementDocument",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "SignedAgreementDocument",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AcceptedAgreementDate",
                table: "Organization",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgreementVersionType",
                table: "AgreementVersion",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AgreementVersion",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "NewestAgreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewestAgreements", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AgreementVersionType", "Discriminator" },
                values: new object[] { 1, "CommunityPharmacistAgreement" });

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AgreementVersionType", "Discriminator" },
                values: new object[] { 1, "CommunityPharmacistAgreement" });

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AgreementVersionType", "Discriminator" },
                values: new object[] { 4, "CommunityPracticeOrgAgreement" });

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AgreementVersionType", "Discriminator" },
                values: new object[] { 3, "OboAgreement" });

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AgreementVersionType", "Discriminator" },
                values: new object[] { 3, "OboAgreement" });

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AgreementVersionType", "Discriminator" },
                values: new object[] { 3, "OboAgreement" });

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AgreementVersionType", "Discriminator" },
                values: new object[] { 3, "OboAgreement" });

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AgreementVersionType", "Discriminator" },
                values: new object[] { 2, "RegulatedUserAgreement" });

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AgreementVersionType", "Discriminator" },
                values: new object[] { 2, "RegulatedUserAgreement" });

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AgreementVersionType", "Discriminator" },
                values: new object[] { 2, "RegulatedUserAgreement" });

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AgreementVersionType", "Discriminator" },
                values: new object[] { 2, "RegulatedUserAgreement" });

            migrationBuilder.CreateIndex(
                name: "IX_SignedAgreementDocument_AgreementId",
                table: "SignedAgreementDocument",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_SignedAgreementDocument_OrganizationId",
                table: "SignedAgreementDocument",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument",
                column: "EnrolleeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SignedAgreementDocument_Agreement_AgreementId",
                table: "SignedAgreementDocument",
                column: "AgreementId",
                principalTable: "Agreement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SignedAgreementDocument_Organization_OrganizationId",
                table: "SignedAgreementDocument",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql(@"
                CREATE OR REPLACE VIEW public.""NewestAgreements""
                AS
                SELECT DISTINCT ON(""Discriminator"")
	                ""Id""
                FROM public.""AgreementVersion""
                ORDER BY ""Discriminator"", ""EffectiveDate"" DESC;
            ");
        }
    }
}
