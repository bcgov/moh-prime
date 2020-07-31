using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class RemoveLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessDay_Location_LocationId",
                table: "BusinessDay");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessDay_Site_SiteId",
                table: "BusinessDay");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Site_LocationId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_BusinessDay_LocationId",
                table: "BusinessDay");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "BusinessDay");

            migrationBuilder.AlterColumn<int>(
                name: "SiteId",
                table: "BusinessDay",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessDay_Site_SiteId",
                table: "BusinessDay",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessDay_Site_SiteId",
                table: "BusinessDay");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Site",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SiteId",
                table: "BusinessDay",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "BusinessDay",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdministratorPharmaNetId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoingBusinessAs = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false),
                    PhysicalAddressId = table.Column<int>(type: "integer", nullable: true),
                    PrivacyOfficerId = table.Column<int>(type: "integer", nullable: true),
                    TechnicalSupportId = table.Column<int>(type: "integer", nullable: true),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Party_AdministratorPharmaNetId",
                        column: x => x.AdministratorPharmaNetId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Location_Address_PhysicalAddressId",
                        column: x => x.PhysicalAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_Party_PrivacyOfficerId",
                        column: x => x.PrivacyOfficerId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_Party_TechnicalSupportId",
                        column: x => x.TechnicalSupportId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Site_LocationId",
                table: "Site",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessDay_LocationId",
                table: "BusinessDay",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_AdministratorPharmaNetId",
                table: "Location",
                column: "AdministratorPharmaNetId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_OrganizationId",
                table: "Location",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_PhysicalAddressId",
                table: "Location",
                column: "PhysicalAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_PrivacyOfficerId",
                table: "Location",
                column: "PrivacyOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_TechnicalSupportId",
                table: "Location",
                column: "TechnicalSupportId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessDay_Location_LocationId",
                table: "BusinessDay",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessDay_Site_SiteId",
                table: "BusinessDay",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
