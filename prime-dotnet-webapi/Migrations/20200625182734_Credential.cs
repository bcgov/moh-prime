using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class Credential : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CredentialId",
                table: "Enrollee",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Credential",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    SchemaId = table.Column<string>(nullable: true),
                    CredentialDefinitionId = table.Column<string>(nullable: true),
                    base64QRCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credential", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Credential_CredentialId",
                table: "Enrollee");

            migrationBuilder.DropTable(
                name: "Credential");

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_CredentialId",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "CredentialId",
                table: "Enrollee");
        }
    }
}
