using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddCertificateToken : Migration
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
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)5);

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AB");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AK");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AL");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AR");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AS");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AZ");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "BC");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "CA");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "CO");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "CT");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "DC");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "DE");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "FL");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "GA");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "GU");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "HI");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "IA");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "ID");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "IL");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "IN");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "KS");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "KY");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "LA");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MA");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MB");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MD");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "ME");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MI");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MN");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MO");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MP");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MS");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MT");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NB");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NC");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "ND");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NE");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NH");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NJ");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NL");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NM");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NS");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NT");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NU");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NV");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NY");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "OH");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "OK");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "ON");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "OR");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "PA");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "PE");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "PR");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "QC");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "RI");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "SC");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "SD");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "SK");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "TN");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "TX");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "UM");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "UT");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "VA");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "VI");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "VT");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "WA");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "WI");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "WV");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "WY");

            migrationBuilder.DeleteData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "YT");

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
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)5);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)6);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)7);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)8);

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
                table: "CountryLookup",
                keyColumn: "Code",
                keyValue: "CA");

            migrationBuilder.DeleteData(
                table: "CountryLookup",
                keyColumn: "Code",
                keyValue: "US");

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

            migrationBuilder.CreateTable(
                name: "EnrolmentCertificateAccessToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentCertificateAccessToken", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CollegeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "Prefix", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "College of Physicians and Surgeons of BC (CPSBC)", "91", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "College of Pharmacists of BC (CPBC)", "P1", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "BC College of Nursing Professionals (BCCNP)", "96", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CountryLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Canada", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "United States", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138) });

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Full - General", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Full - Pharmacist", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Full - Specialty", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Registered Nurse", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Registered Nurse", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "OrganizationTypeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)5, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Primary Care Network", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Community Practice", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Community Pharmacy", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Community Health Practice Access to PharmaNet (ComPAP)", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Health Authority", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "PracticeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Remote Practice", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Reproductive Care", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Sexually Transmitted Infections (STI)", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "StatusLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)5, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Accepted Access Agreement", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "In Progress", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Submitted", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Adjudicated/Approved", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Declined", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)6, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Declined Access Agreement", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "StatusReasonLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)6, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Licence Class", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Insulin Pump Provider", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Not in PharmaNet", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)7, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Self Declaration", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Manual", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Automatic", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Name Discrepancy", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)8, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Contact address or Identity Address Out of British Columbia", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)3, (short)1, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)2, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)3, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)4, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)5, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)5, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CollegePractice",
                columns: new[] { "CollegeCode", "PracticeCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)3, (short)3, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)3, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)1, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)1, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)2, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)2, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)2, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)3, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)1, new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "ProvinceLookup",
                columns: new[] { "Code", "CountryCode", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { "OR", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Oregon", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "OK", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Oklahoma", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "OH", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Ohio", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MP", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Northern Mariana Islands", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NY", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "New York", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NC", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "North Carolina", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "PA", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Pennsylvania", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NM", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "New Mexico", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NJ", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "New Jersey", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NH", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "New Hampshire", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ND", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "North Dakota", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "PR", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Puerto Rico", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "TN", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Tennessee", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "SC", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "South Carolina", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "SD", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "South Dakota", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "TX", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Texas", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "UM", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "United States Minor Outlying Islands", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "UT", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Utah", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NV", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Nevada", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "VI", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Virgin Islands, U.S.", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "VA", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Virginia", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "WA", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Washington", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "WV", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "West Virginia", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "WI", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Wisconsin", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "WY", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Wyoming", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "RI", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Rhode Island", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "VT", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Vermont", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NE", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Nebraska", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MO", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Missouri", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AR", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Arkansas", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AZ", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Arizona", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AS", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "American Samoa", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AK", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Alaska", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AL", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Alabama", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "YT", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Yukon", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NU", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Nunavut", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NT", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Northwest Territories", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "SK", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Saskatchewan", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "QC", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Quebec", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "PE", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Prince Edward Island", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ON", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Ontario", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NS", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Nova Scotia", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NL", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Newfoundland and Labrador", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NB", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "New Brunswick", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MB", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Manitoba", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "BC", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "British Columbia", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "CA", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "California", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "CO", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Colorado", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "CT", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Connecticut", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "DE", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Delaware", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MS", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Mississippi", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MN", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Minnesota", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MI", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Michigan", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MA", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Massachusetts", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MD", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Maryland", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ME", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Maine", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "LA", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Louisiana", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "KY", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Kentucky", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MT", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Montana", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "KS", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Kansas", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "IN", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Indiana", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "IL", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Illinois", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ID", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Idaho", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "HI", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Hawaii", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "GU", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Guam", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "GA", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Georgia", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "FL", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Florida", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "DC", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "District of Columbia", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "IA", "US", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Iowa", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AB", "CA", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000"), "Alberta", new DateTime(2019, 11, 18, 18, 33, 39, 100, DateTimeKind.Local).AddTicks(5138), new Guid("00000000-0000-0000-0000-000000000000") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolmentCertificateAccessToken");

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CountryLookup",
                keyColumn: "Code",
                keyValue: "CA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "CountryLookup",
                keyColumn: "Code",
                keyValue: "US",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AB",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AK",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AL",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AR",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AS",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AZ",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "BC",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "CA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "CO",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "CT",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "DC",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "DE",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "FL",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "GA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "GU",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "HI",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "IA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "ID",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "IL",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "IN",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "KS",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "KY",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "LA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MB",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MD",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "ME",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MI",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MN",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MO",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MP",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MS",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MT",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NB",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NC",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "ND",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NE",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NH",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NJ",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NL",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NM",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NS",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NT",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NU",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NV",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NY",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "OH",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "OK",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "ON",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "OR",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "PA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "PE",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "PR",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "QC",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "RI",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "SC",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "SD",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "SK",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "TN",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "TX",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "UM",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "UT",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "VA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "VI",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "VT",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "WA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "WI",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "WV",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "WY",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "YT",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)6,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)6,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)7,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)8,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781), new DateTime(2019, 11, 12, 19, 22, 15, 119, DateTimeKind.Local).AddTicks(781) });
        }
    }
}
