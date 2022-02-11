using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class FixDeleteEnrolleesWithNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentStatusReference_EnrolleeNote_AdjudicatorNoteId",
                table: "EnrolmentStatusReference");

            migrationBuilder.AlterColumn<int>(
                name: "AdjudicatorNoteId",
                table: "EnrolmentStatusReference",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentStatusReference_EnrolleeNote_AdjudicatorNoteId",
                table: "EnrolmentStatusReference",
                column: "AdjudicatorNoteId",
                principalTable: "EnrolleeNote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentStatusReference_EnrolleeNote_AdjudicatorNoteId",
                table: "EnrolmentStatusReference");

            migrationBuilder.AlterColumn<int>(
                name: "AdjudicatorNoteId",
                table: "EnrolmentStatusReference",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentStatusReference_EnrolleeNote_AdjudicatorNoteId",
                table: "EnrolmentStatusReference",
                column: "AdjudicatorNoteId",
                principalTable: "EnrolleeNote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
