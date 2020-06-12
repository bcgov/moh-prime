using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class OrganizationBusinessEventType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "BusinessEvent",
                nullable: true);

            migrationBuilder.InsertData(
                table: "BusinessEventTypeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 8, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Organization", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEvent_OrganizationId",
                table: "BusinessEvent",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Organization_OrganizationId",
                table: "BusinessEvent",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEvent_Organization_OrganizationId",
                table: "BusinessEvent");

            migrationBuilder.DropIndex(
                name: "IX_BusinessEvent_OrganizationId",
                table: "BusinessEvent");

            migrationBuilder.DeleteData(
                table: "BusinessEventTypeLookup",
                keyColumn: "Code",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "BusinessEvent");
        }
    }
}
