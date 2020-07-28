using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class CascadeDeleteOrganizations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_BusinessEvent_Organization_OrganizationId",
               table: "BusinessEvent");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Organization_OrganizationId",
                table: "BusinessEvent",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
