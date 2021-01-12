using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class CredentialAddRevocationIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CredentialRevocationId",
                table: "Credential",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevocationRegistryId",
                table: "Credential",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CredentialRevocationId",
                table: "Credential");

            migrationBuilder.DropColumn(
                name: "RevocationRegistryId",
                table: "Credential");
        }
    }
}
