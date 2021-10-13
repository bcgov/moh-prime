using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddPartyCertification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartyCertification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    PartyId = table.Column<int>(nullable: false),
                    CollegeCode = table.Column<int>(nullable: false),
                    LicenseCode = table.Column<int>(nullable: false),
                    LicenseNumber = table.Column<string>(nullable: false),
                    PractitionerId = table.Column<string>(nullable: true),
                    RenewalDate = table.Column<DateTimeOffset>(nullable: false),
                    PracticeCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyCertification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyCertification_CollegeLookup_CollegeCode",
                        column: x => x.CollegeCode,
                        principalTable: "CollegeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyCertification_LicenseLookup_LicenseCode",
                        column: x => x.LicenseCode,
                        principalTable: "LicenseLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyCertification_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyCertification_PracticeLookup_PracticeCode",
                        column: x => x.PracticeCode,
                        principalTable: "PracticeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartyCertification_CollegeCode",
                table: "PartyCertification",
                column: "CollegeCode");

            migrationBuilder.CreateIndex(
                name: "IX_PartyCertification_LicenseCode",
                table: "PartyCertification",
                column: "LicenseCode");

            migrationBuilder.CreateIndex(
                name: "IX_PartyCertification_PartyId",
                table: "PartyCertification",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyCertification_PracticeCode",
                table: "PartyCertification",
                column: "PracticeCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyCertification");
        }
    }
}
