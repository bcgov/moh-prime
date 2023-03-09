using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class SeparateHACareTypeToVendorRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthAuthorityCareTypeToVendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HealthAuthorityCareTypeId = table.Column<int>(type: "integer", nullable: false),
                    HealthAuthorityVendorId = table.Column<int>(type: "integer", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthAuthorityCareTypeToVendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthAuthorityCareTypeToVendor_HealthAuthorityCareType_Hea~",
                        column: x => x.HealthAuthorityCareTypeId,
                        principalTable: "HealthAuthorityCareType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthAuthorityCareTypeToVendor_HealthAuthorityVendor_Healt~",
                        column: x => x.HealthAuthorityVendorId,
                        principalTable: "HealthAuthorityVendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthorityCareTypeToVendor_HealthAuthorityCareTypeId",
                table: "HealthAuthorityCareTypeToVendor",
                column: "HealthAuthorityCareTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthorityCareTypeToVendor_HealthAuthorityVendorId",
                table: "HealthAuthorityCareTypeToVendor",
                column: "HealthAuthorityVendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthAuthorityCareTypeToVendor");
        }
    }
}
