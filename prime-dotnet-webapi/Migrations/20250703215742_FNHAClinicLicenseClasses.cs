using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class FNHAClinicLicenseClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LicenseDetail",
                columns: new[] { "Id", "AllowRequestRemoteAccess", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "LicenseCode", "LicensedToProvideCare", "Manual", "Multijurisdictional", "NamedInImReg", "NonPrescribingPrefix", "Prefix", "PrescriberIdType", "UpdatedTimeStamp", "UpdatedUserId", "Validate" },
                values: new object[] { 377, true, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 8, 28, 8, 0, 0, 0, DateTimeKind.Utc), 9, true, true, false, true, null, "91", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true });

            migrationBuilder.InsertData(
                table: "RemoteAccessTypeLicense",
                columns: new[] { "LicenseCode", "RemoteAccessTypeCode", "CreatedTimeStamp", "CreatedUserId", "DeletedDateTime", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 18, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 19, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 25, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 27, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 32, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 35, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 39, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 41, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 45, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 88, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 89, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 175, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 176, 3, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "RemoteAccessTypeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[] { 3, "FNHA Clinic" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 10, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 14, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 15, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 17, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 18, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 19, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 25, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 27, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 32, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 35, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 39, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 41, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 45, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 88, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 89, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 175, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 176, 3 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLookup",
                keyColumn: "Code",
                keyValue: 3);
        }
    }
}
