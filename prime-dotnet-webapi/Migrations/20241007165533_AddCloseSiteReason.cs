using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class AddCloseSiteReason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ClosedDate",
                table: "Site",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteCloseReasonCode",
                table: "Site",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SiteCloseReasonLookup",
                columns: table => new
                {
                    Code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteCloseReasonLookup", x => x.Code);
                });

            migrationBuilder.InsertData(
                table: "SiteCloseReasonLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 1, "Licence cancelled" },
                    { 2, "Closed by Organization" },
                    { 3, "Other" }
                });

            migrationBuilder.InsertData(
                table: "StatusLookup",
                columns: new[] { "Code", "Name" },
                values: new object[] { 7, "Closed" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteCloseReasonLookup");

            migrationBuilder.DeleteData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "ClosedDate",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "SiteCloseReasonCode",
                table: "Site");
        }
    }
}
