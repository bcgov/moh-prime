using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateEducationalMedicalStudentLicence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 1,
                column: "Text",
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

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 2,
                column: "Text",
                value: @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 3,
                column: "Text",
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 4,
                column: "Text",
                value: @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 5,
                column: "Text",
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 6,
                column: "Text",
                value: @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS: TEST OF INSERTION OF INDIVIDUAL L&C COPY</h1>

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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 7,
                column: "Text",
                value: @"<h1>
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 8,
                column: "Text",
                value: @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 9,
                column: "Text",
                value: @"<h1>PHARMANET COMMUNITY PHARMACIST TERMS OF ACCESS</h1>

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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 10,
                column: "Text",
                value: @"<p class=""bold"">
  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
</p>

<h1>PHARMANET USER TERMS OF ACCESS FOR PHARMACISTS</h1>

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
                </a>; and
              </li>
              <li>
                Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                Architecture for Wireless Local Area Network Connectivity”.
                <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet"" target=""_blank"" rel=""noopener noreferrer"">
                  https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet
                </a>
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
            you will only access PharmaNet: at the Approved Practice Site, and using only the technologies and
            applications approved by the Province;
          </li>
          <li>
            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access takes
            place at the Approved Practice Site and the access is required in relation to patients for whom you will be
            providing Direct Patient Care at the Approved Practice Site;
          </li>
          <li>
            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
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
        <strong>Responding to Requests to Correct a Record Contained in PharmaNet.</strong> If you receive a request for
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
      NOTICE OF NON-COMPLIANCE AND DUTY TO INVESTIGATE
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
          <strong>Non-Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
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
        <strong>Suspension or Termination of PharmaNet Access.</strong> If the Province suspends or terminates your
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
        <strong>Termination by Operation of the Information Management Regulation.</strong> This Agreement will
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
        <strong>You Are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
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
        <strong>You Must Indemnify the Province If You Cause a Loss or Claim.</strong> You agree to indemnify and save
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
        <strong>Deemed Receipt.</strong> Any written communication from a party, if personally delivered or sent
        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent by
        mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays) after
        the date the notice was sent.
      </li>
      <li>
        <strong>Substitute Contact Information.</strong> You may notify the Province of a substitute contact mechanism
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
          <strong>Province May Modify this Agreement.</strong> The Province may amend this Agreement, including this
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 11,
                column: "Text",
                value: @"<p class=""text-center"">
  This Agreement is made the {{day}} day of {{month}}, {{year}}
</p>

<h1>ORGANIZATION AGREEMENT FOR PHARMANET USE</h1>

<p>
  This Organization Agreement for PharmaNet Use (the &quot;Agreement&quot;) is executed by {{organizationName}}
  (&quot;Organization&quot;) for the benefit of HER MAJESTY THE QUEEN IN RIGHT OF THE PROVINCE OF BRITISH COLUMBIA, as
  represented by the Minister of Health (the &quot;Province&quot;).
</p>

<p>
  <strong>WHEREAS:</strong>
</p>

<ol type=""A"">
  <li>
    The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links
    pharmacies to a central data system. Every prescription dispensed in community pharmacies in British
    Columbia is entered into PharmaNet.
  </li>
  <li>
    PharmaNet contains highly sensitive confidential information, including personal information, and it is in
    the public interest to ensure that appropriate measures are in place to protect the confidentiality and
    integrity of such information. All access to and use of PharmaNet and PharmaNet Data is subject to the
    Act and other applicable law.
  </li>
  <li>
    The Province permits Authorized Users to access PharmaNet to provide health services to, or to facilitate
    the care of, the individual whose personal information is being accessed.
  </li>
  <li>
    This Agreement sets out the terms by which Organization may permit Authorized Users to access PharmaNet
    at the Site(s) operated by Organization.
  </li>
</ol>

<p>
  <strong>NOW THEREFORE</strong> Organization makes this Agreement knowing that the Province will rely on it
  in permitting access to and use of PharmaNet from Sites operated by Organization. Organization conclusively
  acknowledges that reliance by the Province on this Agreement is in every respect justifiable and that it
  received fair and valuable consideration for this Agreement, the receipt and adequacy of which is hereby
  acknowledged. Organization hereby agrees as follows:
</p>

<p class=""text-center"">
  <strong>ARTICLE 1 – INTERPRETATION</strong>
</p>

<ol type=""1""
    start=""1""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        <p>
          In this Agreement, unless the context otherwise requires, the following definitions will apply:
        </p>

        <ol type=""a"">
          <li>
            <strong>&quot;Act&quot;</strong> means the <em>Pharmaceutical Services Act</em>;
          </li>
          <li>
            <strong>&quot;Approved SSO&quot;</strong> means, in relation to a Site, the software support organization
            identified in section 1 of the Site Request that provides Organization with the SSO-Provided
            Technology used at the Site;
          </li>
          <li>
            <strong>&quot;Associated Technology&quot;</strong> means, in relation to a Site, any information technology
            hardware, software or services used at the Site, other than the SSO-Provided Technology, that is
            in any way used in connection with Site Access or any PharmaNet Data;
          </li>
          <li>
            <p>
              <strong>&quot;Authorized User&quot;</strong> means an individual who is granted access to PharmaNet by the
              Province and who is:
            </p>

            <ol type=""i"">
              <li>
                an employee or independent contractor of Organization, or
              </li>
              <li>
                if Organization is an individual, the Organization;
              </li>
            </ol>
          </li>
          <li>
            <strong>&quot;Information Management Regulation&quot;</strong> means the
            <em>Information Management Regulation</em>,
            B.C. Reg. 74/2015;
          </li>
          <li>
            <strong>&quot;On-Behalf-Of User&quot;</strong> means an Authorized User described in subsection 4 (5) of the
            <em>Information Management Regulation</em> who acts on behalf of a Regulated User when accessing
            PharmaNet;
          </li>
          <li>
            &quot;PharmaNet&quot; means PharmaNet as continued under section 2 of the
            <em>Information Management Regulation</em>;
          </li>
          <li>
            <strong>&quot;PharmaNet Data&quot;</strong> includes any records or information contained in PharmaNet and
            any records
            or information in the custody, control or possession of Organization or any Authorized User as the result of
            any Site Access;
          </li>
          <li>
            <strong>&quot;Regulated User&quot;</strong> means an Authorized User described in subsections 4 (2) to (4)
            of the
            <em>Information Management Regulation</em>;
          </li>
          <li>
            <strong>&quot;Signing Authority&quot;</strong> means the individual identified by Organization as the
            &quot;Signing Authority&quot;
            for a Site, with the associated contact information, as set out in section 2 of the Site Request;
          </li>
          <li>
            <p>
              &quot;Site&quot; means a premises operated by Organization and located in British Columbia that:
            </p>

            <ol type=""i"">
              <li>
                is the subject of a Site Request submitted to the Province, and
              </li>
              <li>
                has been approved for Site Access by the Province in writing
              </li>
            </ol>

            <p class=""underline"">
              For greater certainty, &quot;Site&quot; does not include a location from which remote access to PharmaNet
              takes place;
            </p>
          </li>
          <li>
            <strong>&quot;Site Access&quot;</strong> means any access to or use of PharmaNet at a Site or remotely as
            permitted
            by the Province;
          </li>
          <li>
            <strong>&quot;Site Request&quot;</strong> means, in relation to a Site, the information contained in the
            PharmaNet access
            request form submitted to the Province by the Organization, requesting PharmaNet access at the Site, as such
            information is updated by the Organization from time to time in accordance with section 2.2;
          </li>
          <li>
            <strong>&quot;SSO-Provided Technology&quot;</strong> means any information technology hardware, software or
            services
            provided to Organization by an Approved SSO for the purpose of Site Access;
          </li>
        </ol>
      </li>
      <li>
        Unless otherwise specified, a reference to a statute or regulation by name means a statute or regulation of
        British
        Columbia of that name, as amended or replaced from time to time, and includes any enactments made under the
        authority
        of that statute or regulation.
      </li>
      <li>
        <p>
          The following are the Schedules attached to and incorporated into this Agreement:
        </p>

        <ul>
          <li>
            Schedule A – Specific Privacy and Security Measures
          </li>
        </ul>
      </li>
      <li>
        The main body of this Agreement, the Schedules, and any documents incorporated by reference into this Agreement
        are to
        be interpreted so that all of the provisions are given as full effect as possible. In the event of a conflict,
        unless
        expressly stated to the contrary the main body of the Agreement will prevail over the Schedules, which will
        prevail
        over any document incorporated by reference.
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 2 – REPRESENTATIONS AND WARRANTIES</strong>
</p>

