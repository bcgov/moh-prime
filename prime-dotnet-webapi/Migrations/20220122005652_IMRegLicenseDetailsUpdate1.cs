using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class IMRegLicenseDetailsUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LicenseDetail",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "LicenseCode", "LicensedToProvideCare", "Manual", "NamedInImReg", "Prefix", "PrescriberIdType", "UpdatedTimeStamp", "UpdatedUserId", "Validate" },
                values: new object[,]
                {
                    { 70, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 29, true, false, true, "T9", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 86, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 61, true, true, true, "98", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 85, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 60, true, false, true, "98", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 84, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 55, true, false, true, "L9", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 83, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 54, false, true, true, "L9", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 82, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 53, true, true, true, "L9", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 81, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 52, true, false, true, "L9", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 80, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 45, true, false, true, "Y9", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 87, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 62, true, false, true, "98", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 79, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 43, false, true, true, "Y9", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 77, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 41, true, false, true, "Y9", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 76, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 39, true, false, true, "R9", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 75, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 34, false, true, true, "R9", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 74, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 33, true, true, true, "R9", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 73, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 32, true, false, true, "R9", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 72, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 68, true, true, true, "T9", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 71, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 31, false, true, true, "T9", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 78, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 42, true, true, true, "Y9", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 88, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 2, 1, 8, 0, 0, 0, DateTimeKind.Utc), 63, false, true, true, "98", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 88);
        }
    }
}
