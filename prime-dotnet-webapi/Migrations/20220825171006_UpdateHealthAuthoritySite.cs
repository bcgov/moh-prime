using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateHealthAuthoritySite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthAuthoritySite_HealthAuthorityVendor_HealthAuthorityVe~",
                table: "HealthAuthoritySite");

            migrationBuilder.AlterColumn<int>(
                name: "HealthAuthorityVendorId",
                table: "HealthAuthoritySite",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthAuthoritySite_HealthAuthorityVendor_HealthAuthorityVe~",
                table: "HealthAuthoritySite",
                column: "HealthAuthorityVendorId",
                principalTable: "HealthAuthorityVendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthAuthoritySite_HealthAuthorityVendor_HealthAuthorityVe~",
                table: "HealthAuthoritySite");

            migrationBuilder.AlterColumn<int>(
                name: "HealthAuthorityVendorId",
                table: "HealthAuthoritySite",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthAuthoritySite_HealthAuthorityVendor_HealthAuthorityVe~",
                table: "HealthAuthoritySite",
                column: "HealthAuthorityVendorId",
                principalTable: "HealthAuthorityVendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