<ol type=""1""
    start=""2""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        <p>
          Organization represents and warrants to the Province, as of the date of this
          Agreement and throughout its term, that:
        </p>

        <ol type=""a"">
          <li>
            the information contained in the Site Request for each Site is true and correct;
          </li>
          <li>
            <p>
              if Organization is not an individual:
            </p>

            <ol type=""i"">
              <li>
                Organization has the power and capacity to enter into this Agreement and to comply with its terms;
              </li>
              <li>
                all necessary corporate or other proceedings have been taken to authorize the execution and delivery
                of this Agreement by, or on behalf of, Organization; and
              </li>
              <li>
                this Agreement has been legally and properly executed by, or on behalf of, the Organization and is
                legally binding upon and enforceable against Organization in accordance with its terms.
              </li>
            </ol>
          </li>
        </ol>
      </li>
      <li>
        Organization must immediately notify the Province of any change to the information contained in a Site Request,
        including any change to a Site’s status, location, normal operating hours, Approved SSO, or the name and contact
        information of the Signing Authority or any of the other specific roles set out in the Site Request. Such
        notices
        must be submitted to the Province in the form and manner directed by the Province in its published instructions
        regarding the submission of updated Site Request information, as such instructions may be updated from time to
        time by the Province.
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 3 – SITE ACCESS REQUIREMENTS</strong>
</p>

<ol type=""1""
    start=""3""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        Organization must comply with the Act and all applicable law.
      </li>
      <li>
        Organization must submit a Site Request to the Province for each physical location where it intends to provide
        Site
        Access, and must only provide Site Access from Sites approved in writing by the Province. For greater certainty,
        a
        Site Request is not required for each physical location from which remote access, as permitted under section
        3.6,
        may occur, but Organization must provide, with the Site Request, a list of the locations from which remote
        access
        may occur, and ensure this list remains current for the term of this agreement.
      </li>
      <li>
        Organization must only provide Site Access using SSO-Provided Technology. For the purposes of remote access,
        Organization must ensure that technology used meets the requirements of Schedule A.
      </li>
      <li>
        Unless otherwise authorized by the Province in writing, Organization must at all times use the secure network or
        security technology that the Province certifies or makes available to Organization for the purpose of Site
        Access.
        The use of any such network or technology by Organization may be subject to terms and conditions of use,
        including
        acceptable use policies, established by the Province and communicated to Organization from time to time in
        writing.
      </li>
      <li>
        <p>
          Organization must only make Site Access available to the following individuals:
        </p>

        <ol type=""a"">
          <li>
            Authorized Users when they are physically located at a Site, and, in the case of an On-Behalf-of-User
            accessing
            personal information of a patient on behalf of a Regulated User, only if the Regulated User will be
            delivering
            care to that patient at the same Site at which the access to personal information occurs;
          </li>
          <li>
            Representatives of an Approved SSO for technical support purposes, in accordance with section 6 of the
            <em>Information Management Regulation</em>.
          </li>
        </ol>
      </li>
      <li>
        Despite section 3.5(a), Organization may make Site Access available to Regulated Users who are physically
        located in
        British Columbia and remotely connected to a Site using a VPN or other remote access technology specifically
        approved
        by the Province in writing for the Site.
      </li>
      <li>
        <p>
          Organization must ensure that Authorized Users with Site Access:
        </p>

        <ol type=""a"">
          <li>
            only access PharmaNet to the extent necessary to provide health services to, or facilitate the care of, the
            individual whose personal information is being accessed;
          </li>
          <li>
            first complete any mandatory training program(s) that the Site’s Approved SSO or the Province makes
            available
            in relation to PharmaNet;
          </li>
          <li>
            access PharmaNet using their own separate login identifications and credentials, and do not share or have
            multiple use of any such login identifications and credentials;
          </li>
          <li>
            secure all devices, codes and credentials that enable access to PharmaNet against unauthorized use; and
          </li>
          <li>
            in the case of remote access, comply with the policies of the Province relating to remote access to
            PharmaNet.
          </li>
        </ol>
      </li>
      <li>
        If notified by the Province that an Authorized User’s access to PharmaNet has been suspended or revoked,
        Organization
        will immediately take any local measures necessary to remove the Authorized User’s Site Access. Organization
        will
        only restore Site Access to a previously suspended or revoked Authorized User upon the Province’s specific
        written
        direction.
      </li>
      <li>
        <p>
          For the purposes of this section:
        </p>

        <ol type=""a"">
          <li>
            <strong>&quot;Responsible Authorized User&quot;</strong> means, in relation to any PharmaNet Data, the
            Regulated User by whom,
            or on whose behalf, that data was obtained from PharmaNet; and
          </li>
          <li>
            <strong>&quot;Use&quot;</strong> includes to collect, access, retain, use, de-identify, and disclose.
          </li>
        </ol>

        <p>
          The PharmaNet Data disclosed under this Agreement is disclosed by the Province solely for the Use of the
          Responsible
          User to whom it is disclosed.
        </p>

        <p>
          Organization must not Use any PharmaNet Data, or permit any third party to Use PharmaNet Data, unless the
          Responsible
          User has authorized such Use and it is otherwise permitted under the Act, applicable law, and the limits and
          conditions imposed by the Province on the Responsible User.
        </p>
      </li>
      <li>
        <p>
          Organization must make all reasonable arrangements to protect PharmaNet Data against such risks as
          unauthorized access,
          collection, use, modification, retention, disclosure or disposal, including by:
        </p>

        <ol type=""a"">
          <li>
            taking all reasonable physical, technical and operational measures necessary to ensure Site Access operates
            in
            accordance with sections 3.1 to 3.9 above, and
          </li>
          <li>
            complying with the requirements of Schedule A.
          </li>
        </ol>
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 4 – NON-COMPLIANCE AND INVESTIGATIONS</strong>
</p>

<ol type=""1""
    start=""4""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        Organization must promptly notify the Province, and provide particulars, if Organization does not comply, or
        anticipates
        that it will be unable to comply, with the terms of this Agreement, or if Organization has knowledge of any
        circumstances,
        incidents or events which have or may jeopardize the security, confidentiality or integrity of PharmaNet,
        including any
        attempt by any person to gain unauthorized access to PharmaNet or the networks or equipment used to connect to
        PharmaNet
        or convey PharmaNet Data.
      </li>
      <li>
        Organization must immediately investigate any suspected breaches of this Agreement and take all reasonable steps
        to prevent
        recurrences of any such breaches.
      </li>
      <li>
        Organization must cooperate with any audits or investigations conducted by the Province (including any
        independent auditor
        appointed by the Province) regarding compliance with this Agreement, including by providing access upon request
        to a Site
        and any associated facilities, networks, equipment, systems, books, records and personnel for the purposes of
        such audit
        or investigation.
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 5 – SITE TERMINATION</strong>
</p>

<ol type=""1""
    start=""5""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        <p>
          The Province may terminate all Site Access at a Site immediately, upon notice to the Signing Authority for the
          Site, if:
        </p>

        <ol type=""a"">
          <li>
            the Approved SSO for the Site is no longer approved by the Province to provide information technology
            hardware, software,
            or service in connection with PharmaNet, or
          </li>
          <li>
            the Province determines that the SSO-Provided Technology or Associated Technology in use at the Site, or any
            component
            thereof, is obsolete, unsupported, or otherwise poses an unacceptable security risk to PharmaNet,
          </li>
        </ol>

        <p>
          and the Organization is unable or unwilling to remedy the problem within a timeframe acceptable to the
          Province.
        </p>
      </li>
      <li>
        As a security precaution, the Province may suspend Site Access at a Site after a period of inactivity. If Site
        Access at a
        Site remains inactive for a period of 90 days or more, the Province may, immediately upon notice to the Signing
        Authority
        for the Site, terminate all further Site Access at the Site.
      </li>
      <li>
        Organization must prevent all further Site Access at a Site immediately upon the Province’s termination, in
        accordance with
        this Article 5, of Site Access at the Site.
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 6 – TERM AND TERMINATION</strong>
</p>

<ol type=""1""
    start=""6""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        The term of this Agreement begins on the date first noted above and continues until it is terminated
        in accordance with this Article 6.
      </li>
      <li>
        Organization may terminate this Agreement at any time on notice to the Province.
      </li>
      <li>
        The Province may terminate this Agreement immediately upon notice to Organization if Organization fails to
        comply with any
        provision of this Agreement.
      </li>
      <li>
        The Province may terminate this Agreement immediately upon notice to Organization in the event Organization no
        longer operates
        any Sites where Site Access is permitted.
      </li>
      <li>
        The Province may terminate this Agreement for any reason upon two (2) months advance notice to Organization.
      </li>
      <li>
        Organization must prevent any further Site Access immediately upon termination of this Agreement.
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 7 – DISCLAIMER AND INDEMNITY</strong>
</p>

