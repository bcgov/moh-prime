using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace prime.Migrations
{
    public partial class UpdateApplications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PharmacistRegistrationNumberId",
                schema: "public",
                table: "Application",
                newName: "PharmacistRegistrationNumber");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppliedDate",
                schema: "public",
                table: "Application",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                schema: "public",
                table: "Application",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedDate",
                schema: "public",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                schema: "public",
                table: "Application");

            migrationBuilder.RenameColumn(
                name: "PharmacistRegistrationNumber",
                schema: "public",
                table: "Application",
                newName: "PharmacistRegistrationNumberId");
        }
    }
}
