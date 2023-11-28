using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddPhysicalAssistant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 59,
                column: "Weight",
                value: 31);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 65,
                column: "Weight",
                value: 32);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 66,
                column: "Weight",
                value: 33);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 67,
                column: "Weight",
                value: 34);

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "Name", "Weight" },
                values: new object[] { 92, "Certified Physician Assistant", 30 });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode", "CollegeLicenseGroupingCode" },
                values: new object[] { 1, 92, null });

            migrationBuilder.InsertData(
                table: "LicenseDetail",
                columns: new[] { "Id", "AllowRequestRemoteAccess", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "LicenseCode", "LicensedToProvideCare", "Manual", "NamedInImReg", "NonPrescribingPrefix", "Prefix", "PrescriberIdType", "UpdatedTimeStamp", "UpdatedUserId", "Validate" },
                values: new object[] { 276, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 11, 27, 8, 0, 0, 0, DateTimeKind.Utc), 92, true, true, false, null, "M9", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 1, 92 });

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 92);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 59,
                column: "Weight",
                value: 30);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 65,
                column: "Weight",
                value: 31);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 66,
                column: "Weight",
                value: 32);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 67,
                column: "Weight",
                value: 33);
        }
    }
}
