using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class PartyAddresscs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Party_Address_MailingAddressId",
                table: "Party");

            migrationBuilder.DropForeignKey(
                name: "FK_Party_Address_PhysicalAddressId",
                table: "Party");

            migrationBuilder.DropIndex(
                name: "IX_Party_MailingAddressId",
                table: "Party");

            migrationBuilder.DropIndex(
                name: "IX_Party_PhysicalAddressId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "MailingAddressId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "PhysicalAddressId",
                table: "Party");

            migrationBuilder.CreateTable(
                name: "PartyAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartyId = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyAddress_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyAddress_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddress_AddressId",
                table: "PartyAddress",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddress_PartyId",
                table: "PartyAddress",
                column: "PartyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyAddress");

            migrationBuilder.AddColumn<int>(
                name: "MailingAddressId",
                table: "Party",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhysicalAddressId",
                table: "Party",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Party_MailingAddressId",
                table: "Party",
                column: "MailingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_PhysicalAddressId",
                table: "Party",
                column: "PhysicalAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Party_Address_MailingAddressId",
                table: "Party",
                column: "MailingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Party_Address_PhysicalAddressId",
                table: "Party",
                column: "PhysicalAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
