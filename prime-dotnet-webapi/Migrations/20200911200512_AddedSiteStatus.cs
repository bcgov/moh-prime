using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddedSiteStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Site",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NewestAgreements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewestAgreements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteRegistrationNote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    AdjudicatorId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: false),
                    NoteDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteRegistrationNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteRegistrationNote_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteRegistrationNote_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteRegistrationNote_AdjudicatorId",
                table: "SiteRegistrationNote",
                column: "AdjudicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteRegistrationNote_SiteId",
                table: "SiteRegistrationNote",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewestAgreements");

            migrationBuilder.DropTable(
                name: "SiteRegistrationNote");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Site");
        }
    }
}
