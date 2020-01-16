using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class RemoveCompap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)5);

            migrationBuilder.DeleteData(
                table: "PrivilegeTypeLookup",
                keyColumn: "Code",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "PrivilegeTypeLookup",
                keyColumn: "Code",
                keyValue: (short)2);

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                column: "Name",
                value: "Health Authority");

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                column: "Name",
                value: "Community Practice");

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                column: "Name",
                value: "Community Pharmacy");

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                column: "Name",
                value: "Primary Care Network");

            migrationBuilder.InsertData(
                table: "PrivilegeTypeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2020, 1, 16, 15, 31, 51, 555, DateTimeKind.Local).AddTicks(1715), new Guid("00000000-0000-0000-0000-000000000000"), "Allowable Role", new DateTime(2020, 1, 16, 15, 31, 51, 555, DateTimeKind.Local).AddTicks(1715), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2020, 1, 16, 15, 31, 51, 555, DateTimeKind.Local).AddTicks(1715), new Guid("00000000-0000-0000-0000-000000000000"), "Allowable Transaction", new DateTime(2020, 1, 16, 15, 31, 51, 555, DateTimeKind.Local).AddTicks(1715), new Guid("00000000-0000-0000-0000-000000000000") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                column: "Name",
                value: "Community Health Practice Access to PharmaNet (ComPAP)");

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                column: "Name",
                value: "Health Authority");

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                column: "Name",
                value: "Community Practice");

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                column: "Name",
                value: "Community Pharmacy");

            migrationBuilder.InsertData(
                table: "OrganizationTypeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { (short)5, new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Primary Care Network", new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "PrivilegeTypeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 14, 15, 48, 38, 321, DateTimeKind.Local).AddTicks(6486), new DateTime(2020, 1, 14, 15, 48, 38, 321, DateTimeKind.Local).AddTicks(6486) });

            migrationBuilder.UpdateData(
                table: "PrivilegeTypeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 14, 15, 48, 38, 321, DateTimeKind.Local).AddTicks(6486), new DateTime(2020, 1, 14, 15, 48, 38, 321, DateTimeKind.Local).AddTicks(6486) });
        }
    }
}
