using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class SelfDeclarationDocumentLooseCouple : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelfDeclarationDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    DocumentGuid = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    UploadedDate = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    SelfDeclarationTypeCode = table.Column<int>(nullable: false),
                    SelfDeclarationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfDeclarationDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelfDeclarationDocument_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelfDeclarationDocument_SelfDeclaration_SelfDeclarationId",
                        column: x => x.SelfDeclarationId,
                        principalTable: "SelfDeclaration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SelfDeclarationDocument_SelfDeclarationTypeLookup_SelfDecla~",
                        column: x => x.SelfDeclarationTypeCode,
                        principalTable: "SelfDeclarationTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclarationDocument_EnrolleeId",
                table: "SelfDeclarationDocument",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclarationDocument_SelfDeclarationId",
                table: "SelfDeclarationDocument",
                column: "SelfDeclarationId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclarationDocument_SelfDeclarationTypeCode",
                table: "SelfDeclarationDocument",
                column: "SelfDeclarationTypeCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelfDeclarationDocument");
        }
    }
}
