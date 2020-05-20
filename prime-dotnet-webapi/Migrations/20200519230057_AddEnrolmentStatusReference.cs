using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddEnrolmentStatusReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrolmentStatusReference",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolmentStatusId = table.Column<int>(nullable: false),
                    AdjudicatorNoteId = table.Column<int>(nullable: true),
                    AdminId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentStatusReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatusReference_AdjudicatorNote_AdjudicatorNoteId",
                        column: x => x.AdjudicatorNoteId,
                        principalTable: "AdjudicatorNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatusReference_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatusReference_EnrolmentStatus_EnrolmentStatusId",
                        column: x => x.EnrolmentStatusId,
                        principalTable: "EnrolmentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatusReference_AdjudicatorNoteId",
                table: "EnrolmentStatusReference",
                column: "AdjudicatorNoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatusReference_AdminId",
                table: "EnrolmentStatusReference",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatusReference_EnrolmentStatusId",
                table: "EnrolmentStatusReference",
                column: "EnrolmentStatusId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolmentStatusReference");
        }
    }
}
