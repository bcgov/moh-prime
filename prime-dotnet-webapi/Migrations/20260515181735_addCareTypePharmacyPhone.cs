using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class addCareTypePharmacyPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PharmacyPhone",
                table: "HealthAuthoritySite",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PharmacyPhoneRequired",
                table: "CareTypeLookup",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "CareTypeLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "PharmacyPhoneRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "CareTypeLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "PharmacyPhoneRequired",
                value: false);

            migrationBuilder.Sql(@"Update ""CareTypeLookup"" Set ""PharmacyPhoneRequired"" = true Where ""Name"" in ('In-Patient Pharmacy', 'Pharmacy', 'Pharmacy & Device Providers');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PharmacyPhone",
                table: "HealthAuthoritySite");

            migrationBuilder.DropColumn(
                name: "PharmacyPhoneRequired",
                table: "CareTypeLookup");
        }
    }
}
