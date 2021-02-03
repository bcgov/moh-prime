using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class NewAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Address_MailingAddressId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Address_PhysicalAddressId",
                table: "Enrollee");

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_MailingAddressId",
                table: "Enrollee");

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_PhysicalAddressId",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "MailingAddressId",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "PhysicalAddressId",
                table: "Enrollee");

            migrationBuilder.CreateTable(
                name: "EnrolleeAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnrolleeId = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeAddress_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeAddress_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeAddress_AddressId",
                table: "EnrolleeAddress",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeAddress_EnrolleeId",
                table: "EnrolleeAddress",
                column: "EnrolleeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolleeAddress");

            migrationBuilder.AddColumn<int>(
                name: "MailingAddressId",
                table: "Enrollee",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhysicalAddressId",
                table: "Enrollee",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_MailingAddressId",
                table: "Enrollee",
                column: "MailingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_PhysicalAddressId",
                table: "Enrollee",
                column: "PhysicalAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Address_MailingAddressId",
                table: "Enrollee",
                column: "MailingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Address_PhysicalAddressId",
                table: "Enrollee",
                column: "PhysicalAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
