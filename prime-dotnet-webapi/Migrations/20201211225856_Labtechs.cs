using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class Labtechs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Since all existing Perties are SigningAuthorities, We set the default here to update them and then change it back to ""
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Party",
                nullable: false,
                defaultValue: "SigningAuthority");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Party",
                nullable: false,
                defaultValue: "");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Party",
                newName: "GivenNames");

            migrationBuilder.AddColumn<string>(
                name: "PhoneExtension",
                table: "Party",
                nullable: true);

            // Leftovers from weird Designer state?
            migrationBuilder.UpdateData(
                table: "FacilityLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "Name",
                value: "Acute/ambulatory care");

            migrationBuilder.UpdateData(
                table: "FacilityLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "Name",
                value: "Outpatient or community-based clinic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GivenNames",
                table: "Party",
                newName: "MiddleName");

            migrationBuilder.DropColumn(
                name: "PhoneExtension",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Party");

            migrationBuilder.UpdateData(
                table: "FacilityLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "Name",
                value: "Acute/Ambulatory Care");

            migrationBuilder.UpdateData(
                table: "FacilityLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "Name",
                value: "Outpatient or Community-based Clinic");
        }
    }
}
