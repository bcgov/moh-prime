using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddCollegeForPlrRoleTypeAndPlrAdditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollegeForPlrRoleType",
                columns: table => new
                {
                    ProviderRoleType = table.Column<string>(type: "text", nullable: false),
                    CollegeCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeForPlrRoleType", x => x.ProviderRoleType);
                });

            migrationBuilder.InsertData(
                table: "CollegeForPlrRoleType",
                columns: new[] { "ProviderRoleType", "CollegeCode" },
                values: new object[,]
                {
                    { "MD", 1 },
                    { "CHIRO", 4 },
                    { "PSYCH", 16 },
                    { "OT", 12 },
                    { "DEN", 7 },
                    { "OPT", 14 },
                    { "ND", 11 },
                    { "LPN", 3 },
                    { "PHYSIO", 15 },
                    { "RM", 3 },
                    { "PO", 1 },
                    { "PHARM", 2 },
                    { "RPN", 3 },
                    { "RNP", 3 },
                    { "RN", 3 },
                    { "PTECH", 2 },
                    { "RD", 9 },
                    { "RAC", 18 },
                    { "RMT", 10 }
                });

            migrationBuilder.InsertData(
                table: "IdentifierTypeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "2.16.840.1.113883.3.40.2.46", "MOAID" },
                    { "2.16.840.1.113883.3.40.2.44", "PPID" },
                    { "2.16.840.1.113883.4.538", "NAPID" }
                });

            migrationBuilder.InsertData(
                table: "PlrRoleTypeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[] { "ND", "Naturopathic Doctor" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollegeForPlrRoleType");

            migrationBuilder.DeleteData(
                table: "IdentifierTypeLookup",
                keyColumn: "Code",
                keyValue: "2.16.840.1.113883.3.40.2.44");

            migrationBuilder.DeleteData(
                table: "IdentifierTypeLookup",
                keyColumn: "Code",
                keyValue: "2.16.840.1.113883.3.40.2.46");

            migrationBuilder.DeleteData(
                table: "IdentifierTypeLookup",
                keyColumn: "Code",
                keyValue: "2.16.840.1.113883.4.538");

            migrationBuilder.DeleteData(
                table: "PlrRoleTypeLookup",
                keyColumn: "Code",
                keyValue: "ND");
        }
    }
}
