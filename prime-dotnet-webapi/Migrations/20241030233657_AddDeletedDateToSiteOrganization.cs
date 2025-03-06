using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedDateToSiteOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedDate",
                table: "Site",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedDate",
                table: "Organization",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Organization");
        }
    }
}