<ol type=""1""
    start=""7""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        The PharmaNet access and PharmaNet Data provided under this Agreement are provided &quot;as is&quot; without
        warranty of any kind,
        whether express or implied. All implied warranties, including, without limitation, implied warranties of
        merchantability,
        fitness for a particular purpose, and non-infringement, are hereby expressly disclaimed. The Province does not
        warrant
        the accuracy, completeness or reliability of the PharmaNet Data or the availability of PharmaNet, or that access
        to or
        the operation of PharmaNet will function without error, failure or interruption.
      </li>
      <li>
        Under no circumstances will the Province be liable to any person or business entity for any direct, indirect,
        special,
        incidental, consequential, or other damages based on any use of PharmaNet or the PharmaNet Data, including
        without
        limitation any lost profits, business interruption, or loss of programs or information, even if the Province has
        been specifically advised of the possibility of such damages.
      </li>

      <li>
        Organization must indemnify and save harmless the Province, and the Province’s employees and agents (each an
        <strong>""Indemnified Person""</strong>) from any losses, claims, damages, actions, causes of action, costs and
        expenses that an Indemnified Person may sustain, incur, suffer or be put to at any time, either before or after
        this Agreement ends, which are based upon, arise out of or occur directly or indirectly by reason of any act
        or omission by Organization, or by any Authorized User at the Site, in connection with this Agreement.
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 8 – GENERAL</strong>
</p>

<ol type=""1""
    start=""8""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        <p>
          <strong class=""underline"">Notice.</strong> Except where this Agreement expressly provides for another method
          of delivery, any notice to be given to the Province must be in writing and emailed or mailed to:
        </p>

        <address>
          Director, Information and PharmaNet Innovation<br>
          Ministry of Health<br>
          PO Box 9652, STN PROV GOVT<br>
          Victoria, BC V8W 9P4<br>

          <br>

          <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
        </address>

        <p>
          Any notice to be given to a Signing Authority or the Organization will be in writing and emailed, mailed,
          faxed
          or text messaged to the Signing Authority (in the case of notice to a Signing Authority) or all Signing
          Authorities (in the case of notice to the Organization). A Signing Authority may be required to click a
          URL link or to log in to the Province’s &quot;PRIME&quot; system to receive the content of any such notice.
        </p>

        <p>
          Any written notice from a party, if sent electronically, will be deemed to have been received 24 hours after
          the
          time the notice was sent, or, if sent by mail, will be deemed to have been received 3 days (excluding
          Saturdays,
          Sundays and statutory holidays) after the date the notice was sent.
        </p>
      </li>
      <li>
        <strong class=""underline"">Waiver.</strong> The failure of the Province at any time to insist on performance of
        any
        provision of this Agreement by Organization is not a waiver of its right subsequently to insist on performance
        of
        that or any other provision of this Agreement. A waiver of any provision or breach of this Agreement is
        effective
        only if it is writing and signed by, or on behalf of, the waiving party.
      </li>
      <li>
        <p>
          <strong class=""underline"">Modification.</strong> No modification to this Agreement is effective unless it is
          in writing and signed
          by, or on behalf of, the parties.
        </p>

        <p>
          Notwithstanding the foregoing, the Province may amend this Agreement, including the Schedules and this
          section,
          at any time in its sole discretion, by written notice to Organization, in which case the amendment will become
          effective upon the later of: (i) the date notice of the amendment is delivered to Organization; and (ii) the
          effective date of the amendment specified by the Province. The Province will make reasonable efforts to
          provide
          at least thirty (30) days advance notice of any such amendment, subject to any determination by the Province
          that a shorter notice period is necessary due to changes in the Act, applicable law or applicable policies of
          the Province, or is necessary to maintain privacy and security in relation to PharmaNet or PharmaNet Data.
        </p>

        <p>
          If Organization does not agree with any amendment for which notice has been provided by the Province in
          accordance with this section, Organization must promptly (and in any event prior to the effective date)
          cease Site Access at all Sites and take the steps necessary to terminate this Agreement in accordance
          with Article 6.
        </p>
      </li>
      <li>
        <p>
          <strong class=""underline"">Governing Law.</strong> This Agreement will be governed by and will be construed
          and interpreted in accordance with the laws of British Columbia and the laws of Canada applicable therein.
        </p>
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>SCHEDULE A – SPECIFIC PRIVACY AND SECURITY MEASURES</strong>
</p>

<p>
  Organization must, in relation to each Site and in relation to Remote Access:
</p>

<ol type=""a"">
  <li>
    secure all workstations and printers at the Site to prevent any viewing of PharmaNet Data by persons other
    than Authorized Users;
  </li>
  <li>
    <p>
      implement all privacy and security measures specified in the following documents published by the Province, as
      amended from time to time:
    </p>

    <ol type=""i"">
      <li>
        <p>
          the PharmaNet Professional and Software Conformance Standards
        </p>

        <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards""
           target=""_blank""
           rel=""noopener noreferrer"">
          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards
        </a>
      </li>
      <li>
        <p>
          Office of the Chief Information Officer: &quot;Submission for Technical Security Standard and High Level
          Architecture for Wireless Local Area Network Connectivity&quot;
        </p>

        <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet""
           target=""_blank""
           rel=""noopener noreferrer"">
          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet
        </a>
      </li>
      <li>
        <p>
          Policy for Secure Remote Access to PharmaNet
        </p>

        <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards""
           target=""_blank""
           rel=""noopener noreferrer"">
          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards
        </a>
      </li>
    </ol>
  </li>
  <li>
    ensure that a qualified technical support person is engaged to provide security support for the Site. This person
    should be familiar with the Site’s network configurations, hardware and software, including all SSO-Provided
    Technology
    and Associated Technology, and should be capable of understanding and adhering to the standards set forth in this
    Agreement and Schedule. Note that any such qualified technical support person must not be permitted by Organization
    to access or use PharmaNet in any manner, unless otherwise permitted under this Agreement;
  </li>
  <li>
    establish and maintain documented privacy policies that detail how Organization will meet its privacy obligations
    in relation to the Site;
  </li>
  <li>
    establish breach reporting and response processes in relation to the Site;
  </li>
  <li>
    detail expectations for privacy protection in contracts and service agreements as applicable at the Site;
  </li>
  <li>
    regularly review the administrative, physical and technological safeguards at the Site;
  </li>
  <li>
    establish and maintain a program for monitoring PharmaNet use at the Site, including by making appropriate
    monitoring
    and reporting mechanisms available to Authorized Users for this purpose.
  </li>
</ol>
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 12,
                column: "Text",
                value: @"<p class=""text-center"">
  This Agreement is made the {{day}} day of {{month}}, {{year}}
</p>

<h1>---- PLACEHOLDER TEXT ----</h1>
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 13,
                column: "Text",
                value: @"<p class=""bold"">
  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
</p>

<h1>PHARMANET USER TERMS OF ACCESS FOR PHARMACISTS</h1>

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
                </a>; and
              </li>
              <li>
                Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                Architecture for Wireless Local Area Network Connectivity”.
                <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet"" target=""_blank"" rel=""noopener noreferrer"">
                  https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet
                </a>
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
        PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
        Province under the authority of the Act;
      </li>
      <li>
        specific provisions of the Act (including but not limited to sections 24, 25 and 29) and the Information
        Management Regulation apply directly to you and to On-Behalf-of Users as a result; and
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
            you will only access PharmaNet: at the Approved Practice Site, and using only the technologies and
            applications approved by the Province;
          </li>
          <li>
            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access takes
            place at the Approved Practice Site and the access is required in relation to patients for whom you will be
            providing Direct Patient Care at the Approved Practice Site;
          </li>
          <li>
            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
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
        <strong>Responding to Requests to Correct a Record Contained in PharmaNet.</strong> If you receive a request for
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
      NOTICE OF NON-COMPLIANCE AND DUTY TO INVESTIGATE
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
          <strong>Non-Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
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
        <strong>Suspension or Termination of PharmaNet Access.</strong> If the Province suspends or terminates your
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
        <strong>Termination by Operation of the Information Management Regulation.</strong> This Agreement will
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
        <strong>You Are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
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
        <strong>You Must Indemnify the Province If You Cause a Loss or Claim.</strong> You agree to indemnify and save
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
        <strong>Deemed Receipt.</strong> Any written communication from a party, if personally delivered or sent
        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent by
        mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays) after
        the date the notice was sent.
      </li>
      <li>
        <strong>Substitute Contact Information.</strong> You may notify the Province of a substitute contact mechanism
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
          <strong>Province May Modify this Agreement.</strong> The Province may amend this Agreement, including this
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 14,
                column: "Text",
                value: @"<h1>
  PHARMANET TERMS OF ACCESS FOR PHARMACY OR DEVICE PROVIDER ON-BEHALF-OF USER
