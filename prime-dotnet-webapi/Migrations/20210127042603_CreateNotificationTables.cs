using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class CreateNotificationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrolleeNotification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    AdminId = table.Column<int>(nullable: false),
                    AssigneeId = table.Column<int>(nullable: false),
                    EnrolleeNoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeNotification_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeNotification_Admin_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeNotification_EnrolleeNote_EnrolleeNoteId",
                        column: x => x.EnrolleeNoteId,
                        principalTable: "EnrolleeNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteNotification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    AdminId = table.Column<int>(nullable: false),
                    AssigneeId = table.Column<int>(nullable: false),
                    SiteRegistrationNoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteNotification_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteNotification_Admin_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteNotification_SiteRegistrationNote_SiteRegistrationNoteId",
                        column: x => x.SiteRegistrationNoteId,
                        principalTable: "SiteRegistrationNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeNotification_AdminId",
                table: "EnrolleeNotification",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeNotification_AssigneeId",
                table: "EnrolleeNotification",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeNotification_EnrolleeNoteId",
                table: "EnrolleeNotification",
                column: "EnrolleeNoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SiteNotification_AdminId",
                table: "SiteNotification",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteNotification_AssigneeId",
                table: "SiteNotification",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteNotification_SiteRegistrationNoteId",
                table: "SiteNotification",
                column: "SiteRegistrationNoteId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolleeNotification");

            migrationBuilder.DropTable(
                name: "SiteNotification");
        }
    }
}
