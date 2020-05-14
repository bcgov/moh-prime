using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddEnrolmentStatusAdjudicationNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrolmentStatusAdjudicatorNote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolmentStatusId = table.Column<int>(nullable: false),
                    AdjudicatorNoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentStatusAdjudicatorNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatusAdjudicatorNote_AdjudicatorNote_AdjudicatorN~",
                        column: x => x.AdjudicatorNoteId,
                        principalTable: "AdjudicatorNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatusAdjudicatorNote_EnrolmentStatus_EnrolmentSta~",
                        column: x => x.EnrolmentStatusId,
                        principalTable: "EnrolmentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatusAdjudicatorNote_AdjudicatorNoteId",
                table: "EnrolmentStatusAdjudicatorNote",
                column: "AdjudicatorNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatusAdjudicatorNote_EnrolmentStatusId",
                table: "EnrolmentStatusAdjudicatorNote",
                column: "EnrolmentStatusId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolmentStatusAdjudicatorNote");
        }
    }
}
