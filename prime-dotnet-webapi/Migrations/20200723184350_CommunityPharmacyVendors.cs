using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class CommunityPharmacyVendors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationTypeCode",
                table: "VendorLookup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "OrganizationTypeCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "OrganizationTypeCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 3,
                column: "OrganizationTypeCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "OrganizationTypeCode",
                value: 2);

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "OrganizationTypeCode",
                value: 2);

            migrationBuilder.InsertData(
                table: "VendorLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Email", "Name", "OrganizationTypeCode", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 6, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "", "Telus Health", 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "", "Shoppers Drug Mart", 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "", "Applied Robotics", 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "", "McKesson", 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "", "Commander Group", 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorLookup_OrganizationTypeCode",
                table: "VendorLookup",
                column: "OrganizationTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorLookup_OrganizationTypeLookup_OrganizationTypeCode",
                table: "VendorLookup",
                column: "OrganizationTypeCode",
                principalTable: "OrganizationTypeLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorLookup_OrganizationTypeLookup_OrganizationTypeCode",
                table: "VendorLookup");

            migrationBuilder.DropIndex(
                name: "IX_VendorLookup_OrganizationTypeCode",
                table: "VendorLookup");

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "OrganizationTypeCode",
                table: "VendorLookup");
        }
    }
}
