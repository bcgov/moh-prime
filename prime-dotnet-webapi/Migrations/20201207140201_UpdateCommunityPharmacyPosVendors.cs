using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateCommunityPharmacyPosVendors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 6,
                column: "Name",
                value: "PharmaClik");

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 7,
                column: "Name",
                value: "Nexxsys");

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 8,
                column: "Name",
                value: "Kroll");

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 9,
                column: "Name",
                value: "Assyst Rx-A");

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 10,
                column: "Name",
                value: "WinRx");

            migrationBuilder.InsertData(
                table: "VendorLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Email", "Name", "CareSettingCode", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 11, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "", "Shoppers Drug Mart HealthWatch NG", 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "", "Commander Group", 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "", "BDM", 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                });
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 6,
                column: "Name",
                value: "Telus Health");

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 7,
                column: "Name",
                value: "Shoppers Drug Mart");

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 8,
                column: "Name",
                value: "Applied Robotics");

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 9,
                column: "Name",
                value: "McKesson");

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 10,
                column: "Name",
                value: "Commander Group");

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 13);
        }
    }
}
