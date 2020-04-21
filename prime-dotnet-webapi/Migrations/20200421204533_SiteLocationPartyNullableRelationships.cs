using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class SiteLocationPartyNullableRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Address_AddressId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Party_AdministratorPharmaNetId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Organization_OrganizationId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Party_PrivacyOfficerId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Party_TechnicalSupportId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Party_Address_AddressId",
                table: "Party");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Party_ProvisionerId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Vendor_VendorId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Party_AddressId",
                table: "Party");

            migrationBuilder.DropIndex(
                name: "IX_Location_AddressId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Location");

            migrationBuilder.AlterColumn<int>(
                name: "VendorId",
                table: "Site",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ProvisionerId",
                table: "Site",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Site",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "PhysicalAddressId",
                table: "Party",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TechnicalSupportId",
                table: "Location",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PrivacyOfficerId",
                table: "Location",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Location",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AdministratorPharmaNetId",
                table: "Location",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "PhysicalAddressId",
                table: "Location",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Party_PhysicalAddressId",
                table: "Party",
                column: "PhysicalAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_PhysicalAddressId",
                table: "Location",
                column: "PhysicalAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Party_AdministratorPharmaNetId",
                table: "Location",
                column: "AdministratorPharmaNetId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Organization_OrganizationId",
                table: "Location",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Address_PhysicalAddressId",
                table: "Location",
                column: "PhysicalAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Party_PrivacyOfficerId",
                table: "Location",
                column: "PrivacyOfficerId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Party_TechnicalSupportId",
                table: "Location",
                column: "TechnicalSupportId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Party_Address_PhysicalAddressId",
                table: "Party",
                column: "PhysicalAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Party_ProvisionerId",
                table: "Site",
                column: "ProvisionerId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Vendor_VendorId",
                table: "Site",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Party_AdministratorPharmaNetId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Organization_OrganizationId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Address_PhysicalAddressId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Party_PrivacyOfficerId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Party_TechnicalSupportId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Party_Address_PhysicalAddressId",
                table: "Party");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Party_ProvisionerId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Vendor_VendorId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Party_PhysicalAddressId",
                table: "Party");

            migrationBuilder.DropIndex(
                name: "IX_Location_PhysicalAddressId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "PhysicalAddressId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "PhysicalAddressId",
                table: "Location");

            migrationBuilder.AlterColumn<int>(
                name: "VendorId",
                table: "Site",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProvisionerId",
                table: "Site",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Site",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Party",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TechnicalSupportId",
                table: "Location",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrivacyOfficerId",
                table: "Location",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Location",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdministratorPharmaNetId",
                table: "Location",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Location",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Party_AddressId",
                table: "Party",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_AddressId",
                table: "Location",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Address_AddressId",
                table: "Location",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Party_AdministratorPharmaNetId",
                table: "Location",
                column: "AdministratorPharmaNetId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Organization_OrganizationId",
                table: "Location",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Party_PrivacyOfficerId",
                table: "Location",
                column: "PrivacyOfficerId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Party_TechnicalSupportId",
                table: "Location",
                column: "TechnicalSupportId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Party_Address_AddressId",
                table: "Party",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Party_ProvisionerId",
                table: "Site",
                column: "ProvisionerId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Vendor_VendorId",
                table: "Site",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