</h1>

<p class=""bold"">
  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
</p>

<p class=""bold underline"">
  On-Behalf-of User Access
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
        you are directly supervised by a Practitioner, who has been granted access to PharmaNet by the Province; and
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

        <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">http://www.gov.bc.ca/pharmacarenewsletter</a>
      </li>
      <li>
        <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the <em>Information Management
        Regulation, B.C</em>. Reg. 74/2015.
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
        or an
        enrolled device provider under the <em>Provider Regulation</em>, B.C. Reg. 222/2014,who supervises your access
        to and use of PharmaNet and who has been granted access to PharmaNet by the Province.
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
        disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and directed
        by a Practitioner;
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
        attempt to access PharmaNet from any location other than the Approved Practice Site of a Practitioner, including
        by VPN or other remote access technology;
      </li>
      <li>
        access PharmaNet unless the access is for the purpose of supporting a Practitioner in providing Direct Patient
        Care to a patient at the same Approved Practice Site at which your access occurs;
      </li>
      <li>
        use PharmaNet to submit claims to PharmaCare or a third-party insurer unless directed to do so by a Practitioner
        at an Approved Practice Site that is enrolled as a provider or device provider under the
        <em>Provider Regulation</em>, B.C. Reg. 222/2014.
      </li>
    </ol>
  </li>
</ol>
<ol start=""6"">
  <li>
    Your access to PharmaNet and use of PharmaNet Data are governed by the Pharmaceutical Services Act and you must
    comply with all your duties under that Act.
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
      Director, Information and PharmaNet Innovation<br>
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
      If you do not agree with any amendment for which notice has been provided by the Province in accordance with (i)
      or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
      PharmaNet.
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 15,
                column: "Text",
                value: @"<h1>
  PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER<br>
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 16,
                column: "Text",
                value: @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

<p class=""bold"">
  By enrolling for PharmaNet access, you agree to the following terms (the &quot;Agreement&quot;). Please read them carefully.
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

        <ul>
          <li>
            <strong>&quot;Act&quot;</strong> means the <em>Pharmaceutical Services Act</em>.
          </li>
          <li>
            <strong>&quot;Approved Practice Site&quot;</strong> means the physical site at which you provide Direct
            Patient Care and which is approved by the Province for PharmaNet access. For greater certainty,
            &quot;Approved Practice Site&quot; does not include a location from which remote access to PharmaNet takes
            place;
          </li>
          <li>
            <strong>&quot;Approved SSO&quot;</strong> means a software support organization approved by the Province
            that provides you with the information technology software and/or services through which you and
            On-Behalf-of Users access PharmaNet.
          </li>
          <li>

            <p>
              <strong>&quot;Conformance Standards&quot;</strong> means the following documents published by the
              Province, as amended from time to time:
            </p>

            <ol type=""i"">
              <li>
                PharmaNet Professional and Software Conformance Standards; and
              </li>
              <li>
                Office of the Chief Information Officer: &quot;Submission for Technical Security Standard and High Level
                Architecture for Wireless Local Area Network Connectivity&quot;.
              </li>
            </ol>

          </li>
          <li>
            <strong>&quot;Direct Patient Care&quot;</strong> means, for the purposes of this Agreement, the provision of
            health services to an individual to whom you provide direct patient care in the context of your Practice.
          </li>
          <li>
            <strong>&quot;Information Management Regulation&quot;</strong> means the Information Management Regulation,
            B.C. Reg. 74/2015.
          </li>
          <li>
            <strong>&quot;On-Behalf-of User&quot;</strong> means a member of your staff who (i) requires access to
            PharmaNet to carry out duties in relation to your Practice; (ii) is authorized by you to access PharmaNet
            on your behalf; and (iii) has been granted access to PharmaNet by the Province.
          </li>
          <li>
            <strong>&quot;Personal Information&quot;</strong> means all recorded information that is about an
            identifiable individual or is defined as, or deemed to be, &quot;personal information&quot; or
            &quot;personal health information&quot; pursuant to any Privacy Laws.
          </li>
          <li>
            <strong>&quot;PharmaCare Newsletter&quot;</strong> means the PharmaCare newsletter published by the Province
            on the following website (or such other website as may be specified by the Province from time to time for
            this purpose):

            <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">
              www.gov.bc.ca/pharmacarenewsletter
            </a>
          </li>
          <li>
            <strong>&quot;PharmaNet&quot;</strong> means PharmaNet as continued under section 2 of the Information
            Management Regulation.
          </li>
          <li>
            <strong>&quot;PharmaNet Data&quot;</strong> includes any record or information contained in PharmaNet and
            any record or information in the custody, control or possession of you or an On-Behalf-of User that was
            obtained through your or an On-Behalf-of User’s access to PharmaNet.
          </li>
          <li>
            <strong>&quot;Practice&quot;</strong> means your practice of the health profession regulated under the
            <em>Health Professions Act</em>, or your practice as an enrolled device provider under the Provider
            Regulation, B.C. Reg. 222/2014, as identified by you through PRIME.
          </li>
          <li>
            <strong>&quot;PRIME&quot;</strong> means the online service provided by the Province that allows users to
            apply for, and manage, their access to PharmaNet, and through which users are granted access by the
            Province.
          </li>
          <li>
            <strong>&quot;Privacy Laws&quot;</strong> means the Act, the <em>Freedom of Information and Protection of
            Privacy Act</em>, the <em>Personal Information Protection Act</em>, and any other statutory or legal
            obligations of privacy owed by you or the Province, whether arising under statute, by contract or at common
            law.
          </li>
          <li>
            <strong>&quot;Province&quot;</strong> means Her Majesty the Queen in Right of British Columbia, as
            represented by the Minister of Health.
          </li>
          <li>
            <strong>&quot;Professional College&quot;</strong> is the regulatory body governing your Practice.
          </li>
          <li>

            <p>
              <strong>&quot;Unauthorized Person&quot;</strong> means any person other than:
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
            unless (iii) below applies, you will only access PharmaNet at the Approved Practice Site, and using only the
            technologies and applications approved by the Province.
          </li>
          <li>
            <p>
              you may only access PharmaNet using remote access technology if all of the following conditions are met:
            </p>

            <ol>
              <li>
                the remote access technology used at the Approved Practice Site has been specifically approved in
                writing by the Province,
              </li>
              <li>
                the requirements of the Province’s Policy for Remote Access to PharmaNet
                (<a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards"">https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards</a>) are met,
              </li>
              <li>
                your Approved Practice Site has registered you with the Province for remote access at the Approved
                Practice Site,
              </li>
              <li>
                you have applied to the Province for remote access at the Approved Practice Site and the Province has
                approved that application in writing, and
              </li>
              <li>
                you are physically located in British Columbia at the time of any such remote access.
              </li>
            </ol>
          </li>
          <li>
            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access takes
            place at the Approved Practice Site and the access is in relation to patients for whom you will be providing
            Direct Patient Care at the Approved Practice Site requiring the access to PharmaNet;
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
            Care, including for the purposes of deidentification or aggregation, quality improvement, evaluation, health
            care planning, surveillance, research or other secondary uses;
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
        any electronic system, unless such storage or retention is required for record keeping in accordance with
        Professional College requirements and in connection with your provision of Direct Patient Care and otherwise
        is in compliance with the Conformance Standards. You will not modify any records retained in accordance with
        this section other than as may be expressly authorized in the Conformance Standards. For clarity, you may
        annotate a discrete record provided that the discrete record is not itself modified other than as expressly
        authorized in the Conformance Standards.
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
        Data or &quot;print outs&quot; produced directly from PharmaNet, and will refer any requests for access to such
        records or &quot;print outs&quot; to the Province.
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
        right to access PharmaNet under the <em>Information Management Regulation</em>, this Agreement will
        automatically terminate as of the date of such suspension or termination.
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
        Data is solely at your own risk. All such access and information is provided on an &quot;as is&quot; and
        &quot;as available&quot; basis without warranty or condition of any kind. The Province does not warrant the
        accuracy, completeness or reliability of the PharmaNet Data or the availability of PharmaNet, or that access
        to or the operation of PharmaNet will function without error, failure or interruption.
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
        harmless the Province, and the Province’s employees and agents  (each an <strong>""Indemnified Person""</strong>)
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
");

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 11,
                column: "Validate",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 1,
                column: "Text",
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

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 2,
                column: "Text",
                value: @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 3,
                column: "Text",
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 4,
                column: "Text",
                value: @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 5,
                column: "Text",
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 6,
                column: "Text",
                value: @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS: TEST OF INSERTION OF INDIVIDUAL L&C COPY</h1>

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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 7,
                column: "Text",
                value: @"<h1>
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 8,
                column: "Text",
                value: @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 9,
                column: "Text",
                value: @"<h1>PHARMANET COMMUNITY PHARMACIST TERMS OF ACCESS</h1>

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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 10,
                column: "Text",
                value: @"<p class=""bold"">
  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
