using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AgreementVersionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Enrollee_EnrolleeId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Organization_OrganizationId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Party_PartyId",
                table: "Agreement");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Agreement_OnlyOneForeignKey",
                table: "Agreement");

            migrationBuilder.CreateCheckConstraint(
                name: "CHK_Agreement_OnlyOneForeignKey",
                table: "Agreement",
                sql: "( CASE WHEN \"EnrolleeId\" IS NULL THEN 0 ELSE 1 END + CASE WHEN \"OrganizationId\" IS NULL THEN 0 ELSE 1 END + CASE WHEN \"PartyId\" IS NULL THEN 0 ELSE 1 END) = 1");

            migrationBuilder.AddColumn<int>(
                name: "AgreementId",
                table: "SignedAgreementDocument",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgreementVersionType",
                table: "AgreementVersion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PartyId",
                table: "Agreement",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Agreement",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EnrolleeId",
                table: "Agreement",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 9,
                column: "AgreementVersionType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 10,
                column: "AgreementVersionType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 1,
                column: "AgreementVersionType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 3,
                column: "AgreementVersionType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 5,
                column: "AgreementVersionType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 7,
                column: "AgreementVersionType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 2,
                column: "AgreementVersionType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 4,
                column: "AgreementVersionType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 6,
                column: "AgreementVersionType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 8,
                column: "AgreementVersionType",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_SignedAgreementDocument_AgreementId",
                table: "SignedAgreementDocument",
                column: "AgreementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Enrollee_EnrolleeId",
                table: "Agreement",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Organization_OrganizationId",
                table: "Agreement",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Party_PartyId",
                table: "Agreement",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SignedAgreementDocument_Agreement_AgreementId",
                table: "SignedAgreementDocument",
                column: "AgreementId",
                principalTable: "Agreement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Enrollee_EnrolleeId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Organization_OrganizationId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Party_PartyId",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_SignedAgreementDocument_Agreement_AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropIndex(
                name: "IX_SignedAgreementDocument_AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Agreement_OnlyOneForeignKey",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "AgreementId",
                table: "SignedAgreementDocument");

            migrationBuilder.DropColumn(
                name: "AgreementVersionType",
                table: "AgreementVersion");

            migrationBuilder.CreateCheckConstraint(
                name: "CHK_Agreement_OnlyOneForeignKey",
                table: "Agreement",
                sql: "( CASE WHEN FK_EnrolleeId IS NULL THEN 0 ELSE 1 END+ CASE WHEN FK_OrganizationId IS NULL THEN 0 ELSE 1 END+ CASE WHEN FK_PartyId IS NULL THEN 0 ELSE 1 END) = 1");

            migrationBuilder.AlterColumn<int>(
                name: "PartyId",
                table: "Agreement",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Agreement",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EnrolleeId",
                table: "Agreement",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Enrollee_EnrolleeId",
                table: "Agreement",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Organization_OrganizationId",
                table: "Agreement",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Party_PartyId",
                table: "Agreement",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
