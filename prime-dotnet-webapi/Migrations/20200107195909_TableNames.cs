﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class TableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessAgreementNotes_Enrollee_EnrolleeId",
                table: "AccessAgreementNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_AdjudicatorNotes_Enrollee_EnrolleeId",
                table: "AdjudicatorNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentCertificateNotes_Enrollee_EnrolleeId",
                table: "EnrolmentCertificateNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentStatuses_Enrollee_EnrolleeId",
                table: "EnrolmentStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentStatuses_StatusLookup_StatusCode",
                table: "EnrolmentStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentStatusReasons_EnrolmentStatuses_EnrolmentStatusId",
                table: "EnrolmentStatusReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentStatusReasons_StatusReasonLookup_StatusReasonCode",
                table: "EnrolmentStatusReasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolmentStatusReasons",
                table: "EnrolmentStatusReasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolmentStatuses",
                table: "EnrolmentStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolmentCertificateNotes",
                table: "EnrolmentCertificateNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdjudicatorNotes",
                table: "AdjudicatorNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessAgreementNotes",
                table: "AccessAgreementNotes");

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)1 });

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
                keyValues: new object[] { (short)1, (short)4 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)5 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)6 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)7 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)8 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)9 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)10 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)11 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)12 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)13 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)14 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)15 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)16 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)17 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)18 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)19 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)20 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)21 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)22 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)23 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)24 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)25 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)26 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)27 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)28 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)29 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)30 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)31 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)32 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)33 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)34 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)35 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)36 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)37 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)38 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)39 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)40 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)41 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)42 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)43 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)44 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)45 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)46 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)47 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)48 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)49 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)50 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)51 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)52 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)53 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)54 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)55 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)56 });

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
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 1, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 1, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 1, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 1, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 1, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 2, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 2, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 2, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 2, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 2, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 3, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 3, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 3, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 3, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 3, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 4, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 4, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 4, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 4, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 4, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)11 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)16 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)17 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)22 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)52 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)53 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)55 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)56 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)11 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)16 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)17 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)22 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)52 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)53 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)55 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)56 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)11 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)16 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)17 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)22 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)52 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)53 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)55 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)56 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)11 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)16 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)17 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)22 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)52 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)53 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)55 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)56 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)11 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)16 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)17 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)22 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)52 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)53 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)55 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)56 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)11 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)16 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)17 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)22 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)52 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)53 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)55 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)56 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)11 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)16 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)17 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)22 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)52 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)53 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)55 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)56 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)11 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)16 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)17 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)22 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)52 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)53 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)55 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)56 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)11 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)16 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)17 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)22 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)52 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)53 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)55 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)56 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)1 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)2 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)3 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)4 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)5 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)6 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)7 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)8 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)9 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)10 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)11 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)12 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)13 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)14 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)15 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)16 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)17 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)18 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)19 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)22 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)24 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)25 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)26 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)27 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)28 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)29 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)32 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)33 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)34 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)38 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)39 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)40 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)41 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)42 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)44 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)45 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)46 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)47 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)48 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)50 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)51 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)52 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)53 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)55 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)56 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)20 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)21 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)23 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)30 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)31 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)43 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)49 });

            migrationBuilder.DeleteData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)54 });

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
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)9);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)10);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)11);

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
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)6);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)7);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)8);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)9);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)10);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)11);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)12);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)13);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)14);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)15);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)16);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)17);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)18);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)19);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)20);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)21);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)22);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)23);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)24);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)25);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)26);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)27);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)28);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)29);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)30);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)31);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)32);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)33);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)34);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)35);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)36);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)37);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)38);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)39);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)40);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)41);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)42);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)43);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)44);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)45);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)46);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)47);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)48);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)49);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)50);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)51);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)52);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)53);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)54);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)55);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)56);

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

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "PrivilegeGroup",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PrivilegeGroup",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PrivilegeGroup",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PrivilegeGroup",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PrivilegeGroup",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameTable(
                name: "EnrolmentStatusReasons",
                newName: "EnrolmentStatusReason");

            migrationBuilder.RenameTable(
                name: "EnrolmentStatuses",
                newName: "EnrolmentStatus");

            migrationBuilder.RenameTable(
                name: "EnrolmentCertificateNotes",
                newName: "EnrolmentCertificateNote");

            migrationBuilder.RenameTable(
                name: "AdjudicatorNotes",
                newName: "AdjudicatorNote");

            migrationBuilder.RenameTable(
                name: "AccessAgreementNotes",
                newName: "AccessAgreementNote");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolmentStatusReasons_StatusReasonCode",
                table: "EnrolmentStatusReason",
                newName: "IX_EnrolmentStatusReason_StatusReasonCode");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolmentStatusReasons_EnrolmentStatusId",
                table: "EnrolmentStatusReason",
                newName: "IX_EnrolmentStatusReason_EnrolmentStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolmentStatuses_StatusCode",
                table: "EnrolmentStatus",
                newName: "IX_EnrolmentStatus_StatusCode");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolmentStatuses_EnrolleeId",
                table: "EnrolmentStatus",
                newName: "IX_EnrolmentStatus_EnrolleeId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolmentCertificateNotes_EnrolleeId",
                table: "EnrolmentCertificateNote",
                newName: "IX_EnrolmentCertificateNote_EnrolleeId");

            migrationBuilder.RenameIndex(
                name: "IX_AdjudicatorNotes_EnrolleeId",
                table: "AdjudicatorNote",
                newName: "IX_AdjudicatorNote_EnrolleeId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessAgreementNotes_EnrolleeId",
                table: "AccessAgreementNote",
                newName: "IX_AccessAgreementNote_EnrolleeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolmentStatusReason",
                table: "EnrolmentStatusReason",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolmentStatus",
                table: "EnrolmentStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolmentCertificateNote",
                table: "EnrolmentCertificateNote",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdjudicatorNote",
                table: "AdjudicatorNote",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessAgreementNote",
                table: "AccessAgreementNote",
                column: "Id");

            migrationBuilder.InsertData(
                table: "CollegeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "Prefix", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(1349), new Guid("00000000-0000-0000-0000-000000000000"), "College of Physicians and Surgeons of BC (CPSBC)", "91", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(1349), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(1349), new Guid("00000000-0000-0000-0000-000000000000"), "College of Pharmacists of BC (CPBC)", "P1", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(1349), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(1349), new Guid("00000000-0000-0000-0000-000000000000"), "BC College of Nursing Professionals (BCCNP)", "96", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(1349), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CountryLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { "CA", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(8386), new Guid("00000000-0000-0000-0000-000000000000"), "Canada", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(8386), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "US", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(8386), new Guid("00000000-0000-0000-0000-000000000000"), "United States", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(8386), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 7, 11, 59, 9, 6, DateTimeKind.Local).AddTicks(8864), new DateTime(2020, 1, 7, 11, 59, 9, 6, DateTimeKind.Local).AddTicks(8864) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 7, 11, 59, 9, 6, DateTimeKind.Local).AddTicks(8864), new DateTime(2020, 1, 7, 11, 59, 9, 6, DateTimeKind.Local).AddTicks(8864) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 7, 11, 59, 9, 6, DateTimeKind.Local).AddTicks(8864), new DateTime(2020, 1, 7, 11, 59, 9, 6, DateTimeKind.Local).AddTicks(8864) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 7, 11, 59, 9, 6, DateTimeKind.Local).AddTicks(8864), new DateTime(2020, 1, 7, 11, 59, 9, 6, DateTimeKind.Local).AddTicks(8864) });

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)41, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Practicing Registered Psychiatric Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)40, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Employed Student Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)38, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Registered Nurse (Special Event)", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)37, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Non-Practicing Licensed Graduate Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)36, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional Licensed Graduate Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)32, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Practicing Registered Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)34, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Non-Practicing Registered Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)33, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional Registered Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)42, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional Registered Psychiatric Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)31, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Non-Practicing Pharmacy Technician", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)35, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Practicing Licensed Graduate Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)43, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Non-Practicing Registered Psychiatric Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)48, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional Nurse Practitioner", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)45, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Registered Psychiatric Nurse (Emergency)", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)46, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Employed Student Psychiatric Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)47, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Practicing Nurse Practitioner", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)30, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Non-Practicing Pharmacist", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)49, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Non-practicing Nurse Practitioner", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)50, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Nurse Practitioner (Special Event)", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)51, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Nurse Practitioner (Emergency)", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)52, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Practicing Licensed Practical Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)53, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional Licensed Practical Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)54, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Non-Practicing Licensed Practical Nurse", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)55, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Licensed Practical Nurse (Emergency)", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)56, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Licensed Practical Nurse (Special Event)", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)44, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Registered Psychiatric Nurse (Special Event)", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)29, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Pharmacy Technician", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)39, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Registered Nurse (Emergency)", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)27, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Pharmacist", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Full - Family", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Full - Specialty", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Special", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Osteopathic", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional - Family", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)6, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional - Speciality", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)7, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Academic", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)8, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Conditional - Practice Limitations", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)9, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Conditional - Practice Setting", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)10, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Conditional - Disciplined", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)11, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Educational - Medical Student", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)12, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Educational - Postgraduate Resident", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)13, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Educational - Postgraduate Resident Elective", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)14, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Educational - Postgraduate Fellow", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)15, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Educational - Postgraduate Trainee", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)16, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Clinical Observership", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)17, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Visitor", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)18, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Emergency - Family", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)19, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Emergency - Specialty", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)20, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Retired - Life ", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)21, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Temporarily Inactive", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)22, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Surgical Assistant", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)23, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Administrative", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)24, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Assessment", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)25, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Full Pharmacist", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)26, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Limited Pharmacist", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)28, new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000"), "Student Pharmacist", new DateTime(2020, 1, 7, 11, 59, 8, 996, DateTimeKind.Local).AddTicks(8163), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "OrganizationTypeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(654), new Guid("00000000-0000-0000-0000-000000000000"), "Primary Care Network", new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(654), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(654), new Guid("00000000-0000-0000-0000-000000000000"), "Community Pharmacy", new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(654), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(654), new Guid("00000000-0000-0000-0000-000000000000"), "Health Authority", new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(654), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(654), new Guid("00000000-0000-0000-0000-000000000000"), "Community Health Practice Access to PharmaNet (ComPAP)", new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(654), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(654), new Guid("00000000-0000-0000-0000-000000000000"), "Community Practice", new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(654), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "PracticeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(5415), new Guid("00000000-0000-0000-0000-000000000000"), "Remote Practice", new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(5415), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(5415), new Guid("00000000-0000-0000-0000-000000000000"), "Reproductive Health - STI", new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(5415), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(5415), new Guid("00000000-0000-0000-0000-000000000000"), "Reproductive Health - Contraceptive Management", new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(5415), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(5415), new Guid("00000000-0000-0000-0000-000000000000"), "First Call", new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(5415), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "PrivilegeGroup",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 7, 11, 59, 9, 13, DateTimeKind.Local).AddTicks(8957), new Guid("00000000-0000-0000-0000-000000000000"), "Submit and Access Claims", new DateTime(2020, 1, 7, 11, 59, 9, 13, DateTimeKind.Local).AddTicks(8957), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, new DateTime(2020, 1, 7, 11, 59, 9, 13, DateTimeKind.Local).AddTicks(8957), new Guid("00000000-0000-0000-0000-000000000000"), "Record Medical History", new DateTime(2020, 1, 7, 11, 59, 9, 13, DateTimeKind.Local).AddTicks(8957), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, new DateTime(2020, 1, 7, 11, 59, 9, 13, DateTimeKind.Local).AddTicks(8957), new Guid("00000000-0000-0000-0000-000000000000"), "Access Medical History", new DateTime(2020, 1, 7, 11, 59, 9, 13, DateTimeKind.Local).AddTicks(8957), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, new DateTime(2020, 1, 7, 11, 59, 9, 13, DateTimeKind.Local).AddTicks(8957), new Guid("00000000-0000-0000-0000-000000000000"), "Can be RU (OBO)", new DateTime(2020, 1, 7, 11, 59, 9, 13, DateTimeKind.Local).AddTicks(8957), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, new DateTime(2020, 1, 7, 11, 59, 9, 13, DateTimeKind.Local).AddTicks(8957), new Guid("00000000-0000-0000-0000-000000000000"), "Can be OBO (RU)", new DateTime(2020, 1, 7, 11, 59, 9, 13, DateTimeKind.Local).AddTicks(8957), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "StatusLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(5344), new Guid("00000000-0000-0000-0000-000000000000"), "Declined Access Agreement", new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(5344), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(5344), new Guid("00000000-0000-0000-0000-000000000000"), "Accepted Access Agreement", new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(5344), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(5344), new Guid("00000000-0000-0000-0000-000000000000"), "Declined", new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(5344), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(5344), new Guid("00000000-0000-0000-0000-000000000000"), "Adjudicated/Approved", new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(5344), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(5344), new Guid("00000000-0000-0000-0000-000000000000"), "In Progress", new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(5344), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(5344), new Guid("00000000-0000-0000-0000-000000000000"), "Submitted", new DateTime(2020, 1, 7, 11, 59, 9, 8, DateTimeKind.Local).AddTicks(5344), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "StatusReasonLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000"), "Answered one or more Self Declaration questions \"Yes\"", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000"), "Automatic", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000"), "Manual", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000"), "PharmaNet Error, Licence could not be Validated", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000"), "College Licence not in PharmaNet", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000"), "Birthdate Discrepancy with PharmaNet College Licence", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000"), "Name Discrepancy with PharmaNet College Licence", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000"), "Listed as Non-Practicing on PharmaNet College Licence", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000"), "Insulin Pump Provider", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000"), "Licence Class", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)11, new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000"), "Contact Address or Identity Address not in British Columbia", new DateTime(2020, 1, 7, 11, 59, 9, 9, DateTimeKind.Local).AddTicks(967), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, (short)4, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)23, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)24, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)25, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)26, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)27, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)28, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)29, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)30, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)31, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)32, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)33, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)34, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)35, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)36, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)37, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)22, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)38, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)21, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)19, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)7, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)3, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)2, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)1, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)8, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)9, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)10, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)11, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)12, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)13, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)14, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)15, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)16, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)17, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)18, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)20, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)39, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)40, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)41, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)5, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)56, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)55, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)54, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)53, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)52, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)51, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)50, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)6, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)48, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)47, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)46, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)45, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)44, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)43, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)49, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)42, new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 8, 999, DateTimeKind.Local).AddTicks(5463), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CollegePractice",
                columns: new[] { "CollegeCode", "PracticeCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)3, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(9766), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(9766), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(9766), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(9766), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(9766), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(9766), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(9766), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 4, DateTimeKind.Local).AddTicks(9766), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Privilege",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "Description", "PrivilegeGroupId", "TransactionType", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Update Claims History", 1, "TAC", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "DUE Inquiry", 3, "TDU", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Filled Elsewhere Profile", 3, "TRS", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Most Recent Profile", 3, "TBR", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Pt Profile Request", 3, "TRP", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Name Search", 3, "TPN", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Prescriber Details", 3, "TIP", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Patient Details", 3, "TID", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Drug Monograph", 3, "TDR", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Medication Update", 2, "TMU", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Address Update", 2, "TPA", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "New PHN", 2, "TPH", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Pt Profile Mail Request", 1, "TPM", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Query Claims History", 1, "TDT", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Location Details", 3, "TIL", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Maintain Pt Keyword", 1, "TCP", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 18, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Can be OBO (RU)", 5, "OBO", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000"), "Can be RU (OBO)", 4, "RU", new DateTime(2020, 1, 7, 11, 59, 9, 14, DateTimeKind.Local).AddTicks(4291), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "ProvinceLookup",
                columns: new[] { "Code", "CountryCode", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { "CO", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Colorado", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "CT", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Connecticut", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "DE", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Delaware", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "DC", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "District of Columbia", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "FL", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Florida", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "GA", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Georgia", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "CA", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "California", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "GU", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Guam", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ID", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Idaho", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "IL", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Illinois", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "IN", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Indiana", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "IA", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Iowa", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "KS", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Kansas", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "KY", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Kentucky", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "HI", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Hawaii", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "LA", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Louisiana", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AR", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Arkansas", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AS", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "American Samoa", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "BC", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "British Columbia", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MB", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Manitoba", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NB", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "New Brunswick", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NL", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Newfoundland and Labrador", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NS", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Nova Scotia", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ON", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Ontario", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AZ", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Arizona", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "PE", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Prince Edward Island", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "SK", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Saskatchewan", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NT", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Northwest Territories", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NU", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Nunavut", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "YT", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Yukon", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AL", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Alabama", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AK", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Alaska", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "QC", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Quebec", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "WY", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Wyoming", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ME", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Maine", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MA", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Massachusetts", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "PR", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Puerto Rico", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "RI", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Rhode Island", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "SC", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "South Carolina", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "SD", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "South Dakota", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "TN", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Tennessee", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "TX", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Texas", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "PA", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Pennsylvania", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "UM", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "United States Minor Outlying Islands", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "VT", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Vermont", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "VI", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Virgin Islands, U.S.", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "VA", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Virginia", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "WA", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Washington", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "WV", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "West Virginia", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "WI", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Wisconsin", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "UT", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Utah", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MD", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Maryland", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "OR", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Oregon", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "OH", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Ohio", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MI", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Michigan", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MN", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Minnesota", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MS", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Mississippi", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MO", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Missouri", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MT", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Montana", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NE", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Nebraska", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "OK", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Oklahoma", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NV", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Nevada", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NJ", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "New Jersey", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NM", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "New Mexico", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NY", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "New York", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NC", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "North Carolina", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ND", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "North Dakota", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MP", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Northern Mariana Islands", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NH", "US", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "New Hampshire", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AB", "CA", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000"), "Alberta", new DateTime(2020, 1, 7, 11, 59, 9, 10, DateTimeKind.Local).AddTicks(2183), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "DefaultPrivilege",
                columns: new[] { "PrivilegeId", "LicenseCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)52, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)53, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)55, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)56, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)11, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)16, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)17, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)22, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)17, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)22, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)16, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)11, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)52, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)53, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)55, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)56, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)52, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)53, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)55, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)56, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)11, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)16, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)17, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)22, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)52, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)56, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)55, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)53, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)52, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)22, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)52, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)53, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)55, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 18, (short)30, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 18, (short)31, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 18, (short)20, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 18, (short)21, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 18, (short)23, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 18, (short)43, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)56, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)11, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)16, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)17, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)22, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 17, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)17, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)11, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)11, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)17, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)22, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)16, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)53, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)55, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, (short)56, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)16, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)52, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)53, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)55, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)56, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 18, (short)49, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 1, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 1, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 1, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 1, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)11, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)16, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)17, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)22, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)11, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)16, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)17, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)22, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)52, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)53, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)55, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)56, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)56, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)53, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)52, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)53, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)56, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)25, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)26, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)27, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)28, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)29, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)55, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)11, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)16, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)17, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)22, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)1, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)55, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)2, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)4, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)51, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)39, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)40, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)41, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)42, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)45, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)33, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)46, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)34, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)48, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)38, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)44, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)50, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)52, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)47, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)32, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)22, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)17, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)5, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)6, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)7, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)8, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)9, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)10, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)12, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)13, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)14, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)15, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)18, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)19, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)24, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)11, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)16, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, (short)3, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 18, (short)54, new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 7, 11, 59, 9, 16, DateTimeKind.Local).AddTicks(4112), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AccessAgreementNote_Enrollee_EnrolleeId",
                table: "AccessAgreementNote",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdjudicatorNote_Enrollee_EnrolleeId",
                table: "AdjudicatorNote",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentCertificateNote_Enrollee_EnrolleeId",
                table: "EnrolmentCertificateNote",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentStatus_Enrollee_EnrolleeId",
                table: "EnrolmentStatus",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentStatus_StatusLookup_StatusCode",
                table: "EnrolmentStatus",
                column: "StatusCode",
                principalTable: "StatusLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentStatusReason_EnrolmentStatus_EnrolmentStatusId",
                table: "EnrolmentStatusReason",
                column: "EnrolmentStatusId",
                principalTable: "EnrolmentStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentStatusReason_StatusReasonLookup_StatusReasonCode",
                table: "EnrolmentStatusReason",
                column: "StatusReasonCode",
                principalTable: "StatusReasonLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessAgreementNote_Enrollee_EnrolleeId",
                table: "AccessAgreementNote");

            migrationBuilder.DropForeignKey(
                name: "FK_AdjudicatorNote_Enrollee_EnrolleeId",
                table: "AdjudicatorNote");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentCertificateNote_Enrollee_EnrolleeId",
                table: "EnrolmentCertificateNote");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentStatus_Enrollee_EnrolleeId",
                table: "EnrolmentStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentStatus_StatusLookup_StatusCode",
                table: "EnrolmentStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentStatusReason_EnrolmentStatus_EnrolmentStatusId",
                table: "EnrolmentStatusReason");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrolmentStatusReason_StatusReasonLookup_StatusReasonCode",
                table: "EnrolmentStatusReason");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolmentStatusReason",
                table: "EnrolmentStatusReason");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolmentStatus",
                table: "EnrolmentStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrolmentCertificateNote",
                table: "EnrolmentCertificateNote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdjudicatorNote",
                table: "AdjudicatorNote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessAgreementNote",
                table: "AccessAgreementNote");

            migrationBuilder.RenameTable(
                name: "EnrolmentStatusReason",
                newName: "EnrolmentStatusReasons");

            migrationBuilder.RenameTable(
                name: "EnrolmentStatus",
                newName: "EnrolmentStatuses");

            migrationBuilder.RenameTable(
                name: "EnrolmentCertificateNote",
                newName: "EnrolmentCertificateNotes");

            migrationBuilder.RenameTable(
                name: "AdjudicatorNote",
                newName: "AdjudicatorNotes");

            migrationBuilder.RenameTable(
                name: "AccessAgreementNote",
                newName: "AccessAgreementNotes");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolmentStatusReason_StatusReasonCode",
                table: "EnrolmentStatusReasons",
                newName: "IX_EnrolmentStatusReasons_StatusReasonCode");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolmentStatusReason_EnrolmentStatusId",
                table: "EnrolmentStatusReasons",
                newName: "IX_EnrolmentStatusReasons_EnrolmentStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolmentStatus_StatusCode",
                table: "EnrolmentStatuses",
                newName: "IX_EnrolmentStatuses_StatusCode");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolmentStatus_EnrolleeId",
                table: "EnrolmentStatuses",
                newName: "IX_EnrolmentStatuses_EnrolleeId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolmentCertificateNote_EnrolleeId",
                table: "EnrolmentCertificateNotes",
                newName: "IX_EnrolmentCertificateNotes_EnrolleeId");

            migrationBuilder.RenameIndex(
                name: "IX_AdjudicatorNote_EnrolleeId",
                table: "AdjudicatorNotes",
                newName: "IX_AdjudicatorNotes_EnrolleeId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessAgreementNote_EnrolleeId",
                table: "AccessAgreementNotes",
                newName: "IX_AccessAgreementNotes_EnrolleeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolmentStatusReasons",
                table: "EnrolmentStatusReasons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolmentStatuses",
                table: "EnrolmentStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrolmentCertificateNotes",
                table: "EnrolmentCertificateNotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdjudicatorNotes",
                table: "AdjudicatorNotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessAgreementNotes",
                table: "AccessAgreementNotes",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)11 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)16 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)17 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)20 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)21 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)22 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)23 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)1, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)30 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)2, (short)31 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)35 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)36 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)37 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)43 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)49 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)52 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)53 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)54 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)55 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { (short)3, (short)56 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816), new DateTime(2020, 1, 2, 15, 8, 7, 128, DateTimeKind.Local).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(271), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(271) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(271), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(271) });

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(271), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(271) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 135, DateTimeKind.Local).AddTicks(2150), new DateTime(2020, 1, 2, 15, 8, 7, 135, DateTimeKind.Local).AddTicks(2150) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 135, DateTimeKind.Local).AddTicks(2150), new DateTime(2020, 1, 2, 15, 8, 7, 135, DateTimeKind.Local).AddTicks(2150) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 135, DateTimeKind.Local).AddTicks(2150), new DateTime(2020, 1, 2, 15, 8, 7, 135, DateTimeKind.Local).AddTicks(2150) });

            migrationBuilder.UpdateData(
                table: "CollegePractice",
                keyColumns: new[] { "CollegeCode", "PracticeCode" },
                keyValues: new object[] { (short)3, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 135, DateTimeKind.Local).AddTicks(2150), new DateTime(2020, 1, 2, 15, 8, 7, 135, DateTimeKind.Local).AddTicks(2150) });

            migrationBuilder.UpdateData(
                table: "CountryLookup",
                keyColumn: "Code",
                keyValue: "CA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(5649), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(5649) });

            migrationBuilder.UpdateData(
                table: "CountryLookup",
                keyColumn: "Code",
                keyValue: "US",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(5649), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(5649) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 1, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 1, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 1, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 1, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 1, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 2, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 2, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 2, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 2, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 2, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 3, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 3, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 3, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 3, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 3, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 4, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 4, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 4, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 4, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 4, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 5, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 6, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 7, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)11 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)16 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)17 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)22 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)52 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)53 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)55 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 8, (short)56 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)11 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)16 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)17 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)22 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)52 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)53 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)55 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 9, (short)56 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)11 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)16 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)17 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)22 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)52 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)53 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)55 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 10, (short)56 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)11 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)16 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)17 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)22 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)52 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)53 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)55 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 11, (short)56 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)11 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)16 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)17 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)22 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)52 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)53 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)55 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 12, (short)56 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)11 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)16 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)17 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)22 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)52 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)53 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)55 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 13, (short)56 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)11 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)16 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)17 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)22 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)52 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)53 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)55 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 14, (short)56 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)11 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)16 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)17 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)22 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)52 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)53 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)55 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 15, (short)56 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)11 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)16 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)17 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)22 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)52 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)53 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)55 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 16, (short)56 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)1 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)2 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)3 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)4 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)5 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)6 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)7 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)8 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)9 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)10 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)11 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)12 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)13 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)14 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)15 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)16 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)17 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)18 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)19 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)22 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)24 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)25 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)26 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)27 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)28 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)29 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)32 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)33 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)34 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)38 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)39 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)40 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)41 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)42 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)44 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)45 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)46 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)47 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)48 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)50 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)51 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)52 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)53 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)55 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 17, (short)56 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)20 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)21 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)23 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)30 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)31 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)43 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)49 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "DefaultPrivilege",
                keyColumns: new[] { "PrivilegeId", "LicenseCode" },
                keyValues: new object[] { 18, (short)54 },
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894), new DateTime(2020, 1, 2, 15, 8, 7, 149, DateTimeKind.Local).AddTicks(9894) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 138, DateTimeKind.Local).AddTicks(8283), new DateTime(2020, 1, 2, 15, 8, 7, 138, DateTimeKind.Local).AddTicks(8283) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 138, DateTimeKind.Local).AddTicks(8283), new DateTime(2020, 1, 2, 15, 8, 7, 138, DateTimeKind.Local).AddTicks(8283) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 138, DateTimeKind.Local).AddTicks(8283), new DateTime(2020, 1, 2, 15, 8, 7, 138, DateTimeKind.Local).AddTicks(8283) });

            migrationBuilder.UpdateData(
                table: "JobNameLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 138, DateTimeKind.Local).AddTicks(8283), new DateTime(2020, 1, 2, 15, 8, 7, 138, DateTimeKind.Local).AddTicks(8283) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)6,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)7,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)8,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)9,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)10,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)11,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)12,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)13,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)14,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)15,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)16,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)17,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)18,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)19,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)20,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)21,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)22,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)23,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)24,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)25,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)26,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)27,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)28,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)29,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)30,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)31,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)32,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)33,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)34,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)35,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)36,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)37,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)38,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)39,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)40,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)41,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)42,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)43,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)44,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)45,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)46,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)47,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)48,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)49,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)50,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)51,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)52,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)53,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)54,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)55,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)56,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680), new DateTime(2020, 1, 2, 15, 8, 7, 125, DateTimeKind.Local).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 140, DateTimeKind.Local).AddTicks(3279), new DateTime(2020, 1, 2, 15, 8, 7, 140, DateTimeKind.Local).AddTicks(3279) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 140, DateTimeKind.Local).AddTicks(3279), new DateTime(2020, 1, 2, 15, 8, 7, 140, DateTimeKind.Local).AddTicks(3279) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 140, DateTimeKind.Local).AddTicks(3279), new DateTime(2020, 1, 2, 15, 8, 7, 140, DateTimeKind.Local).AddTicks(3279) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 140, DateTimeKind.Local).AddTicks(3279), new DateTime(2020, 1, 2, 15, 8, 7, 140, DateTimeKind.Local).AddTicks(3279) });

            migrationBuilder.UpdateData(
                table: "OrganizationTypeLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 140, DateTimeKind.Local).AddTicks(3279), new DateTime(2020, 1, 2, 15, 8, 7, 140, DateTimeKind.Local).AddTicks(3279) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 134, DateTimeKind.Local).AddTicks(6976), new DateTime(2020, 1, 2, 15, 8, 7, 134, DateTimeKind.Local).AddTicks(6976) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 134, DateTimeKind.Local).AddTicks(6976), new DateTime(2020, 1, 2, 15, 8, 7, 134, DateTimeKind.Local).AddTicks(6976) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 134, DateTimeKind.Local).AddTicks(6976), new DateTime(2020, 1, 2, 15, 8, 7, 134, DateTimeKind.Local).AddTicks(6976) });

            migrationBuilder.UpdateData(
                table: "PracticeLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 134, DateTimeKind.Local).AddTicks(6976), new DateTime(2020, 1, 2, 15, 8, 7, 134, DateTimeKind.Local).AddTicks(6976) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Privilege",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "PrivilegeGroup",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(2610), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "PrivilegeGroup",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(2610), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "PrivilegeGroup",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(2610), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "PrivilegeGroup",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(2610), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "PrivilegeGroup",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(2610), new DateTime(2020, 1, 2, 15, 8, 7, 147, DateTimeKind.Local).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AB",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AK",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AL",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AR",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AS",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "AZ",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "BC",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "CA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "CO",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "CT",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "DC",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "DE",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "FL",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "GA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "GU",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "HI",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "IA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "ID",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "IL",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "IN",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "KS",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "KY",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "LA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MB",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MD",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "ME",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MI",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MN",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MO",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MP",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MS",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "MT",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NB",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NC",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "ND",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NE",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NH",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NJ",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NL",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NM",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NS",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NT",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NU",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NV",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "NY",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "OH",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "OK",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "ON",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "OR",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "PA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "PE",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "PR",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "QC",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "RI",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "SC",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "SD",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "SK",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "TN",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "TX",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "UM",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "UT",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "VA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "VI",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "VT",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "WA",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "WI",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "WV",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "WY",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "ProvinceLookup",
                keyColumn: "Code",
                keyValue: "YT",
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497), new DateTime(2020, 1, 2, 15, 8, 7, 142, DateTimeKind.Local).AddTicks(9497) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(450), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(450) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(450), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(450) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(450), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(450) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(450), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(450) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(450), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(450) });

            migrationBuilder.UpdateData(
                table: "StatusLookup",
                keyColumn: "Code",
                keyValue: (short)6,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(450), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(450) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)6,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)7,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)8,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)9,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)10,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035) });

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: (short)11,
                columns: new[] { "CreatedTimeStamp", "UpdatedTimeStamp" },
                values: new object[] { new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035), new DateTime(2020, 1, 2, 15, 8, 7, 141, DateTimeKind.Local).AddTicks(7035) });

            migrationBuilder.AddForeignKey(
                name: "FK_AccessAgreementNotes_Enrollee_EnrolleeId",
                table: "AccessAgreementNotes",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdjudicatorNotes_Enrollee_EnrolleeId",
                table: "AdjudicatorNotes",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentCertificateNotes_Enrollee_EnrolleeId",
                table: "EnrolmentCertificateNotes",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentStatuses_Enrollee_EnrolleeId",
                table: "EnrolmentStatuses",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentStatuses_StatusLookup_StatusCode",
                table: "EnrolmentStatuses",
                column: "StatusCode",
                principalTable: "StatusLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentStatusReasons_EnrolmentStatuses_EnrolmentStatusId",
                table: "EnrolmentStatusReasons",
                column: "EnrolmentStatusId",
                principalTable: "EnrolmentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolmentStatusReasons_StatusReasonLookup_StatusReasonCode",
                table: "EnrolmentStatusReasons",
                column: "StatusReasonCode",
                principalTable: "StatusReasonLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