</p>

<h1>PHARMANET USER TERMS OF ACCESS FOR PHARMACISTS</h1>

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
                </a>; and
              </li>
              <li>
                Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                Architecture for Wireless Local Area Network Connectivity”.
                <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet"" target=""_blank"" rel=""noopener noreferrer"">
                  https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet
                </a>
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
            you will only access PharmaNet: at the Approved Practice Site, and using only the technologies and
            applications approved by the Province;
          </li>
          <li>
            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access takes
            place at the Approved Practice Site and the access is required in relation to patients for whom you will be
            providing Direct Patient Care at the Approved Practice Site;
          </li>
          <li>
            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
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
        <strong>Responding to Requests to Correct a Record Contained in PharmaNet.</strong> If you receive a request for
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
      NOTICE OF NON-COMPLIANCE AND DUTY TO INVESTIGATE
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
          <strong>Non-Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
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
        <strong>Suspension or Termination of PharmaNet Access.</strong> If the Province suspends or terminates your
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
        <strong>Termination by Operation of the Information Management Regulation.</strong> This Agreement will
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
        <strong>You Are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
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
        <strong>You Must Indemnify the Province If You Cause a Loss or Claim.</strong> You agree to indemnify and save
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
        <strong>Deemed Receipt.</strong> Any written communication from a party, if personally delivered or sent
        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent by
        mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays) after
        the date the notice was sent.
      </li>
      <li>
        <strong>Substitute Contact Information.</strong> You may notify the Province of a substitute contact mechanism
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
          <strong>Province May Modify this Agreement.</strong> The Province may amend this Agreement, including this
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 11,
                column: "Text",
                value: @"<p class=""text-center"">
  This Agreement is made the {{day}} day of {{month}}, {{year}}
</p>

<h1>ORGANIZATION AGREEMENT FOR PHARMANET USE</h1>

<p>
  This Organization Agreement for PharmaNet Use (the &quot;Agreement&quot;) is executed by {{organizationName}}
  (&quot;Organization&quot;) for the benefit of HER MAJESTY THE QUEEN IN RIGHT OF THE PROVINCE OF BRITISH COLUMBIA, as
  represented by the Minister of Health (the &quot;Province&quot;).
</p>

<p>
  <strong>WHEREAS:</strong>
</p>

<ol type=""A"">
  <li>
    The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links
    pharmacies to a central data system. Every prescription dispensed in community pharmacies in British
    Columbia is entered into PharmaNet.
  </li>
  <li>
    PharmaNet contains highly sensitive confidential information, including personal information, and it is in
    the public interest to ensure that appropriate measures are in place to protect the confidentiality and
    integrity of such information. All access to and use of PharmaNet and PharmaNet Data is subject to the
    Act and other applicable law.
  </li>
  <li>
    The Province permits Authorized Users to access PharmaNet to provide health services to, or to facilitate
    the care of, the individual whose personal information is being accessed.
  </li>
  <li>
    This Agreement sets out the terms by which Organization may permit Authorized Users to access PharmaNet
    at the Site(s) operated by Organization.
  </li>
</ol>

<p>
  <strong>NOW THEREFORE</strong> Organization makes this Agreement knowing that the Province will rely on it
  in permitting access to and use of PharmaNet from Sites operated by Organization. Organization conclusively
  acknowledges that reliance by the Province on this Agreement is in every respect justifiable and that it
  received fair and valuable consideration for this Agreement, the receipt and adequacy of which is hereby
  acknowledged. Organization hereby agrees as follows:
</p>

<p class=""text-center"">
  <strong>ARTICLE 1 – INTERPRETATION</strong>
</p>

<ol type=""1""
    start=""1""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        <p>
          In this Agreement, unless the context otherwise requires, the following definitions will apply:
        </p>

        <ol type=""a"">
          <li>
            <strong>&quot;Act&quot;</strong> means the <em>Pharmaceutical Services Act</em>;
          </li>
          <li>
            <strong>&quot;Approved SSO&quot;</strong> means, in relation to a Site, the software support organization
            identified in section 1 of the Site Request that provides Organization with the SSO-Provided
            Technology used at the Site;
          </li>
          <li>
            <strong>&quot;Associated Technology&quot;</strong> means, in relation to a Site, any information technology
            hardware, software or services used at the Site, other than the SSO-Provided Technology, that is
            in any way used in connection with Site Access or any PharmaNet Data;
          </li>
          <li>
            <p>
              <strong>&quot;Authorized User&quot;</strong> means an individual who is granted access to PharmaNet by the
              Province and who is:
            </p>

            <ol type=""i"">
              <li>
                an employee or independent contractor of Organization, or
              </li>
              <li>
                if Organization is an individual, the Organization;
              </li>
            </ol>
          </li>
          <li>
            <strong>&quot;Information Management Regulation&quot;</strong> means the
            <em>Information Management Regulation</em>,
            B.C. Reg. 74/2015;
          </li>
          <li>
            <strong>&quot;On-Behalf-Of User&quot;</strong> means an Authorized User described in subsection 4 (5) of the
            <em>Information Management Regulation</em> who acts on behalf of a Regulated User when accessing
            PharmaNet;
          </li>
          <li>
            &quot;PharmaNet&quot; means PharmaNet as continued under section 2 of the
            <em>Information Management Regulation</em>;
          </li>
          <li>
            <strong>&quot;PharmaNet Data&quot;</strong> includes any records or information contained in PharmaNet and
            any records
            or information in the custody, control or possession of Organization or any Authorized User as the result of
            any Site Access;
          </li>
          <li>
            <strong>&quot;Regulated User&quot;</strong> means an Authorized User described in subsections 4 (2) to (4)
            of the
            <em>Information Management Regulation</em>;
          </li>
          <li>
            <strong>&quot;Signing Authority&quot;</strong> means the individual identified by Organization as the
            &quot;Signing Authority&quot;
            for a Site, with the associated contact information, as set out in section 2 of the Site Request;
          </li>
          <li>
            <p>
              &quot;Site&quot; means a premises operated by Organization and located in British Columbia that:
            </p>

            <ol type=""i"">
              <li>
                is the subject of a Site Request submitted to the Province, and
              </li>
              <li>
                has been approved for Site Access by the Province in writing
              </li>
            </ol>

            <p class=""underline"">
              For greater certainty, &quot;Site&quot; does not include a location from which remote access to PharmaNet
              takes place;
            </p>
          </li>
          <li>
            <strong>&quot;Site Access&quot;</strong> means any access to or use of PharmaNet at a Site or remotely as
            permitted
            by the Province;
          </li>
          <li>
            <strong>&quot;Site Request&quot;</strong> means, in relation to a Site, the information contained in the
            PharmaNet access
            request form submitted to the Province by the Organization, requesting PharmaNet access at the Site, as such
            information is updated by the Organization from time to time in accordance with section 2.2;
          </li>
          <li>
            <strong>&quot;SSO-Provided Technology&quot;</strong> means any information technology hardware, software or
            services
            provided to Organization by an Approved SSO for the purpose of Site Access;
          </li>
        </ol>
      </li>
      <li>
        Unless otherwise specified, a reference to a statute or regulation by name means a statute or regulation of
        British
        Columbia of that name, as amended or replaced from time to time, and includes any enactments made under the
        authority
        of that statute or regulation.
      </li>
      <li>
        <p>
          The following are the Schedules attached to and incorporated into this Agreement:
        </p>

        <ul>
          <li>
            Schedule A – Specific Privacy and Security Measures
          </li>
        </ul>
      </li>
      <li>
        The main body of this Agreement, the Schedules, and any documents incorporated by reference into this Agreement
        are to
        be interpreted so that all of the provisions are given as full effect as possible. In the event of a conflict,
        unless
        expressly stated to the contrary the main body of the Agreement will prevail over the Schedules, which will
        prevail
        over any document incorporated by reference.
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 2 – REPRESENTATIONS AND WARRANTIES</strong>
</p>

