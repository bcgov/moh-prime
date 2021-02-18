using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddAuditToAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTimeStamp",
                table: "PartyAddress",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "PartyAddress",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedTimeStamp",
                table: "PartyAddress",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "PartyAddress",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTimeStamp",
                table: "EnrolleeAddress",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "EnrolleeAddress",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedTimeStamp",
                table: "EnrolleeAddress",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "EnrolleeAddress",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTimeStamp",
                table: "PartyAddress");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "PartyAddress");

            migrationBuilder.DropColumn(
                name: "UpdatedTimeStamp",
                table: "PartyAddress");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "PartyAddress");

            migrationBuilder.DropColumn(
                name: "CreatedTimeStamp",
                table: "EnrolleeAddress");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "EnrolleeAddress");

            migrationBuilder.DropColumn(
                name: "UpdatedTimeStamp",
                table: "EnrolleeAddress");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "EnrolleeAddress");
        }
    }
}
