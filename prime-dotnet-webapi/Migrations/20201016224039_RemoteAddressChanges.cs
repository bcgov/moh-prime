using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class RemoteAddressChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemoteUserLocation");

            migrationBuilder.DropIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument");

            migrationBuilder.CreateTable(
                name: "RemoteAccessLocation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    InternetProvider = table.Column<string>(nullable: false),
                    PhysicalAddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteAccessLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemoteAccessLocation_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RemoteAccessLocation_Address_PhysicalAddressId",
                        column: x => x.PhysicalAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteAccessLocation_EnrolleeId",
                table: "RemoteAccessLocation",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteAccessLocation_PhysicalAddressId",
                table: "RemoteAccessLocation",
                column: "PhysicalAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemoteAccessLocation");

            migrationBuilder.DropIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument");

            migrationBuilder.CreateTable(
                name: "RemoteUserLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    InternetProvider = table.Column<string>(type: "text", nullable: false),
                    PhysicalAddressId = table.Column<int>(type: "integer", nullable: false),
                    RemoteUserId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteUserLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemoteUserLocation_Address_PhysicalAddressId",
                        column: x => x.PhysicalAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RemoteUserLocation_RemoteUser_RemoteUserId",
                        column: x => x.RemoteUserId,
                        principalTable: "RemoteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument",
                column: "EnrolleeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RemoteUserLocation_PhysicalAddressId",
                table: "RemoteUserLocation",
                column: "PhysicalAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteUserLocation_RemoteUserId",
                table: "RemoteUserLocation",
                column: "RemoteUserId");
        }
    }
}
