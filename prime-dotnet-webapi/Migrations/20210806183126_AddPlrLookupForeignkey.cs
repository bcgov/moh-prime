using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddPlrLookupForeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlrProvider_ProviderRoleType",
                table: "PlrProvider",
                column: "ProviderRoleType");

            migrationBuilder.CreateIndex(
                name: "IX_PlrProvider_StatusReasonCode",
                table: "PlrProvider",
                column: "StatusReasonCode");

            migrationBuilder.AddForeignKey(
                name: "FK_PlrProvider_PlrRoleTypeLookup_ProviderRoleType",
                table: "PlrProvider",
                column: "ProviderRoleType",
                principalTable: "PlrRoleTypeLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlrProvider_PlrStatusReasonLookup_StatusReasonCode",
                table: "PlrProvider",
                column: "StatusReasonCode",
                principalTable: "PlrStatusReasonLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlrProvider_PlrRoleTypeLookup_ProviderRoleType",
                table: "PlrProvider");

            migrationBuilder.DropForeignKey(
                name: "FK_PlrProvider_PlrStatusReasonLookup_StatusReasonCode",
                table: "PlrProvider");

            migrationBuilder.DropIndex(
                name: "IX_PlrProvider_ProviderRoleType",
                table: "PlrProvider");

            migrationBuilder.DropIndex(
                name: "IX_PlrProvider_StatusReasonCode",
                table: "PlrProvider");
        }
    }
}
