using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class NurseCollegeLicenseGroupingLookup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollegeLicenseGroupingCode",
                table: "CollegeLicense",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CollegeLicenseGroupingLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeLicenseGroupingLookup", x => x.Code);
                });

            migrationBuilder.InsertData(
                table: "CollegeLicenseGroupingLookup",
                columns: new[] { "Code", "Name", "Weight" },
                values: new object[,]
                {
                    { 1, "Licensed Practical Nurse", 1 },
                    { 2, "Registered Nurse/Licensed Graduate Nurse", 2 },
                    { 3, "Registered Psychiatric Nurse", 3 },
                    { 4, "Nurse Practitioner", 4 },
                    { 5, "Midwife", 5 }
                });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 32 },
                column: "CollegeLicenseGroupingCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 33 },
                column: "CollegeLicenseGroupingCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 34 },
                column: "CollegeLicenseGroupingCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 35 },
                column: "CollegeLicenseGroupingCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 36 },
                column: "CollegeLicenseGroupingCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 37 },
                column: "CollegeLicenseGroupingCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 38 },
                column: "CollegeLicenseGroupingCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 39 },
                column: "CollegeLicenseGroupingCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 40 },
                column: "CollegeLicenseGroupingCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 41 },
                column: "CollegeLicenseGroupingCode",
                value: 3);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 42 },
                column: "CollegeLicenseGroupingCode",
                value: 3);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 43 },
                column: "CollegeLicenseGroupingCode",
                value: 3);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 44 },
                column: "CollegeLicenseGroupingCode",
                value: 3);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 45 },
                column: "CollegeLicenseGroupingCode",
                value: 3);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 46 },
                column: "CollegeLicenseGroupingCode",
                value: 3);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 47 },
                column: "CollegeLicenseGroupingCode",
                value: 4);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 48 },
                column: "CollegeLicenseGroupingCode",
                value: 4);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 49 },
                column: "CollegeLicenseGroupingCode",
                value: 4);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 50 },
                column: "CollegeLicenseGroupingCode",
                value: 4);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 51 },
                column: "CollegeLicenseGroupingCode",
                value: 4);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 52 },
                column: "CollegeLicenseGroupingCode",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 53 },
                column: "CollegeLicenseGroupingCode",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 54 },
                column: "CollegeLicenseGroupingCode",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 55 },
                column: "CollegeLicenseGroupingCode",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 56 },
                column: "CollegeLicenseGroupingCode",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 57 },
                column: "CollegeLicenseGroupingCode",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 58 },
                column: "CollegeLicenseGroupingCode",
                value: 4);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 60 },
                column: "CollegeLicenseGroupingCode",
                value: 5);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 61 },
                column: "CollegeLicenseGroupingCode",
                value: 5);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 62 },
                column: "CollegeLicenseGroupingCode",
                value: 5);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 63 },
                column: "CollegeLicenseGroupingCode",
                value: 5);

            migrationBuilder.CreateIndex(
                name: "IX_CollegeLicense_CollegeLicenseGroupingCode",
                table: "CollegeLicense",
                column: "CollegeLicenseGroupingCode");

            migrationBuilder.AddForeignKey(
                name: "FK_CollegeLicense_CollegeLicenseGroupingLookup_CollegeLicenseG~",
                table: "CollegeLicense",
                column: "CollegeLicenseGroupingCode",
                principalTable: "CollegeLicenseGroupingLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollegeLicense_CollegeLicenseGroupingLookup_CollegeLicenseG~",
                table: "CollegeLicense");

            migrationBuilder.DropTable(
                name: "CollegeLicenseGroupingLookup");

            migrationBuilder.DropIndex(
                name: "IX_CollegeLicense_CollegeLicenseGroupingCode",
                table: "CollegeLicense");

            migrationBuilder.DropColumn(
                name: "CollegeLicenseGroupingCode",
                table: "CollegeLicense");
        }
    }
}
