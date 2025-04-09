using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class ArchvedSiteChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ArchivedDate",
                table: "Site",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "StatusLookup",
                columns: new[] { "Code", "Name" },
                values: new object[] { 8, "Archived" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "ArchivedDate",
                table: "Site");
        }
    }
}
