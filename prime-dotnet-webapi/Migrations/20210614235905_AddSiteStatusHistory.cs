using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddSiteStatusHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    StatusType = table.Column<int>(nullable: false),
                    StatusDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteStatus_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteStatus_SiteId",
                table: "SiteStatus",
                column: "SiteId");


            // Migrate the site status data to the new SiteStatus table
            // bump old site status by 1, and insert into new table
            migrationBuilder.Sql(@"
                    INSERT INTO public.""SiteStatus"" (""CreatedUserId"", ""CreatedTimeStamp"", ""UpdatedUserId"", ""UpdatedTimeStamp"", ""SiteId"", ""StatusType"", ""StatusDate"")
                    SELECT ""CreatedUserId"", ""CreatedTimeStamp"", ""UpdatedUserId"", ""UpdatedTimeStamp"", ""Id"" AS ""SiteId"", (""Status"" + 1) AS ""StatusType"", ""UpdatedTimeStamp"" AS ""StatusDate""
                    FROM public.""Site""
                    WHERE ""SubmittedDate"" IS NOT NULL;
            ");

            // the old status 1 without SubmittedDate becomes the new Active
            migrationBuilder.Sql(@"
                    INSERT INTO public.""SiteStatus"" (""CreatedUserId"", ""CreatedTimeStamp"", ""UpdatedUserId"", ""UpdatedTimeStamp"", ""SiteId"", ""StatusType"", ""StatusDate"")
                    SELECT ""CreatedUserId"", ""CreatedTimeStamp"", ""UpdatedUserId"", ""UpdatedTimeStamp"", ""Id"" AS ""SiteId"", 1 AS ""StatusType"", ""UpdatedTimeStamp"" AS ""StatusDate""
                    FROM public.""Site""
                    WHERE ""Status"" = 1 AND ""SubmittedDate"" IS NULL;
            ");

            // Drop the Status column of Site table
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Site"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteStatus");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Site",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
