using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddHAOrgAgreementDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthAuthorityOrganizationAgreementDocumentId",
                table: "HealthAuthorityOrganization",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HealthAuthorityOrganizationAgreementDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DocumentGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Filename = table.Column<string>(type: "text", nullable: true),
                    UploadedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthAuthorityOrganizationAgreementDocument", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthorityOrganization_HealthAuthorityOrganizationAgre~",
                table: "HealthAuthorityOrganization",
                column: "HealthAuthorityOrganizationAgreementDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthAuthorityOrganization_HealthAuthorityOrganizationAgre~",
                table: "HealthAuthorityOrganization",
                column: "HealthAuthorityOrganizationAgreementDocumentId",
                principalTable: "HealthAuthorityOrganizationAgreementDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthAuthorityOrganization_HealthAuthorityOrganizationAgre~",
                table: "HealthAuthorityOrganization");

            migrationBuilder.DropTable(
                name: "HealthAuthorityOrganizationAgreementDocument");

            migrationBuilder.DropIndex(
                name: "IX_HealthAuthorityOrganization_HealthAuthorityOrganizationAgre~",
                table: "HealthAuthorityOrganization");

            migrationBuilder.DropColumn(
                name: "HealthAuthorityOrganizationAgreementDocumentId",
                table: "HealthAuthorityOrganization");
        }
    }
}
