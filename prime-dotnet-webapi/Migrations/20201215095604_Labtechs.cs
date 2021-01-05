using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class Labtechs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Party",
                newName: "GivenNames");

            migrationBuilder.AddColumn<string>(
                name: "PhoneExtension",
                table: "Party",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PartyEnrolment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartyId = table.Column<int>(nullable: false),
                    PartyType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyEnrolment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyEnrolment_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "FacilityLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "Name",
                value: "Acute/ambulatory care");

            migrationBuilder.UpdateData(
                table: "FacilityLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "Name",
                value: "Outpatient or community-based clinic");

            migrationBuilder.CreateIndex(
                name: "IX_Party_UserId",
                table: "Party",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartyEnrolment_PartyId",
                table: "PartyEnrolment",
                column: "PartyId");

            migrationBuilder.Sql(@"
                insert into ""PartyEnrolment""
                (
                    ""PartyId"",
                    ""PartyType""
                )
                select
                    p.""Id"" as ""PartyId"",
                    2 as ""PartyType""
                from ""Party"" p
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyEnrolment");

            migrationBuilder.DropIndex(
                name: "IX_Party_UserId",
                table: "Party");

            migrationBuilder.RenameColumn(
                name: "GivenNames",
                table: "Party",
                newName: "MiddleName");

            migrationBuilder.DropColumn(
                name: "PhoneExtension",
                table: "Party");

            migrationBuilder.UpdateData(
                table: "FacilityLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "Name",
                value: "Acute/Ambulatory Care");

            migrationBuilder.UpdateData(
                table: "FacilityLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "Name",
                value: "Outpatient or Community-based Clinic");
        }
    }
}
