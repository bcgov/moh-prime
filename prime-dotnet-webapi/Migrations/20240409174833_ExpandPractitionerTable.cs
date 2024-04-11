using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class ExpandPractitionerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateofBirth",
                table: "Practitioner",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EffectiveDate",
                table: "Practitioner",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleInitial",
                table: "Practitioner",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Practitioner",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateofBirth",
                table: "Practitioner");

            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "Practitioner");

            migrationBuilder.DropColumn(
                name: "MiddleInitial",
                table: "Practitioner");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Practitioner");
        }
    }
}
