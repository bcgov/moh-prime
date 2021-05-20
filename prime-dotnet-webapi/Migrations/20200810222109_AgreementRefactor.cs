using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AgreementRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agreement Deletions
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTerm_GlobalClause_GlobalClauseId",
                table: "AccessTerm");

            migrationBuilder.DropTable(
                name: "AccessTermLicenseClassClause");

            migrationBuilder.DropTable(
                name: "GlobalClause");

            migrationBuilder.DropTable(
                name: "LicenseClassClauseMapping");

            migrationBuilder.DropTable(
                name: "LicenseClassClause");

            migrationBuilder.DropIndex(
                name: "IX_AccessTerm_GlobalClauseId",
                table: "AccessTerm");

            migrationBuilder.DropColumn(
                name: "GlobalClauseId",
                table: "AccessTerm");

            // Limit & Conditions Changes
            migrationBuilder.DropForeignKey(
                name: "FK_LimitsConditionsClause_Enrollee_EnrolleeId",
                table: "LimitsConditionsClause");

            migrationBuilder.DropIndex(
                name: "IX_LimitsConditionsClause_EnrolleeId",
                table: "LimitsConditionsClause");

            migrationBuilder.DropColumn(
                name: "EnrolleeId",
                table: "LimitsConditionsClause");

            migrationBuilder.RenameColumn(
                name: "Clause",
                table: "LimitsConditionsClause",
                newName: "Text");

            // Unlink UserClause so it can be changed
            migrationBuilder.DropIndex(
                name: "IX_AccessTerm_UserClauseId",
                table: "AccessTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessTerm_UserClause_UserClauseId",
                table: "AccessTerm");

            // Update UserClause
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClause",
                table: "UserClause");

            migrationBuilder.DropColumn(
                name: "EnrolleeClassification",
                table: "UserClause");

            migrationBuilder.RenameColumn(
                name: "Clause",
                table: "UserClause",
                newName: "Text");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "UserClause",
                nullable: false,
                defaultValue: "");

            // Rename and re-link Agreement
            migrationBuilder.AddPrimaryKey(
                name: "PK_Agreement",
                table: "UserClause",
                column: "Id");

            migrationBuilder.RenameTable(
                name: "UserClause",
                newName: "Agreement");

            migrationBuilder.RenameColumn(
                name: "UserClauseId",
                table: "AccessTerm",
                newName: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessTerm_AgreementId",
                table: "AccessTerm",
                column: "AgreementId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTerm_Agreement_AgreementId",
                table: "AccessTerm",
                column: "AgreementId",
                principalTable: "Agreement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.UpdateData(
               table: "Agreement",
               keyColumn: "Id",
               keyValue: 1,
               columns: new[] { "Discriminator" },
               values: new object[] { "OboAgreement" });

            migrationBuilder.UpdateData(
                table: "Agreement",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Discriminator" },
                values: new object[] { "OboAgreement" });

            migrationBuilder.UpdateData(
                table: "Agreement",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Discriminator" },
                values: new object[] { "OboAgreement" });

            migrationBuilder.UpdateData(
                table: "Agreement",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Discriminator" },
                values: new object[] { "OboAgreement" });

            migrationBuilder.UpdateData(
                table: "Agreement",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Discriminator" },
                values: new object[] { "RegulatedUserAgreement" });

            migrationBuilder.UpdateData(
                table: "Agreement",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Discriminator" },
                values: new object[] { "RegulatedUserAgreement" });

            migrationBuilder.UpdateData(
                table: "Agreement",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Discriminator" },
                values: new object[] { "RegulatedUserAgreement" });

            migrationBuilder.UpdateData(
                table: "Agreement",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Discriminator" },
                values: new object[] { "RegulatedUserAgreement" });

            #region Insert new Community Pharmacy Agreement
            migrationBuilder.InsertData(
                table: "Agreement",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "Discriminator", "EffectiveDate", "Text", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 9, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "CommunityPharmacistAgreement", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<h1>PHARMANET COMMUNITY PHARMACIST TERMS OF ACCESS</h1>

<p class=""bold"">
  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
</p>

<ol>
  <li>

    <p class=""bold underline"">
      BACKGROUND
    </p>

    <p>
      The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links B.C.
      pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered into
      PharmaNet.
    </p>

    <p>
      The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet is to
      enhance patient care by providing timely and relevant information to persons involved in the provision of direct
      patient care.
    </p>

    <p class=""bold"">
      PharmaNet contains highly sensitive confidential information, including Personal Information and the proprietary
      and confidential information of third-party licensors to the Province, and it is in the public interest to ensure
      that appropriate measures are in place to protect the confidentiality of all such information. All access to and
      use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
    </p>

  </li>
  <li>

    <p class=""bold underline"">
      INTERPRETATION
    </p>

    <ol type=""a"">
      <li>

        <p>
          <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will have the
          meanings given below:
        </p>

        <ul class=""list-unstyled"">
          <li>
            <strong>“Act”</strong> means the <em>Pharmaceutical Services Act</em>.
          </li>
          <li>
            <strong>“Approved Practice Site”</strong> means the physical site at which you provide Direct Patient Care
            and which is approved by the Province for PharmaNet access.
            <span class=""underline"">For greater certainty, “Approved Practice Site” does not include a location from which remote access to PharmaNet takes place;</span>
          </li>
          <li>
            <strong>“Approved SSO”</strong> means a software support organization approved by the Province that provides
            you with the information technology software and/or services through which you and On-Behalf-of Users access
            PharmaNet.
          </li>
          <li>
            <strong>“Claim”</strong> means a claim made under the Act for payment in respect of a benefit under the Act.
          </li>
          <li>

            <p>
              <strong>“Conformance Standards”</strong> means the following documents published by the Province, as
              amended from time to time:
            </p>

            <ol type=""i"">
              <li>
                PharmaNet Professional and Software Conformance Standards<br>
                <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards"" target=""_blank"" rel=""noopener noreferrer"">
                  https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards
                </a>;
              </li>
              <li>
                Policy for Secure Remote Access to PharmaNet
                <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards"" target=""_blank"" rel=""noopener noreferrer"">
                  https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards
                </a>; and
              </li>
              <li>
                iii. Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                Architecture for Wireless Local Area Network Connectivity”.
              </li>
            </ol>

          </li>
          <li>
            <strong>“Device Provider”</strong> means a person enrolled under section 11 of the Act in the class of
            provider known as “device provider”.
          </li>
          <li>
            <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
            services to an individual to whom you provide direct patient care in the context of your Practice.
          </li>
          <li>
            <strong>“Information Management Regulation”</strong> means the <em>Information Management Regulation</em>,
            B.C. Reg.
            74/2015.
          </li>
          <li>
            <strong>“On-Behalf-of User”</strong> means a member of your staff who (i) requires access to PharmaNet to
            carry out duties in relation to your Practice; (ii) is authorized by you to access PharmaNet on your behalf;
            and (iii) has been granted access to PharmaNet by the Province.
          </li>
          <li>
            <strong>“Personal Information”</strong> means all recorded information that is about an identifiable
            individual or is defined as, or deemed to be, “personal information” or “personal health information”
            pursuant to any Privacy Laws.
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
            <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record
            or information in the custody, control or possession of you or an On-Behalf-of User that was obtained
            through your or an On-Behalf-of User’s access to PharmaNet.
          </li>
          <li>
            <strong>“Practice”</strong> means your practice of the health profession regulated under the <em>Health
                      Professions Act</em>, or your practice as a Device Provider, as identified by you through PRIME
            or another mechanism provided by the Province.
          </li>
          <li>
            <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for,
            and manage, their access to PharmaNet, and through which users are granted access by the Province.
          </li>
          <li>
            <strong>“Privacy Laws”</strong> means the Act, the
            <em>Freedom of Information and Protection of Privacy Act</em>, the Personal Information Protection Act, and
            any other statutory or legal obligations of privacy owed by you or the Province, whether arising under
            statute, by contract or at common law.
          </li>
          <li>
            <strong>“Provider”</strong> means a person enrolled under section 11 of the Act for the purpose of receiving
            payment for providing benefits.
          </li>
          <li>
            <strong>“Provider Regulation”</strong> means the <em>Provider Regulation</em>, B.C. Reg. 222/2014.
          </li>
          <li>
            <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
            Minister of Health.
          </li>
          <li>
            <strong>“Professional College”</strong> is the regulatory body governing your Practice.
          </li>
          <li>
            <p>
              <strong>“Unauthorized Person”</strong> means any person other than:
            </p>

            <ol type=""i"">
              <li>
                you,
              </li>
              <li>
                an On-Behalf-of User, or
              </li>
              <li>
                a representative of an Approved SSO that is accessing PharmaNet for technical support purposes in
                accordance with section 6 of the <em>Information Management Regulation</em>.
              </li>
            </ol>

          </li>
        </ul>

      </li>
      <li>
        <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or regulation by
        name means the statute or regulation of British Columbia of that name, as amended or replaced from time to time,
        and includes any enactment made under the authority of that statute or regulation.
      </li>
      <li>

        <p>
          <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this Agreement:
        </p>

        <ol type=""i"">
          <li>
            i. a provision in the body of this Agreement will prevail over any conflicting provision in any further
            limits or conditions communicated to you in writing by the Province, unless the conflicting provision
            expressly states otherwise; and a provision referred to in (i) above will prevail over any conflicting
            provision in the Conformance Standards.
          </li>
        </ol>

      </li>
    </ol>

  </li>
  <li>

    <p class=""bold underline"">
      APPLICATION OF LEGISLATION
    </p>

    <p>
      You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act, the
      Information Management Regulation and all Privacy Laws applicable to PharmaNet and PharmaNet Data.
    </p>

  </li>
  <li>

    <p class=""bold underline"">
      NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
    </p>

    <p>
      You acknowledge that:
    </p>

    <ol type=""a"">
      <li>
        a. PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
        Province under the authority of the Act;
      </li>
      <li>
        b. specific provisions of the Act (including but not limited to sections 24, 25 and 29) and the Information
        Management Regulation apply directly to you and to On-Behalf-of Users as a result; and
      </li>
      <li>
        c. this Agreement documents limits and conditions, set by the minister in writing, that the Act requires you to
        comply with.
      </li>
    </ol>

  </li>
  <li>

    <p class=""bold underline"">
      ACCESS
    </p>

    <ol type=""a"">
      <li>
        <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
        compliance with the limits and conditions set out in this Agreement. The Province may from time to time, at its
        discretion, amend or change the scope of your access privileges to PharmaNet as privacy, security, business and
        clinical practice requirements change. In such circumstances, the Province will use reasonable efforts to notify
        you of such changes.
      </li>
      <li>

        <p>
          <strong>Requirements for Access.</strong> The following requirements apply to your access to PharmaNet:
        </p>

        <ol type=""i"">
          <li>
            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, for so
            long as you are a registrant in good standing with the Professional College and your registration permits
            you to deliver Direct Patient Care requiring access to PharmaNet or, in the case of access as a Device
            Provider, for so long as you are enrolled as a Device Provider;
          </li>
          <li>
            <p>you will only access PharmaNet:</p>

            <ul>
              <li>
                at the Approved Practice Site, and using only the technologies and applications approved by the
                Province; or
              </li>
              <li>
                • using a VPN or similar remote access technology, if you are physically located in British Columbia and
                remotely connected to the Approved Practice Site using a VPN or other remote access technology
                specifically approved by the Province in writing for the Approved Practice Site.
              </li>
            </ul>
          </li>
          <li>
            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access
            takes place at the Approved Practice Site and the access is required in relation to patients for whom you
            will be providing Direct Patient Care at the Approved Practice Site;
          </li>
          <li>
            you must ensure that your On-Behalf-of Users do not access PharmaNet using VPN or other remote access
            technology;
          </li>
          <li>
            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will
            ensure that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct
            Patient Care;
          </li>
          <li>
            you will not submit Claims on PharmaNet other than from an Approved Practice Site in respect of which a
            person is enrolled as a Provider, and you will ensure that On-Behalf-of Users submit Claims on PharmaNet
            only from an Approved Practice Site in respect of which a person is enrolled as a Provider;
          </li>
          <li>
            you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of market
            research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet Data, for the
            purpose of market research;
          </li>
          <li>
            subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure that
            On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of Direct Patient
            Care, including for the purposes of quality improvement, evaluation, health care planning, surveillance,
            research or other secondary uses;
          </li>
          <li>
            you will not permit any Unauthorized Person to access PharmaNet, and you will take all reasonable
            measures to ensure that no Unauthorized Person can access PharmaNet;
          </li>
          <li>
            you will complete any training program(s) that your Approved SSO makes available to you in relation to
            PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
          </li>
          <li>
            you will immediately notify the Province when you or an On-Behalf-of User no longer require access to
            PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave of absence
            from your staff, or where the On-Behalf-of User’s access-related duties in relation to the Practice have
            changed;
          </li>
          <li>
            you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional limits or
            conditions applicable to you, as may be communicated to you by the Province in writing;
          </li>
          <li>
            you represent and warrant that all information provided by you in connection with your application for
            PharmaNet access, including through PRIME, is true and correct.
          </li>
        </ol>

      </li>
      <li>
        <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this Agreement
        for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
        PharmaNet Data.
      </li>
      <li>

        <p>
          <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable measures to
          safeguard Personal Information, including any Personal Information in PharmaNet Data while it is in the
          custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
        </p>

        <ol type=""i"">
          <li>
            take all reasonable steps to ensure the physical security of Personal Information, generally and as
            required by Privacy Laws;
          </li>
          <li>
            secure all workstations and printers in a protected area in the Approved Practice Site to prevent
            viewing of PharmaNet Data by Unauthorized Persons;
          </li>
          <li>
            ensure separate access credential (such as user name and password) for each On-Behalf-of User, and
            prohibit sharing or other multiple use of your access credential, or an On-Behalf-of User’s access
            credential, for access to PharmaNet;
          </li>
          <li>
            secure any workstations used to access PharmaNet and all devices, codes or passwords that enable access
            to PharmaNet by yourself or any On-Behalf-of User;
          </li>
          <li>
            take such other privacy and security measures as the Province may reasonably require from time-to-time.
          </li>
        </ol>

      </li>
      <li>
        <strong>Conformance Standards.</strong> You will comply with, and will ensure On-Behalf-of Users comply with,
        the rules specified in the Conformance Standards when accessing and recording information in PharmaNet.
      </li>
    </ol>

  </li>
  <li>

    <p class=""bold underline"">
      DISCLOSURE, STORAGE, AND ACCESS REQUESTS
    </p>

    <ol type=""a"">
      <li>
        <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper files or
        any electronic system, unless such storage or retention is required for record keeping in accordance with the
        Act, the Provider Regulation, and Professional College requirements and in connection with your provision of
        Direct Patient Care and otherwise is in compliance with the Conformance Standards. You will not modify any
        records retained in accordance with this section other than as may be expressly authorized in the Conformance
        Standards. For clarity, you may annotate a discrete record provided that the discrete record is not itself
        modified other than as expressly authorized in the Conformance Standards.
      </li>
      <li>
        <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with section
        6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the purpose of
        monitoring your own Practice.
      </li>
      <li>
        <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do not,
        disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient Care or is
        otherwise authorized under section 24(1) of the Act.
      </li>
      <li>
        <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of Users do
        not, disclose PharmaNet Data for the purpose of market research.
      </li>
      <li>
        <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in accordance
        with section 6(a) of this Agreement, you will not provide to patients any copies of records containing PharmaNet
        Data or “print outs” produced directly from PharmaNet, and will refer any requests for access to such records or
        “print outs” to the Province.
      </li>
      <li>
        <strong>Responding to Requests to Correct a Record contained in PharmaNet.</strong> If you receive a request for
        correction of any record or information contained in PharmaNet, you will refer the request to the Province.
      </li>
      <li>
        <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the Province if
        you receive any order, demand or request compelling, or threatening to compel, disclosure of records contained
        in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For greater
        certainty, the foregoing requires that you notify the Province only with respect to any access requests or
        demands for records contained in PharmaNet, and not records retained by you in accordance with section 6(a) of
        this Agreement.
      </li>
    </ol>

  </li>
  <li>

    <p class=""bold underline"">
      ACCURACY
    </p>

    <p>
      You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of User
      in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material inaccuracy or
      error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it if
      necessary, and notify the Province of the inaccuracy or error and any steps taken.
    </p>

  </li>
  <li>

    <p class=""bold underline"">
      INVESTIGATIONS, AUDITS, AND REPORTING
    </p>

    <ol type=""a"">
      <li>
        <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations conducted by
        the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
        Agreement, including providing access upon request to your facilities, data management systems, books, records
        and personnel for the purposes of such audit or investigation.
      </li>
      <li>
        <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province may
        report any material breach of this Agreement to your Professional College or to the Information and Privacy
        Commissioner of British Columbia.
      </li>
    </ol>

  </li>
  <li>

    <p class=""bold underline"">
      NOTICE OF NON COMPLIANCE AND DUTY TO INVESTIGATE
    </p>

    <ol type=""a"">
      <li>
        <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this Agreement,
        and will take all reasonable steps to prevent recurrences of any such breaches, including taking any steps
        necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of User’s
        access rights.
      </li>
      <li>

        <p>
          <strong>Non Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
        </p>

        <ol type=""i"">
          <li>
            you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User will be unable
            to comply with the terms of this Agreement in any respect, or
          </li>
          <li>
            you have knowledge of any circumstances, incidents or events which have or may jeopardize the security,
            confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person, to access
            PharmaNet.
          </li>
        </ol>

      </li>
    </ol>

  </li>
  <li>

    <p class=""bold underline"">
      TERM OF AGREEMENT, SUSPENSION & TERMINATION
    </p>

    <ol type=""a"">
      <li>
        <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet by the
        Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or (e)
        below.
      </li>
      <li>
        <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written notice to
        the Province.
      </li>
      <li>
        <strong>Suspension or Termination of PharmaNet access.</strong> If the Province suspends or terminates your
        right, or an On-Behalf-of User’s right, to access PharmaNet under the
        <em>Information Management Regulation</em>, the Province may also terminate this Agreement at any time
        thereafter upon written notice to you.
      </li>
      <li>
        <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate this
        Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to you if
        you or an On-Behalf-of User fail to comply with any provision of this Agreement.
      </li>
      <li>
        <strong>Termination by operation of the Information Management Regulation.</strong> This Agreement will
        terminate automatically if your access to PharmaNet ends by operation of section 18 of the
        <em>Information Management Regulation</em>.
      </li>
      <li>
        <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may suspend your
        account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the Province’s
        policies. Please contact the Province immediately if your account has been suspended for inactivity but you
        still require access to PharmaNet.
      </li>
    </ol>

  </li>
  <li>

    <p class=""bold underline"">
      DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
    </p>

    <ol type=""a"">
      <li>
        <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and PharmaNet
        Data is solely at your own risk. All such access and information is provided on an “as is” and “as available”
        basis without warranty or condition of any kind. The Province does not warrant the accuracy, completeness or
        reliability of the PharmaNet Data or the availability of PharmaNet, or that access to or the operation of
        PharmaNet will function without error, failure or interruption.
      </li>
      <li>
        <strong>You are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
        you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or acting
        upon such information. The clinical or other information disclosed to you or an On-Behalf-of User pursuant to
        this Agreement is in no way intended to be a substitute for professional judgment.
      </li>
      <li>
        <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the Province
        for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or PharmaNet
        Data.
      </li>
      <li>
        <strong>You Must Indemnify the Province if You Cause a Loss or Claim.</strong> You agree to indemnify and save
        harmless the Province, and the Province’s employees and agents (each an <strong>""Indemnified Person""</strong>)
        from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified Person may
        sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are based
        upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
        On-Behalf-of User, in connection with this Agreement or in connection with access to PharmaNet by you or an
        On-Behalf-of User.
      </li>
    </ol>

  </li>
  <li>

    <p class=""bold underline"">
      NOTICE
    </p>

    <ol type=""a"">
      <li>

        <p>
          <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another method of
          delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to be
          effective, must be in writing and emailed or mailed to:
        </p>

        <address>
          Director, Information and PharmaNet Innovation<br>
          Ministry of Health<br>
          PO Box 9652, STN PROV GOVT<br>
          Victoria, BC V8W 9P4<br>

          <br>

          <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
        </address>

      </li>
      <li>
        <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will be in
        writing and delivered by the Province to you using any of the contact mechanisms identified by you in PRIME,
        including by mail to a specified postal address, email to a specified email address or text message to the
        specified cell phone number. You may be required to click a URL link or log into PRIME to receive the content of
        any such notice.
      </li>
      <li>
        <strong>Deemed receipt.</strong> Any written communication from a party, if personally delivered or sent
        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent by
        mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays) after
        the date the notice was sent.
      </li>
      <li>
        <strong>Substitute contact information.</strong> You may notify the Province of a substitute contact mechanism
        by updating your contact information in PRIME.
      </li>
    </ol>

  </li>
  <li>

    <p class=""bold underline"">
      GENERAL
    </p>

    <ol type=""a"">
      <li>

        <p>
          <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and is
          severable from any other covenant, and if any of them are held by a court, or other decision-maker, to be
          invalid, this Agreement will be interpreted as if such provisions were not included.
        </p>

      </li>
      <li>

        <p>
          <strong>Survival.</strong> Sections 3, 4, 5(b)(vii) (viii), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any other
          provision of this Agreement that expressly or by its nature continues after termination, shall survive
          termination of this Agreement.
        </p>

      </li>
      <li>

        <p>
          <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and interpreted in
          accordance with the laws of British Columbia and the laws of Canada applicable therein.
        </p>

      </li>
      <li>

        <p>
          <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be assigned
          without the prior written approval of the Province.
        </p>

      </li>
      <li>

        <p>
          <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any provision of
          this Agreement by you is not a waiver of its right subsequently to insist on performance of that or any other
          provision of this Agreement.
        </p>

      </li>
      <li>

        <p>
          <strong>Province may modify this Agreement.</strong> The Province may amend this Agreement, including this
          section, at any time in its sole discretion:
        </p>

        <ol type=""i"">
          <li>
            by written notice to you, in which case the amendment will become effective upon the later of (A) the
            date notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
            by the Province, if any; or
          </li>
          <li>
            by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
            specify the effective date of the amendment, which date will be at least 30 (thirty) days after the date
            that the PharmaCare Newsletter containing the notice is first published.
          </li>
        </ol>

        <p>
          If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment described in
          (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this Agreement will be
          deemed to have been so amended as of the effective date. If you do not agree with any amendment for which
          notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly (and in any
          event before the effective date) cease all access or use of PharmaNet by yourself and all On-Behalf-of Users,
          and take the steps necessary to terminate this Agreement in accordance with section 10.
        </p>

      </li>
    </ol>

  </li>
</ol>
", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });
            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTerm_Agreement_AgreementId",
                table: "AccessTerm");

            migrationBuilder.DropTable(
                name: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_AccessTerm_AgreementId",
                table: "AccessTerm");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "LimitsConditionsClause");

            migrationBuilder.DropColumn(
                name: "AgreementId",
                table: "AccessTerm");

            migrationBuilder.AddColumn<string>(
                name: "Clause",
                table: "LimitsConditionsClause",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnrolleeId",
                table: "LimitsConditionsClause",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GlobalClauseId",
                table: "AccessTerm",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserClauseId",
                table: "AccessTerm",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GlobalClause",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Clause = table.Column<string>(type: "text", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EffectiveDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalClause", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LicenseClassClause",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Clause = table.Column<string>(type: "text", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EffectiveDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseClassClause", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClause",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Clause = table.Column<string>(type: "text", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EffectiveDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EnrolleeClassification = table.Column<string>(type: "text", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClause", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessTermLicenseClassClause",
                columns: table => new
                {
                    AccessTermId = table.Column<int>(type: "integer", nullable: false),
                    LicenseClassClauseId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessTermLicenseClassClause", x => new { x.AccessTermId, x.LicenseClassClauseId });
                    table.ForeignKey(
                        name: "FK_AccessTermLicenseClassClause_AccessTerm_AccessTermId",
                        column: x => x.AccessTermId,
                        principalTable: "AccessTerm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessTermLicenseClassClause_LicenseClassClause_LicenseClas~",
                        column: x => x.LicenseClassClauseId,
                        principalTable: "LicenseClassClause",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LicenseClassClauseMapping",
                columns: table => new
                {
                    LicenseCode = table.Column<int>(type: "integer", nullable: false),
                    OrganizatonTypeCode = table.Column<int>(type: "integer", nullable: false),
                    LicenseClassClauseId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseClassClauseMapping", x => new { x.LicenseCode, x.OrganizatonTypeCode, x.LicenseClassClauseId });
                    table.ForeignKey(
                        name: "FK_LicenseClassClauseMapping_LicenseClassClause_LicenseClassCl~",
                        column: x => x.LicenseClassClauseId,
                        principalTable: "LicenseClassClause",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicenseClassClauseMapping_LicenseLookup_LicenseCode",
                        column: x => x.LicenseCode,
                        principalTable: "LicenseLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicenseClassClauseMapping_OrganizationTypeLookup_Organizato~",
                        column: x => x.OrganizatonTypeCode,
                        principalTable: "OrganizationTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GlobalClause",
                columns: new[] { "Id", "Clause", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 1, "", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "LicenseClassClause",
                columns: new[] { "Id", "Clause", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "Type", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, "", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "Dispense", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, "", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "Prescribe", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "UserClause",
                columns: new[] { "Id", "Clause", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "EnrolleeClassification", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, @"<h1>PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER</h1>

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
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "OBO", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <ol>
                  <li>

                    <p class=""bold underline"">
                      BACKGROUND
                    </p>

                    <p>
                      The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links B.C.
                      pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered into
                      PharmaNet.
                    </p>

                    <p>
                      The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet is to
                      enhance patient care by providing timely and relevant information to persons involved in the provision of direct
                      patient care.
                    </p>

                    <p class=""bold underline"">
                      PharmaNet contains highly sensitive confidential information, including Personal Information and the proprietary
                      and confidential information of third-party licensors to the Province, and it is in the public interest to
                      ensure that appropriate measures are in place to protect the confidentiality of all such information. All access
                      to and use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INTERPRETATION
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will have the
                          meanings given below:
                        </p>

                        <ul class=""list-unstyled"">
                          <li>
                            <strong>“Act”</strong> means the Pharmaceutical Services Act.
                          </li>
                          <li>
                            <strong>“Approved SSO”</strong> means a software support organization approved by the Province that provides
                            you with the information technology software and/or services through which you and On-Behalf-of Users access
                            PharmaNet.
                          </li>
                          <li>

                            <p>
                              <strong>“Conformance Standards”</strong> means the following documents published by the Province, as
                              amended
                              from time to time:
                            </p>

                            <ol type=""i"">
                              <li>
                                PharmaNet Professional and Software Conformance Standards; and
                              </li>
                              <li>
                                Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                                Architecture for Wireless Local Area Network Connectivity”.
                              </li>
                            </ol>

                          </li>
                          <li>
                            <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                            services to an individual to whom you provide direct patient care in the context of your Practice.
                          </li>
                          <li>
                            <strong>“Information Management Regulation”</strong> means the Information Management Regulation, B.C. Reg.
                            74/2015.
                          </li>
                          <li>
                            <strong>“On-Behalf-of User”</strong> means a member of your staff who (i) requires access to PharmaNet to
                            carry out duties in relation to your Practice; and (ii) is authorized by you to access PharmaNet on your
                            behalf; and (iii) has been granted access to PharmaNet by the Province.
                          </li>
                          <li>
                            <strong>“Personal Information”</strong> means all recorded information that is about an identifiable
                            individual or is defined as, or deemed to be, “personal information” or “personal health information”
                            pursuant to any Privacy Laws.
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
                            <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record
                            or information in the custody, control or possession of you or a On-Behalf-of User that was obtained through
                            your or a On-Behalf-of User’s access to PharmaNet.
                          </li>
                          <li>
                            <strong>“Practice”</strong> means your practice of the health profession regulated under the Health
                            Professions Act and identified by you through PRIME.
                          </li>
                          <li>
                            <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for,
                            and manage, their access to PharmaNet, and through which users are granted access by the Province.
                          </li>
                          <li>
                            <strong>“Privacy Laws”</strong> means the Act, the Freedom of Information and Protection of Privacy Act, the
                            Personal Information Protection Act, and any other statutory or legal obligations of privacy owed by you or
                            the Province, whether arising under statute, by contract or at common law.
                          </li>
                          <li>
                            <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                            Minister of Health.
                          </li>
                          <li>
                            <strong>“Professional College”</strong> is the regulatory body governing your Practice.
                          </li>
                          <li>

                            <p>
                              <strong>“Unauthorized Person”</strong> means any person other than:
                            </p>

                            <ol type=""i"">
                              <li>
                                you,
                              </li>
                              <li>
                                an On-Behalf-of User, or
                              </li>
                              <li>
                                a representative of an Approved SSO that is accessing PharmaNet for technical support purposes in
                                accordance with section 6 of the Information Management Regulation.
                              </li>
                            </ol>

                          </li>
                        </ul>

                      </li>
                      <li>
                        <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or regulation by
                        name means the statute or regulation of British Columbia of that name, as amended or replaced from time to time,
                        and includes any enactment made under the authority of that statute or regulation.
                      </li>
                      <li>

                        <p>
                          <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this Agreement:
                        </p>

                        <ol type=""i"">
                          <li>
                            a provision in the body of this Agreement will prevail over any conflicting provision in any further limits
                            or conditions communicated to you in writing by the Province, unless the conflicting provision expressly
                            states otherwise; and
                          </li>
                          <li>
                            a provision referred to in (i) above will prevail over any conflicting provision in the Conformance
                            Standards.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      APPLICATION OF LEGISLATION
                    </p>

                    <p>
                      You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act and all
                      Privacy Laws applicable to PharmaNet and PharmaNet Data.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
                    </p>

                    <p>
                      You acknowledge that:
                    </p>

                    <ol type=""a"">
                      <li>
                        PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
                        Province under the authority of the Act;
                      </li>
                      <li>
                        specific provisions of the Act, including the Information Management Regulation and sections 24, 25 and 29 of
                        the Act, apply directly to you and to On-Behalf-of Users as a result; and
                      </li>
                      <li>
                        this Agreement documents limits and conditions, set by the minister in writing, that the Act requires you to
                        comply with.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCESS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
                        compliance with the limits and conditions set out in section 5(b) below and otherwise in this Agreement. The
                        Province may from time to time, at its discretion, amend or change the scope of your access privileges to
                        PharmaNet as privacy, security, business and clinical practice requirements change. In such circumstances, the
                        Province will use reasonable efforts to notify you of such changes.
                      </li>
                      <li>

                        <p>
                          <strong>Limits and Conditions of Access.</strong> The following limits and conditions apply to your access to
                          PharmaNet:
                        </p>

                        <ol type=""i"">
                          <li>
                            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, for so
                            long as you are a registrant in good standing with the Professional College and your licence permits you to
                            deliver Direct Patient Care requiring access to PharmaNet;
                          </li>
                          <li>
                            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, at a
                            location approved by the Province, and using only the technologies and applications approved by the
                            Province. For greater certainty, you must not access PharmaNet using a VPN or similar remote access
                            technology to an approved location, unless that VPN or remote access technology has first been approved by
                            the Province in writing for use at the Practice;
                          </li>
                          <li>
                            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
                            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
                          </li>
                          <li>
                            you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of market
                            research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet Data, for the
                            purpose of market research;
                          </li>
                          <li>
                            subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure that
                            On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of Direct Patient
                            Care, including for the purposes of quality improvement, evaluation, health care planning, surveillance,
                            research or other secondary uses;
                          </li>
                          <li>
                            you will not permit any Unauthorized Person to access PharmaNet, and you will take all reasonable measures
                            to ensure that no Unauthorized Person can access PharmaNet;
                          </li>
                          <li>
                            you will complete any training program(s) that your Approved SSO makes available to you in relation to
                            PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
                          </li>
                          <li>
                            you will immediately notify the Province when you or an On-Behalf-of User no longer require access to
                            PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave of absence
                            from your staff, or where the On-Behalf-of User’s access-related duties in relation to the Practice have
                            changed;
                          </li>
                          <li>
                            you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional limits or
                            conditions applicable to you, as may be communicated to you by the Province in writing;
                          </li>
                          <li>
                            you represent and warrant that all information provided by you in connection with your application for
                            PharmaNet access, including through PRIME, is true and correct.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this Agreement
                        for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
                        PharmaNet Data.
                      </li>
                      <li>

                        <p>
                          <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable measures to
                          safeguard Personal Information, including any Personal Information in the PharmaNet Data while it is in the
                          custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
                        </p>

                        <ol type=""i"">
                          <li>
                            take all reasonable steps to ensure the physical security of Personal Information, generally and as required
                            by Privacy Laws;
                          </li>
                          <li>
                            secure all workstations and printers in a protected area in the Practice to prevent viewing of PharmaNet
                            Data by Unauthorized Persons;
                          </li>
                          <li>
                            ensure separate access credential (such as user name and password) for each On-Behalf-of User, and prohibit
                            sharing or other multiple use of your access credential, or an On-Behalf-of User’s access credential, for
                            access to PharmaNet;
                          </li>
                          <li>
                            secure any workstations used to access PharmaNet and all devices, codes or passwords that enable access to
                            PharmaNet by yourself or any On-Behalf-of User;
                          </li>
                          <li>
                            take such other privacy and security measures as the Province may reasonably require from time-to-time.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Conformance Standards - Business Rules.</strong> You will comply with, and will ensure On-Behalf-of
                        Users comply with, the business rules specified in the Conformance Standards when accessing and recording
                        information in PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLOSURE, STORAGE, AND ACCESS REQUESTS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper files or
                        any electronic system, unless such storage or retention is required for record keeping in accordance with
                        Professional College requirements and in connection with your provision of Direct Patient Care and otherwise is
                        in compliance with the Conformance Standards. You will not modify any records retained in accordance with this
                        section other than as may be expressly authorized in the Conformance Standards. For clarity, you may annotate a
                        discrete record provided that the discrete record is not itself modified other than as expressly authorized in
                        the Conformance Standards.
                      </li>
                      <li>
                        <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with section
                        6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the purpose of
                        monitoring your own Practice.
                      </li>
                      <li>
                        <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do not,
                        disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient Care or is
                        otherwise authorized under section 24(1) of the Act.
                      </li>
                      <li>
                        <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of Users do
                        not, disclose PharmaNet Data for the purpose of market research.
                      </li>
                      <li>
                        <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in accordance
                        with section 6(a) of this Agreement, you will not provide to patients any copies of records containing PharmaNet
                        Data or “print outs” produced directly from PharmaNet, and will refer any requests for access to such records or
                        “print outs” to the Province.
                      </li>
                      <li>
                        <strong>Responding to Requests to Correct a Record contained in PharmaNet.</strong> If you receive a request for
                        correction of any record or information contained in PharmaNet, you will refer the request to the Province.
                      </li>
                      <li>
                        <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the Province if
                        you receive any order, demand or request compelling, or threatening to compel, disclosure of records contained
                        in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For greater
                        certainty, the foregoing requires that you notify the Province only with respect to any access requests or
                        demands for records contained in PharmaNet, and not records retained by you in accordance with section 6(a) of
                        this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCURACY
                    </p>

                    <p>
                      You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of User
                      in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material inaccuracy or
                      error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it if
                      necessary, and notify the Province of the inaccuracy or error and any steps taken.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INVESTIGATIONS, AUDITS, AND REPORTING
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations conducted by
                        the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
                        Agreement, including providing access upon request to your facilities, data management systems, books, records
                        and personnel for the purposes of such audit or investigation.
                      </li>
                      <li>
                        <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province may
                        report any material breach of this Agreement to your Professional College or to the Information and Privacy
                        Commissioner of British Columbia.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE OF NON COMPLIANCE AND DUTY TO INVESTIGATE
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this Agreement,
                        and will take all reasonable steps to prevent recurrences of any such breaches, including taking any steps
                        necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of User’s
                        access rights.
                      </li>
                      <li>

                        <p>
                          <strong>Non Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
                        </p>

                        <ol type=""i"">
                          <li>
                            you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User will be unable
                            to comply with the terms of this Agreement in any respect, or
                          </li>
                          <li>
                            you have knowledge of any circumstances, incidents or events which have or may jeopardize the security,
                            confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person, to access
                            PharmaNet.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      TERM OF AGREEMENT, SUSPENSION & TERMINATION
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet by the
                        Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or (e)
                        below.
                      </li>
                      <li>
                        <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written notice to
                        the Province.
                      </li>
                      <li>
                        <strong>Suspension or Termination of PharmaNet access.</strong> If the Province suspends or terminates your
                        right, or an On-Behalf-of User’s right, to access PharmaNet under the Information Management Regulation, the
                        Province may also terminate this Agreement at any time thereafter upon written notice to you.
                      </li>
                      <li>
                        <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate this
                        Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to you if
                        you or an On-Behalf-of User fail to comply with any provision of this Agreement.
                      </li>
                      <li>
                        <strong>Termination by operation of the Information Management Regulation.</strong> This Agreement will
                        terminate automatically if your access to PharmaNet ends by operation of section 18 of the Information
                        Management Regulation.
                      </li>
                      <li>
                        <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may suspend your
                        account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the Province’s
                        policies. Please contact the Province immediately if your account has been suspended for inactivity but you
                        still require access to PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and PharmaNet
                        Data is solely at your own risk. All such access and information is provided on an “as is” and “as available”
                        basis without warranty or condition of any kind. The Province does not warrant the accuracy, completeness or
                        reliability of the PharmaNet Data or the availability of PharmaNet, or that access to or the operation of
                        PharmaNet will function without error, failure or interruption.
                      </li>
                      <li>
                        <strong>You are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
                        you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or acting
                        upon such information. The clinical or other information disclosed to you or an On-Behalf-of User pursuant to
                        this Agreement is in no way intended to be a substitute for professional judgment.
                      </li>
                      <li>
                        <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the Province
                        for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or PharmaNet
                        Data.
                      </li>
                      <li>
                        <strong>You Must Indemnify the Province if You Cause a Loss or Claim.</strong> You agree to indemnify and save
                        harmless the Province, and the Province’s employees and agents (each an <strong>""Indemnified Person""</strong>)
                        from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified Person may
                        sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are based
                        upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
                        On-Behalf-of User, in connection with this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another method of
                          delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to be
                          effective,
                          must be in writing and emailed or mailed to:
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
                        <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will be in
                        writing and delivered by the Province to you using any of the contact mechanisms identified by you in PRIME,
                        including by mail to a specified postal address, email to a specified email address or text message to the
                        specified cell phone number. You may be required to click a URL link or log into PRIME to receive the content
                        of any such notice.
                      </li>
                      <li>
                        <strong>Deemed receipt.</strong> Any written communication from a party, if personally delivered or sent
                        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent
                        by mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays)
                        after the date the notice was sent.
                      </li>
                      <li>
                        <strong>Substitute contact information.</strong> You may notify the Province of a substitute contact mechanism
                        by updating your contact information in PRIME.
                      </li>
                    </ol>

                  </li>
                  {$lcPlaceholder}
                  <li>

                    <p class=""bold underline"">
                      GENERAL
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Entire Agreement.</strong> This Agreement constitutes the entire agreement between the parties with
                          respect to the subject matter of this agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and is
                          severable from any other covenant, and if any of them are held by a court, or other decision-maker, to be
                          invalid, this Agreement will be interpreted as if such provisions were not included.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Survival.</strong> Sections 3, 4, 5(b)(iv) (v), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any other
                          provision of this Agreement that expressly or by its nature continues after termination, shall survive
                          termination of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and interpreted in
                          accordance with the laws of British Columbia and the laws of Canada applicable therein.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be assigned
                          without the prior written approval of the Province.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any provision of
                          this Agreement by you is not a waiver of its right subsequently to insist on performance of that or any other
                          provision of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Province may modify this Agreement.</strong> The Province may amend this Agreement, including this
                          section, at any time in its sole discretion:
                        </p>

                        <ol type=""i"">
                          <li>
                            by written notice to you, in which case the amendment will become effective upon the later of (A) the date
                            notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified by
                            the Province, if any; or
                          </li>
                          <li>
                            by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                            specify the effective date of the amendment, which date will be at least 30 (thirty) days after the date
                            that the PharmaCare Newsletter containing the notice is first published.
                          </li>
                        </ol>

                        <p>
                          If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment described in
                          (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this Agreement will be
                          deemed to have been so amended as of the effective date. If you do not agree with any amendment for which
                          notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly (and in any
                          event before the effective date) cease all access or use of PharmaNet by yourself and all On-Behalf-of Users,
                          and take the steps necessary to terminate this Agreement in accordance with section 10.
                        </p>

                      </li>
                    </ol>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "RU", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, @"<h1>PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER</h1>

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
                                your employment duties in relation to a Practitioner require you to access PharmaNet (and PharmaNet
                                Data) to
                                support the Practitioner’s delivery of Direct Patient Care;
                            </li>
                            <li>
                                you are directly supervised by a Practitioner who has been granted access to PharmaNet by the Province;
                                and
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
                                <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of
                                health
                                services to an individual to whom a Practitioner provides direct patient care in the context of their
                                Practice.
                            </li>
                            <li>
                                <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on
                                the
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
                                <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any
                                record or
                                information in the custody, control or possession of you or a Practitioner that was obtained through
                                access to
                                PharmaNet by anyone.
                            </li>
                            <li>
                                <strong>“Practice”</strong> means a Practitioner’s practice of their health profession.
                            </li>
                            <li>
                                <strong>“Practitioner”</strong> means a health professional regulated under the Health Professions Act
                                who
                                supervises your access and use of PharmaNet and who has been granted access to PharmaNet by the
                                Province.
                            </li>
                            <li>
                                <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply
                                for, and
                                manage, their access to PharmaNet, and through which users are granted access by the Province.
                            </li>
                            <li>
                                <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by
                                the
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
                                access and use PharmaNet and PharmaNet Data only to support Direct Patient Care delivered by the
                                Practitioner to
                                the individuals whose PharmaNet Data you are accessing;
                            </li>
                            <li>
                                only access PharmaNet as permitted by law and directed by the Practitioner;
                            </li>
                            <li>
                                maintain all PharmaNet Data, whether accessed on PharmaNet or otherwise disclosed to you in any manner,
                                in
                                strict confidence;
                            </li>
                            <li>
                                maintain the security of PharmaNet, and any applications, connections, or networks used to access
                                PharmaNet;
                            </li>
                            <li>
                                complete all training required by the Practice’s PharmaNet software vendor and the Province before
                                accessing
                                PharmaNet;
                            </li>
                            <li>
                                notify the Province if you have any reason to suspect that PharmaNet, or any PharmaNet Data, is or has
                                been
                                accessed or used inappropriately by any person.
                            </li>
                        </ol>

                        <p class=""bold"">
                            You must not:
                        </p>

                        <ol type=""a""
                            start=""7"">
                            <li>
                                disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and
                                directed
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
                                take any action that might compromise the integrity of PharmaNet, its information, or the provincial
                                drug plan,
                                such as altering information or submitting false information;
                            </li>
                            <li>
                                test the security related to PharmaNet;
                            </li>
                            <li>
                                you must not attempt to access PharmaNet from any location other than the approved Practice site of the
                                Practitioner, including by VPN or other remote access technology,
                                unless that VPN or remote access technology has first been approved by the Province in writing for use
                                at the Practice.
                                You must be physically located in BC whenever you use approved VPN or other approved remote access
                                technology to access PharmaNet.
                            </li>
                        </ol>

                        <p>
                            Your access to PharmaNet and use of PharmaNet Data are governed by the Pharmaceutical Services Act and you
                            must
                            comply with all your duties under that Act.
                        </p>

                        <p>
                            The Province may, in writing and from time to time, set further limits and conditions in respect of
                            PharmaNet,
                            either for you or for the Practitioner(s), and that you must comply with any such further limits and
                            conditions.
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
                                by written notice to you, in which case the amendment will become effective upon the later of (A) the
                                date
                                notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
                                by the
                                Province, if any; or
                            </li>
                            <li>
                                by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                                specify
                                the effective date of the amendment, which date will be at least thirty (30) days after the date that
                                the
                                PharmaCare Newsletter containing the notice is first published.
                            </li>
                        </ol>

                        <p>
                            If you do not agree with any amendment for which notice has been provided by the Province in accordance with
                            (i)
                            or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
                            PharmaNet.
                        </p>

                        <p>
                            Any written notice to you under (i) above will be in writing and delivered by the Province to you using any
                            of the
                            contact mechanisms identified by you in PRIME, including by mail to a specified postal address, email to a
                            specified email address or text message to a specified cell phone number. You may be required to click a URL
                            link
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
                            Unless otherwise specified, a reference to a statute or regulation by name means the statute or regulation
                            of
                            British Columbia of that name, as amended or replaced from time to time, and includes any enactment made
                            under the
                            authority of that statute or regulation.
                        </p>

                    </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -8, 0, 0, 0)), "OBO", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

                <p class=""bold"">
                    By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <ol>
                    <li>

                        <p class=""bold underline"">
                            BACKGROUND
                        </p>

                        <p>
                            The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links
                            B.C.
                            pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered
                            into
                            PharmaNet.
                        </p>

                        <p>
                            The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet
                            is to
                            enhance patient care by providing timely and relevant information to persons involved in the provision of
                            direct
                            patient care.
                        </p>

                        <p class=""bold underline"">
                            PharmaNet contains highly sensitive confidential information, including Personal Information and the
                            proprietary
                            and confidential information of third-party licensors to the Province, and it is in the public interest to
                            ensure that appropriate measures are in place to protect the confidentiality of all such information. All
                            access
                            to and use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
                        </p>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            INTERPRETATION
                        </p>

                        <ol type=""a"">
                            <li>

                                <p>
                                    <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will
                                    have the
                                    meanings given below:
                                </p>

                                <ul class=""list-unstyled"">
                                    <li>
                                        <strong>“Act”</strong> means the Pharmaceutical Services Act.
                                    </li>
                                    <li>
                                        <strong>“Approved SSO”</strong> means a software support organization approved by the Province
                                        that provides
                                        you with the information technology software and/or services through which you and On-Behalf-of
                                        Users access
                                        PharmaNet.
                                    </li>
                                    <li>

                                        <p>
                                            <strong>“Conformance Standards”</strong> means the following documents published by the
                                            Province, as
                                            amended
                                            from time to time:
                                        </p>

                                        <ol type=""i"">
                                            <li>
                                                PharmaNet Professional and Software Conformance Standards; and
                                            </li>
                                            <li>
                                                Office of the Chief Information Officer: “Submission for Technical Security Standard and
                                                High Level
                                                Architecture for Wireless Local Area Network Connectivity”.
                                            </li>
                                        </ol>

                                    </li>
                                    <li>
                                        <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision
                                        of health
                                        services to an individual to whom you provide direct patient care in the context of your
                                        Practice.
                                    </li>
                                    <li>
                                        <strong>“Information Management Regulation”</strong> means the Information Management
                                        Regulation, B.C. Reg.
                                        74/2015.
                                    </li>
                                    <li>
                                        <strong>“On-Behalf-of User”</strong> means a member of your staff who (i) requires access to
                                        PharmaNet to
                                        carry out duties in relation to your Practice; and (ii) is authorized by you to access PharmaNet
                                        on your
                                        behalf; and (iii) has been granted access to PharmaNet by the Province.
                                    </li>
                                    <li>
                                        <strong>“Personal Information”</strong> means all recorded information that is about an
                                        identifiable
                                        individual or is defined as, or deemed to be, “personal information” or “personal health
                                        information”
                                        pursuant to any Privacy Laws.
                                    </li>
                                    <li>
                                        <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the
                                        Province on the
                                        following website (or such other website as may be specified by the Province from time to time
                                        for this
                                        purpose):

                                        <br><br>

                                        <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                                    </li>
                                    <li>
                                        <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information
                                        Management
                                        Regulation.
                                    </li>
                                    <li>
                                        <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and
                                        any record
                                        or information in the custody, control or possession of you or a On-Behalf-of User that was
                                        obtained through
                                        your or a On-Behalf-of User’s access to PharmaNet.
                                    </li>
                                    <li>
                                        <strong>“Practice”</strong> means your practice of the health profession regulated under the
                                        Health
                                        Professions Act and identified by you through PRIME.
                                    </li>
                                    <li>
                                        <strong>“PRIME”</strong> means the online service provided by the Province that allows users to
                                        apply for,
                                        and manage, their access to PharmaNet, and through which users are granted access by the
                                        Province.
                                    </li>
                                    <li>
                                        <strong>“Privacy Laws”</strong> means the Act, the Freedom of Information and Protection of
                                        Privacy Act, the
                                        Personal Information Protection Act, and any other statutory or legal obligations of privacy
                                        owed by you or
                                        the Province, whether arising under statute, by contract or at common law.
                                    </li>
                                    <li>
                                        <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as
                                        represented by the
                                        Minister of Health.
                                    </li>
                                    <li>
                                        <strong>“Professional College”</strong> is the regulatory body governing your Practice.
                                    </li>
                                    <li>

                                        <p>
                                            <strong>“Unauthorized Person”</strong> means any person other than:
                                        </p>

                                        <ol type=""i"">
                                            <li>
                                                you,
                                            </li>
                                            <li>
                                                an On-Behalf-of User, or
                                            </li>
                                            <li>
                                                a representative of an Approved SSO that is accessing PharmaNet for technical support
                                                purposes in
                                                accordance with section 6 of the Information Management Regulation.
                                            </li>
                                        </ol>

                                    </li>
                                </ul>

                            </li>
                            <li>
                                <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or
                                regulation by
                                name means the statute or regulation of British Columbia of that name, as amended or replaced from time
                                to time,
                                and includes any enactment made under the authority of that statute or regulation.
                            </li>
                            <li>

                                <p>
                                    <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this
                                    Agreement:
                                </p>

                                <ol type=""i"">
                                    <li>
                                        a provision in the body of this Agreement will prevail over any conflicting provision in any
                                        further limits
                                        or conditions communicated to you in writing by the Province, unless the conflicting provision
                                        expressly
                                        states otherwise; and
                                    </li>
                                    <li>
                                        a provision referred to in (i) above will prevail over any conflicting provision in the
                                        Conformance
                                        Standards.
                                    </li>
                                </ol>

                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            APPLICATION OF LEGISLATION
                        </p>

                        <p>
                            You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act and
                            all
                            Privacy Laws applicable to PharmaNet and PharmaNet Data.
                        </p>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
                        </p>

                        <p>
                            You acknowledge that:
                        </p>

                        <ol type=""a"">
                            <li>
                                PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by
                                the
                                Province under the authority of the Act;
                            </li>
                            <li>
                                specific provisions of the Act, including the Information Management Regulation and sections 24, 25 and
                                29 of
                                the Act, apply directly to you and to On-Behalf-of Users as a result; and
                            </li>
                            <li>
                                this Agreement documents limits and conditions, set by the minister in writing, that the Act requires
                                you to
                                comply with.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            ACCESS
                        </p>

                        <ol type=""a"">
                            <li>
                                <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
                                compliance with the limits and conditions set out in section 5(b) below and otherwise in this Agreement.
                                The
                                Province may from time to time, at its discretion, amend or change the scope of your access privileges
                                to
                                PharmaNet as privacy, security, business and clinical practice requirements change. In such
                                circumstances, the
                                Province will use reasonable efforts to notify you of such changes.
                            </li>
                            <li>

                                <p>
                                    <strong>Limits and Conditions of Access.</strong> The following limits and conditions apply to your
                                    access to
                                    PharmaNet:
                                </p>

                                <ol type=""i"">
                                    <li>
                                        you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access
                                        PharmaNet, for so
                                        long as you are a registrant in good standing with the Professional College and your licence
                                        permits you to
                                        deliver Direct Patient Care requiring access to PharmaNet;
                                    </li>
                                    <li>
                                        you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access
                                        PharmaNet, at a location approved by the Province, and using only the technologies and
                                        applications approved by the Province. For greater certainty, you must not access PharmaNet
                                        using a VPN or similar remote access technology to an approved location, unless that VPN or
                                        remote access technology has first been approved by the Province in writing for use at the
                                        Practice. You, or your On-Behalf-of Users, must be physically located in BC whenever you use VPN
                                        or similar remote access technology to access PharmaNet.
                                    </li>
                                    <li>
                                        you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you
                                        will ensure
                                        that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct
                                        Patient Care;
                                    </li>
                                    <li>
                                        you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of
                                        market
                                        research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet
                                        Data, for the
                                        purpose of market research;
                                    </li>
                                    <li>
                                        subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure
                                        that
                                        On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of
                                        Direct Patient
                                        Care, including for the purposes of quality improvement, evaluation, health care planning,
                                        surveillance,
                                        research or other secondary uses;
                                    </li>
                                    <li>
                                        you will not permit any Unauthorized Person to access PharmaNet, and you will take all
                                        reasonable measures
                                        to ensure that no Unauthorized Person can access PharmaNet;
                                    </li>
                                    <li>
                                        you will complete any training program(s) that your Approved SSO makes available to you in
                                        relation to
                                        PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
                                    </li>
                                    <li>
                                        you will immediately notify the Province when you or an On-Behalf-of User no longer require
                                        access to
                                        PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave
                                        of absence
                                        from your staff, or where the On-Behalf-of User’s access-related duties in relation to the
                                        Practice have
                                        changed;
                                    </li>
                                    <li>
                                        you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional
                                        limits or
                                        conditions applicable to you, as may be communicated to you by the Province in writing;
                                    </li>
                                    <li>
                                        you represent and warrant that all information provided by you in connection with your
                                        application for
                                        PharmaNet access, including through PRIME, is true and correct.
                                    </li>
                                </ol>

                            </li>
                            <li>
                                <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this
                                Agreement
                                for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
                                PharmaNet Data.
                            </li>
                            <li>

                                <p>
                                    <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable
                                    measures to
                                    safeguard Personal Information, including any Personal Information in the PharmaNet Data while it is
                                    in the
                                    custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
                                </p>

                                <ol type=""i"">
                                    <li>
                                        take all reasonable steps to ensure the physical security of Personal Information, generally and
                                        as required
                                        by Privacy Laws;
                                    </li>
                                    <li>
                                        secure all workstations and printers in a protected area in the Practice to prevent viewing of
                                        PharmaNet
                                        Data by Unauthorized Persons;
                                    </li>
                                    <li>
                                        ensure separate access credential (such as user name and password) for each On-Behalf-of User,
                                        and prohibit
                                        sharing or other multiple use of your access credential, or an On-Behalf-of User’s access
                                        credential, for
                                        access to PharmaNet;
                                    </li>
                                    <li>
                                        secure any workstations used to access PharmaNet and all devices, codes or passwords that enable
                                        access to
                                        PharmaNet by yourself or any On-Behalf-of User;
                                    </li>
                                    <li>
                                        take such other privacy and security measures as the Province may reasonably require from
                                        time-to-time.
                                    </li>
                                </ol>

                            </li>
                            <li>
                                <strong>Conformance Standards - Business Rules.</strong> You will comply with, and will ensure
                                On-Behalf-of
                                Users comply with, the business rules specified in the Conformance Standards when accessing and
                                recording
                                information in PharmaNet.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            DISCLOSURE, STORAGE, AND ACCESS REQUESTS
                        </p>

                        <ol type=""a"">
                            <li>
                                <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper
                                files or
                                any electronic system, unless such storage or retention is required for record keeping in accordance
                                with
                                Professional College requirements and in connection with your provision of Direct Patient Care and
                                otherwise is
                                in compliance with the Conformance Standards. You will not modify any records retained in accordance
                                with this
                                section other than as may be expressly authorized in the Conformance Standards. For clarity, you may
                                annotate a
                                discrete record provided that the discrete record is not itself modified other than as expressly
                                authorized in
                                the Conformance Standards.
                            </li>
                            <li>
                                <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with
                                section
                                6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the
                                purpose of
                                monitoring your own Practice.
                            </li>
                            <li>
                                <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do
                                not,
                                disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient
                                Care or is
                                otherwise authorized under section 24(1) of the Act.
                            </li>
                            <li>
                                <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of
                                Users do
                                not, disclose PharmaNet Data for the purpose of market research.
                            </li>
                            <li>
                                <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in
                                accordance
                                with section 6(a) of this Agreement, you will not provide to patients any copies of records containing
                                PharmaNet
                                Data or “print outs” produced directly from PharmaNet, and will refer any requests for access to such
                                records or
                                “print outs” to the Province.
                            </li>
                            <li>
                                <strong>Responding to Requests to Correct a Record contained in PharmaNet.</strong> If you receive a
                                request for
                                correction of any record or information contained in PharmaNet, you will refer the request to the
                                Province.
                            </li>
                            <li>
                                <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the
                                Province if
                                you receive any order, demand or request compelling, or threatening to compel, disclosure of records
                                contained
                                in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For
                                greater
                                certainty, the foregoing requires that you notify the Province only with respect to any access requests
                                or
                                demands for records contained in PharmaNet, and not records retained by you in accordance with section
                                6(a) of
                                this Agreement.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            ACCURACY
                        </p>

                        <p>
                            You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of
                            User
                            in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material
                            inaccuracy or
                            error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it
                            if
                            necessary, and notify the Province of the inaccuracy or error and any steps taken.
                        </p>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            INVESTIGATIONS, AUDITS, AND REPORTING
                        </p>

                        <ol type=""a"">
                            <li>
                                <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations
                                conducted by
                                the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
                                Agreement, including providing access upon request to your facilities, data management systems, books,
                                records
                                and personnel for the purposes of such audit or investigation.
                            </li>
                            <li>
                                <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province
                                may
                                report any material breach of this Agreement to your Professional College or to the Information and
                                Privacy
                                Commissioner of British Columbia.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            NOTICE OF NON COMPLIANCE AND DUTY TO INVESTIGATE
                        </p>

                        <ol type=""a"">
                            <li>
                                <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this
                                Agreement,
                                and will take all reasonable steps to prevent recurrences of any such breaches, including taking any
                                steps
                                necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of
                                User’s
                                access rights.
                            </li>
                            <li>

                                <p>
                                    <strong>Non Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
                                </p>

                                <ol type=""i"">
                                    <li>
                                        you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User
                                        will be unable
                                        to comply with the terms of this Agreement in any respect, or
                                    </li>
                                    <li>
                                        you have knowledge of any circumstances, incidents or events which have or may jeopardize the
                                        security,
                                        confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person,
                                        to access
                                        PharmaNet.
                                    </li>
                                </ol>

                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            TERM OF AGREEMENT, SUSPENSION & TERMINATION
                        </p>

                        <ol type=""a"">
                            <li>
                                <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet
                                by the
                                Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or
                                (e)
                                below.
                            </li>
                            <li>
                                <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written
                                notice to
                                the Province.
                            </li>
                            <li>
                                <strong>Suspension or Termination of PharmaNet access.</strong> If the Province suspends or terminates
                                your
                                right, or an On-Behalf-of User’s right, to access PharmaNet under the Information Management Regulation,
                                the
                                Province may also terminate this Agreement at any time thereafter upon written notice to you.
                            </li>
                            <li>
                                <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate
                                this
                                Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to
                                you if
                                you or an On-Behalf-of User fail to comply with any provision of this Agreement.
                            </li>
                            <li>
                                <strong>Termination by operation of the Information Management Regulation.</strong> This Agreement will
                                terminate automatically if your access to PharmaNet ends by operation of section 18 of the Information
                                Management Regulation.
                            </li>
                            <li>
                                <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may
                                suspend your
                                account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the
                                Province’s
                                policies. Please contact the Province immediately if your account has been suspended for inactivity but
                                you
                                still require access to PharmaNet.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
                        </p>

                        <ol type=""a"">
                            <li>
                                <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and
                                PharmaNet
                                Data is solely at your own risk. All such access and information is provided on an “as is” and “as
                                available”
                                basis without warranty or condition of any kind. The Province does not warrant the accuracy,
                                completeness or
                                reliability of the PharmaNet Data or the availability of PharmaNet, or that access to or the operation
                                of
                                PharmaNet will function without error, failure or interruption.
                            </li>
                            <li>
                                <strong>You are Responsible.</strong> You are responsible for verifying the accuracy of information
                                disclosed to
                                you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or
                                acting
                                upon such information. The clinical or other information disclosed to you or an On-Behalf-of User
                                pursuant to
                                this Agreement is in no way intended to be a substitute for professional judgment.
                            </li>
                            <li>
                                <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the
                                Province
                                for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or
                                PharmaNet
                                Data.
                            </li>
                            <li>
                                <strong>You Must Indemnify the Province if You Cause a Loss or Claim.</strong> You agree to indemnify
                                and save
                                harmless the Province, and the Province’s employees and agents (each an
                                <strong>""Indemnified Person""</strong>)
                                from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified
                                Person may
                                sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are
                                based
                                upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
                                On-Behalf-of User, in connection with this Agreement.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            NOTICE
                        </p>

                        <ol type=""a"">
                            <li>

                                <p>
                                    <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another
                                    method of
                                    delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to
                                    be
                                    effective,
                                    must be in writing and emailed or mailed to:
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
                                <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will
                                be in
                                writing and delivered by the Province to you using any of the contact mechanisms identified by you in
                                PRIME,
                                including by mail to a specified postal address, email to a specified email address or text message to
                                the
                                specified cell phone number. You may be required to click a URL link or log into PRIME to receive the
                                content
                                of any such notice.
                            </li>
                            <li>
                                <strong>Deemed receipt.</strong> Any written communication from a party, if personally delivered or sent
                                electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if
                                sent
                                by mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory
                                holidays)
                                after the date the notice was sent.
                            </li>
                            <li>
                                <strong>Substitute contact information.</strong> You may notify the Province of a substitute contact
                                mechanism
                                by updating your contact information in PRIME.
                            </li>
                        </ol>

                    </li>
                    {$lcPlaceholder}
                    <li>

                        <p class=""bold underline"">
                            GENERAL
                        </p>

                        <ol type=""a"">
                            <li>

                                <p>
                                    <strong>Entire Agreement.</strong> This Agreement constitutes the entire agreement between the
                                    parties with
                                    respect to the subject matter of this agreement.
                                </p>

                            </li>
                            <li>

                                <p>
                                    <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and
                                    is
                                    severable from any other covenant, and if any of them are held by a court, or other decision-maker,
                                    to be
                                    invalid, this Agreement will be interpreted as if such provisions were not included.
                                </p>

                            </li>
                            <li>

                                <p>
                                    <strong>Survival.</strong> Sections 3, 4, 5(b)(iv) (v), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any
                                    other
                                    provision of this Agreement that expressly or by its nature continues after termination, shall
                                    survive
                                    termination of this Agreement.
                                </p>

                            </li>
                            <li>

                                <p>
                                    <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and
                                    interpreted in
                                    accordance with the laws of British Columbia and the laws of Canada applicable therein.
                                </p>

                            </li>
                            <li>

                                <p>
                                    <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be
                                    assigned
                                    without the prior written approval of the Province.
                                </p>

                            </li>
                            <li>

                                <p>
                                    <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any
                                    provision of
                                    this Agreement by you is not a waiver of its right subsequently to insist on performance of that or
                                    any other
                                    provision of this Agreement.
                                </p>

                            </li>
                            <li>

                                <p>
                                    <strong>Province may modify this Agreement.</strong> The Province may amend this Agreement,
                                    including this
                                    section, at any time in its sole discretion:
                                </p>

                                <ol type=""i"">
                                    <li>
                                        by written notice to you, in which case the amendment will become effective upon the later of
                                        (A) the date
                                        notice of the amendment is first delivered to you, or (B) the effective date of the amendment
                                        specified by
                                        the Province, if any; or
                                    </li>
                                    <li>
                                        by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the
                                        notice will
                                        specify the effective date of the amendment, which date will be at least 30 (thirty) days after
                                        the date
                                        that the PharmaCare Newsletter containing the notice is first published.
                                    </li>
                                </ol>

                                <p>
                                    If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment
                                    described in
                                    (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this
                                    Agreement will be
                                    deemed to have been so amended as of the effective date. If you do not agree with any amendment for
                                    which
                                    notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly
                                    (and in any
                                    event before the effective date) cease all access or use of PharmaNet by yourself and all
                                    On-Behalf-of Users,
                                    and take the steps necessary to terminate this Agreement in accordance with section 10.
                                </p>

                            </li>
                        </ol>

                    </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -8, 0, 0, 0)), "RU", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, @"<h1>PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER</h1>

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
                                your employment duties in relation to a Practitioner require you to access PharmaNet (and PharmaNet
                                Data) to
                                support the Practitioner’s delivery of Direct Patient Care;
                            </li>
                            <li>
                                you are directly supervised by a Practitioner who has been granted access to PharmaNet by the Province;
                                and
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
                                <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of
                                health
                                services to an individual to whom a Practitioner provides direct patient care in the context of their
                                Practice.
                            </li>
                            <li>
                                <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on
                                the
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
                                <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any
                                record or
                                information in the custody, control or possession of you or a Practitioner that was obtained through
                                access to
                                PharmaNet by anyone.
                            </li>
                            <li>
                                <strong>“Practice”</strong> means a Practitioner’s practice of their health profession.
                            </li>
                            <li>
                                <strong>“Practitioner”</strong> means a health professional regulated under the Health Professions Act
                                who
                                supervises your access and use of PharmaNet and who has been granted access to PharmaNet by the
                                Province.
                            </li>
                            <li>
                                <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply
                                for, and
                                manage, their access to PharmaNet, and through which users are granted access by the Province.
                            </li>
                            <li>
                                <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by
                                the
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
                                access and use PharmaNet and PharmaNet Data only to support Direct Patient Care delivered by the
                                Practitioner to
                                the individuals whose PharmaNet Data you are accessing;
                            </li>
                            <li>
                                only access PharmaNet as permitted by law and directed by the Practitioner;
                            </li>
                            <li>
                                maintain all PharmaNet Data, whether accessed on PharmaNet or otherwise disclosed to you in any manner,
                                in
                                strict confidence;
                            </li>
                            <li>
                                maintain the security of PharmaNet, and any applications, connections, or networks used to access
                                PharmaNet;
                            </li>
                            <li>
                                complete all training required by the Practice’s PharmaNet software vendor and the Province before
                                accessing
                                PharmaNet;
                            </li>
                            <li>
                                notify the Province if you have any reason to suspect that PharmaNet, or any PharmaNet Data, is or has
                                been
                                accessed or used inappropriately by any person.
                            </li>
                        </ol>

                        <p class=""bold"">
                            You must not:
                        </p>

                        <ol type=""a""
                            start=""7"">
                            <li>
                                disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and
                                directed
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
                                take any action that might compromise the integrity of PharmaNet, its information, or the provincial
                                drug plan,
                                such as altering information or submitting false information;
                            </li>
                            <li>
                                test the security related to PharmaNet;
                            </li>
                            <li>
                                attempt to access PharmaNet from any location other than the approved Practice site of the
                                Practitioner, including by VPN or other remote access technology,
                                unless that VPN or remote access technology has first been approved by the Province in writing for use
                                at the Practice.
                                You must be physically located in BC whenever you use approved VPN or other approved remote access
                                technology to access PharmaNet.
                            </li>
                        </ol>

                        <p>
                            Your access to PharmaNet and use of PharmaNet Data are governed by the Pharmaceutical Services Act and you
                            must
                            comply with all your duties under that Act.
                        </p>

                        <p>
                            The Province may, in writing and from time to time, set further limits and conditions in respect of
                            PharmaNet,
                            either for you or for the Practitioner(s), and that you must comply with any such further limits and
                            conditions.
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
                                by written notice to you, in which case the amendment will become effective upon the later of (A) the
                                date
                                notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
                                by the
                                Province, if any; or
                            </li>
                            <li>
                                by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                                specify
                                the effective date of the amendment, which date will be at least thirty (30) days after the date that
                                the
                                PharmaCare Newsletter containing the notice is first published.
                            </li>
                        </ol>

                        <p>
                            If you do not agree with any amendment for which notice has been provided by the Province in accordance with
                            (i)
                            or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
                            PharmaNet.
                        </p>

                        <p>
                            Any written notice to you under (i) above will be in writing and delivered by the Province to you using any
                            of the
                            contact mechanisms identified by you in PRIME, including by mail to a specified postal address, email to a
                            specified email address or text message to a specified cell phone number. You may be required to click a URL
                            link
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
                            Unless otherwise specified, a reference to a statute or regulation by name means the statute or regulation
                            of
                            British Columbia of that name, as amended or replaced from time to time, and includes any enactment made
                            under the
                            authority of that statute or regulation.
                        </p>

                    </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "OBO", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS: TEST OF INSERTION OF INDIVIDUAL L&C COPY</h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <ol>
                  <li>

                    <p class=""bold underline"">
                      BACKGROUND
                    </p>

                    <p>
                      The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links B.C.
                      pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered into
                      PharmaNet.
                    </p>

                    <p>
                      The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet is to
                      enhance patient care by providing timely and relevant information to persons involved in the provision of direct
                      patient care.
                    </p>

                    <p class=""bold underline"">
                      PharmaNet contains highly sensitive confidential information, including Personal Information and the proprietary
                      and confidential information of third-party licensors to the Province, and it is in the public interest to ensure
                      that appropriate measures are in place to protect the confidentiality of all such information. All access to and
                      use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INTERPRETATION
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will have the
                          meanings given below:
                        </p>

                        <ul class=""list-unstyled"">
                          <li>
                            <strong>“Act”</strong> means the <em>Pharmaceutical Services Act</em>.
                          </li>
                          <li>
                            <strong>“Approved Practice Site”</strong> means the physical site at which you provide Direct Patient Care
                            and which is approved by the Province for PharmaNet access.
                            <span class=""underline"">For greater certainty, “Approved Practice Site” does not include a location from which remote access to PharmaNet takes place;</span>
                          </li>
                          <li>
                            <strong>“Approved SSO”</strong> means a software support organization approved by the Province that provides
                            you with the information technology software and/or services through which you and On-Behalf-of Users access
                            PharmaNet.
                          </li>
                          <li>

                            <p>
                              <strong>“Conformance Standards”</strong> means the following documents published by the Province, as
                              amended from time to time:
                            </p>

                            <ol type=""i"">
                              <li>
                                PharmaNet Professional and Software Conformance Standards; and
                              </li>
                              <li>
                                Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                                Architecture for Wireless Local Area Network Connectivity”.
                              </li>
                            </ol>

                          </li>
                          <li>
                            <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                            services to an individual to whom you provide direct patient care in the context of your Practice.
                          </li>
                          <li>
                            <strong>“Information Management Regulation”</strong> means the Information Management Regulation, B.C. Reg.
                            74/2015.
                          </li>
                          <li>
                            <strong>“On-Behalf-of User”</strong> means a member of your staff who (i) requires access to PharmaNet to
                            carry out duties in relation to your Practice; (ii) is authorized by you to access PharmaNet on your behalf;
                            and (iii) has been granted access to PharmaNet by the Province.
                          </li>
                          <li>
                            <strong>“Personal Information”</strong> means all recorded information that is about an identifiable
                            individual or is defined as, or deemed to be, “personal information” or “personal health information”
                            pursuant to any Privacy Laws.
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
                            <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record
                            or information in the custody, control or possession of you or an On-Behalf-of User that was obtained
                            through your or an On-Behalf-of User’s access to PharmaNet.
                          </li>
                          <li>
                            <strong>“Practice”</strong> means your practice of the health profession regulated under the <em>Health
                            Professions Act</em>, or your practice as an enrolled device provider under the
                            <em>Provider Regulation</em>, B.C. Reg.222/2014, as identified by you through PRIME.
                          </li>
                          <li>
                            <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for,
                            and manage, their access to PharmaNet, and through which users are granted access by the Province.
                          </li>
                          <li>
                            <strong>“Privacy Laws”</strong> means the Act, the
                            <em>Freedom of Information and Protection of Privacy Act</em>,
                            the <em>Personal Information Protection Act</em>, and any other statutory or legal obligations of privacy
                            owed by you or the Province, whether arising under statute, by contract or at common law.
                          </li>
                          <li>
                            <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                            Minister of Health.
                          </li>
                          <li>
                            <strong>“Professional College”</strong> is the regulatory body governing your Practice.
                          </li>
                          <li>

                            <p>
                              <strong>“Unauthorized Person”</strong> means any person other than:
                            </p>

                            <ol type=""i"">
                              <li>
                                you,
                              </li>
                              <li>
                                an On-Behalf-of User, or
                              </li>
                              <li>
                                a representative of an Approved SSO that is accessing PharmaNet for technical support purposes in
                                accordance with section 6 of the <em>Information Management Regulation</em>.
                              </li>
                            </ol>

                          </li>
                        </ul>

                      </li>
                      <li>
                        <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or regulation by
                        name means the statute or regulation of British Columbia of that name, as amended or replaced from time to time,
                        and includes any enactment made under the authority of that statute or regulation.
                      </li>
                      <li>

                        <p>
                          <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this Agreement:
                        </p>

                        <ol type=""i"">
                          <li>
                            a provision in the body of this Agreement will prevail over any conflicting provision in any further
                            limits or conditions communicated to you in writing by the Province, unless the conflicting provision
                            expressly states otherwise; and
                          </li>
                          <li>
                            a provision referred to in (i) above will prevail over any conflicting provision in the Conformance
                            Standards.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      APPLICATION OF LEGISLATION
                    </p>

                    <p>
                      You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act and all
                      Privacy Laws applicable to PharmaNet and PharmaNet Data.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
                    </p>

                    <p>
                      You acknowledge that:
                    </p>

                    <ol type=""a"">
                      <li>
                        PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
                        Province under the authority of the Act;
                      </li>
                      <li>
                        specific provisions of the Act, including the <em>Information Management Regulation</em> and sections 24, 25 and
                        29 of the Act, apply directly to you and to On-Behalf-of Users as a result; and
                      </li>
                      <li>
                        this Agreement documents limits and conditions, set by the minister in writing, that the Act requires you to
                        comply with.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCESS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
                        compliance with the limits and conditions set out in section 5(b) below and otherwise in this Agreement. The
                        Province may from time to time, at its discretion, amend or change the scope of your access privileges to
                        PharmaNet as privacy, security, business and clinical practice requirements change. In such circumstances, the
                        Province will use reasonable efforts to notify you of such changes.
                      </li>
                      <li>

                        <p>
                          <strong>Limits and Conditions of Access.</strong> The following limits and conditions apply to your access to
                          PharmaNet:
                        </p>

                        <ol type=""i"">
                          <li>
                            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, for so
                            long as you are a registrant in good standing with the Professional College and your registration permits
                            you to deliver Direct Patient Care requiring access to PharmaNet;
                          </li>
                          <li>
                            <p>you will only access PharmaNet:</p>

                            <ul>
                              <li>
                                at the Approved Practice Site, and using only the technologies and applications approved by the
                                Province; or
                              </li>
                              <li>
                                using a VPN or similar remote access technology, if you are physically located in British Columbia and
                                remotely connected to the Approved Practice Site using a VPN or other remote access technology
                                specifically approved by the Province in writing for the Approved Practice Site.
                              </li>
                            </ul>
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access
                            takes place at the Approved Practice Site and the access is in relation to patients for whom you will be
                            providing Direct Patient Care at the Approved Practice Site requiring the access to PharmaNet;
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of Users do not access PharmaNet using VPN or other remote access
                            technology
                          </li>
                          <li>
                            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
                            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
                          </li>
                          <li>
                            you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of market
                            research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet Data, for the
                            purpose of market research;
                          </li>
                          <li>
                            subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure that
                            On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of Direct Patient
                            Care, including for the purposes of quality improvement, evaluation, health care planning, surveillance,
                            research or other secondary uses;
                          </li>
                          <li>
                            you will not permit any Unauthorized Person to access PharmaNet, and you will take all reasonable measures
                            to ensure that no Unauthorized Person can access PharmaNet;
                          </li>
                          <li>
                            you will complete any training program(s) that your Approved SSO makes available to you in relation to
                            PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
                          </li>
                          <li>
                            you will immediately notify the Province when you or an On-Behalf-of User no longer require access to
                            PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave of absence
                            from your staff, or where the On-Behalf-of User’s access-related duties in relation to the Practice have
                            changed;
                          </li>
                          <li>
                            you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional limits or
                            conditions applicable to you, as may be communicated to you by the Province in writing;
                          </li>
                          <li>
                            you represent and warrant that all information provided by you in connection with your application for
                            PharmaNet access, including through PRIME, is true and correct.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this Agreement
                        for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
                        PharmaNet Data.
                      </li>
                      <li>

                        <p>
                          <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable measures to
                          safeguard Personal Information, including any Personal Information in the PharmaNet Data while it is in the
                          custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
                        </p>

                        <ol type=""i"">
                          <li>
                            take all reasonable steps to ensure the physical security of Personal Information, generally and as
                            required by Privacy Laws;
                          </li>
                          <li>
                            secure all workstations and printers in a protected area in the Practice to prevent viewing of PharmaNet
                            Data by Unauthorized Persons;
                          </li>
                          <li>
                            ensure separate access credential (such as user name and password) for each On-Behalf-of User, and prohibit
                            sharing or other multiple use of your access credential, or an On-Behalf-of User’s access credential, for
                            access to PharmaNet;
                          </li>
                          <li>
                            secure any workstations used to access PharmaNet and all devices, codes or passwords that enable access to
                            PharmaNet by yourself or any On-Behalf-of User;
                          </li>
                          <li>
                            take such other privacy and security measures as the Province may reasonably require from time-to-time.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Conformance Standards.</strong> You will comply with, and will ensure On-Behalf-of
                        Users comply with, the rules specified in the Conformance Standards when accessing and recording information in
                        PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLOSURE, STORAGE, AND ACCESS REQUESTS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper files or
                        any electronic system, unless such storage or retention is required for record keeping in accordance with
                        Professional College requirements and in connection with your provision of Direct Patient Care and otherwise is
                        in compliance with the Conformance Standards. You will not modify any records retained in accordance with this
                        section other than as may be expressly authorized in the Conformance Standards. For clarity, you may annotate a
                        discrete record provided that the discrete record is not itself modified other than as expressly authorized in
                        the Conformance Standards.
                      </li>
                      <li>
                        <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with section
                        6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the purpose of
                        monitoring your own Practice.
                      </li>
                      <li>
                        <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do not,
                        disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient Care or is
                        otherwise authorized under section 24(1) of the Act.
                      </li>
                      <li>
                        <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of Users do
                        not, disclose PharmaNet Data for the purpose of market research.
                      </li>
                      <li>
                        <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in accordance
                        with section 6(a) of this Agreement, you will not provide to patients any copies of records containing PharmaNet
                        Data or “print outs” produced directly from PharmaNet, and will refer any requests for access to such records or
                        “print outs” to the Province.
                      </li>
                      <li>
                        <strong>Responding to Requests to Correct a Record contained in PharmaNet.</strong> If you receive a request for
                        correction of any record or information contained in PharmaNet, you will refer the request to the Province.
                      </li>
                      <li>
                        <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the Province if
                        you receive any order, demand or request compelling, or threatening to compel, disclosure of records contained
                        in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For greater
                        certainty, the foregoing requires that you notify the Province only with respect to any access requests or
                        demands for records contained in PharmaNet, and not records retained by you in accordance with section 6(a) of
                        this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCURACY
                    </p>

                    <p>
                      You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of User
                      in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material inaccuracy or
                      error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it if
                      necessary, and notify the Province of the inaccuracy or error and any steps taken.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INVESTIGATIONS, AUDITS, AND REPORTING
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations conducted by
                        the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
                        Agreement, including providing access upon request to your facilities, data management systems, books, records
                        and personnel for the purposes of such audit or investigation.
                      </li>
                      <li>
                        <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province may
                        report any material breach of this Agreement to your Professional College or to the Information and Privacy
                        Commissioner of British Columbia.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE OF NON COMPLIANCE AND DUTY TO INVESTIGATE
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this Agreement,
                        and will take all reasonable steps to prevent recurrences of any such breaches, including taking any steps
                        necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of User’s
                        access rights.
                      </li>
                      <li>

                        <p>
                          <strong>Non Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
                        </p>

                        <ol type=""i"">
                          <li>
                            you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User will be unable
                            to comply with the terms of this Agreement in any respect, or
                          </li>
                          <li>
                            you have knowledge of any circumstances, incidents or events which have or may jeopardize the security,
                            confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person, to access
                            PharmaNet.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      TERM OF AGREEMENT, SUSPENSION & TERMINATION
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet by the
                        Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or (e)
                        below.
                      </li>
                      <li>
                        <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written notice to
                        the Province.
                      </li>
                      <li>
                        <strong>Suspension or Termination of PharmaNet access.</strong> If the Province suspends or terminates your
                        right, or an On-Behalf-of User’s right, to access PharmaNet under the
                        <em>Information Management Regulation</em>, the Province may also terminate this Agreement at any time
                        thereafter upon written notice to you.
                      </li>
                      <li>
                        <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate this
                        Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to you if
                        you or an On-Behalf-of User fail to comply with any provision of this Agreement.
                      </li>
                      <li>
                        <strong>Termination by operation of the Information Management Regulation.</strong> This Agreement will
                        terminate automatically if your access to PharmaNet ends by operation of section 18 of the
                        <em>Information Management Regulation</em>.
                      </li>
                      <li>
                        <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may suspend your
                        account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the Province’s
                        policies. Please contact the Province immediately if your account has been suspended for inactivity but you
                        still require access to PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and PharmaNet
                        Data is solely at your own risk. All such access and information is provided on an “as is” and “as available”
                        basis without warranty or condition of any kind. The Province does not warrant the accuracy, completeness or
                        reliability of the PharmaNet Data or the availability of PharmaNet, or that access to or the operation of
                        PharmaNet will function without error, failure or interruption.
                      </li>
                      <li>
                        <strong>You are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
                        you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or acting
                        upon such information. The clinical or other information disclosed to you or an On-Behalf-of User pursuant to
                        this Agreement is in no way intended to be a substitute for professional judgment.
                      </li>
                      <li>
                        <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the Province
                        for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or PharmaNet
                        Data.
                      </li>
                      <li>
                        <strong>You Must Indemnify the Province if You Cause a Loss or Claim.</strong> You agree to indemnify and save
                        harmless the Province, and the Province’s employees and agents (each an <strong>""Indemnified Person""</strong>)
                        from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified Person may
                        sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are based
                        upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
                        On-Behalf-of User, in connection with this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another method of
                          delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to be
                          effective, must be in writing and emailed or mailed to:
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
                        <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will be in
                        writing and delivered by the Province to you using any of the contact mechanisms identified by you in PRIME,
                        including by mail to a specified postal address, email to a specified email address or text message to the
                        specified cell phone number. You may be required to click a URL link or log into PRIME to receive the content of
                        any such notice.
                      </li>
                      <li>
                        <strong>Deemed receipt.</strong> Any written communication from a party, if personally delivered or sent
                        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent by
                        mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays) after
                        the date the notice was sent.
                      </li>
                      <li>
                        <strong>Substitute contact information.</strong> You may notify the Province of a substitute contact mechanism
                        by updating your contact information in PRIME.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      GENERAL
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and is
                          severable from any other covenant, and if any of them are held by a court, or other decision-maker, to be
                          invalid, this Agreement will be interpreted as if such provisions were not included.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Survival.</strong> Sections 3, 4, 5(b)(vi) (vii), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any other
                          provision of this Agreement that expressly or by its nature continues after termination, shall survive
                          termination of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and interpreted in
                          accordance with the laws of British Columbia and the laws of Canada applicable therein.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be assigned
                          without the prior written approval of the Province.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any provision of
                          this Agreement by you is not a waiver of its right subsequently to insist on performance of that or any other
                          provision of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Province may modify this Agreement.</strong> The Province may amend this Agreement, including this
                          section, at any time in its sole discretion:
                        </p>

                        <ol type=""i"">
                          <li>
                            by written notice to you, in which case the amendment will become effective upon the later of (A) the
                            date notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
                            by the Province, if any; or
                          </li>
                          <li>
                            by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                            specify the effective date of the amendment, which date will be at least 30 (thirty) days after the date
                            that the PharmaCare Newsletter containing the notice is first published.
                          </li>
                        </ol>

                        <p>
                          If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment described in
                          (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this Agreement will be
                          deemed to have been so amended as of the effective date. If you do not agree with any amendment for which
                          notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly (and in any
                          event before the effective date) cease all access or use of PharmaNet by yourself and all On-Behalf-of Users,
                          and take the steps necessary to terminate this Agreement in accordance with section 10.
                        </p>

                      </li>
                    </ol>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "RU", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, @"<h1>
                  PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER<br>
                  with individual limits and conditions added
                </h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <p class=""bold underline"">
                  On Behalf-of-User Access
                </p>

                <ol>
                  <li>
                    <p>
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
                </ol>

                <p class=""bold underline"">
                  Definitions
                </p>

                <ol start=""2"">
                  <li>
                    <p>
                      In these terms, capitalized terms will have the following meanings:
                    </p>

                    <ul class=""list-unstyled"">
                      <li>
                        <strong>“Approved Practice Site”</strong> means the physical site at which a Practitioner provides Direct
                        Patient Care and which is approved by the Province for PharmaNet access. For greater certainty, “Approved
                        Practice Site” does not include a location from which remote access to PharmaNet takes place.
                      </li>
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
                        <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the <em>Information Management
                        Regulation</em>, B.C. Reg. 74/2015.
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
                        <strong>“Practitioner”</strong> means a health professional regulated under the <em>Health Professions Act</em>,
                        or an enrolled device provide under the <em>Provider Regulation</em> B.C. Reg. 222/2014, who supervises your
                        access to and use of PharmaNet and who has been granted access to PharmaNet by the Province.
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
                    Unless otherwise specified, a reference to a statute or regulation by name means the statute or regulation of
                    British Columbia of that name, as amended or replaced from time to time, and includes any enactment made under the
                    authority of that statute or regulation.
                  </li>
                </ol>

                <p class=""bold underline"">
                  Terms of Access to PharmaNet
                </p>

                <ol start=""4"">
                  <li>

                    <p>
                      You must:
                    </p>

                    <ol type=""a"">
                      <li>
                        access and use PharmaNet and PharmaNet Data only at the Approved Practice Site of a Practitioner;
                      </li>
                      <li>
                        access and use PharmaNet and PharmaNet Data only to support Direct Patient Care delivered by a Practitioner to
                        the individuals whose PharmaNet Data you are accessing, and only if the Practitioner is or will be delivering
                        Direct Patient Care requiring that access to those individuals at the same Approved Practice Site at which the
                        access occurs;
                      </li>
                      <li>
                        only access PharmaNet as permitted by law and directed by a Practitioner;
                      </li>
                      <li>
                        maintain all PharmaNet Data, whether accessed on PharmaNet or otherwise disclosed to you in any manner, in
                        strict confidence;
                      </li>
                      <li>
                        maintain the security of PharmaNet, and any applications, connections, or networks used to access PharmaNet;
                      </li>
                      <li>
                        complete all training required by the Approved Practice Site’s PharmaNet software vendor and the Province before
                        accessing PharmaNet;
                      </li>
                      <li>
                        notify the Province if you have any reason to suspect that PharmaNet, or any PharmaNet Data, is or has been
                        accessed or used inappropriately by any person.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p>
                      You must not:
                    </p>

                    <ol type=""a"">
                      <li>
                        disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and
                        directed by a Practitioner;
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
                        attempt to access PharmaNet from any location other than the Approved Practice Site of a Practitioner,
                        including by VPN or other remote access technology;
                      </li>
                      <li>
                        access PharmaNet unless the access is for the purpose of supporting a Practitioner in providing Direct
                        Patient Care to a patient at the same Approved Practice Site at which your access occurs.
                      </li>
                    </ol>
                  </li>
                </ol>
                <ol start=""6"">
                  <li>
                    Your access to PharmaNet and use of PharmaNet Data are governed by the <em>Pharmaceutical Services Act</em> and you
                    must comply with all your duties under that Act.
                  </li>
                  <li>
                    The Province may, in writing and from time to time, set further limits and conditions in respect of PharmaNet,
                    either for you or for the Practitioner(s), and that you must comply with any such further limits and conditions.
                  </li>
                </ol>

                <p class=""bold underline"">
                  How to Notify the Province
                </p>

                <ol start=""8"">
                  <li>

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
                </ol>

                <p class=""bold underline"">
                  Province May Modify These Terms
                </p>

                <ol start=""9"">
                  <li>
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
                </ol>

                <p class=""bold underline"">
                  Governing Law
                </p>

                <ol start=""10"">
                  <li>

                    <p>
                      These terms will be governed by and will be construed and interpreted in accordance with the laws of British
                      Columbia and the laws of Canada applicable therein.
                    </p>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "OBO", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <ol>
                  <li>

                    <p class=""bold underline"">
                      BACKGROUND
                    </p>

                    <p>
                      The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links B.C.
                      pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered into
                      PharmaNet.
                    </p>

                    <p>
                      The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet is to
                      enhance patient care by providing timely and relevant information to persons involved in the provision of direct
                      patient care.
                    </p>

                    <p class=""bold underline"">
                      PharmaNet contains highly sensitive confidential information, including Personal Information and the proprietary
                      and confidential information of third-party licensors to the Province, and it is in the public interest to ensure
                      that appropriate measures are in place to protect the confidentiality of all such information. All access to and
                      use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INTERPRETATION
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will have the
                          meanings given below:
                        </p>

                        <ul class=""list-unstyled"">
                          <li>
                            <strong>“Act”</strong> means the <em>Pharmaceutical Services Act</em>.
                          </li>
                          <li>
                            <strong>“Approved Practice Site”</strong> means the physical site at which you provide Direct Patient Care
                            and which is approved by the Province for PharmaNet access.
                            <span class=""underline"">For greater certainty, “Approved Practice Site” does not include a location from which remote access to PharmaNet takes place;</span>
                          </li>
                          <li>
                            <strong>“Approved SSO”</strong> means a software support organization approved by the Province that provides
                            you with the information technology software and/or services through which you and On-Behalf-of Users access
                            PharmaNet.
                          </li>
                          <li>

                            <p>
                              <strong>“Conformance Standards”</strong> means the following documents published by the Province, as
                              amended from time to time:
                            </p>

                            <ol type=""i"">
                              <li>
                                PharmaNet Professional and Software Conformance Standards; and
                              </li>
                              <li>
                                Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                                Architecture for Wireless Local Area Network Connectivity”.
                              </li>
                            </ol>

                          </li>
                          <li>
                            <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                            services to an individual to whom you provide direct patient care in the context of your Practice.
                          </li>
                          <li>
                            <strong>“Information Management Regulation”</strong> means the Information Management Regulation, B.C. Reg.
                            74/2015.
                          </li>
                          <li>
                            <strong>“On-Behalf-of User”</strong> means a member of your staff who (i) requires access to PharmaNet to
                            carry out duties in relation to your Practice; (ii) is authorized by you to access PharmaNet on your behalf;
                            and (iii) has been granted access to PharmaNet by the Province.
                          </li>
                          <li>
                            <strong>“Personal Information”</strong> means all recorded information that is about an identifiable
                            individual or is defined as, or deemed to be, “personal information” or “personal health information”
                            pursuant to any Privacy Laws.
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
                            <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record
                            or information in the custody, control or possession of you or an On-Behalf-of User that was obtained
                            through your or an On-Behalf-of User’s access to PharmaNet.
                          </li>
                          <li>
                            <strong>“Practice”</strong> means your practice of the health profession regulated under the <em>Health
                            Professions Act</em>, or your practice as an enrolled device provider under the
                            <em>Provider Regulation</em>, B.C. Reg.222/2014, as identified by you through PRIME.
                          </li>
                          <li>
                            <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for,
                            and manage, their access to PharmaNet, and through which users are granted access by the Province.
                          </li>
                          <li>
                            <strong>“Privacy Laws”</strong> means the Act, the
                            <em>Freedom of Information and Protection of Privacy Act</em>,
                            the <em>Personal Information Protection Act</em>, and any other statutory or legal obligations of privacy
                            owed by you or the Province, whether arising under statute, by contract or at common law.
                          </li>
                          <li>
                            <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                            Minister of Health.
                          </li>
                          <li>
                            <strong>“Professional College”</strong> is the regulatory body governing your Practice.
                          </li>
                          <li>

                            <p>
                              <strong>“Unauthorized Person”</strong> means any person other than:
                            </p>

                            <ol type=""i"">
                              <li>
                                you,
                              </li>
                              <li>
                                an On-Behalf-of User, or
                              </li>
                              <li>
                                a representative of an Approved SSO that is accessing PharmaNet for technical support purposes in
                                accordance with section 6 of the <em>Information Management Regulation</em>.
                              </li>
                            </ol>

                          </li>
                        </ul>

                      </li>
                      <li>
                        <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or regulation by
                        name means the statute or regulation of British Columbia of that name, as amended or replaced from time to time,
                        and includes any enactment made under the authority of that statute or regulation.
                      </li>
                      <li>

                        <p>
                          <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this Agreement:
                        </p>

                        <ol type=""i"">
                          <li>
                            a provision in the body of this Agreement will prevail over any conflicting provision in any further
                            limits or conditions communicated to you in writing by the Province, unless the conflicting provision
                            expressly states otherwise; and
                          </li>
                          <li>
                            a provision referred to in (i) above will prevail over any conflicting provision in the Conformance
                            Standards.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      APPLICATION OF LEGISLATION
                    </p>

                    <p>
                      You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act and all
                      Privacy Laws applicable to PharmaNet and PharmaNet Data.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
                    </p>

                    <p>
                      You acknowledge that:
                    </p>

                    <ol type=""a"">
                      <li>
                        PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
                        Province under the authority of the Act;
                      </li>
                      <li>
                        specific provisions of the Act, including the <em>Information Management Regulation</em> and sections 24, 25 and
                        29 of the Act, apply directly to you and to On-Behalf-of Users as a result; and
                      </li>
                      <li>
                        this Agreement documents limits and conditions, set by the minister in writing, that the Act requires you to
                        comply with.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCESS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
                        compliance with the limits and conditions set out in section 5(b) below and otherwise in this Agreement. The
                        Province may from time to time, at its discretion, amend or change the scope of your access privileges to
                        PharmaNet as privacy, security, business and clinical practice requirements change. In such circumstances, the
                        Province will use reasonable efforts to notify you of such changes.
                      </li>
                      <li>

                        <p>
                          <strong>Limits and Conditions of Access.</strong> The following limits and conditions apply to your access to
                          PharmaNet:
                        </p>

                        <ol type=""i"">
                          <li>
                            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, for so
                            long as you are a registrant in good standing with the Professional College and your registration permits
                            you to deliver Direct Patient Care requiring access to PharmaNet;
                          </li>
                          <li>
                            <p>you will only access PharmaNet:</p>

                            <ul>
                              <li>
                                at the Approved Practice Site, and using only the technologies and applications approved by the
                                Province; or
                              </li>
                              <li>
                                using a VPN or similar remote access technology, if you are physically located in British Columbia and
                                remotely connected to the Approved Practice Site using a VPN or other remote access technology
                                specifically approved by the Province in writing for the Approved Practice Site.
                              </li>
                            </ul>
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access
                            takes place at the Approved Practice Site and the access is in relation to patients for whom you will be
                            providing Direct Patient Care at the Approved Practice Site requiring the access to PharmaNet;
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of Users do not access PharmaNet using VPN or other remote access
                            technology
                          </li>
                          <li>
                            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
                            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
                          </li>
                          <li>
                            you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of market
                            research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet Data, for the
                            purpose of market research;
                          </li>
                          <li>
                            subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure that
                            On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of Direct Patient
                            Care, including for the purposes of quality improvement, evaluation, health care planning, surveillance,
                            research or other secondary uses;
                          </li>
                          <li>
                            you will not permit any Unauthorized Person to access PharmaNet, and you will take all reasonable measures
                            to ensure that no Unauthorized Person can access PharmaNet;
                          </li>
                          <li>
                            you will complete any training program(s) that your Approved SSO makes available to you in relation to
                            PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
                          </li>
                          <li>
                            you will immediately notify the Province when you or an On-Behalf-of User no longer require access to
                            PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave of absence
                            from your staff, or where the On-Behalf-of User’s access-related duties in relation to the Practice have
                            changed;
                          </li>
                          <li>
                            you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional limits or
                            conditions applicable to you, as may be communicated to you by the Province in writing;
                          </li>
                          <li>
                            you represent and warrant that all information provided by you in connection with your application for
                            PharmaNet access, including through PRIME, is true and correct.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this Agreement
                        for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
                        PharmaNet Data.
                      </li>
                      <li>

                        <p>
                          <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable measures to
                          safeguard Personal Information, including any Personal Information in the PharmaNet Data while it is in the
                          custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
                        </p>

                        <ol type=""i"">
                          <li>
                            take all reasonable steps to ensure the physical security of Personal Information, generally and as
                            required by Privacy Laws;
                          </li>
                          <li>
                            secure all workstations and printers in a protected area in the Practice to prevent viewing of PharmaNet
                            Data by Unauthorized Persons;
                          </li>
                          <li>
                            ensure separate access credential (such as user name and password) for each On-Behalf-of User, and prohibit
                            sharing or other multiple use of your access credential, or an On-Behalf-of User’s access credential, for
                            access to PharmaNet;
                          </li>
                          <li>
                            secure any workstations used to access PharmaNet and all devices, codes or passwords that enable access to
                            PharmaNet by yourself or any On-Behalf-of User;
                          </li>
                          <li>
                            take such other privacy and security measures as the Province may reasonably require from time-to-time.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Conformance Standards.</strong> You will comply with, and will ensure On-Behalf-of
                        Users comply with, the rules specified in the Conformance Standards when accessing and recording information in
                        PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLOSURE, STORAGE, AND ACCESS REQUESTS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper files or
                        any electronic system, unless such storage or retention is required for record keeping in accordance with
                        Professional College requirements and in connection with your provision of Direct Patient Care and otherwise is
                        in compliance with the Conformance Standards. You will not modify any records retained in accordance with this
                        section other than as may be expressly authorized in the Conformance Standards. For clarity, you may annotate a
                        discrete record provided that the discrete record is not itself modified other than as expressly authorized in
                        the Conformance Standards.
                      </li>
                      <li>
                        <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with section
                        6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the purpose of
                        monitoring your own Practice.
                      </li>
                      <li>
                        <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do not,
                        disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient Care or is
                        otherwise authorized under section 24(1) of the Act.
                      </li>
                      <li>
                        <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of Users do
                        not, disclose PharmaNet Data for the purpose of market research.
                      </li>
                      <li>
                        <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in accordance
                        with section 6(a) of this Agreement, you will not provide to patients any copies of records containing PharmaNet
                        Data or “print outs” produced directly from PharmaNet, and will refer any requests for access to such records or
                        “print outs” to the Province.
                      </li>
                      <li>
                        <strong>Responding to Requests to Correct a Record contained in PharmaNet.</strong> If you receive a request for
                        correction of any record or information contained in PharmaNet, you will refer the request to the Province.
                      </li>
                      <li>
                        <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the Province if
                        you receive any order, demand or request compelling, or threatening to compel, disclosure of records contained
                        in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For greater
                        certainty, the foregoing requires that you notify the Province only with respect to any access requests or
                        demands for records contained in PharmaNet, and not records retained by you in accordance with section 6(a) of
                        this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCURACY
                    </p>

                    <p>
                      You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of User
                      in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material inaccuracy or
                      error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it if
                      necessary, and notify the Province of the inaccuracy or error and any steps taken.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INVESTIGATIONS, AUDITS, AND REPORTING
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations conducted by
                        the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
                        Agreement, including providing access upon request to your facilities, data management systems, books, records
                        and personnel for the purposes of such audit or investigation.
                      </li>
                      <li>
                        <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province may
                        report any material breach of this Agreement to your Professional College or to the Information and Privacy
                        Commissioner of British Columbia.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE OF NON COMPLIANCE AND DUTY TO INVESTIGATE
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this Agreement,
                        and will take all reasonable steps to prevent recurrences of any such breaches, including taking any steps
                        necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of User’s
                        access rights.
                      </li>
                      <li>

                        <p>
                          <strong>Non Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
                        </p>

                        <ol type=""i"">
                          <li>
                            you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User will be unable
                            to comply with the terms of this Agreement in any respect, or
                          </li>
                          <li>
                            you have knowledge of any circumstances, incidents or events which have or may jeopardize the security,
                            confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person, to access
                            PharmaNet.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      TERM OF AGREEMENT, SUSPENSION & TERMINATION
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet by the
                        Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or (e)
                        below.
                      </li>
                      <li>
                        <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written notice to
                        the Province.
                      </li>
                      <li>
                        <strong>Suspension or Termination of PharmaNet access.</strong> If the Province suspends or terminates your
                        right, or an On-Behalf-of User’s right, to access PharmaNet under the
                        <em>Information Management Regulation</em>, the Province may also terminate this Agreement at any time
                        thereafter upon written notice to you.
                      </li>
                      <li>
                        <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate this
                        Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to you if
                        you or an On-Behalf-of User fail to comply with any provision of this Agreement.
                      </li>
                      <li>
                        <strong>Termination by operation of the Information Management Regulation.</strong> This Agreement will
                        terminate automatically if your access to PharmaNet ends by operation of section 18 of the
                        <em>Information Management Regulation</em>.
                      </li>
                      <li>
                        <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may suspend your
                        account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the Province’s
                        policies. Please contact the Province immediately if your account has been suspended for inactivity but you
                        still require access to PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and PharmaNet
                        Data is solely at your own risk. All such access and information is provided on an “as is” and “as available”
                        basis without warranty or condition of any kind. The Province does not warrant the accuracy, completeness or
                        reliability of the PharmaNet Data or the availability of PharmaNet, or that access to or the operation of
                        PharmaNet will function without error, failure or interruption.
                      </li>
                      <li>
                        <strong>You are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
                        you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or acting
                        upon such information. The clinical or other information disclosed to you or an On-Behalf-of User pursuant to
                        this Agreement is in no way intended to be a substitute for professional judgment.
                      </li>
                      <li>
                        <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the Province
                        for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or PharmaNet
                        Data.
                      </li>
                      <li>
                        <strong>You Must Indemnify the Province if You Cause a Loss or Claim.</strong> You agree to indemnify and save
                        harmless the Province, and the Province’s employees and agents (each an <strong>""Indemnified Person""</strong>)
                        from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified Person may
                        sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are based
                        upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
                        On-Behalf-of User, in connection with this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another method of
                          delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to be
                          effective, must be in writing and emailed or mailed to:
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
                        <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will be in
                        writing and delivered by the Province to you using any of the contact mechanisms identified by you in PRIME,
                        including by mail to a specified postal address, email to a specified email address or text message to the
                        specified cell phone number. You may be required to click a URL link or log into PRIME to receive the content of
                        any such notice.
                      </li>
                      <li>
                        <strong>Deemed receipt.</strong> Any written communication from a party, if personally delivered or sent
                        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent by
                        mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays) after
                        the date the notice was sent.
                      </li>
                      <li>
                        <strong>Substitute contact information.</strong> You may notify the Province of a substitute contact mechanism
                        by updating your contact information in PRIME.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      GENERAL
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and is
                          severable from any other covenant, and if any of them are held by a court, or other decision-maker, to be
                          invalid, this Agreement will be interpreted as if such provisions were not included.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Survival.</strong> Sections 3, 4, 5(b)(vi) (vii), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any other
                          provision of this Agreement that expressly or by its nature continues after termination, shall survive
                          termination of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and interpreted in
                          accordance with the laws of British Columbia and the laws of Canada applicable therein.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be assigned
                          without the prior written approval of the Province.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any provision of
                          this Agreement by you is not a waiver of its right subsequently to insist on performance of that or any other
                          provision of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Province may modify this Agreement.</strong> The Province may amend this Agreement, including this
                          section, at any time in its sole discretion:
                        </p>

                        <ol type=""i"">
                          <li>
                            by written notice to you, in which case the amendment will become effective upon the later of (A) the
                            date notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
                            by the Province, if any; or
                          </li>
                          <li>
                            by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                            specify the effective date of the amendment, which date will be at least 30 (thirty) days after the date
                            that the PharmaCare Newsletter containing the notice is first published.
                          </li>
                        </ol>

                        <p>
                          If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment described in
                          (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this Agreement will be
                          deemed to have been so amended as of the effective date. If you do not agree with any amendment for which
                          notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly (and in any
                          event before the effective date) cease all access or use of PharmaNet by yourself and all On-Behalf-of Users,
                          and take the steps necessary to terminate this Agreement in accordance with section 10.
                        </p>

                      </li>
                    </ol>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "RU", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "LicenseClassClauseMapping",
                columns: new[] { "LicenseCode", "OrganizatonTypeCode", "LicenseClassClauseId", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, 2, 1, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 1, 3, 1, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 1, 2, 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 1, 3, 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LimitsConditionsClause_EnrolleeId",
                table: "LimitsConditionsClause",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessTerm_GlobalClauseId",
                table: "AccessTerm",
                column: "GlobalClauseId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessTerm_UserClauseId",
                table: "AccessTerm",
                column: "UserClauseId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessTermLicenseClassClause_LicenseClassClauseId",
                table: "AccessTermLicenseClassClause",
                column: "LicenseClassClauseId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseClassClauseMapping_LicenseClassClauseId",
                table: "LicenseClassClauseMapping",
                column: "LicenseClassClauseId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseClassClauseMapping_OrganizatonTypeCode",
                table: "LicenseClassClauseMapping",
                column: "OrganizatonTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTerm_GlobalClause_GlobalClauseId",
                table: "AccessTerm",
                column: "GlobalClauseId",
                principalTable: "GlobalClause",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTerm_UserClause_UserClauseId",
                table: "AccessTerm",
                column: "UserClauseId",
                principalTable: "UserClause",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LimitsConditionsClause_Enrollee_EnrolleeId",
                table: "LimitsConditionsClause",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
