using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class NewAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_EnrolleeAddress_EnrolleeId_AddressId",
                table: "EnrolleeAddress",
                columns: new[] { "EnrolleeId", "AddressId" },
                unique: true);

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
                name: "IX_PartyAddress_PartyId_AddressId",
                table: "PartyAddress",
                columns: new[] { "PartyId", "AddressId" },
                unique: true);

            migrationBuilder.InsertData(
                table: "StatusReasonLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 17, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "No address from BCSC. Enrollee entered address.", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeAddress_AddressId",
                table: "EnrolleeAddress",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeAddress_EnrolleeId",
                table: "EnrolleeAddress",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddress_AddressId",
                table: "PartyAddress",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddress_PartyId",
                table: "PartyAddress",
                column: "PartyId");

            // // Migrate data
            migrationBuilder.Sql(@"
                INSERT INTO ""EnrolleeAddress""
                (
                    ""AddressId"",
                    ""EnrolleeId""
                )
                SELECT
                    e.""MailingAddressId"" as ""AddressId"",
                    e.""Id"" as ""EnrolleeId""
                FROM
                    ""Enrollee"" e
                WHERE
                    e.""MailingAddressId"" is not null
            ");

            migrationBuilder.Sql(@"
                INSERT INTO ""EnrolleeAddress""
                (
                    ""AddressId"",
                    ""EnrolleeId""
                )
                SELECT
                    e.""PhysicalAddressId"" as ""AddressId"",
                    e.""Id"" as ""EnrolleeId""
                FROM
                    ""Enrollee"" e
                WHERE
                    e.""PhysicalAddressId"" is not null
            ");

            migrationBuilder.Sql(@"
                INSERT INTO ""PartyAddress""
                (
                    ""AddressId"",
                    ""PartyId""
                )
                SELECT
                    p.""MailingAddressId"" as ""AddressId"",
                    p.""Id"" as ""PartyId""
                FROM
                    ""Party"" p
                WHERE
                    p.""MailingAddressId"" is not null
            ");

            migrationBuilder.Sql(@"
                INSERT INTO ""PartyAddress""
                (
                    ""AddressId"",
                    ""PartyId""
                )
                SELECT
                    p.""PhysicalAddressId"" as ""AddressId"",
                    p.""Id"" as ""PartyId""
                FROM
                    ""Party"" p
                WHERE
                    p.""PhysicalAddressId"" is not null
            ");

            migrationBuilder.Sql(@"
                UPDATE ""Address"" a
                SET ""AddressType"" = 3
                FROM ""EnrolleeAddress"" ea
                WHERE a.""Id"" = ea.""AddressId""
                AND a.""AddressType"" = 1;
            ");

            migrationBuilder.Sql(@"
                UPDATE ""Address"" a
                SET ""AddressType"" = 3
                FROM ""PartyAddress"" pa
                WHERE a.""Id"" = pa.""AddressId""
                AND a.""AddressType"" = 1;
            ");

            // // Drop unnecessary columns
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Address_MailingAddressId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Address_PhysicalAddressId",
                table: "Enrollee");

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

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_MailingAddressId",
                table: "Enrollee");

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_PhysicalAddressId",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "MailingAddressId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "PhysicalAddressId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "MailingAddressId",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "PhysicalAddressId",
                table: "Enrollee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolleeAddress");

            migrationBuilder.DropTable(
                name: "PartyAddress");

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 17);

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
                name: "IX_Party_MailingAddressId",
                table: "Party",
                column: "MailingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_PhysicalAddressId",
                table: "Party",
                column: "PhysicalAddressId");

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
