using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class DeviceProviderVendors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AgreementVersion",
                columns: new[] { "Id", "AgreementType", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "Text", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 17, 7, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<p class=""text-center"">
  This Agreement is made the {{day}} day of {{month}}, {{year}}
</p>

<h1>---- PLACEHOLDER TEXT ----</h1>
", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "VendorLookup",
                columns: new[] { "Code", "CareSettingCode", "Email", "Name" },
                values: new object[,]
                {
                    { 14, 4, "", "Assyst Rx-A" },
                    { 15, 4, "", "Commander Group" },
                    { 16, 4, "", "Kroll" },
                    { 17, 4, "", "Nexxsys" },
                    { 18, 4, "", "PharmaClik" },
                    { 19, 4, "", "Shoppers Drug Mart HealthWatch NG" },
                    { 20, 4, "", "WinRx" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 20);
        }
    }
}
