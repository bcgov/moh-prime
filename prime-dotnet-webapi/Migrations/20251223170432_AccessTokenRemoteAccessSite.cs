using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class AccessTokenRemoteAccessSite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessTokenRemoteAccessSite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnrolmentCertificateAccessTokenId = table.Column<Guid>(type: "uuid", nullable: false),
                    SiteId = table.Column<int>(type: "integer", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessTokenRemoteAccessSite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessTokenRemoteAccessSite_EnrolmentCertificateAccessToken~",
                        column: x => x.EnrolmentCertificateAccessTokenId,
                        principalTable: "EnrolmentCertificateAccessToken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessTokenRemoteAccessSite_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessTokenRemoteAccessSite_EnrolmentCertificateAccessToken~",
                table: "AccessTokenRemoteAccessSite",
                column: "EnrolmentCertificateAccessTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessTokenRemoteAccessSite_SiteId",
                table: "AccessTokenRemoteAccessSite",
                column: "SiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessTokenRemoteAccessSite");
        }
    }
}