<ol type=""1""
    start=""2""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        <p>
          Organization represents and warrants to the Province, as of the date of this
          Agreement and throughout its term, that:
        </p>

        <ol type=""a"">
          <li>
            the information contained in the Site Request for each Site is true and correct;
          </li>
          <li>
            <p>
              if Organization is not an individual:
            </p>

            <ol type=""i"">
              <li>
                Organization has the power and capacity to enter into this Agreement and to comply with its terms;
              </li>
              <li>
                all necessary corporate or other proceedings have been taken to authorize the execution and delivery
                of this Agreement by, or on behalf of, Organization; and
              </li>
              <li>
                this Agreement has been legally and properly executed by, or on behalf of, the Organization and is
                legally binding upon and enforceable against Organization in accordance with its terms.
              </li>
            </ol>
          </li>
        </ol>
      </li>
      <li>
        Organization must immediately notify the Province of any change to the information contained in a Site Request,
        including any change to a Site’s status, location, normal operating hours, Approved SSO, or the name and contact
        information of the Signing Authority or any of the other specific roles set out in the Site Request. Such
        notices
        must be submitted to the Province in the form and manner directed by the Province in its published instructions
        regarding the submission of updated Site Request information, as such instructions may be updated from time to
        time by the Province.
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 3 – SITE ACCESS REQUIREMENTS</strong>
</p>

<ol type=""1""
    start=""3""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        Organization must comply with the Act and all applicable law.
      </li>
      <li>
        Organization must submit a Site Request to the Province for each physical location where it intends to provide
        Site
        Access, and must only provide Site Access from Sites approved in writing by the Province. For greater certainty,
        a
        Site Request is not required for each physical location from which remote access, as permitted under section
        3.6,
        may occur, but Organization must provide, with the Site Request, a list of the locations from which remote
        access
        may occur, and ensure this list remains current for the term of this agreement.
      </li>
      <li>
        Organization must only provide Site Access using SSO-Provided Technology. For the purposes of remote access,
        Organization must ensure that technology used meets the requirements of Schedule A.
      </li>
      <li>
        Unless otherwise authorized by the Province in writing, Organization must at all times use the secure network or
        security technology that the Province certifies or makes available to Organization for the purpose of Site
        Access.
        The use of any such network or technology by Organization may be subject to terms and conditions of use,
        including
        acceptable use policies, established by the Province and communicated to Organization from time to time in
        writing.
      </li>
      <li>
        <p>
          Organization must only make Site Access available to the following individuals:
        </p>

        <ol type=""a"">
          <li>
            Authorized Users when they are physically located at a Site, and, in the case of an On-Behalf-of-User
            accessing
            personal information of a patient on behalf of a Regulated User, only if the Regulated User will be
            delivering
            care to that patient at the same Site at which the access to personal information occurs;
          </li>
          <li>
            Representatives of an Approved SSO for technical support purposes, in accordance with section 6 of the
            <em>Information Management Regulation</em>.
          </li>
        </ol>
      </li>
      <li>
        Despite section 3.5(a), Organization may make Site Access available to Regulated Users who are physically
        located in
        British Columbia and remotely connected to a Site using a VPN or other remote access technology specifically
        approved
        by the Province in writing for the Site.
      </li>
      <li>
        <p>
          Organization must ensure that Authorized Users with Site Access:
        </p>

        <ol type=""a"">
          <li>
            only access PharmaNet to the extent necessary to provide health services to, or facilitate the care of, the
            individual whose personal information is being accessed;
          </li>
          <li>
            first complete any mandatory training program(s) that the Site’s Approved SSO or the Province makes
            available
            in relation to PharmaNet;
          </li>
          <li>
            access PharmaNet using their own separate login identifications and credentials, and do not share or have
            multiple use of any such login identifications and credentials;
          </li>
          <li>
            secure all devices, codes and credentials that enable access to PharmaNet against unauthorized use; and
          </li>
          <li>
            in the case of remote access, comply with the policies of the Province relating to remote access to
            PharmaNet.
          </li>
        </ol>
      </li>
      <li>
        If notified by the Province that an Authorized User’s access to PharmaNet has been suspended or revoked,
        Organization
        will immediately take any local measures necessary to remove the Authorized User’s Site Access. Organization
        will
        only restore Site Access to a previously suspended or revoked Authorized User upon the Province’s specific
        written
        direction.
      </li>
      <li>
        <p>
          For the purposes of this section:
        </p>

        <ol type=""a"">
          <li>
            <strong>&quot;Responsible Authorized User&quot;</strong> means, in relation to any PharmaNet Data, the
            Regulated User by whom,
            or on whose behalf, that data was obtained from PharmaNet; and
          </li>
          <li>
            <strong>&quot;Use&quot;</strong> includes to collect, access, retain, use, de-identify, and disclose.
          </li>
        </ol>

        <p>
          The PharmaNet Data disclosed under this Agreement is disclosed by the Province solely for the Use of the
          Responsible
          User to whom it is disclosed.
        </p>

        <p>
          Organization must not Use any PharmaNet Data, or permit any third party to Use PharmaNet Data, unless the
          Responsible
          User has authorized such Use and it is otherwise permitted under the Act, applicable law, and the limits and
          conditions imposed by the Province on the Responsible User.
        </p>
      </li>
      <li>
        <p>
          Organization must make all reasonable arrangements to protect PharmaNet Data against such risks as
          unauthorized access,
          collection, use, modification, retention, disclosure or disposal, including by:
        </p>

        <ol type=""a"">
          <li>
            taking all reasonable physical, technical and operational measures necessary to ensure Site Access operates
            in
            accordance with sections 3.1 to 3.9 above, and
          </li>
          <li>
            complying with the requirements of Schedule A.
          </li>
        </ol>
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 4 – NON-COMPLIANCE AND INVESTIGATIONS</strong>
</p>

<ol type=""1""
    start=""4""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        Organization must promptly notify the Province, and provide particulars, if Organization does not comply, or
        anticipates
        that it will be unable to comply, with the terms of this Agreement, or if Organization has knowledge of any
        circumstances,
        incidents or events which have or may jeopardize the security, confidentiality or integrity of PharmaNet,
        including any
        attempt by any person to gain unauthorized access to PharmaNet or the networks or equipment used to connect to
        PharmaNet
        or convey PharmaNet Data.
      </li>
      <li>
        Organization must immediately investigate any suspected breaches of this Agreement and take all reasonable steps
        to prevent
        recurrences of any such breaches.
      </li>
      <li>
        Organization must cooperate with any audits or investigations conducted by the Province (including any
        independent auditor
        appointed by the Province) regarding compliance with this Agreement, including by providing access upon request
        to a Site
        and any associated facilities, networks, equipment, systems, books, records and personnel for the purposes of
        such audit
        or investigation.
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 5 – SITE TERMINATION</strong>
</p>

<ol type=""1""
    start=""5""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        <p>
          The Province may terminate all Site Access at a Site immediately, upon notice to the Signing Authority for the
          Site, if:
        </p>

        <ol type=""a"">
          <li>
            the Approved SSO for the Site is no longer approved by the Province to provide information technology
            hardware, software,
            or service in connection with PharmaNet, or
          </li>
          <li>
            the Province determines that the SSO-Provided Technology or Associated Technology in use at the Site, or any
            component
            thereof, is obsolete, unsupported, or otherwise poses an unacceptable security risk to PharmaNet,
          </li>
        </ol>

        <p>
          and the Organization is unable or unwilling to remedy the problem within a timeframe acceptable to the
          Province.
        </p>
      </li>
      <li>
        As a security precaution, the Province may suspend Site Access at a Site after a period of inactivity. If Site
        Access at a
        Site remains inactive for a period of 90 days or more, the Province may, immediately upon notice to the Signing
        Authority
        for the Site, terminate all further Site Access at the Site.
      </li>
      <li>
        Organization must prevent all further Site Access at a Site immediately upon the Province’s termination, in
        accordance with
        this Article 5, of Site Access at the Site.
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 6 – TERM AND TERMINATION</strong>
</p>

<ol type=""1""
    start=""6""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        The term of this Agreement begins on the date first noted above and continues until it is terminated
        in accordance with this Article 6.
      </li>
      <li>
        Organization may terminate this Agreement at any time on notice to the Province.
      </li>
      <li>
        The Province may terminate this Agreement immediately upon notice to Organization if Organization fails to
        comply with any
        provision of this Agreement.
      </li>
      <li>
        The Province may terminate this Agreement immediately upon notice to Organization in the event Organization no
        longer operates
        any Sites where Site Access is permitted.
      </li>
      <li>
        The Province may terminate this Agreement for any reason upon two (2) months advance notice to Organization.
      </li>
      <li>
        Organization must prevent any further Site Access immediately upon termination of this Agreement.
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 7 – DISCLAIMER AND INDEMNITY</strong>
</p>

