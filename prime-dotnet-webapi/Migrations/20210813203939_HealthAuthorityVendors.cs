using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class HealthAuthorityVendors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VendorLookup",
                columns: new[] { "Code", "CareSettingCode", "Email", "Name" },
                values: new object[,]
                {
                    { 21, 1, "", "CareConnect" },
                    { 22, 1, "", "Excelleris" },
                    { 23, 1, "", "iClinic" },
                    { 24, 1, "", "Medinet" },
                    { 25, 1, "", "Plexia" },
                    { 26, 1, "", "PharmaClik" },
                    { 27, 1, "", "Nexxsys" },
                    { 28, 1, "", "Kroll" },
                    { 29, 1, "", "Assyst Rx-A" },
                    { 30, 1, "", "WinRx" },
                    { 31, 1, "", "Shoppers Drug Mart HealthWatch NG" },
                    { 32, 1, "", "Commander Group" },
                    { 33, 1, "", "BDM" },
                    { 34, 1, "", "Meditech" },
                    { 35, 1, "", "Cerner" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 35);
        }
    }
}
