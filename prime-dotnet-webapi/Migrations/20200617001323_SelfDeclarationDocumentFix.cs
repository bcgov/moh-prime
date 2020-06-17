using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class SelfDeclarationDocumentFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelfDeclarationDocument_SelfDeclaration_SelfDeclarationId",
                table: "SelfDeclarationDocument");

            migrationBuilder.DropIndex(
                name: "IX_SelfDeclarationDocument_SelfDeclarationId",
                table: "SelfDeclarationDocument");

            migrationBuilder.DropColumn(
                name: "SelfDeclarationId",
                table: "SelfDeclarationDocument");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelfDeclarationId",
                table: "SelfDeclarationDocument",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclarationDocument_SelfDeclarationId",
                table: "SelfDeclarationDocument",
                column: "SelfDeclarationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelfDeclarationDocument_SelfDeclaration_SelfDeclarationId",
                table: "SelfDeclarationDocument",
                column: "SelfDeclarationId",
                principalTable: "SelfDeclaration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
