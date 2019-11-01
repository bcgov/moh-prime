using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddCountryProvinceLookups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)2 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)3 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)4 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)5 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)1 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)5 });

            migrationBuilder.DeleteData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)1 });

            migrationBuilder.DeleteData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)2 });

            migrationBuilder.DeleteData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)3 });

            migrationBuilder.DeleteData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)4 });

            migrationBuilder.DeleteData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)1 });

            migrationBuilder.DeleteData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)2 });

            migrationBuilder.DeleteData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)3 });

            migrationBuilder.DeleteData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)4 });

            migrationBuilder.DeleteData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)1 });

            migrationBuilder.DeleteData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)2 });

            migrationBuilder.DeleteData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)3 });

            migrationBuilder.DeleteData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)4 });

            migrationBuilder.DeleteData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)5);

            migrationBuilder.DeleteData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)6);

            migrationBuilder.DeleteData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)5);

            migrationBuilder.DeleteData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)4);

            migrationBuilder.CreateTable(
                name: "CountryLookup",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ProvinceLookup",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvinceLookup", x => x.Code);
                });

            migrationBuilder.InsertData(
                table: "CollegeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "Prefix", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "College of Physicians and Surgeons of BC (CPSBC)", "91", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "College of Pharmacists of BC (CPBC)", "P1", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "College of Registered Nurses of BC (CRNBC)", "96", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "None", null, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CountryLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { "CA", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Canada", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)6,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)7,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)8,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254) });

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Full - General", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Full - Pharmacist", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Full - Specialty", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Registered Nurse", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Registered Nurse", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.UpdateData(
                table: "OrganizationNameLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254) });

            migrationBuilder.UpdateData(
                table: "OrganizationNameLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254) });

            migrationBuilder.InsertData(
                table: "OrganizationTypeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Health Authority", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Pharmacy", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "PracticeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)4, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "None", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Sexually Transmitted Infections (STI)", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Remote Practice", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Reproductive Care", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "ProvinceLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { "QC", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Quebec", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AB", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Alberta", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "BC", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "British Columbia", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MB", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Manitoba", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NB", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "New Brunswick", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NL", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Newfoundland and Labrador", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NS", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Nova Scotia", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ON", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Ontario", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "PE", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Prince Edward Island", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "SK", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Saskatchewan", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NT", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Northwest Territories", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NU", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Nunavut", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "YT", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Yukon", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "StatusLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)4, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Declined", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Adjudicated/Approved", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Accepted TOS (Terms of Service)", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "In Progress", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Submitted", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)6, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), "Declined TOS (Terms of Service)", new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)3, (short)1, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)2, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)3, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)4, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)5, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)5, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CollegePractice",
                columns: new[] { "CollegeCode", "PracticeCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, (short)4, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)3, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)3, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)3, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)2, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)1, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)2, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)4, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)1, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)1, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)2, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)4, new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 1, 12, 25, 5, 968, DateTimeKind.Local).AddTicks(7254), new Guid("00000000-0000-0000-0000-000000000000") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryLookup");

            migrationBuilder.DropTable(
                name: "ProvinceLookup");

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)6,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)7,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)8,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "OrganizationNameLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "OrganizationNameLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)6,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997) });
        }
    }
}
