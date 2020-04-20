using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class BusinessEventAddPartySite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEvent_Enrollee_EnrolleeId",
                table: "BusinessEvent");

            migrationBuilder.AlterColumn<int>(
                name: "EnrolleeId",
                table: "BusinessEvent",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "BusinessEvent",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "BusinessEvent",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEvent_PartyId",
                table: "BusinessEvent",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEvent_SiteId",
                table: "BusinessEvent",
                column: "SiteId");

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

            migrationBuilder.DropIndex(
                name: "IX_BusinessEvent_PartyId",
                table: "BusinessEvent");

            migrationBuilder.DropIndex(
                name: "IX_BusinessEvent_SiteId",
                table: "BusinessEvent");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "BusinessEvent");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "BusinessEvent");

            migrationBuilder.AlterColumn<int>(
                name: "EnrolleeId",
                table: "BusinessEvent",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Enrollee_EnrolleeId",
                table: "BusinessEvent",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
