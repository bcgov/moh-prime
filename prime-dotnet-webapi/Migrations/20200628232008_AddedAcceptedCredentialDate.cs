using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddedAcceptedCredentialDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "base64QRCode",
                table: "Credential",
                newName: "Base64QRCode");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AcceptedCredentialDate",
                table: "Credential",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Credential",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedCredentialDate",
                table: "Credential");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Credential");

            migrationBuilder.RenameColumn(
                name: "Base64QRCode",
                table: "Credential",
                newName: "base64QRCode");
        }
    }
}
