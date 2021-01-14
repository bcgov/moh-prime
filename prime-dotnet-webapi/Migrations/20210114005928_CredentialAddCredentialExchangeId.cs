using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class CredentialAddCredentialExchangeId : Migration
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
                name: "CredentialExchangeId",
                table: "Credential",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CredentialExchangeId",
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
