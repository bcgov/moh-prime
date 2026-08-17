using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class NaturopathicLicenseChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LicenseDetail",
                columns: new[] { "Id", "AllowRequestRemoteAccess", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "LicenseCode", "LicensedToProvideCare", "Manual", "Multijurisdictional", "NamedInImReg", "NonPrescribingPrefix", "Prefix", "PrescriberIdType", "UpdatedTimeStamp", "UpdatedUserId", "Validate" },
                values: new object[,]
                {
                    { 393, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 8, 6, 8, 0, 0, 0, DateTimeKind.Utc), 166, true, false, false, true, null, "97", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 394, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 8, 6, 8, 0, 0, 0, DateTimeKind.Utc), 167, true, false, false, true, null, "97", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 395, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 8, 6, 8, 0, 0, 0, DateTimeKind.Utc), 169, false, false, false, false, null, "97", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 395);
        }
    }
}
