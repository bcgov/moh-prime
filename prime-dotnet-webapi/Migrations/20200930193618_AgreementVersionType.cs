using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AgreementVersionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgreementId",
                table: "SignedAgreementDocument",
                nullable: true,
                defaultValue: null);

            migrationBuilder.AddColumn<int>(
                name: "AgreementVersionType",
                table: "AgreementVersion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SignedAgreementDocument_AgreementId",
                table: "SignedAgreementDocument",
                column: "AgreementId");

            migrationBuilder.AddForeignKey(
                name: "FK_SignedAgreementDocument_Agreement_AgreementId",
                table: "SignedAgreementDocument",
                column: "AgreementId",
                principalTable: "Agreement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignedAgreementDocument_Agreement_AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropIndex(
                name: "IX_SignedAgreementDocument_AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropColumn(
                name: "AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropColumn(
                name: "AgreementVersionType",
                table: "AgreementVersion");
        }
    }
}
