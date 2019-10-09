using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdatedCodeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "CollegeLookup",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CollegeLicense",
                columns: table => new
                {
                    CollegeCode = table.Column<short>(nullable: false),
                    LicenseCode = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeLicense", x => new { x.CollegeCode, x.LicenseCode });
                    table.ForeignKey(
                        name: "FK_CollegeLicense_CollegeLookup_CollegeCode",
                        column: x => x.CollegeCode,
                        principalTable: "CollegeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollegeLicense_LicenseLookup_LicenseCode",
                        column: x => x.LicenseCode,
                        principalTable: "LicenseLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode" },
                values: new object[,]
                {
                    { (short)1, (short)2 },
                    { (short)1, (short)3 },
                    { (short)2, (short)4 },
                    { (short)2, (short)5 },
                    { (short)3, (short)1 },
                    { (short)3, (short)5 }
                });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                column: "Prefix",
                value: "91");

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                column: "Prefix",
                value: "P1");

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                column: "Prefix",
                value: "96");

            migrationBuilder.CreateIndex(
                name: "IX_CollegeLicense_LicenseCode",
                table: "CollegeLicense",
                column: "LicenseCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollegeLicense");

            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "CollegeLookup");
        }
    }
}
