using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class AddSiteSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Agreement_EitherPartyOrEnrollee",
                table: "Agreement");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "HealthAuthorityContact",
                type: "character varying(55)",
                maxLength: 55,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "SiteSubmission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SiteId = table.Column<int>(type: "integer", nullable: false),
                    ProfileSnapshot = table.Column<string>(type: "json", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSubmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteSubmission_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Agreement_EitherPartyOrEnrollee",
                table: "Agreement",
                sql: "( CASE WHEN \"EnrolleeId\" IS NULL THEN 0 ELSE 1 END\r\n                     + CASE WHEN \"PartyId\" IS NULL THEN 0 ELSE 1 END) = 1");

            migrationBuilder.CreateIndex(
                name: "IX_SiteSubmission_SiteId",
                table: "SiteSubmission",
                column: "SiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteSubmission");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Agreement_EitherPartyOrEnrollee",
                table: "Agreement");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "HealthAuthorityContact",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55);

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Agreement_EitherPartyOrEnrollee",
                table: "Agreement",
                sql: "( CASE WHEN \"EnrolleeId\" IS NULL THEN 0 ELSE 1 END\n                     + CASE WHEN \"PartyId\" IS NULL THEN 0 ELSE 1 END) = 1");
        }
    }
}
