using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddBusinessHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours24",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "HoursSpecial",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "HoursWeekend",
                table: "Location");

            migrationBuilder.CreateTable(
                name: "BusinessDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessDay_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessDay_LocationId",
                table: "BusinessDay",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessDay");

            migrationBuilder.AddColumn<bool>(
                name: "Hours24",
                table: "Location",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HoursSpecial",
                table: "Location",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HoursWeekend",
                table: "Location",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
