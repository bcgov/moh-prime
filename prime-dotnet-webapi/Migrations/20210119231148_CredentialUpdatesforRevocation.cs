using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class CredentialUpdatesforRevocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Credential_CredentialId",
                table: "Enrollee");

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_CredentialId",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "CredentialId",
                table: "Enrollee");

            migrationBuilder.AddColumn<string>(
                name: "ConnectionId",
                table: "Credential",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CredentialExchangeId",
                table: "Credential",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RevokedCredentialDate",
                table: "Credential",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EnrolleeCredential",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    CredentialId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeCredential", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeCredential_Credential_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "Credential",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeCredential_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeCredential_CredentialId",
                table: "EnrolleeCredential",
                column: "CredentialId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeCredential_EnrolleeId",
                table: "EnrolleeCredential",
                column: "EnrolleeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolleeCredential");

            migrationBuilder.DropColumn(
                name: "ConnectionId",
                table: "Credential");

            migrationBuilder.DropColumn(
                name: "CredentialExchangeId",
                table: "Credential");

            migrationBuilder.DropColumn(
                name: "RevokedCredentialDate",
                table: "Credential");

            migrationBuilder.AddColumn<int>(
                name: "CredentialId",
                table: "Enrollee",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_CredentialId",
                table: "Enrollee",
                column: "CredentialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Credential_CredentialId",
                table: "Enrollee",
                column: "CredentialId",
                principalTable: "Credential",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
