using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class BCCNMLicenseChangePart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LicenseDetail",
                columns: new[] { "Id", "AllowRequestRemoteAccess", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "LicenseCode", "LicensedToProvideCare", "Manual", "Multijurisdictional", "NamedInImReg", "NonPrescribingPrefix", "Prefix", "PrescriberIdType", "UpdatedTimeStamp", "UpdatedUserId", "Validate" },
                values: new object[,]
                {
                    { 391, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 6, 24, 8, 0, 0, 0, DateTimeKind.Utc), 53, true, true, false, false, null, "L9", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 392, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2026, 6, 24, 8, 0, 0, 0, DateTimeKind.Utc), 61, true, true, false, false, null, "98", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 392);
        }
    }
}
