using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class ChangedSiteRegRemoteUserToSingular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RemoteUserCertification_RemoteUserId",
                table: "RemoteUserCertification");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteUserCertification_RemoteUserId",
                table: "RemoteUserCertification",
                column: "RemoteUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RemoteUserCertification_RemoteUserId",
                table: "RemoteUserCertification");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteUserCertification_RemoteUserId",
                table: "RemoteUserCertification",
                column: "RemoteUserId");
        }
    }
}
