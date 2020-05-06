using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateBusinessEventDeleteAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEvent_Enrollee_EnrolleeId",
                table: "BusinessEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEvent_Party_PartyId",
                table: "BusinessEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEvent_Site_SiteId",
                table: "BusinessEvent");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Enrollee_EnrolleeId",
                table: "BusinessEvent",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Party_PartyId",
                table: "BusinessEvent",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Site_SiteId",
                table: "BusinessEvent",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEvent_Enrollee_EnrolleeId",
                table: "BusinessEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEvent_Party_PartyId",
                table: "BusinessEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEvent_Site_SiteId",
                table: "BusinessEvent");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Enrollee_EnrolleeId",
                table: "BusinessEvent",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Party_PartyId",
                table: "BusinessEvent",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Site_SiteId",
                table: "BusinessEvent",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
