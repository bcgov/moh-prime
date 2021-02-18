using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class StoreOnlySelectedHealthAuthorities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolleeHealthAuthority_FacilityLookup_FacilityCode",
                table: "EnrolleeHealthAuthority");

            migrationBuilder.DropIndex(
                name: "IX_EnrolleeHealthAuthority_FacilityCode",
                table: "EnrolleeHealthAuthority");

            migrationBuilder.DropColumn(
                name: "FacilityCode",
                table: "EnrolleeHealthAuthority");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacilityCode",
                table: "EnrolleeHealthAuthority",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeHealthAuthority_FacilityCode",
                table: "EnrolleeHealthAuthority",
                column: "FacilityCode");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolleeHealthAuthority_FacilityLookup_FacilityCode",
                table: "EnrolleeHealthAuthority",
                column: "FacilityCode",
                principalTable: "FacilityLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
