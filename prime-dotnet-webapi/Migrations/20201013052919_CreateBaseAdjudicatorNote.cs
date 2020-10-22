using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class CreateBaseAdjudicatorNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentStatusReference_AdjudicatorNote_AdjudicatorNoteId",
                table: "EnrolmentStatusReference");

            migrationBuilder.DropIndex(
                name: "IX_AdjudicatorNote_AdjudicatorId",
                table: "AdjudicatorNote");

            migrationBuilder.DropIndex(
                name: "IX_AdjudicatorNote_EnrolleeId",
                table: "AdjudicatorNote");

            migrationBuilder.RenameTable(
                name: "AdjudicatorNote",
                newName: "EnrolleeNote");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeNote_AdjudicatorId",
                table: "EnrolleeNote",
                column: "AdjudicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeNote_EnrolleeId",
                table: "EnrolleeNote",
                column: "EnrolleeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentStatusReference_EnrolleeNote_AdjudicatorNoteId",
                table: "EnrolmentStatusReference",
                column: "AdjudicatorNoteId",
                principalTable: "EnrolleeNote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "AccessAgreementNote",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdjudicatorId",
                table: "AccessAgreementNote",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccessAgreementNote_AdjudicatorId",
                table: "AccessAgreementNote",
                column: "AdjudicatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessAgreementNote_Admin_AdjudicatorId",
                table: "AccessAgreementNote",
                column: "AdjudicatorId",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessAgreementNote_Admin_AdjudicatorId",
                table: "AccessAgreementNote");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentStatusReference_EnrolleeNote_AdjudicatorNoteId",
                table: "EnrolmentStatusReference");

            migrationBuilder.DropTable(
                name: "EnrolleeNote");

            migrationBuilder.DropIndex(
                name: "IX_AccessAgreementNote_AdjudicatorId",
                table: "AccessAgreementNote");

            migrationBuilder.DropColumn(
                name: "AdjudicatorId",
                table: "AccessAgreementNote");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "AccessAgreementNote",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "AdjudicatorNote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdjudicatorId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnrolleeId = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    NoteDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjudicatorNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdjudicatorNote_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdjudicatorNote_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 11,
                column: "EffectiveDate",
                value: new DateTimeOffset(new DateTime(2020, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_AdjudicatorNote_AdjudicatorId",
                table: "AdjudicatorNote",
                column: "AdjudicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AdjudicatorNote_EnrolleeId",
                table: "AdjudicatorNote",
                column: "EnrolleeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentStatusReference_AdjudicatorNote_AdjudicatorNoteId",
                table: "EnrolmentStatusReference",
                column: "AdjudicatorNoteId",
                principalTable: "AdjudicatorNote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
