using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddHealthAuthorityTechnicalSupportVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthAuthorityTechnicalSupportVendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HealthAuthorityTechnicalSupportId = table.Column<int>(type: "integer", nullable: false),
                    HealthAuthorityVendorId = table.Column<int>(type: "integer", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthAuthorityTechnicalSupportVendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthAuthorityTechnicalSupportVendor_HealthAuthorityContac~",
                        column: x => x.HealthAuthorityTechnicalSupportId,
                        principalTable: "HealthAuthorityContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthAuthorityTechnicalSupportVendor_HealthAuthorityVendor~",
                        column: x => x.HealthAuthorityVendorId,
                        principalTable: "HealthAuthorityVendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthorityTechnicalSupportVendor_HealthAuthorityTechni~",
                table: "HealthAuthorityTechnicalSupportVendor",
                column: "HealthAuthorityTechnicalSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthAuthorityTechnicalSupportVendor_HealthAuthorityVendor~",
                table: "HealthAuthorityTechnicalSupportVendor",
                column: "HealthAuthorityVendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthAuthorityTechnicalSupportVendor");
        }
    }
}
