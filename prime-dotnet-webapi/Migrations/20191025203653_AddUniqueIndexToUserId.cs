using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddUniqueIndexToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enrolment_EnrolleeId",
                table: "Enrolment");

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

            migrationBuilder.InsertData(
                table: "CollegeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "Prefix", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "College of Physicians and Surgeons of BC (CPSBC)", "91", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "College of Pharmacists of BC (CPBC)", "P1", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "College of Registered Nurses of BC (CRNBC)", "96", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "None", null, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") }
                });

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

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)5, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Registered Nurse", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Full - Specialty", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Registered Nurse", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Full - General", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Full - Pharmacist", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") }
                });

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

            migrationBuilder.InsertData(
                table: "OrganizationTypeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Health Authority", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Pharmacy", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "PracticeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Remote Practice", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Reproductive Care", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Sexually Transmitted Infections (STI)", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "None", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "StatusLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)5, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Accepted TOS (Terms of Service)", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "In Progress", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Submitted", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Adjudicated/Approved", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Declined", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)6, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), "Declined TOS (Terms of Service)", new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)3, (short)1, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)2, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)3, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)4, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)5, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)5, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CollegePractice",
                columns: new[] { "CollegeCode", "PracticeCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, (short)4, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)3, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)3, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)3, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)2, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)1, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)2, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)4, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)1, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)1, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)2, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)4, new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 25, 13, 36, 52, 670, DateTimeKind.Local).AddTicks(3997), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrolment_EnrolleeId",
                table: "Enrolment",
                column: "EnrolleeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_UserId",
                table: "Enrollee",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enrolment_EnrolleeId",
                table: "Enrolment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_UserId",
                table: "Enrollee");

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)1, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)2, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)6,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)7,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)8,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "OrganizationNameLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "OrganizationNameLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)6,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498) });

            migrationBuilder.CreateIndex(
                name: "IX_Enrolment_EnrolleeId",
                table: "Enrolment",
                column: "EnrolleeId");
        }
    }
}
