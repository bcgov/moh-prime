using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class ClauseOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTerm_LimitsConditionsClause_LimitsConditionsClauseId",
                table: "AccessTerm");

            migrationBuilder.AlterColumn<string>(
                name: "Clause",
                table: "LimitsConditionsClause",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "LimitsConditionsClauseId",
                table: "AccessTerm",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTerm_LimitsConditionsClause_LimitsConditionsClauseId",
                table: "AccessTerm",
                column: "LimitsConditionsClauseId",
                principalTable: "LimitsConditionsClause",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTerm_LimitsConditionsClause_LimitsConditionsClauseId",
                table: "AccessTerm");

            migrationBuilder.AlterColumn<string>(
                name: "Clause",
                table: "LimitsConditionsClause",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LimitsConditionsClauseId",
                table: "AccessTerm",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTerm_LimitsConditionsClause_LimitsConditionsClauseId",
                table: "AccessTerm",
                column: "LimitsConditionsClauseId",
                principalTable: "LimitsConditionsClause",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
