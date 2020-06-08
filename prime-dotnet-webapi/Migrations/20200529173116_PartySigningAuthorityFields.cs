using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class PartySigningAuthorityFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MailingAddressId",
                table: "Party",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Party",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredFirstName",
                table: "Party",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredLastName",
                table: "Party",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredMiddleName",
                table: "Party",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Party_MailingAddressId",
                table: "Party",
                column: "MailingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Party_Address_MailingAddressId",
                table: "Party",
                column: "MailingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Party_Address_MailingAddressId",
                table: "Party");

            migrationBuilder.DropIndex(
                name: "IX_Party_MailingAddressId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "MailingAddressId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "PreferredFirstName",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "PreferredLastName",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "PreferredMiddleName",
                table: "Party");
        }
    }
}
