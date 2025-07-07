using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class LicenceClassUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "RemoteAccessTypeLicense",
                keyColumns: new[] { "LicenseCode", "RemoteAccessTypeCode" },
                keyValues: new object[] { 13, 2 });

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 377,
                column: "EffectiveDate",
                value: new DateTime(2023, 8, 28, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.InsertData(
                table: "LicenseDetail",
                columns: new[] { "Id", "AllowRequestRemoteAccess", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "LicenseCode", "LicensedToProvideCare", "Manual", "Multijurisdictional", "NamedInImReg", "NonPrescribingPrefix", "Prefix", "PrescriberIdType", "UpdatedTimeStamp", "UpdatedUserId", "Validate" },
                values: new object[] { 378, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 7, 3, 8, 0, 0, 0, DateTimeKind.Utc), 13, true, true, false, false, null, "91", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 378);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 377,
                column: "EffectiveDate",
                value: new DateTime(2025, 7, 1, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.InsertData(
                table: "RemoteAccessTypeLicense",
                columns: new[] { "LicenseCode", "RemoteAccessTypeCode", "CreatedTimeStamp", "CreatedUserId", "DeletedDateTime", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 13, 1, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, 2, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTimeOffset(new DateTime(2025, 2, 19, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });
        }
    }
}
