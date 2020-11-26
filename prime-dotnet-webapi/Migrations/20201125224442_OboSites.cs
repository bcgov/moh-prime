using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class OboSites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OboSite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    CareSettingCode = table.Column<int>(nullable: false),
                    SiteName = table.Column<string>(nullable: true),
                    PEC = table.Column<string>(nullable: true),
                    Facility = table.Column<string>(nullable: true),
                    PhysicalAddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OboSite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OboSite_CareSettingLookup_CareSettingCode",
                        column: x => x.CareSettingCode,
                        principalTable: "CareSettingLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OboSite_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OboSite_Address_PhysicalAddressId",
                        column: x => x.PhysicalAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OboSite_CareSettingCode",
                table: "OboSite",
                column: "CareSettingCode");

            migrationBuilder.CreateIndex(
                name: "IX_OboSite_EnrolleeId",
                table: "OboSite",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_OboSite_PhysicalAddressId",
                table: "OboSite",
                column: "PhysicalAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OboSite");
        }
    }
}
