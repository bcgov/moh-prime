using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class CreateBusinessLicenceAndMigrateBusincessLicenceDocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessLicenceDocument_Site_SiteId",
                table: "BusinessLicenceDocument");

            migrationBuilder.AddColumn<int>(
                name: "BusinessLicenceId",
                table: "BusinessLicenceDocument",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BusinessLicence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    DeferredLicenceReason = table.Column<string>(nullable: true),
                    Completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessLicence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessLicence_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLicenceDocument_BusinessLicenceId",
                table: "BusinessLicenceDocument",
                column: "BusinessLicenceId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLicence_SiteId",
                table: "BusinessLicence",
                column: "SiteId",
                unique: true);

            migrationBuilder.Sql(@"
                insert into ""BusinessLicence""
                (
                    ""SiteId"",
                    ""Completed"",
                    ""CreatedUserId"",
                    ""CreatedTimeStamp"",
                    ""UpdatedUserId"",
                    ""UpdatedTimeStamp""
                )
                select distinct on (bld.""SiteId"")
                    bld.""SiteId"" as ""SiteId"",
                    true as ""Completed"",
                    bld.""CreatedUserId"" as ""CreatedUserId"",
                    current_timestamp as ""CreatedTimeStamp"",
                    bld.""UpdatedUserId"" as ""UpdatedUserId"",
                    current_timestamp as ""UpdatedTimeStamp""
                from ""BusinessLicenceDocument"" bld;
            ");

            migrationBuilder.Sql(@"
                update ""BusinessLicenceDocument"" bld
                set ""BusinessLicenceId"" = bl.""Id""
                from ""BusinessLicence"" bl
                where bl.""SiteId"" = bld.""SiteId"";
            ");

            migrationBuilder.Sql(@"
                delete from ""BusinessLicenceDocument""
                where ""Id"" not in (
	                select distinct on (bld.""SiteId"") ""Id""
	                from ""BusinessLicenceDocument"" bld
	                ORDER  BY bld.""SiteId"", bld.""UploadedDate"" DESC
                );
            ");

            migrationBuilder.DropIndex(
                name: "IX_BusinessLicenceDocument_SiteId",
                table: "BusinessLicenceDocument");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "BusinessLicenceDocument");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessLicenceDocument_BusinessLicence_BusinessLicenceId",
                table: "BusinessLicenceDocument",
                column: "BusinessLicenceId",
                principalTable: "BusinessLicence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessLicenceDocument_BusinessLicence_BusinessLicenceId",
                table: "BusinessLicenceDocument");

            migrationBuilder.DropTable(
                name: "BusinessLicence");

            migrationBuilder.DropIndex(
                name: "IX_BusinessLicenceDocument_BusinessLicenceId",
                table: "BusinessLicenceDocument");

            migrationBuilder.DropColumn(
                name: "BusinessLicenceId",
                table: "BusinessLicenceDocument");

            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "BusinessLicenceDocument",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLicenceDocument_SiteId",
                table: "BusinessLicenceDocument",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessLicenceDocument_Site_SiteId",
                table: "BusinessLicenceDocument",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
