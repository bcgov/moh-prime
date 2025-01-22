using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class EnableNonBCLicenceClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicenceClass",
                table: "UnlistedCertification",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Multijurisdictional",
                table: "LicenseDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 3,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 4,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 5,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 6,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 7,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 8,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 9,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 10,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 11,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 12,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 13,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 14,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 15,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 16,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 17,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 18,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 19,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 20,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 21,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 22,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 23,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 24,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 25,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 26,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 27,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 28,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 29,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 30,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 31,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 32,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 33,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 34,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 35,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 36,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 37,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 39,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 40,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 41,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 42,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 43,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 45,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 46,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 47,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 48,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 49,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 51,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 52,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 53,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 54,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 55,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 59,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 60,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 61,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 62,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 63,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 64,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 65,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 66,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 67,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 68,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 69,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 70,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 71,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 72,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 73,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 74,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 75,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 76,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 77,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 78,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 79,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 80,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 81,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 82,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 83,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 84,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 85,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 86,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 87,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 88,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 89,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 90,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 91,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 92,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 93,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 94,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 95,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 96,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 97,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 98,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 99,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 100,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 101,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 102,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 103,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 104,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 105,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 106,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 107,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 108,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 109,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 110,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 111,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 112,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 113,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 114,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 115,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 116,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 117,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 118,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 119,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 120,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 121,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 122,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 123,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 124,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 125,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 126,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 127,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 128,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 129,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 130,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 131,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 132,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 133,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 134,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 135,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 136,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 137,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 138,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 139,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 140,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 141,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 142,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 143,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 144,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 145,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 146,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 147,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 148,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 149,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 150,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 151,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 152,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 153,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 154,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 155,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 156,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 157,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 158,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 159,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 160,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 161,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 162,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 163,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 164,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 165,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 166,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 167,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 168,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 169,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 170,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 171,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 172,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 173,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 174,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 175,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 176,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 177,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 178,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 179,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 180,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 181,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 182,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 183,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 184,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 185,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 186,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 187,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 188,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 189,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 190,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 191,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 192,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 193,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 194,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 195,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 196,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 197,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 198,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 199,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 200,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 201,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 202,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 203,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 204,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 205,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 206,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 207,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 208,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 209,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 210,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 211,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 212,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 213,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 214,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 215,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 216,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 217,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 218,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 219,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 220,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 221,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 222,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 223,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 224,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 225,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 226,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 227,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 228,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 229,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 230,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 231,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 232,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 233,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 234,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 235,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 236,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 237,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 261,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 262,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 263,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 264,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 265,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 266,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 267,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 268,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 269,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 270,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 271,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 272,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 273,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 274,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 275,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 276,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 277,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 278,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 279,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 280,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 281,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 282,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 283,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 284,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 285,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 286,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 287,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 288,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 289,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 290,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 291,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 292,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 293,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 294,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 295,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 296,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 297,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 298,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 299,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 300,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 301,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 302,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 303,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 304,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 305,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 306,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 307,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 308,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 309,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 310,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 311,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 312,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 313,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 314,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 315,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 316,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 317,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 318,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 319,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 320,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 321,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 322,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 323,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 324,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 325,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 326,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 327,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 328,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 329,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 330,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 331,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 332,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 333,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 334,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 335,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 336,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 337,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 338,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 339,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 340,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 341,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 342,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 343,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 344,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 345,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 346,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 347,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 348,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 349,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 350,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 351,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 352,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 353,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 354,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 355,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 356,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 357,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 358,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 359,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 360,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 361,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 362,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 363,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 364,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 365,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 366,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 367,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 368,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 369,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 370,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 371,
                column: "Multijurisdictional",
                value: false);

            migrationBuilder.InsertData(
                table: "LicenseDetail",
                columns: new[] { "Id", "AllowRequestRemoteAccess", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "LicenseCode", "LicensedToProvideCare", "Manual", "Multijurisdictional", "NamedInImReg", "NonPrescribingPrefix", "Prefix", "PrescriberIdType", "UpdatedTimeStamp", "UpdatedUserId", "Validate" },
                values: new object[,]
                {
                    { 372, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 11, 12, 8, 0, 0, 0, DateTimeKind.Utc), 175, true, true, true, true, "RX", "R9", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 373, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 11, 12, 8, 0, 0, 0, DateTimeKind.Utc), 176, true, true, true, true, "YX", "Y9", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 374, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 11, 12, 8, 0, 0, 0, DateTimeKind.Utc), 177, true, true, true, true, null, "L9", 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 375, true, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 11, 12, 8, 0, 0, 0, DateTimeKind.Utc), 89, true, true, true, true, null, "91", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true }
                });

            migrationBuilder.InsertData(
                table: "StatusReasonLookup",
                columns: new[] { "Code", "Name" },
                values: new object[] { 22, "Enrollee has unlisted (typically non-BC) licences" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 22);

            migrationBuilder.DropColumn(
                name: "LicenceClass",
                table: "UnlistedCertification");

            migrationBuilder.DropColumn(
                name: "Multijurisdictional",
                table: "LicenseDetail");
        }
    }
}
