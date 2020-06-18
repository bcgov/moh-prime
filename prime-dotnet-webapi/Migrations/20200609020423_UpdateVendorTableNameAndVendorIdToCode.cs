using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class UpdateVendorTableNameAndVendorIdToCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_Vendor_VendorId",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_VendorId",
                table: "Site");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor");

            migrationBuilder.Sql(@"
                ALTER TABLE ""Vendor""
                RENAME COLUMN ""Id"" TO ""Code"";

                ALTER TABLE ""Site""
                RENAME COLUMN ""VendorId"" TO ""VendorCode"";
            ");

            migrationBuilder.RenameTable(
                name: "Vendor",
                newName: "VendorLookup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorLookup",
                table: "VendorLookup",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Site_VendorCode",
                table: "Site",
                column: "VendorCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_VendorLookup_VendorCode",
                table: "Site",
                column: "VendorCode",
                principalTable: "VendorLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(@"
                UPDATE ""VendorLookup""
                SET ""Name"" = 'Medinet'
                WHERE ""Code"" = 4;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE ""VendorLookup""
                SET ""Name"" = 'MediNet'
                WHERE ""Code"" = 4;
            ");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_VendorLookup_VendorCode",
                table: "Site");

            migrationBuilder.DropIndex(
                name: "IX_Site_VendorCode",
                table: "Site");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorLookup",
                table: "VendorLookup");

            migrationBuilder.RenameTable(
                name: "VendorLookup",
                newName: "Vendor");

            migrationBuilder.Sql(@"
                ALTER TABLE ""Site""
                RENAME COLUMN ""VendorCode"" TO ""VendorId"";

                ALTER TABLE ""Vendor""
                RENAME COLUMN ""Code"" TO ""Id"";
            ");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Site_VendorId",
                table: "Site",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Vendor_VendorId",
                table: "Site",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
