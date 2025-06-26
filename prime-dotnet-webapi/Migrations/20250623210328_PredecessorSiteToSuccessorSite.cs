using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class PredecessorSiteToSuccessorSite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PredecessorSiteToSuccessorSite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PredecessorSiteId = table.Column<int>(type: "integer", nullable: false),
                    SuccessorSiteId = table.Column<int>(type: "integer", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredecessorSiteToSuccessorSite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PredecessorSiteToSuccessorSite_Site_PredecessorSiteId",
                        column: x => x.PredecessorSiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PredecessorSiteToSuccessorSite_Site_SuccessorSiteId",
                        column: x => x.SuccessorSiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PredecessorSiteToSuccessorSite_PredecessorSiteId",
                table: "PredecessorSiteToSuccessorSite",
                column: "PredecessorSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_PredecessorSiteToSuccessorSite_SuccessorSiteId",
                table: "PredecessorSiteToSuccessorSite",
                column: "SuccessorSiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PredecessorSiteToSuccessorSite");
        }
    }
}
