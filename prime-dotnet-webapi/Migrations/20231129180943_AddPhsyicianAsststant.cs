using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddPhsyicianAsststant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AgreementVersion",
                columns: new[] { "Id", "AgreementType", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "Text", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 35, 12, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2023, 11, 29, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "<h1>\n  PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER<br>\n</h1>\n\n<p class=\"bold\">\n  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.\n</p>\n\n<p class=\"bold underline\">\n  On Behalf-of-User Access\n</p>\n\n<ol>\n  <li>\n    <p>\n      You represent and warrant to the Province that:\n    </p>\n\n    <ol type=\"a\">\n      <li>\n        your employment duties in relation to a Practitioner require you to access PharmaNet (and PharmaNet Data) to\n        support the Practitioner’s delivery of Direct Patient Care;\n      </li>\n      <li>\n        you are directly supervised by a Practitioner who has been granted access to PharmaNet by the Province; and\n      </li>\n      <li>\n        all information provided by you in connection with your application for PharmaNet access, including all\n        information submitted through PRIME, is true and correct.\n      </li>\n    </ol>\n\n  </li>\n</ol>\n\n<p class=\"bold underline\">\n  Definitions\n</p>\n\n<ol start=\"2\">\n  <li>\n    <p>\n      In these terms, capitalized terms will have the following meanings:\n    </p>\n\n    <ul class=\"list-unstyled\">\n      <li>\n        <strong>“Approved Practice Site”</strong> means the physical site at which a Practitioner provides Direct\n        Patient Care and which is approved by the Province for PharmaNet access. For greater certainty, “Approved\n        Practice Site” does not include a location from which remote access to PharmaNet takes place.\n      </li>\n      <li>\n        <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health\n        services to an individual to whom a Practitioner provides direct patient care in the context of their Practice.\n      </li>\n      <li>\n        <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the\n        following website (or such other website as may be specified by the Province from time to time for this\n        purpose):\n\n        <br><br>\n\n        <a href=\"http://www.gov.bc.ca/pharmacarenewsletter\" target=\"_blank\" rel=\"noopener noreferrer\">www.gov.bc.ca/pharmacarenewsletter</a>\n      </li>\n      <li>\n        <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the <em>Information Management\n        Regulation</em>, B.C. Reg. 74/2015.\n      </li>\n      <li>\n        <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record or\n        information in the custody, control or possession of you or a Practitioner that was obtained through access to\n        PharmaNet by anyone.\n      </li>\n      <li>\n        <strong>“Practice”</strong> means a Practitioner’s practice of their health profession.\n      </li>\n      <li>\n        <strong>“Practitioner”</strong> means a health professional regulated under the <em>Health Professions Act</em>,\n        or an enrolled device provide under the <em>Provider Regulation</em> B.C. Reg. 222/2014, who supervises your\n        access to and use of PharmaNet and who has been granted access to PharmaNet by the Province.\n      </li>\n      <li>\n        <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for, and\n        manage, their access to PharmaNet, and through which users are granted access by the Province.\n      </li>\n      <li>\n        <strong>“Province”</strong> means His Majesty the King in Right of British Columbia, as represented by the\n        Minister of Health.\n      </li>\n    </ul>\n\n  </li>\n  <li>\n    Unless otherwise specified, a reference to a statute or regulation by name means the statute or regulation of\n    British Columbia of that name, as amended or replaced from time to time, and includes any enactment made under the\n    authority of that statute or regulation.\n  </li>\n</ol>\n\n<p class=\"bold underline\">\n  Terms of Access to PharmaNet\n</p>\n\n<ol start=\"4\">\n  <li>\n\n    <p>\n      You must:\n    </p>\n\n    <ol type=\"a\">\n      <li>\n        access and use PharmaNet and PharmaNet Data only at the Approved Practice Site of a Practitioner;\n      </li>\n      <li>\n        access and use PharmaNet and PharmaNet Data only to support Direct Patient Care delivered by a Practitioner to\n        the individuals whose PharmaNet Data you are accessing, and only if the Practitioner is or will be delivering\n        Direct Patient Care requiring that access to those individuals at the same Approved Practice Site at which the\n        access occurs;\n      </li>\n      <li>\n        only access PharmaNet as permitted by law and directed by a Practitioner;\n      </li>\n      <li>\n        maintain all PharmaNet Data, whether accessed on PharmaNet or otherwise disclosed to you in any manner, in\n        strict confidence;\n      </li>\n      <li>\n        maintain the security of PharmaNet, and any applications, connections, or networks used to access PharmaNet;\n      </li>\n      <li>\n        complete all training required by the Approved Practice Site’s PharmaNet software vendor and the Province before\n        accessing PharmaNet;\n      </li>\n      <li>\n        notify the Province if you have any reason to suspect that PharmaNet, or any PharmaNet Data, is or has been\n        accessed or used inappropriately by any person.\n      </li>\n    </ol>\n\n  </li>\n  <li>\n\n    <p>\n      You must not:\n    </p>\n\n    <ol type=\"a\">\n      <li>\n        disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and\n        directed by a Practitioner;\n      </li>\n      <li>\n        permit any person to use any user IDs, passwords or credentials provided to you to access PharmaNet;\n      </li>\n      <li>\n        reveal, share or compromise any user IDs, passwords or credentials for PharmaNet;\n      </li>\n      <li>\n        use, or attempt to use, the user IDs, passwords or credentials of any other person to access PharmaNet;\n      </li>\n      <li>\n        take any action that might compromise the integrity of PharmaNet, its information, or the provincial drug plan,\n        such as altering information or submitting false information;\n      </li>\n      <li>\n        test the security related to PharmaNet;\n      </li>\n      <li>\n        attempt to access PharmaNet from any location other than the Approved Practice Site of a Practitioner,\n        including by VPN or other remote access technology;\n      </li>\n      <li>\n        access PharmaNet unless the access is for the purpose of supporting a Practitioner in providing Direct\n        Patient Care to a patient at the same Approved Practice Site at which your access occurs.\n      </li>\n    </ol>\n  </li>\n</ol>\n<ol start=\"6\">\n  <li>\n    Your access to PharmaNet and use of PharmaNet Data are governed by the <em>Pharmaceutical Services Act</em> and you\n    must comply with all your duties under that Act.\n  </li>\n  <li>\n    The Province may, in writing and from time to time, set further limits and conditions in respect of PharmaNet,\n    either for you or for the Practitioner(s), and that you must comply with any such further limits and conditions.\n  </li>\n</ol>\n\n<p class=\"bold underline\">\n  How to Notify the Province\n</p>\n\n<ol start=\"8\">\n  <li>\n\n    <p>\n      Notice to the Province may be sent in writing to:\n    </p>\n\n    <address>\n      Director, Information and PharmaNet Development<br>\n      Ministry of Health<br>\n      PO Box 9652, STN PROV GOVT<br>\n      Victoria, BC V8W 9P4<br>\n\n      <br>\n\n      <a href=\"mailto:PRIMESupport@gov.bc.ca\">PRIMESupport@gov.bc.ca</a>\n    </address>\n\n  </li>\n</ol>\n\n<p class=\"bold underline\">\n  Province May Modify These Terms\n</p>\n\n<ol start=\"9\">\n  <li>\n    <p>\n      The Province may amend these terms, including this section, at any time in its sole discretion:\n    </p>\n\n    <ol type=\"i\">\n      <li>\n        by written notice to you, in which case the amendment will become effective upon the later of (A) the date\n        notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified by the\n        Province, if any; or\n      </li>\n      <li>\n        by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will specify\n        the effective date of the amendment, which date will be at least thirty (30) days after the date that the\n        PharmaCare Newsletter containing the notice is first published.\n      </li>\n    </ol>\n\n    <p>\n      If you do not agree with any amendment for which notice has been provided by the Province in accordance with (i)\n      or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of\n      PharmaNet.\n    </p>\n\n    <p>\n      Any written notice to you under (i) above will be in writing and delivered by the Province to you using any of the\n      contact mechanisms identified by you in PRIME, including by mail to a specified postal address, email to a\n      specified email address or text message to a specified cell phone number. You may be required to click a URL link\n      or log into PRIME to receive the contents of any such notice.\n    </p>\n\n  </li>\n</ol>\n\n<p class=\"bold underline\">\n  Governing Law\n</p>\n\n<ol start=\"10\">\n  <li>\n\n    <p>\n      These terms will be governed by and will be construed and interpreted in accordance with the laws of British\n      Columbia and the laws of Canada applicable therein.\n    </p>\n\n  </li>\n</ol>\n", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 59,
                column: "Weight",
                value: 31);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 65,
                column: "Weight",
                value: 32);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 66,
                column: "Weight",
                value: 33);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 67,
                column: "Weight",
                value: 34);

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "Name", "Weight" },
                values: new object[] { 92, "Certified Physician Assistant", 30 });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode", "CollegeLicenseGroupingCode" },
                values: new object[] { 1, 92, null });

            migrationBuilder.InsertData(
                table: "LicenseDetail",
                columns: new[] { "Id", "AllowRequestRemoteAccess", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "LicenseCode", "LicensedToProvideCare", "Manual", "NamedInImReg", "NonPrescribingPrefix", "Prefix", "PrescriberIdType", "UpdatedTimeStamp", "UpdatedUserId", "Validate" },
                values: new object[] { 276, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 11, 27, 8, 0, 0, 0, DateTimeKind.Utc), 92, true, true, false, null, "M9", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 1, 92 });

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 92);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 59,
                column: "Weight",
                value: 30);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 65,
                column: "Weight",
                value: 31);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 66,
                column: "Weight",
                value: 32);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 67,
                column: "Weight",
                value: 33);
        }
    }
}
