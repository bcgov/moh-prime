using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class CreateDocumentsForAdjudications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrolleeAdjudicationDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    DocumentGuid = table.Column<Guid>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    UploadedDate = table.Column<DateTimeOffset>(nullable: false),
                    AdjudicatorId = table.Column<int>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeAdjudicationDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeAdjudicationDocument_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeAdjudicationDocument_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteAdjudicationDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    DocumentGuid = table.Column<Guid>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    UploadedDate = table.Column<DateTimeOffset>(nullable: false),
                    AdjudicatorId = table.Column<int>(nullable: false),
                    SiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAdjudicationDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteAdjudicationDocument_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteAdjudicationDocument_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeAdjudicationDocument_AdjudicatorId",
                table: "EnrolleeAdjudicationDocument",
                column: "AdjudicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeAdjudicationDocument_EnrolleeId",
                table: "EnrolleeAdjudicationDocument",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAdjudicationDocument_AdjudicatorId",
                table: "SiteAdjudicationDocument",
                column: "AdjudicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAdjudicationDocument_SiteId",
                table: "SiteAdjudicationDocument",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolleeAdjudicationDocument");

            migrationBuilder.DropTable(
                name: "SiteAdjudicationDocument");
        }
    }
}
