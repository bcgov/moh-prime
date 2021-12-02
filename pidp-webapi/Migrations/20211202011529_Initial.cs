using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pidp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryLookup",
                columns: table => new
                {
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Party",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<LocalDate>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProvinceLookup",
                columns: table => new
                {
                    Code = table.Column<int>(type: "integer", nullable: false),
                    CountryCode = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvinceLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AddressType = table.Column<int>(type: "integer", nullable: false),
                    CountryCode = table.Column<int>(type: "integer", nullable: false),
                    ProvinceCode = table.Column<int>(type: "integer", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    Street2 = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Postal = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    PartyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_CountryLookup_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "CountryLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_ProvinceLookup_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "ProvinceLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CountryLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 1, "Canada" },
                    { 2, "United States" }
                });

            migrationBuilder.InsertData(
                table: "ProvinceLookup",
                columns: new[] { "Code", "CountryCode", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Alberta" },
                    { 2, 1, "British Columbia" },
                    { 3, 1, "Manitoba" },
                    { 4, 1, "New Brunswick" },
                    { 5, 1, "Newfoundland and Labrador" },
                    { 6, 1, "Nova Scotia" },
                    { 7, 1, "Ontario" },
                    { 8, 1, "Prince Edward Island" },
                    { 9, 1, "Quebec" },
                    { 10, 1, "Saskatchewan" },
                    { 11, 1, "Northwest Territories" },
                    { 12, 1, "Nunavut" },
                    { 13, 1, "Yukon" },
                    { 14, 2, "Alabama" },
                    { 15, 2, "Alaska" },
                    { 16, 2, "American Samoa" },
                    { 17, 2, "Arizona" },
                    { 18, 2, "Arkansas" },
                    { 19, 2, "California" },
                    { 20, 2, "Colorado" },
                    { 21, 2, "Connecticut" },
                    { 22, 2, "Delaware" },
                    { 23, 2, "District of Columbia" },
                    { 24, 2, "Florida" },
                    { 25, 2, "Georgia" },
                    { 26, 2, "Guam" },
                    { 27, 2, "Hawaii" },
                    { 28, 2, "Idaho" },
                    { 29, 2, "Illinois" },
                    { 30, 2, "Indiana" },
                    { 31, 2, "Iowa" },
                    { 32, 2, "Kansas" },
                    { 33, 2, "Kentucky" },
                    { 34, 2, "Louisiana" },
                    { 35, 2, "Maine" },
                    { 36, 2, "Maryland" },
                    { 37, 2, "Massachusetts" },
                    { 38, 2, "Michigan" },
                    { 39, 2, "Minnesota" },
                    { 40, 2, "Mississippi" },
                    { 41, 2, "Missouri" },
                    { 42, 2, "Montana" },
                    { 43, 2, "Nebraska" },
                    { 44, 2, "Nevada" },
                    { 45, 2, "New Hampshire" },
                    { 46, 2, "New Jersey" },
                    { 47, 2, "New Mexico" },
                    { 48, 2, "New York" },
                    { 49, 2, "North Carolina" },
                    { 50, 2, "North Dakota" },
                    { 51, 2, "Northern Mariana Islands" },
                    { 52, 2, "Ohio" },
                    { 53, 2, "Oklahoma" },
                    { 54, 2, "Oregon" },
                    { 55, 2, "Pennsylvania" },
                    { 56, 2, "Puerto Rico" },
                    { 57, 2, "Rhode Island" },
                    { 58, 2, "South Carolina" },
                    { 59, 2, "South Dakota" },
                    { 60, 2, "Tennessee" },
                    { 61, 2, "Texas" },
                    { 62, 2, "United States Minor Outlying Islands" },
                    { 63, 2, "Utah" },
                    { 64, 2, "Vermont" },
                    { 65, 2, "Virgin Islands, U.S." },
                    { 66, 2, "Virginia" },
                    { 67, 2, "Washington" },
                    { 68, 2, "West Virginia" },
                    { 69, 2, "Wisconsin" },
                    { 70, 2, "Wyoming" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryCode",
                table: "Address",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Address_PartyId",
                table: "Address",
                column: "PartyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_ProvinceCode",
                table: "Address",
                column: "ProvinceCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "CountryLookup");

            migrationBuilder.DropTable(
                name: "Party");

            migrationBuilder.DropTable(
                name: "ProvinceLookup");
        }
    }
}
