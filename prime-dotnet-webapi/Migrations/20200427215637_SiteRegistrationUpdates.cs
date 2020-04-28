using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class SiteRegistrationUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEvent_Enrollee_EnrolleeId",
                table: "BusinessEvent");

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

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Vendor",
                nullable: true);

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

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SubmittedDate",
                table: "Site",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhysicalAddressId",
                table: "Party",
                nullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "EnrolleeId",
                table: "BusinessEvent",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "BusinessEvent",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "BusinessEvent",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Vendor",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "Email", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "CareConnect@phsa.ca", "CareConnect", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "support@excelleris.com", "Excelleris", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "help@iclinicemr.com", "iClinic", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "prime@medinet.ca", "MediNet", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "service@plexia.ca", "Plexia", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Party_PhysicalAddressId",
                table: "Party",
                column: "PhysicalAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_PhysicalAddressId",
                table: "Location",
                column: "PhysicalAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEvent_PartyId",
                table: "BusinessEvent",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEvent_SiteId",
                table: "BusinessEvent",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Enrollee_EnrolleeId",
                table: "BusinessEvent",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Party_PartyId",
                table: "BusinessEvent",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Site_SiteId",
                table: "BusinessEvent",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_BusinessEvent_Enrollee_EnrolleeId",
                table: "BusinessEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEvent_Party_PartyId",
                table: "BusinessEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessEvent_Site_SiteId",
                table: "BusinessEvent");

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

            migrationBuilder.DropIndex(
                name: "IX_BusinessEvent_PartyId",
                table: "BusinessEvent");

            migrationBuilder.DropIndex(
                name: "IX_BusinessEvent_SiteId",
                table: "BusinessEvent");

            migrationBuilder.DeleteData(
                table: "Vendor",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vendor",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vendor",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vendor",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vendor",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "SubmittedDate",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "PhysicalAddressId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "PhysicalAddressId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "BusinessEvent");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "BusinessEvent");

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

            migrationBuilder.AlterColumn<int>(
                name: "EnrolleeId",
                table: "BusinessEvent",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Party_AddressId",
                table: "Party",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_AddressId",
                table: "Location",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessEvent_Enrollee_EnrolleeId",
                table: "BusinessEvent",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
