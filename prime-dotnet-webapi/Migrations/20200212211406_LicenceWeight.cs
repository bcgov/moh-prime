using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class LicenceWeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "LicenseLookup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)1,
                column: "Weight",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)2,
                column: "Weight",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)3,
                column: "Weight",
                value: 12);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)4,
                column: "Weight",
                value: 11);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)5,
                column: "Weight",
                value: 3);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)6,
                column: "Weight",
                value: 4);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)7,
                column: "Weight",
                value: 10);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)8,
                column: "Weight",
                value: 6);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)9,
                column: "Weight",
                value: 5);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)10,
                column: "Weight",
                value: 7);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)11,
                column: "Weight",
                value: 18);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)12,
                column: "Weight",
                value: 14);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)13,
                column: "Weight",
                value: 15);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)14,
                column: "Weight",
                value: 16);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)15,
                column: "Weight",
                value: 17);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)16,
                column: "Weight",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)17,
                column: "Weight",
                value: 13);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)18,
                column: "Weight",
                value: 22);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)19,
                column: "Weight",
                value: 23);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)20,
                column: "Weight",
                value: 20);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)21,
                column: "Weight",
                value: 24);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)22,
                column: "Weight",
                value: 8);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)23,
                column: "Weight",
                value: 19);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)24,
                column: "Weight",
                value: 21);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)25,
                column: "Weight",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)26,
                column: "Weight",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)27,
                column: "Weight",
                value: 4);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)28,
                column: "Weight",
                value: 3);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)29,
                column: "Weight",
                value: 6);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)30,
                column: "Weight",
                value: 5);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)31,
                column: "Weight",
                value: 7);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)32,
                column: "Weight",
                value: 6);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)33,
                column: "Weight",
                value: 7);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)34,
                column: "Weight",
                value: 10);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)35,
                column: "Weight",
                value: 12);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)36,
                column: "Weight",
                value: 13);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)37,
                column: "Weight",
                value: 14);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)38,
                column: "Weight",
                value: 8);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)39,
                column: "Weight",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)40,
                column: "Weight",
                value: 11);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)41,
                column: "Weight",
                value: 15);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)42,
                column: "Weight",
                value: 16);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)43,
                column: "Weight",
                value: 19);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)44,
                column: "Weight",
                value: 17);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)45,
                column: "Weight",
                value: 18);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)46,
                column: "Weight",
                value: 20);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)47,
                column: "Weight",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)48,
                column: "Weight",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)49,
                column: "Weight",
                value: 5);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)50,
                column: "Weight",
                value: 3);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)51,
                column: "Weight",
                value: 4);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)52,
                column: "Weight",
                value: 21);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)53,
                column: "Weight",
                value: 22);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)54,
                column: "Weight",
                value: 25);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)55,
                column: "Weight",
                value: 23);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)56,
                column: "Weight",
                value: 24);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)57,
                column: "Weight",
                value: 26);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: (short)58,
                column: "Weight",
                value: 27);

            migrationBuilder.UpdateData(
                table: "UserClause",
                keyColumn: "Id",
                keyValue: 1,
                column: "Clause",
                value: @"<h1>PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER</h1>

<p class=""bold"">
  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
</p>

<ol>
  <li>

    <p class=""bold underline"">
      On Behalf of User Access
    </p>

    <p class=""bold"">
      You represent and warrant to the Province that:
    </p>

    <ol type=""a"">
      <li>
        your employment duties in relation to a Practitioner require you to access PharmaNet (and PharmaNet Data) to
        support the Practitioner’s delivery of Direct Patient Care;
      </li>
      <li>
        you are directly supervised by a Practitioner who has been granted access to PharmaNet by the Province; and
      </li>
      <li>
        all information provided by you in connection with your application for PharmaNet access, including all
        information submitted through PRIME, is true and correct.
      </li>
    </ol>

  </li>
  <li>

    <p class=""bold underline"">
      Definitions
    </p>

    <p class=""bold"">
      In these terms, capitalized terms will have the following meanings:
    </p>

    <ul class=""list-unstyled"">
      <li>
        <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
        services to an individual to whom a Practitioner provides direct patient care in the context of their Practice.
      </li>
      <li>
        <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the
        following website (or such other website as may be specified by the Province from time to time for this
        purpose):

        <br><br>

        <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
      </li>
      <li>
        <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information Management
        Regulation.
      </li>
      <li>
        <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record or
        information in the custody, control or possession of you or a Practitioner that was obtained through access to
        PharmaNet by anyone.
      </li>
      <li>
        <strong>“Practice”</strong> means a Practitioner’s practice of their health profession.
      </li>
      <li>
        <strong>“Practitioner”</strong> means a health professional regulated under the Health Professions Act who
        supervises your access and use of PharmaNet and who has been granted access to PharmaNet by the Province.
      </li>
      <li>
        <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for, and
        manage, their access to PharmaNet, and through which users are granted access by the Province.
      </li>
      <li>
        <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
        Minister of Health.
      </li>
    </ul>

  </li>
  <li>

    <p class=""bold underline"">
      Terms of Access to PharmaNet
    </p>

    <p class=""bold"">
      You must:
    </p>

    <ol type=""a"">
      <li>
        access and use PharmaNet and PharmaNet Data only to support Direct Patient Care delivered by the Practitioner to
        the individuals whose PharmaNet Data you are accessing;
      </li>
      <li>
        only access PharmaNet as permitted by law and directed by the Practitioner;
      </li>
      <li>
        maintain all PharmaNet Data, whether accessed on PharmaNet or otherwise disclosed to you in any manner, in
        strict confidence;
      </li>
      <li>
        maintain the security of PharmaNet, and any applications, connections, or networks used to access PharmaNet;
      </li>
      <li>
        complete all training required by the Practice’s PharmaNet software vendor and the Province before accessing
        PharmaNet;
      </li>
      <li>
        notify the Province if you have any reason to suspect that PharmaNet, or any PharmaNet Data, is or has been
        accessed or used inappropriately by any person.
      </li>
    </ol>

    <p class=""bold"">
      You must not:
    </p>

    <ol type=""a""
        start=""7"">
      <li>
        disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and directed
        by the Practitioner;
      </li>
      <li>
        permit any person to use any user IDs, passwords or credentials provided to you to access PharmaNet;
      </li>
      <li>
        reveal, share or compromise any user IDs, passwords or credentials for PharmaNet;
      </li>
      <li>
        use, or attempt to use, the user IDs, passwords or credentials of any other person to access PharmaNet;
      </li>
      <li>
        take any action that might compromise the integrity of PharmaNet, its information, or the provincial drug plan,
        such as altering information or submitting false information;
      </li>
      <li>
        test the security related to PharmaNet;
      </li>
      <li>
        attempt to access PharmaNet from any location other than the approved Practice site of the Practitioner,
        including by VPN or other remote access technology, unless that VPN or remote access technology has first been
        approved by the Province in writing for use at the Practice.
      </li>
    </ol>

    <p>
      Your access to PharmaNet and use of PharmaNet Data are governed by the Pharmaceutical Services Act and you must
      comply with all your duties under that Act.
    </p>

    <p>
      The Province may, in writing and from time to time, set further limits and conditions in respect of PharmaNet,
      either for you or for the Practitioner(s), and that you must comply with any such further limits and conditions.
    </p>

  </li>
  <li>

    <p class=""bold underline"">
      How to Notify the Province
    </p>

    <p>
      Notice to the Province may be sent in writing to:
    </p>

    <address>
      Director, Information and PharmaNet Development<br>
      Ministry of Health<br>
      PO Box 9652, STN PROV GOVT<br>
      Victoria, BC V8W 9P4<br>

      <br>

      <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
    </address>

  </li>
  <li>

    <p class=""bold underline"">
      Province may modify these terms
    </p>

    <p>
      The Province may amend these terms, including this section, at any time in its sole discretion:
    </p>

    <ol type=""i"">
      <li>
        by written notice to you, in which case the amendment will become effective upon the later of (A) the date
        notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified by the
        Province, if any; or
      </li>
      <li>
        by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will specify
        the effective date of the amendment, which date will be at least thirty (30) days after the date that the
        PharmaCare Newsletter containing the notice is first published.
      </li>
    </ol>

    <p>
      If you do not agree with any amendment for which notice has been provided by the Province in accordance with (i)
      or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
      PharmaNet.
    </p>

    <p>
      Any written notice to you under (i) above will be in writing and delivered by the Province to you using any of the
      contact mechanisms identified by you in PRIME, including by mail to a specified postal address, email to a
      specified email address or text message to a specified cell phone number. You may be required to click a URL link
      or log into PRIME to receive the contents of any such notice.
    </p>

  </li>
  {$lcPlaceholder}
  <li>

    <p class=""bold underline"">
      Governing Law
    </p>

    <p>
      These terms will be governed by and will be construed and interpreted in accordance with the laws of British
      Columbia and the laws of Canada applicable therein.
    </p>

    <p>
      Unless otherwise specified, a reference to a statute or regulation by name means the statute or regulation of
      British Columbia of that name, as amended or replaced from time to time, and includes any enactment made under the
      authority of that statute or regulation.
    </p>

  </li>
</ol>
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "LicenseLookup");

            migrationBuilder.UpdateData(
                table: "UserClause",
                keyColumn: "Id",
                keyValue: 1,
                column: "Clause",
                value: @"<h1>PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER</h1>

<p class=""bold"">
  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
</p>

<ol>
  <li>

    <p class=""bold underline"">
      On Behalf of User Access
    </p>

    <p class=""bold"">
      You represent and warrant to the Province that:
    </p>

    <ol type=""a"">
      <li>
        your employment duties in relation to a Practitioner require you to access PharmaNet (and PharmaNet Data) to
        support the Practitioner’s delivery of Direct Patient Care;
      </li>
      <li>
        you are directly supervised by a Practitioner who has been granted access to PharmaNet by the Province; and
      </li>
      <li>
        all information provided by you in connection with your application for PharmaNet access, including all
        information submitted through PRIME, is true and correct.
      </li>
    </ol>

  </li>
  <li>

    <p class=""bold underline"">
      Definitions
    </p>

    <p class=""bold"">
      In these terms, capitalized terms will have the following meanings:
    </p>

    <ul class=""list-unstyled"">
      <li>
        <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
        services to an individual to whom a Practitioner provides direct patient care in the context of their Practice.
      </li>
      <li>
        <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the
        following website (or such other website as may be specified by the Province from time to time for this
        purpose):

        <br><br>

        <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
      </li>
      <li>
        <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information Management
        Regulation.
      </li>
      <li>
        <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record or
        information in the custody, control or possession of you or a Practitioner that was obtained through access to
        PharmaNet by anyone.
      </li>
      <li>
        <strong>“Practice”</strong> means a Practitioner’s practice of their health profession.
      </li>
      <li>
        <strong>“Practitioner”</strong> means a health professional regulated under the Health Professions Act who
        supervises your access and use of PharmaNet and who has been granted access to PharmaNet by the Province.
      </li>
      <li>
        <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for, and
        manage, their access to PharmaNet, and through which users are granted access by the Province.
      </li>
      <li>
        <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
        Minister of Health.
      </li>
    </ul>

  </li>
  <li>

    <p class=""bold underline"">
      Terms of Access to PharmaNet
    </p>

    <p class=""bold"">
      You must:
    </p>

    <ol type=""a"">
      <li>
        access and use PharmaNet and PharmaNet Data only to support Direct Patient Care delivered by the Practitioner to
        the individuals whose PharmaNet Data you are accessing;
      </li>
      <li>
        only access PharmaNet as permitted by law and directed by the Practitioner;
      </li>
      <li>
        maintain all PharmaNet Data, whether accessed on PharmaNet or otherwise disclosed to you in any manner, in
        strict confidence;
      </li>
      <li>
        maintain the security of PharmaNet, and any applications, connections, or networks used to access PharmaNet;
      </li>
      <li>
        complete all training required by the Practice’s PharmaNet software vendor and the Province before accessing
        PharmaNet;
      </li>
      <li>
        notify the Province if you have any reason to suspect that PharmaNet, or any PharmaNet Data, is or has been
        accessed or used inappropriately by any person.
      </li>
    </ol>

    <p class=""bold"">
      You must:
    </p>

    <ol type=""a""
        start=""7"">
      <li>
        disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and directed
        by the Practitioner;
      </li>
      <li>
        permit any person to use any user IDs, passwords or credentials provided to you to access PharmaNet;
      </li>
      <li>
        reveal, share or compromise any user IDs, passwords or credentials for PharmaNet;
      </li>
      <li>
        use, or attempt to use, the user IDs, passwords or credentials of any other person to access PharmaNet;
      </li>
      <li>
        take any action that might compromise the integrity of PharmaNet, its information, or the provincial drug plan,
        such as altering information or submitting false information;
      </li>
      <li>
        test the security related to PharmaNet;
      </li>
      <li>
        attempt to access PharmaNet from any location other than the approved Practice site of the Practitioner,
        including by VPN or other remote access technology, unless that VPN or remote access technology has first been
        approved by the Province in writing for use at the Practice.
      </li>
    </ol>

    <p>
      Your access to PharmaNet and use of PharmaNet Data are governed by the Pharmaceutical Services Act and you must
      comply with all your duties under that Act.
    </p>

    <p>
      The Province may, in writing and from time to time, set further limits and conditions in respect of PharmaNet,
      either for you or for the Practitioner(s), and that you must comply with any such further limits and conditions.
    </p>

  </li>
  <li>

    <p class=""bold underline"">
      How to Notify the Province
    </p>

    <p>
      Notice to the province may be sent in writing to:
    </p>

    <address>
      Director, Information and PharmaNet Development<br>
      Ministry of Health<br>
      PO Box 9652, STN PROV GOVT<br>
      Victoria, BC V8W 9P4<br>

      <br>

      <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
    </address>

  </li>
  <li>

    <p class=""bold underline"">
      Province may modify these terms
    </p>

    <p>
      The Province may amend these terms, including this section, at any time in its sole discretion:
    </p>

    <ol type=""i"">
      <li>
        by written notice to you, in which case the amendment will become effective upon the later of (A) the date
        notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified by the
        Province, if any; or
      </li>
      <li>
        by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will specify
        the effective date of the amendment, which date will be at least thirty (30) days after the date that the
        PharmaCare Newsletter containing the notice is first published.
      </li>
    </ol>

    <p>
      If you do not agree with any amendment for which notice has been provided by the Province in accordance with (i)
      or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
      PharmaNet.
    </p>

    <p>
      Any written notice to you under (i) above will be in writing and delivered by the Province to you using any of the
      contact mechanisms identified by you in PRIME, including by mail to a specified postal address, email to a
      specified email address or text message to a specified cell phone number. You may be required to click a URL link
      or log into PRIME to receive the contents of any such notice.
    </p>

  </li>
  {$lcPlaceholder}
  <li>

    <p class=""bold underline"">
      Governing Law
    </p>

    <p>
      These terms will be governed by and will be construed and interpreted in accordance with the laws of British
      Columbia and the laws of Canada applicable therein.
    </p>

    <p>
      Unless otherwise specified, a reference to a statute or regulation by name means the statute or regulation of
      British Columbia of that name, as amended or replaced from time to time, and includes any enactment made under the
      authority of that statute or regulation.
    </p>

  </li>
</ol>
");
        }
    }
}
