using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class ChangePharmacyVendors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 6,
                column: "Name",
                value: "TELUS Health - Kroll");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VendorLookup",
                keyColumn: "Code",
                keyValue: 6,
                column: "Name",
                value: "TELUS Health");

            migrationBuilder.InsertData(
                table: "VendorLookup",
                columns: new[] { "Code", "CareSettingCode", "Email", "Name" },
                values: new object[] { 11, 3, "", "BDM (Hospital pharmacy only)" });
        }
    }
}
