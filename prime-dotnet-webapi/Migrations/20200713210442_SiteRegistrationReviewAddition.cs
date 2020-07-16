using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class SiteRegistrationReviewAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "SignedAgreement",
                newName: "Filename");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "SelfDeclarationDocument",
                newName: "Filename");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "BusinessLicence",
                newName: "Filename");

            migrationBuilder.CreateTable(
                name: "SiteRegistrationReview",
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
                    SiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteRegistrationReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteRegistrationReview_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteRegistrationReview_SiteId",
                table: "SiteRegistrationReview",
                column: "SiteId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteRegistrationReview");

            migrationBuilder.RenameColumn(
                name: "Filename",
                table: "SignedAgreement",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "Filename",
                table: "SelfDeclarationDocument",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "Filename",
                table: "BusinessLicence",
                newName: "FileName");
        }
    }
}
