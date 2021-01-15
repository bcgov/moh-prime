using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class CreateEscalationTablesAndReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrolmentEscalation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeNoteId = table.Column<int>(nullable: false),
                    AdminId = table.Column<int>(nullable: false),
                    AssigneeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentEscalation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolmentEscalation_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolmentEscalation_Admin_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolmentEscalation_EnrolleeNote_EnrolleeNoteId",
                        column: x => x.EnrolleeNoteId,
                        principalTable: "EnrolleeNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteEscalation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    SiteRegistrationNoteId = table.Column<int>(nullable: false),
                    AdminId = table.Column<int>(nullable: false),
                    AssigneeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteEscalation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteEscalation_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteEscalation_Admin_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteEscalation_SiteRegistrationNote_SiteRegistrationNoteId",
                        column: x => x.SiteRegistrationNoteId,
                        principalTable: "SiteRegistrationNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentEscalation_AdminId",
                table: "EnrolmentEscalation",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentEscalation_AssigneeId",
                table: "EnrolmentEscalation",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentEscalation_EnrolleeNoteId",
                table: "EnrolmentEscalation",
                column: "EnrolleeNoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SiteEscalation_AdminId",
                table: "SiteEscalation",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteEscalation_AssigneeId",
                table: "SiteEscalation",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteEscalation_SiteRegistrationNoteId",
                table: "SiteEscalation",
                column: "SiteRegistrationNoteId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolmentEscalation");

            migrationBuilder.DropTable(
                name: "SiteEscalation");
        }
    }
}
