using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class PartyNameToBusinessEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PartyName",
                table: "BusinessEvent",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyName",
                table: "BusinessEvent");
        }
    }
}
