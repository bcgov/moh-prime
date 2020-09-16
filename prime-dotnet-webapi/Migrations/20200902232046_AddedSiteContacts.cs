using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddedSiteContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_Party_AdministratorPharmaNetId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Party_PrivacyOfficerId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Party_TechnicalSupportId",
                table: "Site");

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    JobRoleTitle = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    SMSPhone = table.Column<string>(nullable: true),
                    PhysicalAddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Address_PhysicalAddressId",
                        column: x => x.PhysicalAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_PhysicalAddressId",
                table: "Contact",
                column: "PhysicalAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Contact_AdministratorPharmaNetId",
                table: "Site",
                column: "AdministratorPharmaNetId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Contact_PrivacyOfficerId",
                table: "Site",
                column: "PrivacyOfficerId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Contact_TechnicalSupportId",
                table: "Site",
                column: "TechnicalSupportId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_Contact_AdministratorPharmaNetId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Contact_PrivacyOfficerId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Contact_TechnicalSupportId",
                table: "Site");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Party_AdministratorPharmaNetId",
                table: "Site",
                column: "AdministratorPharmaNetId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Party_PrivacyOfficerId",
                table: "Site",
                column: "PrivacyOfficerId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Party_TechnicalSupportId",
                table: "Site",
                column: "TechnicalSupportId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
