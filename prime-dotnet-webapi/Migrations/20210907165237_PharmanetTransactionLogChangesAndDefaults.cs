using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class PharmanetTransactionLogChangesAndDefaults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "PharmanetTransactionLog");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "PharmanetTransactionLog");

            migrationBuilder.DropColumn(
                name: "UpdatedTimeStamp",
                table: "PharmanetTransactionLog");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "PharmanetTransactionLog");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedTimeStamp",
                table: "PharmanetTransactionLog",
                nullable: false,
                defaultValueSql: "current_timestamp",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "LocationIpAddress",
                table: "PharmanetTransactionLog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceIpAddress",
                table: "PharmanetTransactionLog",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationIpAddress",
                table: "PharmanetTransactionLog");

            migrationBuilder.DropColumn(
                name: "SourceIpAddress",
                table: "PharmanetTransactionLog");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedTimeStamp",
                table: "PharmanetTransactionLog",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldDefaultValueSql: "current_timestamp");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "PharmanetTransactionLog",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "PharmanetTransactionLog",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedTimeStamp",
                table: "PharmanetTransactionLog",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "PharmanetTransactionLog",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
