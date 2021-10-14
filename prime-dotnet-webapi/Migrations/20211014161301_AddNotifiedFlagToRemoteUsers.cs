using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddNotifiedFlagToRemoteUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Notified",
                table: "RemoteUser",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notified",
                table: "RemoteUser");
        }
    }
}