<ol type=""1""
    start=""7""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        The PharmaNet access and PharmaNet Data provided under this Agreement are provided &quot;as is&quot; without
        warranty of any kind,
        whether express or implied. All implied warranties, including, without limitation, implied warranties of
        merchantability,
        fitness for a particular purpose, and non-infringement, are hereby expressly disclaimed. The Province does not
        warrant
        the accuracy, completeness or reliability of the PharmaNet Data or the availability of PharmaNet, or that access
        to or
        the operation of PharmaNet will function without error, failure or interruption.
      </li>
      <li>
        Under no circumstances will the Province be liable to any person or business entity for any direct, indirect,
        special,
        incidental, consequential, or other damages based on any use of PharmaNet or the PharmaNet Data, including
        without
        limitation any lost profits, business interruption, or loss of programs or information, even if the Province has
        been specifically advised of the possibility of such damages.
      </li>

      <li>
        Organization must indemnify and save harmless the Province, and the Province’s employees and agents (each an
        <strong>""Indemnified Person""</strong>) from any losses, claims, damages, actions, causes of action, costs and
        expenses that an Indemnified Person may sustain, incur, suffer or be put to at any time, either before or after
        this Agreement ends, which are based upon, arise out of or occur directly or indirectly by reason of any act
        or omission by Organization, or by any Authorized User at the Site, in connection with this Agreement.
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>ARTICLE 8 – GENERAL</strong>
</p>

<ol type=""1""
    start=""8""
    class=""decimal"">
  <li>
    <ol type=""1"">
      <li>
        <p>
          <strong class=""underline"">Notice.</strong> Except where this Agreement expressly provides for another method
          of delivery, any notice to be given to the Province must be in writing and emailed or mailed to:
        </p>

        <address>
          Director, Information and PharmaNet Innovation<br>
          Ministry of Health<br>
          PO Box 9652, STN PROV GOVT<br>
          Victoria, BC V8W 9P4<br>

          <br>

          <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
        </address>

        <p>
          Any notice to be given to a Signing Authority or the Organization will be in writing and emailed, mailed,
          faxed
          or text messaged to the Signing Authority (in the case of notice to a Signing Authority) or all Signing
          Authorities (in the case of notice to the Organization). A Signing Authority may be required to click a
          URL link or to log in to the Province’s &quot;PRIME&quot; system to receive the content of any such notice.
        </p>

        <p>
          Any written notice from a party, if sent electronically, will be deemed to have been received 24 hours after
          the
          time the notice was sent, or, if sent by mail, will be deemed to have been received 3 days (excluding
          Saturdays,
          Sundays and statutory holidays) after the date the notice was sent.
        </p>
      </li>
      <li>
        <strong class=""underline"">Waiver.</strong> The failure of the Province at any time to insist on performance of
        any
        provision of this Agreement by Organization is not a waiver of its right subsequently to insist on performance
        of
        that or any other provision of this Agreement. A waiver of any provision or breach of this Agreement is
        effective
        only if it is writing and signed by, or on behalf of, the waiving party.
      </li>
      <li>
        <p>
          <strong class=""underline"">Modification.</strong> No modification to this Agreement is effective unless it is
          in writing and signed
          by, or on behalf of, the parties.
        </p>

        <p>
          Notwithstanding the foregoing, the Province may amend this Agreement, including the Schedules and this
          section,
          at any time in its sole discretion, by written notice to Organization, in which case the amendment will become
          effective upon the later of: (i) the date notice of the amendment is delivered to Organization; and (ii) the
          effective date of the amendment specified by the Province. The Province will make reasonable efforts to
          provide
          at least thirty (30) days advance notice of any such amendment, subject to any determination by the Province
          that a shorter notice period is necessary due to changes in the Act, applicable law or applicable policies of
          the Province, or is necessary to maintain privacy and security in relation to PharmaNet or PharmaNet Data.
        </p>

        <p>
          If Organization does not agree with any amendment for which notice has been provided by the Province in
          accordance with this section, Organization must promptly (and in any event prior to the effective date)
          cease Site Access at all Sites and take the steps necessary to terminate this Agreement in accordance
          with Article 6.
        </p>
      </li>
      <li>
        <p>
          <strong class=""underline"">Governing Law.</strong> This Agreement will be governed by and will be construed
          and interpreted in accordance with the laws of British Columbia and the laws of Canada applicable therein.
        </p>
      </li>
    </ol>
  </li>
</ol>

<p class=""text-center"">
  <strong>SCHEDULE A – SPECIFIC PRIVACY AND SECURITY MEASURES</strong>
</p>

<p>
  Organization must, in relation to each Site and in relation to Remote Access:
</p>

<ol type=""a"">
  <li>
    secure all workstations and printers at the Site to prevent any viewing of PharmaNet Data by persons other
    than Authorized Users;
  </li>
  <li>
    <p>
      implement all privacy and security measures specified in the following documents published by the Province, as
      amended from time to time:
    </p>

    <ol type=""i"">
      <li>
        <p>
          the PharmaNet Professional and Software Conformance Standards
        </p>

        <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards""
           target=""_blank""
           rel=""noopener noreferrer"">
          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards
        </a>
      </li>
      <li>
        <p>
          Office of the Chief Information Officer: &quot;Submission for Technical Security Standard and High Level
          Architecture for Wireless Local Area Network Connectivity&quot;
        </p>

        <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet""
           target=""_blank""
           rel=""noopener noreferrer"">
          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet
        </a>
      </li>
      <li>
        <p>
          Policy for Secure Remote Access to PharmaNet
        </p>

        <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards""
           target=""_blank""
           rel=""noopener noreferrer"">
          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards
        </a>
      </li>
    </ol>
  </li>
  <li>
    ensure that a qualified technical support person is engaged to provide security support for the Site. This person
    should be familiar with the Site’s network configurations, hardware and software, including all SSO-Provided
    Technology
    and Associated Technology, and should be capable of understanding and adhering to the standards set forth in this
    Agreement and Schedule. Note that any such qualified technical support person must not be permitted by Organization
    to access or use PharmaNet in any manner, unless otherwise permitted under this Agreement;
  </li>
  <li>
    establish and maintain documented privacy policies that detail how Organization will meet its privacy obligations
    in relation to the Site;
  </li>
  <li>
    establish breach reporting and response processes in relation to the Site;
  </li>
  <li>
    detail expectations for privacy protection in contracts and service agreements as applicable at the Site;
  </li>
  <li>
    regularly review the administrative, physical and technological safeguards at the Site;
  </li>
  <li>
    establish and maintain a program for monitoring PharmaNet use at the Site, including by making appropriate
    monitoring
    and reporting mechanisms available to Authorized Users for this purpose.
  </li>
</ol>
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 12,
                column: "Text",
                value: @"<p class=""text-center"">
  This Agreement is made the {{day}} day of {{month}}, {{year}}
</p>

<h1>---- PLACEHOLDER TEXT ----</h1>
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 13,
                column: "Text",
                value: @"<p class=""bold"">
  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
</p>

<h1>PHARMANET USER TERMS OF ACCESS FOR PHARMACISTS</h1>

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
                </a>; and
              </li>
              <li>
                Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                Architecture for Wireless Local Area Network Connectivity”.
                <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet"" target=""_blank"" rel=""noopener noreferrer"">
                  https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet
                </a>
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
        PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
        Province under the authority of the Act;
      </li>
      <li>
        specific provisions of the Act (including but not limited to sections 24, 25 and 29) and the Information
        Management Regulation apply directly to you and to On-Behalf-of Users as a result; and
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
            you will only access PharmaNet: at the Approved Practice Site, and using only the technologies and
            applications approved by the Province;
          </li>
          <li>
            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access takes
            place at the Approved Practice Site and the access is required in relation to patients for whom you will be
            providing Direct Patient Care at the Approved Practice Site;
          </li>
          <li>
            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
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
        <strong>Responding to Requests to Correct a Record Contained in PharmaNet.</strong> If you receive a request for
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
      NOTICE OF NON-COMPLIANCE AND DUTY TO INVESTIGATE
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
          <strong>Non-Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
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
        <strong>Suspension or Termination of PharmaNet Access.</strong> If the Province suspends or terminates your
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
        <strong>Termination by Operation of the Information Management Regulation.</strong> This Agreement will
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
        <strong>You Are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
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
        <strong>You Must Indemnify the Province If You Cause a Loss or Claim.</strong> You agree to indemnify and save
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
        <strong>Deemed Receipt.</strong> Any written communication from a party, if personally delivered or sent
        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent by
        mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays) after
        the date the notice was sent.
      </li>
      <li>
        <strong>Substitute Contact Information.</strong> You may notify the Province of a substitute contact mechanism
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
          <strong>Province May Modify this Agreement.</strong> The Province may amend this Agreement, including this
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 14,
                column: "Text",
                value: @"<h1>
  PHARMANET TERMS OF ACCESS FOR PHARMACY OR DEVICE PROVIDER ON-BEHALF-OF USER
