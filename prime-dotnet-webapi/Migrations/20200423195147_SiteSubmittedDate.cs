using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class SiteSubmittedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SubmittedDate",
                table: "Site",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Vendor",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "CareConnect");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedDate",
                table: "Site");

            migrationBuilder.UpdateData(
                table: "Vendor",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Care Connect");
        }
    }
}
