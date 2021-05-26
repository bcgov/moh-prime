using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class HALikeCareSettingWithJobTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthAuthorityCode",
                table: "OboSite",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "OboSite",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OboSite_HealthAuthorityCode",
                table: "OboSite",
                column: "HealthAuthorityCode");

            migrationBuilder.AddForeignKey(
                name: "FK_OboSite_HealthAuthorityLookup_HealthAuthorityCode",
                table: "OboSite",
                column: "HealthAuthorityCode",
                principalTable: "HealthAuthorityLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OboSite_HealthAuthorityLookup_HealthAuthorityCode",
                table: "OboSite");

            migrationBuilder.DropIndex(
                name: "IX_OboSite_HealthAuthorityCode",
                table: "OboSite");

            migrationBuilder.DropColumn(
                name: "HealthAuthorityCode",
                table: "OboSite");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "OboSite");
        }
    }
}