</h1>

<p class=""bold"">
  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
</p>

<p class=""bold underline"">
  On-Behalf-of User Access
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
        you are directly supervised by a Practitioner, who has been granted access to PharmaNet by the Province; and
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

        <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">http://www.gov.bc.ca/pharmacarenewsletter</a>
      </li>
      <li>
        <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the <em>Information Management
        Regulation, B.C</em>. Reg. 74/2015.
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
        or an
        enrolled device provider under the <em>Provider Regulation</em>, B.C. Reg. 222/2014,who supervises your access
        to and use of PharmaNet and who has been granted access to PharmaNet by the Province.
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
        disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and directed
        by a Practitioner;
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
        attempt to access PharmaNet from any location other than the Approved Practice Site of a Practitioner, including
        by VPN or other remote access technology;
      </li>
      <li>
        access PharmaNet unless the access is for the purpose of supporting a Practitioner in providing Direct Patient
        Care to a patient at the same Approved Practice Site at which your access occurs;
      </li>
      <li>
        use PharmaNet to submit claims to PharmaCare or a third-party insurer unless directed to do so by a Practitioner
        at an Approved Practice Site that is enrolled as a provider or device provider under the
        <em>Provider Regulation</em>, B.C. Reg. 222/2014.
      </li>
    </ol>
  </li>
</ol>
<ol start=""6"">
  <li>
    Your access to PharmaNet and use of PharmaNet Data are governed by the Pharmaceutical Services Act and you must
    comply with all your duties under that Act.
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
      Director, Information and PharmaNet Innovation<br>
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
      If you do not agree with any amendment for which notice has been provided by the Province in accordance with (i)
      or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
      PharmaNet.
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 15,
                column: "Text",
                value: @"<h1>
  PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER<br>
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
");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 16,
                column: "Text",
                value: @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

<p class=""bold"">
  By enrolling for PharmaNet access, you agree to the following terms (the &quot;Agreement&quot;). Please read them carefully.
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

        <ul>
          <li>
            <strong>&quot;Act&quot;</strong> means the <em>Pharmaceutical Services Act</em>.
          </li>
          <li>
            <strong>&quot;Approved Practice Site&quot;</strong> means the physical site at which you provide Direct
            Patient Care and which is approved by the Province for PharmaNet access. For greater certainty,
            &quot;Approved Practice Site&quot; does not include a location from which remote access to PharmaNet takes
            place;
          </li>
          <li>
            <strong>&quot;Approved SSO&quot;</strong> means a software support organization approved by the Province
            that provides you with the information technology software and/or services through which you and
            On-Behalf-of Users access PharmaNet.
          </li>
          <li>

            <p>
              <strong>&quot;Conformance Standards&quot;</strong> means the following documents published by the
              Province, as amended from time to time:
            </p>

            <ol type=""i"">
              <li>
                PharmaNet Professional and Software Conformance Standards; and
              </li>
              <li>
                Office of the Chief Information Officer: &quot;Submission for Technical Security Standard and High Level
                Architecture for Wireless Local Area Network Connectivity&quot;.
              </li>
            </ol>

          </li>
          <li>
            <strong>&quot;Direct Patient Care&quot;</strong> means, for the purposes of this Agreement, the provision of
            health services to an individual to whom you provide direct patient care in the context of your Practice.
          </li>
          <li>
            <strong>&quot;Information Management Regulation&quot;</strong> means the Information Management Regulation,
            B.C. Reg. 74/2015.
          </li>
          <li>
            <strong>&quot;On-Behalf-of User&quot;</strong> means a member of your staff who (i) requires access to
            PharmaNet to carry out duties in relation to your Practice; (ii) is authorized by you to access PharmaNet
            on your behalf; and (iii) has been granted access to PharmaNet by the Province.
          </li>
          <li>
            <strong>&quot;Personal Information&quot;</strong> means all recorded information that is about an
            identifiable individual or is defined as, or deemed to be, &quot;personal information&quot; or
            &quot;personal health information&quot; pursuant to any Privacy Laws.
          </li>
          <li>
            <strong>&quot;PharmaCare Newsletter&quot;</strong> means the PharmaCare newsletter published by the Province
            on the following website (or such other website as may be specified by the Province from time to time for
            this purpose):

            <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">
              www.gov.bc.ca/pharmacarenewsletter
            </a>
          </li>
          <li>
            <strong>&quot;PharmaNet&quot;</strong> means PharmaNet as continued under section 2 of the Information
            Management Regulation.
          </li>
          <li>
            <strong>&quot;PharmaNet Data&quot;</strong> includes any record or information contained in PharmaNet and
            any record or information in the custody, control or possession of you or an On-Behalf-of User that was
            obtained through your or an On-Behalf-of User’s access to PharmaNet.
          </li>
          <li>
            <strong>&quot;Practice&quot;</strong> means your practice of the health profession regulated under the
            <em>Health Professions Act</em>, or your practice as an enrolled device provider under the Provider
            Regulation, B.C. Reg. 222/2014, as identified by you through PRIME.
          </li>
          <li>
            <strong>&quot;PRIME&quot;</strong> means the online service provided by the Province that allows users to
            apply for, and manage, their access to PharmaNet, and through which users are granted access by the
            Province.
          </li>
          <li>
            <strong>&quot;Privacy Laws&quot;</strong> means the Act, the <em>Freedom of Information and Protection of
            Privacy Act</em>, the <em>Personal Information Protection Act</em>, and any other statutory or legal
            obligations of privacy owed by you or the Province, whether arising under statute, by contract or at common
            law.
          </li>
          <li>
            <strong>&quot;Province&quot;</strong> means Her Majesty the Queen in Right of British Columbia, as
            represented by the Minister of Health.
          </li>
          <li>
            <strong>&quot;Professional College&quot;</strong> is the regulatory body governing your Practice.
          </li>
          <li>

            <p>
              <strong>&quot;Unauthorized Person&quot;</strong> means any person other than:
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
            unless (iii) below applies, you will only access PharmaNet at the Approved Practice Site, and using only the
            technologies and applications approved by the Province.
          </li>
          <li>
            <p>
              you may only access PharmaNet using remote access technology if all of the following conditions are met:
            </p>

            <ol>
              <li>
                the remote access technology used at the Approved Practice Site has been specifically approved in
                writing by the Province,
              </li>
              <li>
                the requirements of the Province’s Policy for Remote Access to PharmaNet
                (<a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards"">https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards</a>) are met,
              </li>
              <li>
                your Approved Practice Site has registered you with the Province for remote access at the Approved
                Practice Site,
              </li>
              <li>
                you have applied to the Province for remote access at the Approved Practice Site and the Province has
                approved that application in writing, and
              </li>
              <li>
                you are physically located in British Columbia at the time of any such remote access.
              </li>
            </ol>
          </li>
          <li>
            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access takes
            place at the Approved Practice Site and the access is in relation to patients for whom you will be providing
            Direct Patient Care at the Approved Practice Site requiring the access to PharmaNet;
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
            Care, including for the purposes of deidentification or aggregation, quality improvement, evaluation, health
            care planning, surveillance, research or other secondary uses;
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
        any electronic system, unless such storage or retention is required for record keeping in accordance with
        Professional College requirements and in connection with your provision of Direct Patient Care and otherwise
        is in compliance with the Conformance Standards. You will not modify any records retained in accordance with
        this section other than as may be expressly authorized in the Conformance Standards. For clarity, you may
        annotate a discrete record provided that the discrete record is not itself modified other than as expressly
        authorized in the Conformance Standards.
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
        Data or &quot;print outs&quot; produced directly from PharmaNet, and will refer any requests for access to such
        records or &quot;print outs&quot; to the Province.
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
        right to access PharmaNet under the <em>Information Management Regulation</em>, this Agreement will
        automatically terminate as of the date of such suspension or termination.
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
        Data is solely at your own risk. All such access and information is provided on an &quot;as is&quot; and
        &quot;as available&quot; basis without warranty or condition of any kind. The Province does not warrant the
        accuracy, completeness or reliability of the PharmaNet Data or the availability of PharmaNet, or that access
        to or the operation of PharmaNet will function without error, failure or interruption.
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
        harmless the Province, and the Province’s employees and agents  (each an <strong>""Indemnified Person""</strong>)
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
");

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 11,
                column: "Validate",
                value: true);
        }
    }
}
