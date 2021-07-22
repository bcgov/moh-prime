using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class MutlipleBusinessLicences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BusinessLicence_SiteId",
                table: "BusinessLicence");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExpiryDate",
                table: "BusinessLicence",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UploadedDate",
                table: "BusinessLicence",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLicence_SiteId",
                table: "BusinessLicence",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BusinessLicence_SiteId",
                table: "BusinessLicence");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "BusinessLicence");

            migrationBuilder.DropColumn(
                name: "UploadedDate",
                table: "BusinessLicence");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLicence_SiteId",
                table: "BusinessLicence",
                column: "SiteId",
                unique: true);
        }
    }
}
