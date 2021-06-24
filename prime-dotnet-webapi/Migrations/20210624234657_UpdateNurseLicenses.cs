using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateNurseLicenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 38 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 44 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 50 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 56 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 57 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 58 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, 50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, 58 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, 50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, 58 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, 50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, 58 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, 50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, 58 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, 50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, 58 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, 50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, 58 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, 50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, 58 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, 50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, 58 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, 50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, 58 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, 50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, 58 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, 50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, 58 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, 50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, 58 });

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 58);

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "LicensedToProvideCare", "Manual", "Name", "NamedInImReg", "Prefix", "PrescriberIdType", "Validate", "Weight" },
                values: new object[] { 69, true, true, "Student Midwife", false, "98", null, false, 32 });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode", "CollegeLicenseGroupingCode" },
                values: new object[] { 3, 69, 5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 3, 69 });

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 69);

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "LicensedToProvideCare", "Manual", "Name", "NamedInImReg", "Prefix", "PrescriberIdType", "Validate", "Weight" },
                values: new object[,]
                {
                    { 50, true, true, "Temporary Nurse Practitioner (Special Event)", true, "96", 2, true, 3 },
                    { 38, true, true, "Temporary Registered Nurse (Special Event)", false, "R9", 1, false, 8 },
                    { 44, true, true, "Temporary Registered Psychiatric Nurse (Special Event)", false, "Y9", 1, false, 17 },
                    { 56, true, true, "Temporary Licensed Practical Nurse (Special Event)", false, "L9", null, false, 24 },
                    { 57, false, true, "Non-Practicing Licensed Nurse Practitioner", true, "96", null, true, 26 },
                    { 58, true, true, "Temporary Nurse Practitioner (time-limited)", true, "96", null, true, 27 }
                });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode", "CollegeLicenseGroupingCode" },
                values: new object[,]
                {
                    { 3, 50, 4 },
                    { 3, 58, 4 },
                    { 3, 57, 1 },
                    { 3, 56, 1 },
                    { 3, 38, 2 },
                    { 3, 44, 3 }
                });

            migrationBuilder.InsertData(
                table: "DefaultPrivilege",
                columns: new[] { "PrivilegeId", "LicenseCode" },
                values: new object[,]
                {
                    { 10, 50 },
                    { 14, 58 },
                    { 13, 58 },
                    { 12, 58 },
                    { 11, 58 },
                    { 10, 58 },
                    { 9, 58 },
                    { 8, 58 },
                    { 7, 58 },
                    { 6, 58 },
                    { 5, 58 },
                    { 5, 50 },
                    { 6, 50 },
                    { 7, 50 },
                    { 15, 58 },
                    { 8, 50 },
                    { 16, 50 },
                    { 15, 50 },
                    { 14, 50 },
                    { 13, 50 },
                    { 12, 50 },
                    { 11, 50 },
                    { 9, 50 },
                    { 16, 58 }
                });
        }
    }
}
