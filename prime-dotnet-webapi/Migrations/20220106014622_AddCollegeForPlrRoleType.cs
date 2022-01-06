using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddCollegeForPlrRoleType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollegeForPlrRoleType",
                columns: table => new
                {
                    RoleTypeCode = table.Column<string>(type: "text", nullable: false),
                    CollegeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeForPlrRoleType", x => x.RoleTypeCode);
                });

            migrationBuilder.InsertData(
                table: "CollegeForPlrRoleType",
                columns: new[] { "RoleTypeCode", "CollegeId" },
                values: new object[,]
                {
                    { "RN", 3 },
                    { "PTECH", 2 },
                    { "RMT", 10 },
                    { "PHYSIO", 15 },
                    { "CHIRO", 4 },
                    { "PSYCH", 16 },
                    { "OT", 12 },
                    { "DEN", 7 },
                    { "RD", 9 },
                    { "OPT", 14 },
                    { "LPN", 3 },
                    { "RM", 3 },
                    { "RAC", 18 },
                    { "PO", 1 },
                    { "PHARM", 2 },
                    { "RPN", 3 },
                    { "RNP", 3 },
                    { "MD", 1 },
                    { "NAP", 11 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollegeForPlrRoleType");
        }
    }
}
