using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class RemoveEnrolleeCredentials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolleeCredential");

            migrationBuilder.AddColumn<int>(
                name: "EnrolleeId",
                table: "Credential",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Credential_EnrolleeId",
                table: "Credential",
                column: "EnrolleeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credential_Enrollee_EnrolleeId",
                table: "Credential",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credential_Enrollee_EnrolleeId",
                table: "Credential");

            migrationBuilder.DropIndex(
                name: "IX_Credential_EnrolleeId",
                table: "Credential");

            migrationBuilder.DropColumn(
                name: "EnrolleeId",
                table: "Credential");

            migrationBuilder.CreateTable(
                name: "EnrolleeCredential",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CredentialId = table.Column<int>(type: "integer", nullable: false),
                    EnrolleeId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false)
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
    }
}
