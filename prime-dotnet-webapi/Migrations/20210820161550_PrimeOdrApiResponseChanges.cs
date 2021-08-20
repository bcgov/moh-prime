using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class PrimeOdrApiResponseChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "PharmanetTransactionLog");

            migrationBuilder.AddColumn<string>(
                name: "LocationIpAddress",
                table: "PharmanetTransactionLog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceIpAddress",
                table: "PharmanetTransactionLog",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationIpAddress",
                table: "PharmanetTransactionLog");

            migrationBuilder.DropColumn(
                name: "SourceIpAddress",
                table: "PharmanetTransactionLog");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "PharmanetTransactionLog",
                type: "text",
                nullable: true);
        }
    }
}
