using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddOralHealthProfessional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "CollegeLookup",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Discontinued",
                table: "CollegeLicense",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 26,
                column: "Text",
                value: "<h1>ORGANIZATION AGREEMENT FOR PHARMANET USE</h1>\r\n\r\n<p>\r\n  <strong>BETWEEN:</strong>\r\n</p>\r\n\r\n<p>\r\n  HIS MAJESTY THE KING IN RIGHT OF THE PROVINCE OF BRITISH COLUMBIA, as represented by the Minister of Health\r\n  (the &quot;Province&quot;).\r\n</p>\r\n\r\n<p>\r\n  <strong>AND:</strong>\r\n</p>\r\n\r\n<p>\r\n  {{organization_name}} (&quot;Organization&quot;)\r\n</p>\r\n\r\n<p>\r\n  <strong>WHEREAS:</strong>\r\n</p>\r\n\r\n<ol type=\"A\">\r\n  <li>\r\n    The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links\r\n    pharmacies to a central data system. Every prescription dispensed in community pharmacies in British\r\n    Columbia is entered into PharmaNet.\r\n  </li>\r\n  <li>\r\n    PharmaNet contains highly sensitive confidential information, including personal information, and it is\r\n    in the public interest to ensure that appropriate measures are in place to protect the confidentiality\r\n    and integrity of such information. All access to and use of PharmaNet and PharmaNet Data is subject to\r\n    the Act and other applicable law.\r\n  </li>\r\n  <li>\r\n    The Province permits Authorized Users to access PharmaNet to provide health services to, or to facilitate\r\n    the care of, the individual whose personal information is being accessed.\r\n  </li>\r\n  <li>\r\n    Organization is a service provider to HealthLink BC, the Province’s self-care program providing health\r\n    information and advice to British Columbia through integrated print, web, and telephony channels to help\r\n    the public make better decisions about their health, and provides Services to the Province in accordance\r\n    with the Service Contract,\r\n  </li>\r\n  <li>\r\n    Pharmacists at Organization require access to PharmaNet so Organization can provide the Services in\r\n    accordance with the Service Contract.\r\n  </li>\r\n  <li>\r\n    This Agreement sets out the terms by which Organization may permit Authorized Users to access PharmaNet\r\n    at the Site(s) operated by Organization.\r\n  </li>\r\n</ol>\r\n\r\n<p>\r\n  <strong>NOW THEREFORE</strong> in consideration of the promises and the covenants, agreements, representations\r\n  and warranties set out in this Agreement (the receipt and sufficiency of which is hereby acknowledged by each\r\n  party), the parties agree as follows:\r\n</p>\r\n\r\n<p class=\"text-center\">\r\n  <strong>ARTICLE 1 – INTERPRETATION</strong>\r\n</p>\r\n\r\n<ol type=\"1\"\r\n    start=\"1\"\r\n    class=\"decimal\">\r\n  <li>\r\n    <ol type=\"1\">\r\n      <li>\r\n        <p>\r\n          In this Agreement, unless the context otherwise requires, the following definitions will apply:\r\n        </p>\r\n\r\n        <ol type=\"a\">\r\n          <li>\r\n            <strong>&quot;Act&quot;</strong> means the <em>Pharmaceutical Services Act</em>;\r\n          </li>\r\n          <li>\r\n            <strong>&quot;Approved SSO&quot;</strong> means, in relation to a Site, the software support organization\r\n            identified in section 1 of the Site Request that provides Organization with the SSO-Provided Technology\r\n            used at the Site;\r\n          </li>\r\n          <li>\r\n            <strong>&quot;Associated Technology&quot;</strong> means, in relation to a Site, any information technology\r\n            hardware, software or services used at the Site, other than the SSO-Provided Technology, that is in any way\r\n            used in connection with Site Access or any PharmaNet Data;\r\n          </li>\r\n          <li>\r\n            <p>\r\n              <strong>&quot;Authorized User&quot;</strong> means an individual who is granted access to PharmaNet by the\r\n              Province and who is:\r\n            </p>\r\n\r\n            <ol type=\"i\">\r\n              <li>\r\n                an employee or independent contractor of Organization, or\r\n              </li>\r\n              <li>\r\n                if Organization is an individual, the Organization;\r\n              </li>\r\n            </ol>\r\n          </li>\r\n          <li>\r\n            <strong>&quot;Information Management Regulation&quot;</strong> means the\r\n            <em>Information Management Regulation</em>, B.C. Reg. 74/2015;\r\n          </li>\r\n          <li>\r\n            <strong>&quot;PharmaNet&quot;</strong> means PharmaNet as continued under section 2 of the\r\n            <em>Information Management Regulation</em>;\r\n          </li>\r\n          <li>\r\n            <strong>&quot;PharmaNet Data&quot;</strong> includes any records or information contained in PharmaNet\r\n            and any records or information in the custody, control or possession of Organization or any Authorized User\r\n            as the result of any Site Access;\r\n          </li>\r\n          <li>\r\n            <strong>&quot;Regulated User&quot;</strong> means an Authorized User described in subsections 4 (2) to (4)\r\n            of the <em>Information Management Regulation</em>;\r\n          </li>\r\n          <li>\r\n            <strong>&quot;Service Contract&quot;</strong> means the contract for services between the Province and\r\n            Organization, contract file number 2021-075, dated November 1, 2020, as amended from time to time by the\r\n            parties in accordance with its terms;\r\n          </li>\r\n          <li>\r\n            <strong>&quot;Signing Authority&quot;</strong> means the individual identified by Organization as the\r\n            &quot;Signing Authority&quot; for a Site, with the associated contact information, as set out in section 2\r\n            of the Site Request;\r\n          </li>\r\n          <li>\r\n            <p>\r\n              <strong>&quot;Site&quot;</strong> means a licensed community pharmacy premises operated by Organization\r\n              and located in British Columbia that:\r\n            </p>\r\n\r\n            <ol type=\"i\">\r\n              <li>\r\n                is the subject of a Site Request submitted to the Province, and\r\n              </li>\r\n              <li>\r\n                has been approved for Site Access by the Province in writing\r\n              </li>\r\n            </ol>\r\n          </li>\r\n          <li>\r\n            <strong>&quot;Site Access&quot;</strong> means any access to or use of PharmaNet at a Site as permitted by\r\n            the Province;\r\n          </li>\r\n          <li>\r\n            <strong>&quot;Site Request&quot;</strong> means, in relation to a Site, the information contained in the\r\n            PharmaNet access request form submitted to the Province by the Organization, requesting PharmaNet access at\r\n            the Site, as such information is updated by the Organization from time to time in accordance with\r\n            section 2.2;\r\n          </li>\r\n          <li>\r\n            <strong>&quot;SSO-Provided Technology&quot;</strong> means any information technology hardware, software or\r\n            services provided to Organization by an Approved SSO for the purpose of Site Access;\r\n          </li>\r\n        </ol>\r\n      </li>\r\n      <li>\r\n        Unless otherwise specified, a reference to a statute or regulation by name means a statute or regulation of\r\n        British Columbia of that name, as amended or replaced from time to time, and includes any enactments made\r\n        under the authority of that statute or regulation.\r\n      </li>\r\n      <li>\r\n        <p>\r\n          The following are the Schedules attached to and incorporated into this Agreement:\r\n        </p>\r\n\r\n        <ul>\r\n          <li>\r\n            Schedule A – Specific Privacy and Security Measures\r\n          </li>\r\n        </ul>\r\n      </li>\r\n      <li>\r\n        The main body of this Agreement, the Schedules, and any documents incorporated by reference into this Agreement\r\n        are to be interpreted so that all of the provisions are given as full effect as possible. In the event of a\r\n        conflict, unless expressly stated to the contrary, the main body of the Agreement will prevail over the\r\n        Schedules, which will prevail over any document incorporated by reference.\r\n      </li>\r\n      <li>\r\n        For greater certainty, nothing in this Agreement is intended to modify or otherwise limit the applicability of\r\n        the privacy, security or confidentiality obligations agreed to by the Organization in the Service Contract.\r\n      </li>\r\n    </ol>\r\n  </li>\r\n</ol>\r\n\r\n<p class=\"text-center\">\r\n  <strong>ARTICLE 2 – REPRESENTATIONS AND WARRANTIES</strong>\r\n</p>\r\n\r\n<ol type=\"1\"\r\n    start=\"2\"\r\n    class=\"decimal\">\r\n  <li>\r\n    <ol type=\"1\">\r\n      <li>\r\n        <p>\r\n          Organization represents and warrants to the Province, as of the date of this Agreement and throughout its\r\n          term, that:\r\n        </p>\r\n\r\n        <ol type=\"a\">\r\n          <li>\r\n            the information contained in the Site Request for each Site is true and correct;\r\n          </li>\r\n          <li>\r\n            <p>\r\n              if Organization is not an individual:\r\n            </p>\r\n\r\n            <ol type=\"i\">\r\n              <li>\r\n                Organization has the power and capacity to enter into this Agreement and to comply with its terms;\r\n              </li>\r\n              <li>\r\n                all necessary corporate or other proceedings have been taken to authorize the execution and delivery of\r\n                this Agreement by, or on behalf of, Organization; and\r\n              </li>\r\n              <li>\r\n                this Agreement has been legally and properly executed by, or on behalf of, the Organization and is\r\n                legally binding upon and enforceable against Organization in accordance with its terms.\r\n              </li>\r\n            </ol>\r\n          </li>\r\n        </ol>\r\n      </li>\r\n      <li>\r\n        Organization must notify the Province at least seven (7) days in advance of any change to the information\r\n        contained in a Site Request, including any change to a Site’s status, location, normal operating hours,\r\n        Approved SSO, or the name and contact information of the Signing Authority or any of the other specific\r\n        roles set out in the Site Request. Such notices must be submitted to the Province in the form and manner\r\n        directed by the Province in its published instructions regarding the submission of updated Site Request\r\n        information, as such instructions may be updated from time to time by the Province.\r\n      </li>\r\n    </ol>\r\n  </li>\r\n</ol>\r\n\r\n<p class=\"text-center\">\r\n  <strong>ARTICLE 3 – SITE ACCESS REQUIREMENTS</strong>\r\n</p>\r\n\r\n<ol type=\"1\"\r\n    start=\"3\"\r\n    class=\"decimal\">\r\n  <li>\r\n    <ol type=\"1\">\r\n      <li>\r\n        Organization must comply with the Act and all applicable law.\r\n      </li>\r\n      <li>\r\n        Organization must submit a Site Request to the Province for each physical location where it intends to provide\r\n        Site Access, and must only provide Site Access from Sites approved in writing by the Province and only as\r\n        authorized by the Province.\r\n      </li>\r\n      <li>\r\n        Organization must only provide Site Access using SSO-Provided Technology.\r\n      </li>\r\n      <li>\r\n        Unless otherwise authorized by the Province in writing, Organization must at all times use the secure network\r\n        or security technology that the Province certifies or makes available to Organization for the purpose of Site\r\n        Access. The use of any such network or technology by Organization may be subject to terms and conditions of\r\n        use, including acceptable use policies, established by the Province and communicated to Organization from time\r\n        to time in writing (including through this Agreement), and Organization must comply with all such terms and\r\n        conditions of use.\r\n      </li>\r\n      <li>\r\n        <p>\r\n          Organization must only make Site Access available to the following individuals:\r\n        </p>\r\n\r\n        <ol type=\"a\">\r\n          <li>\r\n            Authorized Users when they are physically located at a Site;\r\n          </li>\r\n          <li>\r\n            Representatives of an Approved SSO for technical support purposes, in accordance with section 6 of the\r\n            <em>Information Management Regulation</em>.\r\n          </li>\r\n        </ol>\r\n      </li>\r\n      <li>\r\n        <p>\r\n          Organization must ensure that Authorized Users with Site Access:\r\n        </p>\r\n        <ol type=\"a\">\r\n          <li>\r\n            only access PharmaNet to the extent necessary to provide Services in accordance with the Service Contract;\r\n          </li>\r\n          <li>\r\n            first complete any mandatory training program(s) that the Site’s Approved SSO or the Province makes\r\n            available in relation to PharmaNet;\r\n          </li>\r\n          <li>\r\n            access PharmaNet using their own separate login identifications and credentials, and do not share or have\r\n            multiple use of any such login identifications and credentials;\r\n          </li>\r\n          <li>\r\n            secure all devices, codes and credentials that enable access to PharmaNet against unauthorized use;\r\n          </li>\r\n        </ol>\r\n      </li>\r\n      <li>\r\n        If notified by the Province that an Authorized User’s access to PharmaNet has been suspended or revoked,\r\n        Organization will immediately take any local measures necessary to remove the Authorized User’s Site Access.\r\n        Organization will only restore Site Access to a previously suspended or revoked Authorized User upon the\r\n        Province’s specific written direction.\r\n      </li>\r\n      <li>\r\n        <p>\r\n          For the purposes of this section:\r\n        </p>\r\n\r\n        <ol type=\"a\">\r\n          <li>\r\n            <strong>&quot;Responsible Authorized User&quot;</strong> means, in relation to any PharmaNet Data, the\r\n            Regulated User by whom that data was obtained from PharmaNet; and\r\n          </li>\r\n          <li>\r\n            <strong>&quot;Use&quot;</strong> includes to collect, access, retain, use, de-identify, and disclose.\r\n          </li>\r\n        </ol>\r\n\r\n        <p>\r\n          The PharmaNet Data disclosed under this Agreement is disclosed by the Province solely for the Use of the\r\n          Responsible Authorized User to whom it is disclosed.\r\n        </p>\r\n\r\n        <p>\r\n          Organization must not Use any PharmaNet Data, or permit any third party to Use PharmaNet Data, unless the\r\n          Responsible Authorized User has authorized such Use and it is otherwise permitted under the Act, applicable\r\n          law, and the limits and conditions imposed by the Province on the Responsible Authorized User.\r\n        </p>\r\n\r\n        <p>\r\n          Organization explicitly acknowledges that sections 24 and 25 of the Act apply to all PharmaNet Data.\r\n        </p>\r\n\r\n        <p>\r\n          This Agreement documents limits and conditions, set by the Minister in writing, that the Act requires\r\n          Organization and Authorized Users to comply with.\r\n        </p>\r\n      </li>\r\n      <li>\r\n        <p>\r\n          Organization must make all reasonable arrangements to protect PharmaNet Data against such risks as\r\n          unauthorized access, collection, use, modification, retention, disclosure or disposal, including by:\r\n        </p>\r\n\r\n        <ol type=\"a\">\r\n          <li>\r\n            taking all reasonable physical, technical and operational measures necessary to ensure Site Access operates\r\n            in accordance with sections 3.1 to 3.9 above, and\r\n          </li>\r\n          <li>\r\n            complying with the requirements of Schedule A.\r\n          </li>\r\n        </ol>\r\n      </li>\r\n      <li>\r\n        Organization must ensure that no Authorized User submits Claims on PharmaNet other than from a Site in respect\r\n        of which\r\n        a person is enrolled as a Provider.\r\n      </li>\r\n    </ol>\r\n  </li>\r\n</ol>\r\n\r\n<p class=\"text-center\">\r\n  <strong>ARTICLE 4 – NON-COMPLIANCE AND INVESTIGATIONS</strong>\r\n</p>\r\n\r\n<ol type=\"1\"\r\n    start=\"4\"\r\n    class=\"decimal\">\r\n  <li>\r\n    <ol type=\"1\">\r\n      <li>\r\n        Organization must promptly notify the Province, and provide particulars, if Organization does not comply, or\r\n        anticipates that it will be unable to comply, with the terms of this Agreement, or if Organization has knowledge\r\n        of any circumstances, incidents or events which have or may jeopardize the security, confidentiality or\r\n        integrity of PharmaNet, including any attempt by any person to gain unauthorized access to PharmaNet or the\r\n        networks or equipment used to connect to PharmaNet or convey PharmaNet Data.\r\n      </li>\r\n      <li>\r\n        Organization must immediately investigate any suspected breaches of this Agreement and take all reasonable steps\r\n        to prevent recurrences of any such breaches.\r\n      </li>\r\n      <li>\r\n        Organization must cooperate with any audits or investigations conducted by the Province (including any\r\n        independent auditor appointed by the Province) regarding compliance with this Agreement, including by providing\r\n        access upon request to a Site and any associated facilities, networks, equipment, systems, books, records and\r\n        personnel for the purposes of such audit or investigation.\r\n      </li>\r\n    </ol>\r\n  </li>\r\n</ol>\r\n\r\n<p class=\"text-center\">\r\n  <strong>ARTICLE 5 – SITE TERMINATION</strong>\r\n</p>\r\n\r\n<ol type=\"1\"\r\n    start=\"5\"\r\n    class=\"decimal\">\r\n  <li>\r\n    <ol type=\"1\">\r\n      <li>\r\n        <p>\r\n          The Province may terminate all Site Access at a Site immediately, upon notice to the Signing Authority for\r\n          the Site, if:\r\n        </p>\r\n\r\n        <ol type=\"a\">\r\n          <li>\r\n            the Approved SSO for the Site is no longer approved by the Province to provide information technology\r\n            hardware, software, or service in connection with PharmaNet, or\r\n          </li>\r\n          <li>\r\n            the Province determines that the SSO-Provided Technology or Associated Technology in use at the Site, or\r\n            any component thereof, is obsolete, unsupported, or otherwise poses an unacceptable security risk to\r\n            PharmaNet and Organization is unable or unwilling to remedy the problem within a time frame acceptable to\r\n            the Province.\r\n          </li>\r\n        </ol>\r\n      </li>\r\n      <li>\r\n        As a security precaution, the Province may suspend Site Access at a Site after a period of inactivity. If Site\r\n        Access at a Site remains inactive for a period of 90 days or more, the Province may, immediately upon notice to\r\n        the Signing Authority for the Site, terminate all further Site Access at the Site.\r\n      </li>\r\n      <li>\r\n        Organization must prevent all further Site Access at a Site immediately upon the Province’s termination, in\r\n        accordance with this Article 5 of Site Access at the Site.\r\n      </li>\r\n    </ol>\r\n  </li>\r\n</ol>\r\n\r\n<p class=\"text-center\">\r\n  <strong>ARTICLE 6 – TERM AND TERMINATION</strong>\r\n</p>\r\n\r\n<ol type=\"1\"\r\n    start=\"6\"\r\n    class=\"decimal\">\r\n  <li>\r\n    <ol type=\"1\">\r\n      <li>\r\n        <p>\r\n          The term of this Agreement begins on the date first noted above and continues until the earliest of:\r\n        </p>\r\n\r\n        <ol type=\"a\">\r\n          <li>\r\n            the expiration or earlier termination of the term of the Service Contract; or\r\n          </li>\r\n          <li>\r\n            the date this Agreement is terminated in accordance with this Article 6.\r\n          </li>\r\n        </ol>\r\n      </li>\r\n      <li>\r\n        Organization may terminate this Agreement at any time on notice to the Province.\r\n      </li>\r\n      <li>\r\n        The Province may terminate this Agreement immediately upon notice to Organization if Organization fails to\r\n        comply with any provision of this Agreement.\r\n      </li>\r\n      <li>\r\n        The Province may terminate this Agreement immediately upon notice to Organization in the event Organization no\r\n        longer operates any Sites where Site Access is permitted.\r\n      </li>\r\n      <li>\r\n        The Province may terminate this Agreement for any reason upon two (2) months advance notice to Organization.\r\n      </li>\r\n      <li>\r\n        Organization must prevent any further Site Access immediately upon expiration or termination of the term of\r\n        this Agreement.\r\n      </li>\r\n    </ol>\r\n  </li>\r\n</ol>\r\n\r\n<p class=\"text-center\">\r\n  <strong>ARTICLE 7 – DISCLAIMER AND INDEMNITY</strong>\r\n</p>\r\n\r\n<ol type=\"1\"\r\n    start=\"7\"\r\n    class=\"decimal\">\r\n  <li>\r\n    <ol type=\"1\">\r\n      <li>\r\n        The PharmaNet access and PharmaNet Data provided under this Agreement are provided \"as is\" without warranty of\r\n        any kind, whether express or implied. All implied warranties, including, without limitation, implied warranties\r\n        of merchantability, fitness for a particular purpose, and non-infringement, are hereby expressly disclaimed.\r\n        The Province does not warrant the accuracy, completeness or reliability of the PharmaNet Data or the\r\n        availability of PharmaNet, or that access to or the operation of PharmaNet will function without error,\r\n        failure or interruption.\r\n      </li>\r\n      <li>\r\n        Under no circumstances will the Province be liable to any person or business entity for any direct, indirect,\r\n        special, incidental, consequential, or other damages based on any use of PharmaNet or the PharmaNet Data,\r\n        including without limitation any lost profits, business interruption, or loss of programs or information, even\r\n        if the Province has been specifically advised of the possibility of such damages.\r\n      </li>\r\n    </ol>\r\n  </li>\r\n</ol>\r\n\r\n<p class=\"text-center\">\r\n  <strong>ARTICLE 8 – GENERAL</strong>\r\n</p>\r\n\r\n<ol type=\"1\"\r\n    start=\"8\"\r\n    class=\"decimal\">\r\n  <li>\r\n    <ol type=\"1\">\r\n      <li>\r\n        <p>\r\n          <strong class=\"underline\">Notice.</strong> Except where this Agreement expressly provides for another method\r\n          of delivery, any notice to be given to the Province must be in writing and mailed or emailed to:\r\n        </p>\r\n\r\n        <address>\r\n          Director, Information and PharmaNet Innovation<br>\r\n          Ministry of Health<br>\r\n          PO Box 9652, STN PROV GOVT<br>\r\n          Victoria, BC V8W 9P4<br>\r\n\r\n          <br>\r\n\r\n          <a href=\"mailto:PRIMESupport@gov.bc.ca\">PRIMESupport@gov.bc.ca</a>\r\n        </address>\r\n\r\n        <p>\r\n          Any notice to be given to a Signing Authority or the Organization will be in writing and emailed, mailed,\r\n          faxed or text-messaged to the Signing Authority (in the case of notice to a Signing Authority) or all Signing\r\n          Authorities (in the case of notice to the Organization). A Signing Authority may be required to click a URL\r\n          link or to log in to the Province’s \"PRIME\" system to receive the content of any such notice.\r\n        </p>\r\n\r\n        <p>\r\n          Any written notice from a party, if sent electronically, will be deemed to have been received 24 hours after\r\n          the time the notice was sent, or, if sent by mail, will be deemed to have been received 3 days (excluding\r\n          Saturdays, Sundays and statutory holidays) after the date the notice was sent.\r\n        </p>\r\n      </li>\r\n      <li>\r\n        <strong class=\"underline\">Waiver.</strong> The failure of the Province at any time to insist on performance of\r\n        any provision of this Agreement by Organization is not a waiver of its right subsequently to insist on\r\n        performance of that or any other provision of this Agreement. A waiver of any provision or breach of this\r\n        Agreement is effective only if it is writing and signed by, or on behalf of, the waiving party.\r\n      </li>\r\n      <li>\r\n        <strong class=\"underline\">Modification.</strong> No modification to this Agreement is effective unless it is\r\n        in writing and signed by, or on behalf of, the parties.\r\n      </li>\r\n      <li>\r\n        <p>\r\n          <strong class=\"underline\">Governing Law.</strong> This Agreement will be governed by and will be construed\r\n          and interpreted in accordance with the laws of British Columbia and the laws of Canada applicable therein.\r\n        </p>\r\n      </li>\r\n      <li>\r\n        <p>\r\n          <strong class=\"underline\">Survival.</strong> Sections 3.1, 3.8, 3.9, 4, 7 and any other provision of this\r\n          Agreement that expressly or by its nature continues after termination, shall survive termination of this\r\n          Agreement.\r\n        </p>\r\n      </li>\r\n    </ol>\r\n  </li>\r\n</ol>\r\n\r\n{{signature_block}}\r\n\r\n<p class=\"text-center break-before\">\r\n  <strong>SCHEDULE A – SPECIFIC PRIVACY AND SECURITY MEASURES</strong>\r\n</p>\r\n\r\n<p>\r\n  Organization must, in relation to each Site\r\n</p>\r\n\r\n<ol type=\"a\">\r\n  <li>\r\n    secure all workstations and printers at the Site to prevent any viewing of PharmaNet Data by persons other than\r\n    Authorized Users;\r\n  </li>\r\n  <li>\r\n    <p>\r\n      implement all privacy and security measures specified in the following documents published by the Province, as\r\n      amended from time to time:\r\n    </p>\r\n\r\n    <ol type=\"i\">\r\n      <li>\r\n        <p>\r\n          the PharmaNet Professional and Software Conformance Standards\r\n        </p>\r\n\r\n        <a href=\"https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards\"\r\n           target=\"_blank\"\r\n           rel=\"noopener noreferrer\">\r\n          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards\r\n        </a>\r\n      </li>\r\n      <li>\r\n        <p>\r\n          Office of the Chief Information Officer: \"Submission for Technical Security Standard and High Level\r\n          Architecture for Wireless Local Area Network Connectivity\"\r\n        </p>\r\n\r\n        <a href=\"https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet\"\r\n           target=\"_blank\"\r\n           rel=\"noopener noreferrer\">\r\n          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet\r\n        </a>\r\n      </li>\r\n    </ol>\r\n  </li>\r\n  <li>\r\n    ensure that a qualified technical support person is engaged to provide security support for the Site. This person\r\n    should be familiar with the Site’s network configurations, hardware and software, including all SSO-Provided\r\n    Technology and Associated Technology, and should be capable of understanding and adhering to the standards set\r\n    forth in this Agreement and Schedule. Note that any such qualified technical support person must not be permitted\r\n    by Organization to access or use PharmaNet in any manner, unless otherwise permitted under this Agreement;\r\n  </li>\r\n  <li>\r\n    establish and maintain documented privacy policies that detail how Organization will meet its privacy obligations\r\n    in relation to the Site;\r\n  </li>\r\n  <li>\r\n    establish breach reporting and response processes in relation to the Site;\r\n  </li>\r\n  <li>\r\n    detail expectations for privacy protection in contracts and service agreements as applicable at the Site;\r\n  </li>\r\n  <li>\r\n    regularly review the administrative, physical and technological safeguards at the Site;\r\n  </li>\r\n  <li>\r\n    establish and maintain a program for monitoring PharmaNet use at the Site, including by making appropriate\r\n    monitoring and reporting mechanisms available to Authorized Users for this purpose.\r\n  </li>\r\n</ol>\r\n");

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 5, 64 },
                column: "Discontinued",
                value: true);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 6, 64 },
                column: "Discontinued",
                value: true);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 7, 70 },
                column: "Discontinued",
                value: true);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 7, 75 },
                column: "Discontinued",
                value: true);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 7, 76 },
                column: "Discontinued",
                value: true);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 7, 77 },
                column: "Discontinued",
                value: true);

            migrationBuilder.UpdateData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 8, 64 },
                column: "Discontinued",
                value: true);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "Weight",
                value: 10);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "Weight",
                value: 20);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 3,
                column: "Weight",
                value: 30);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "Weight",
                value: 50);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "Weight",
                value: 999);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 6,
                column: "Weight",
                value: 999);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 7,
                column: "Weight",
                value: 999);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 8,
                column: "Weight",
                value: 60);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 9,
                column: "Weight",
                value: 70);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 10,
                column: "Weight",
                value: 80);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 11,
                column: "Weight",
                value: 90);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 12,
                column: "Weight",
                value: 100);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 13,
                column: "Weight",
                value: 110);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 14,
                column: "Weight",
                value: 120);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 15,
                column: "Weight",
                value: 130);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 16,
                column: "Weight",
                value: 140);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 17,
                column: "Weight",
                value: 160);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 18,
                column: "Weight",
                value: 170);

            migrationBuilder.UpdateData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 19,
                column: "Weight",
                value: 180);

            migrationBuilder.InsertData(
                table: "CollegeLookup",
                columns: new[] { "Code", "Name", "Weight" },
                values: new object[] { 20, "College of Oral Health Professionals", 40 });

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "Name", "Weight" },
                values: new object[,]
                {
                    { 92, "Limited (Educational and Volunteer) Dentist", 6 },
                    { 93, "Limited (Armed Forces/Government) Dentist", 7 },
                    { 94, "Dental Therapist", 8 },
                    { 95, "Dental Technician", 9 },
                    { 96, "Full Denturist", 10 },
                    { 97, "Student Dentistry", 11 },
                    { 101, "Temporary", 15 },
                    { 100, "Student Denturism", 14 },
                    { 91, "Limited (Academic) Dentist", 5 },
                    { 90, "Limited Dentist (Restricted to Specialty)", 4 },
                    { 89, "Full Dentist", 3 },
                    { 88, "Dental Hygiene Practitioner", 2 },
                    { 87, "Dental Hygienists", 1 },
                    { 98, "Limited (Grandparented) Denturist", 12 },
                    { 99, "Student dental technology", 13 }
                });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode", "CollegeLicenseGroupingCode", "Discontinued" },
                values: new object[,]
                {
                    { 20, 87, null, false },
                    { 20, 100, null, false },
                    { 20, 99, null, false },
                    { 20, 98, null, false },
                    { 20, 97, null, false },
                    { 20, 96, null, false },
                    { 20, 95, null, false },
                    { 20, 101, null, false },
                    { 20, 93, null, false },
                    { 20, 92, null, false },
                    { 20, 94, null, false },
                    { 20, 89, null, false },
                    { 20, 91, null, false },
                    { 20, 90, null, false },
                    { 20, 88, null, false }
                });

            migrationBuilder.InsertData(
                table: "LicenseDetail",
                columns: new[] { "Id", "AllowRequestRemoteAccess", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "LicenseCode", "LicensedToProvideCare", "Manual", "NamedInImReg", "NonPrescribingPrefix", "Prefix", "PrescriberIdType", "UpdatedTimeStamp", "UpdatedUserId", "Validate" },
                values: new object[,]
                {
                    { 251, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 100, true, false, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 238, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 87, true, false, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 250, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 99, true, false, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 249, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 98, true, false, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 239, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 88, true, false, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 248, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 97, true, false, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 243, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 92, false, true, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 247, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 96, true, false, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 240, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 89, true, false, true, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 246, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 95, true, false, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 245, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 94, true, false, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 241, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 90, true, false, true, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 244, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 93, false, true, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 242, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 91, false, true, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 252, false, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), 101, true, true, false, null, "", null, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 87 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 88 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 89 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 90 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 91 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 92 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 93 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 94 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 95 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 96 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 97 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 98 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 99 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 100 });

            migrationBuilder.DeleteData(
                table: "CollegeLicense",
                keyColumns: new[] { "CollegeCode", "LicenseCode" },
                keyValues: new object[] { 20, 101 });

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "CollegeLookup",
                keyColumn: "Code",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 101);

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "CollegeLookup");

            migrationBuilder.DropColumn(
                name: "Discontinued",
                table: "CollegeLicense");

            migrationBuilder.UpdateData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 26,
                column: "Text",
                value: "<h1>ORGANIZATION AGREEMENT FOR PHARMANET USE</h1>\n\n<p>\n  <strong>BETWEEN:</strong>\n</p>\n\n<p>\n  HIS MAJESTY THE KING IN RIGHT OF THE PROVINCE OF BRITISH COLUMBIA, as represented by the Minister of Health\n  (the &quot;Province&quot;).\n</p>\n\n<p>\n  <strong>AND:</strong>\n</p>\n\n<p>\n  {{organization_name}} (&quot;Organization&quot;)\n</p>\n\n<p>\n  <strong>WHEREAS:</strong>\n</p>\n\n<ol type=\"A\">\n  <li>\n    The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links\n    pharmacies to a central data system. Every prescription dispensed in community pharmacies in British\n    Columbia is entered into PharmaNet.\n  </li>\n  <li>\n    PharmaNet contains highly sensitive confidential information, including personal information, and it is\n    in the public interest to ensure that appropriate measures are in place to protect the confidentiality\n    and integrity of such information. All access to and use of PharmaNet and PharmaNet Data is subject to\n    the Act and other applicable law.\n  </li>\n  <li>\n    The Province permits Authorized Users to access PharmaNet to provide health services to, or to facilitate\n    the care of, the individual whose personal information is being accessed.\n  </li>\n  <li>\n    Organization is a service provider to HealthLink BC, the Province’s self-care program providing health\n    information and advice to British Columbia through integrated print, web, and telephony channels to help\n    the public make better decisions about their health, and provides Services to the Province in accordance\n    with the Service Contract,\n  </li>\n  <li>\n    Pharmacists at Organization require access to PharmaNet so Organization can provide the Services in\n    accordance with the Service Contract.\n  </li>\n  <li>\n    This Agreement sets out the terms by which Organization may permit Authorized Users to access PharmaNet\n    at the Site(s) operated by Organization.\n  </li>\n</ol>\n\n<p>\n  <strong>NOW THEREFORE</strong> in consideration of the promises and the covenants, agreements, representations\n  and warranties set out in this Agreement (the receipt and sufficiency of which is hereby acknowledged by each\n  party), the parties agree as follows:\n</p>\n\n<p class=\"text-center\">\n  <strong>ARTICLE 1 – INTERPRETATION</strong>\n</p>\n\n<ol type=\"1\"\n    start=\"1\"\n    class=\"decimal\">\n  <li>\n    <ol type=\"1\">\n      <li>\n        <p>\n          In this Agreement, unless the context otherwise requires, the following definitions will apply:\n        </p>\n\n        <ol type=\"a\">\n          <li>\n            <strong>&quot;Act&quot;</strong> means the <em>Pharmaceutical Services Act</em>;\n          </li>\n          <li>\n            <strong>&quot;Approved SSO&quot;</strong> means, in relation to a Site, the software support organization\n            identified in section 1 of the Site Request that provides Organization with the SSO-Provided Technology\n            used at the Site;\n          </li>\n          <li>\n            <strong>&quot;Associated Technology&quot;</strong> means, in relation to a Site, any information technology\n            hardware, software or services used at the Site, other than the SSO-Provided Technology, that is in any way\n            used in connection with Site Access or any PharmaNet Data;\n          </li>\n          <li>\n            <p>\n              <strong>&quot;Authorized User&quot;</strong> means an individual who is granted access to PharmaNet by the\n              Province and who is:\n            </p>\n\n            <ol type=\"i\">\n              <li>\n                an employee or independent contractor of Organization, or\n              </li>\n              <li>\n                if Organization is an individual, the Organization;\n              </li>\n            </ol>\n          </li>\n          <li>\n            <strong>&quot;Information Management Regulation&quot;</strong> means the\n            <em>Information Management Regulation</em>, B.C. Reg. 74/2015;\n          </li>\n          <li>\n            <strong>&quot;PharmaNet&quot;</strong> means PharmaNet as continued under section 2 of the\n            <em>Information Management Regulation</em>;\n          </li>\n          <li>\n            <strong>&quot;PharmaNet Data&quot;</strong> includes any records or information contained in PharmaNet\n            and any records or information in the custody, control or possession of Organization or any Authorized User\n            as the result of any Site Access;\n          </li>\n          <li>\n            <strong>&quot;Regulated User&quot;</strong> means an Authorized User described in subsections 4 (2) to (4)\n            of the <em>Information Management Regulation</em>;\n          </li>\n          <li>\n            <strong>&quot;Service Contract&quot;</strong> means the contract for services between the Province and\n            Organization, contract file number 2021-075, dated November 1, 2020, as amended from time to time by the\n            parties in accordance with its terms;\n          </li>\n          <li>\n            <strong>&quot;Signing Authority&quot;</strong> means the individual identified by Organization as the\n            &quot;Signing Authority&quot; for a Site, with the associated contact information, as set out in section 2\n            of the Site Request;\n          </li>\n          <li>\n            <p>\n              <strong>&quot;Site&quot;</strong> means a licensed community pharmacy premises operated by Organization\n              and located in British Columbia that:\n            </p>\n\n            <ol type=\"i\">\n              <li>\n                is the subject of a Site Request submitted to the Province, and\n              </li>\n              <li>\n                has been approved for Site Access by the Province in writing\n              </li>\n            </ol>\n          </li>\n          <li>\n            <strong>&quot;Site Access&quot;</strong> means any access to or use of PharmaNet at a Site as permitted by\n            the Province;\n          </li>\n          <li>\n            <strong>&quot;Site Request&quot;</strong> means, in relation to a Site, the information contained in the\n            PharmaNet access request form submitted to the Province by the Organization, requesting PharmaNet access at\n            the Site, as such information is updated by the Organization from time to time in accordance with\n            section 2.2;\n          </li>\n          <li>\n            <strong>&quot;SSO-Provided Technology&quot;</strong> means any information technology hardware, software or\n            services provided to Organization by an Approved SSO for the purpose of Site Access;\n          </li>\n        </ol>\n      </li>\n      <li>\n        Unless otherwise specified, a reference to a statute or regulation by name means a statute or regulation of\n        British Columbia of that name, as amended or replaced from time to time, and includes any enactments made\n        under the authority of that statute or regulation.\n      </li>\n      <li>\n        <p>\n          The following are the Schedules attached to and incorporated into this Agreement:\n        </p>\n\n        <ul>\n          <li>\n            Schedule A – Specific Privacy and Security Measures\n          </li>\n        </ul>\n      </li>\n      <li>\n        The main body of this Agreement, the Schedules, and any documents incorporated by reference into this Agreement\n        are to be interpreted so that all of the provisions are given as full effect as possible. In the event of a\n        conflict, unless expressly stated to the contrary, the main body of the Agreement will prevail over the\n        Schedules, which will prevail over any document incorporated by reference.\n      </li>\n      <li>\n        For greater certainty, nothing in this Agreement is intended to modify or otherwise limit the applicability of\n        the privacy, security or confidentiality obligations agreed to by the Organization in the Service Contract.\n      </li>\n    </ol>\n  </li>\n</ol>\n\n<p class=\"text-center\">\n  <strong>ARTICLE 2 – REPRESENTATIONS AND WARRANTIES</strong>\n</p>\n\n<ol type=\"1\"\n    start=\"2\"\n    class=\"decimal\">\n  <li>\n    <ol type=\"1\">\n      <li>\n        <p>\n          Organization represents and warrants to the Province, as of the date of this Agreement and throughout its\n          term, that:\n        </p>\n\n        <ol type=\"a\">\n          <li>\n            the information contained in the Site Request for each Site is true and correct;\n          </li>\n          <li>\n            <p>\n              if Organization is not an individual:\n            </p>\n\n            <ol type=\"i\">\n              <li>\n                Organization has the power and capacity to enter into this Agreement and to comply with its terms;\n              </li>\n              <li>\n                all necessary corporate or other proceedings have been taken to authorize the execution and delivery of\n                this Agreement by, or on behalf of, Organization; and\n              </li>\n              <li>\n                this Agreement has been legally and properly executed by, or on behalf of, the Organization and is\n                legally binding upon and enforceable against Organization in accordance with its terms.\n              </li>\n            </ol>\n          </li>\n        </ol>\n      </li>\n      <li>\n        Organization must notify the Province at least seven (7) days in advance of any change to the information\n        contained in a Site Request, including any change to a Site’s status, location, normal operating hours,\n        Approved SSO, or the name and contact information of the Signing Authority or any of the other specific\n        roles set out in the Site Request. Such notices must be submitted to the Province in the form and manner\n        directed by the Province in its published instructions regarding the submission of updated Site Request\n        information, as such instructions may be updated from time to time by the Province.\n      </li>\n    </ol>\n  </li>\n</ol>\n\n<p class=\"text-center\">\n  <strong>ARTICLE 3 – SITE ACCESS REQUIREMENTS</strong>\n</p>\n\n<ol type=\"1\"\n    start=\"3\"\n    class=\"decimal\">\n  <li>\n    <ol type=\"1\">\n      <li>\n        Organization must comply with the Act and all applicable law.\n      </li>\n      <li>\n        Organization must submit a Site Request to the Province for each physical location where it intends to provide\n        Site Access, and must only provide Site Access from Sites approved in writing by the Province and only as\n        authorized by the Province.\n      </li>\n      <li>\n        Organization must only provide Site Access using SSO-Provided Technology.\n      </li>\n      <li>\n        Unless otherwise authorized by the Province in writing, Organization must at all times use the secure network\n        or security technology that the Province certifies or makes available to Organization for the purpose of Site\n        Access. The use of any such network or technology by Organization may be subject to terms and conditions of\n        use, including acceptable use policies, established by the Province and communicated to Organization from time\n        to time in writing (including through this Agreement), and Organization must comply with all such terms and\n        conditions of use.\n      </li>\n      <li>\n        <p>\n          Organization must only make Site Access available to the following individuals:\n        </p>\n\n        <ol type=\"a\">\n          <li>\n            Authorized Users when they are physically located at a Site;\n          </li>\n          <li>\n            Representatives of an Approved SSO for technical support purposes, in accordance with section 6 of the\n            <em>Information Management Regulation</em>.\n          </li>\n        </ol>\n      </li>\n      <li>\n        <p>\n          Organization must ensure that Authorized Users with Site Access:\n        </p>\n        <ol type=\"a\">\n          <li>\n            only access PharmaNet to the extent necessary to provide Services in accordance with the Service Contract;\n          </li>\n          <li>\n            first complete any mandatory training program(s) that the Site’s Approved SSO or the Province makes\n            available in relation to PharmaNet;\n          </li>\n          <li>\n            access PharmaNet using their own separate login identifications and credentials, and do not share or have\n            multiple use of any such login identifications and credentials;\n          </li>\n          <li>\n            secure all devices, codes and credentials that enable access to PharmaNet against unauthorized use;\n          </li>\n        </ol>\n      </li>\n      <li>\n        If notified by the Province that an Authorized User’s access to PharmaNet has been suspended or revoked,\n        Organization will immediately take any local measures necessary to remove the Authorized User’s Site Access.\n        Organization will only restore Site Access to a previously suspended or revoked Authorized User upon the\n        Province’s specific written direction.\n      </li>\n      <li>\n        <p>\n          For the purposes of this section:\n        </p>\n\n        <ol type=\"a\">\n          <li>\n            <strong>&quot;Responsible Authorized User&quot;</strong> means, in relation to any PharmaNet Data, the\n            Regulated User by whom that data was obtained from PharmaNet; and\n          </li>\n          <li>\n            <strong>&quot;Use&quot;</strong> includes to collect, access, retain, use, de-identify, and disclose.\n          </li>\n        </ol>\n\n        <p>\n          The PharmaNet Data disclosed under this Agreement is disclosed by the Province solely for the Use of the\n          Responsible Authorized User to whom it is disclosed.\n        </p>\n\n        <p>\n          Organization must not Use any PharmaNet Data, or permit any third party to Use PharmaNet Data, unless the\n          Responsible Authorized User has authorized such Use and it is otherwise permitted under the Act, applicable\n          law, and the limits and conditions imposed by the Province on the Responsible Authorized User.\n        </p>\n\n        <p>\n          Organization explicitly acknowledges that sections 24 and 25 of the Act apply to all PharmaNet Data.\n        </p>\n\n        <p>\n          This Agreement documents limits and conditions, set by the Minister in writing, that the Act requires\n          Organization and Authorized Users to comply with.\n        </p>\n      </li>\n      <li>\n        <p>\n          Organization must make all reasonable arrangements to protect PharmaNet Data against such risks as\n          unauthorized access, collection, use, modification, retention, disclosure or disposal, including by:\n        </p>\n\n        <ol type=\"a\">\n          <li>\n            taking all reasonable physical, technical and operational measures necessary to ensure Site Access operates\n            in accordance with sections 3.1 to 3.9 above, and\n          </li>\n          <li>\n            complying with the requirements of Schedule A.\n          </li>\n        </ol>\n      </li>\n      <li>\n        Organization must ensure that no Authorized User submits Claims on PharmaNet other than from a Site in respect\n        of which\n        a person is enrolled as a Provider.\n      </li>\n    </ol>\n  </li>\n</ol>\n\n<p class=\"text-center\">\n  <strong>ARTICLE 4 – NON-COMPLIANCE AND INVESTIGATIONS</strong>\n</p>\n\n<ol type=\"1\"\n    start=\"4\"\n    class=\"decimal\">\n  <li>\n    <ol type=\"1\">\n      <li>\n        Organization must promptly notify the Province, and provide particulars, if Organization does not comply, or\n        anticipates that it will be unable to comply, with the terms of this Agreement, or if Organization has knowledge\n        of any circumstances, incidents or events which have or may jeopardize the security, confidentiality or\n        integrity of PharmaNet, including any attempt by any person to gain unauthorized access to PharmaNet or the\n        networks or equipment used to connect to PharmaNet or convey PharmaNet Data.\n      </li>\n      <li>\n        Organization must immediately investigate any suspected breaches of this Agreement and take all reasonable steps\n        to prevent recurrences of any such breaches.\n      </li>\n      <li>\n        Organization must cooperate with any audits or investigations conducted by the Province (including any\n        independent auditor appointed by the Province) regarding compliance with this Agreement, including by providing\n        access upon request to a Site and any associated facilities, networks, equipment, systems, books, records and\n        personnel for the purposes of such audit or investigation.\n      </li>\n    </ol>\n  </li>\n</ol>\n\n<p class=\"text-center\">\n  <strong>ARTICLE 5 – SITE TERMINATION</strong>\n</p>\n\n<ol type=\"1\"\n    start=\"5\"\n    class=\"decimal\">\n  <li>\n    <ol type=\"1\">\n      <li>\n        <p>\n          The Province may terminate all Site Access at a Site immediately, upon notice to the Signing Authority for\n          the Site, if:\n        </p>\n\n        <ol type=\"a\">\n          <li>\n            the Approved SSO for the Site is no longer approved by the Province to provide information technology\n            hardware, software, or service in connection with PharmaNet, or\n          </li>\n          <li>\n            the Province determines that the SSO-Provided Technology or Associated Technology in use at the Site, or\n            any component thereof, is obsolete, unsupported, or otherwise poses an unacceptable security risk to\n            PharmaNet and Organization is unable or unwilling to remedy the problem within a time frame acceptable to\n            the Province.\n          </li>\n        </ol>\n      </li>\n      <li>\n        As a security precaution, the Province may suspend Site Access at a Site after a period of inactivity. If Site\n        Access at a Site remains inactive for a period of 90 days or more, the Province may, immediately upon notice to\n        the Signing Authority for the Site, terminate all further Site Access at the Site.\n      </li>\n      <li>\n        Organization must prevent all further Site Access at a Site immediately upon the Province’s termination, in\n        accordance with this Article 5 of Site Access at the Site.\n      </li>\n    </ol>\n  </li>\n</ol>\n\n<p class=\"text-center\">\n  <strong>ARTICLE 6 – TERM AND TERMINATION</strong>\n</p>\n\n<ol type=\"1\"\n    start=\"6\"\n    class=\"decimal\">\n  <li>\n    <ol type=\"1\">\n      <li>\n        <p>\n          The term of this Agreement begins on the date first noted above and continues until the earliest of:\n        </p>\n\n        <ol type=\"a\">\n          <li>\n            the expiration or earlier termination of the term of the Service Contract; or\n          </li>\n          <li>\n            the date this Agreement is terminated in accordance with this Article 6.\n          </li>\n        </ol>\n      </li>\n      <li>\n        Organization may terminate this Agreement at any time on notice to the Province.\n      </li>\n      <li>\n        The Province may terminate this Agreement immediately upon notice to Organization if Organization fails to\n        comply with any provision of this Agreement.\n      </li>\n      <li>\n        The Province may terminate this Agreement immediately upon notice to Organization in the event Organization no\n        longer operates any Sites where Site Access is permitted.\n      </li>\n      <li>\n        The Province may terminate this Agreement for any reason upon two (2) months advance notice to Organization.\n      </li>\n      <li>\n        Organization must prevent any further Site Access immediately upon expiration or termination of the term of\n        this Agreement.\n      </li>\n    </ol>\n  </li>\n</ol>\n\n<p class=\"text-center\">\n  <strong>ARTICLE 7 – DISCLAIMER AND INDEMNITY</strong>\n</p>\n\n<ol type=\"1\"\n    start=\"7\"\n    class=\"decimal\">\n  <li>\n    <ol type=\"1\">\n      <li>\n        The PharmaNet access and PharmaNet Data provided under this Agreement are provided \"as is\" without warranty of\n        any kind, whether express or implied. All implied warranties, including, without limitation, implied warranties\n        of merchantability, fitness for a particular purpose, and non-infringement, are hereby expressly disclaimed.\n        The Province does not warrant the accuracy, completeness or reliability of the PharmaNet Data or the\n        availability of PharmaNet, or that access to or the operation of PharmaNet will function without error,\n        failure or interruption.\n      </li>\n      <li>\n        Under no circumstances will the Province be liable to any person or business entity for any direct, indirect,\n        special, incidental, consequential, or other damages based on any use of PharmaNet or the PharmaNet Data,\n        including without limitation any lost profits, business interruption, or loss of programs or information, even\n        if the Province has been specifically advised of the possibility of such damages.\n      </li>\n    </ol>\n  </li>\n</ol>\n\n<p class=\"text-center\">\n  <strong>ARTICLE 8 – GENERAL</strong>\n</p>\n\n<ol type=\"1\"\n    start=\"8\"\n    class=\"decimal\">\n  <li>\n    <ol type=\"1\">\n      <li>\n        <p>\n          <strong class=\"underline\">Notice.</strong> Except where this Agreement expressly provides for another method\n          of delivery, any notice to be given to the Province must be in writing and mailed or emailed to:\n        </p>\n\n        <address>\n          Director, Information and PharmaNet Innovation<br>\n          Ministry of Health<br>\n          PO Box 9652, STN PROV GOVT<br>\n          Victoria, BC V8W 9P4<br>\n\n          <br>\n\n          <a href=\"mailto:PRIMESupport@gov.bc.ca\">PRIMESupport@gov.bc.ca</a>\n        </address>\n\n        <p>\n          Any notice to be given to a Signing Authority or the Organization will be in writing and emailed, mailed,\n          faxed or text-messaged to the Signing Authority (in the case of notice to a Signing Authority) or all Signing\n          Authorities (in the case of notice to the Organization). A Signing Authority may be required to click a URL\n          link or to log in to the Province’s \"PRIME\" system to receive the content of any such notice.\n        </p>\n\n        <p>\n          Any written notice from a party, if sent electronically, will be deemed to have been received 24 hours after\n          the time the notice was sent, or, if sent by mail, will be deemed to have been received 3 days (excluding\n          Saturdays, Sundays and statutory holidays) after the date the notice was sent.\n        </p>\n      </li>\n      <li>\n        <strong class=\"underline\">Waiver.</strong> The failure of the Province at any time to insist on performance of\n        any provision of this Agreement by Organization is not a waiver of its right subsequently to insist on\n        performance of that or any other provision of this Agreement. A waiver of any provision or breach of this\n        Agreement is effective only if it is writing and signed by, or on behalf of, the waiving party.\n      </li>\n      <li>\n        <strong class=\"underline\">Modification.</strong> No modification to this Agreement is effective unless it is\n        in writing and signed by, or on behalf of, the parties.\n      </li>\n      <li>\n        <p>\n          <strong class=\"underline\">Governing Law.</strong> This Agreement will be governed by and will be construed\n          and interpreted in accordance with the laws of British Columbia and the laws of Canada applicable therein.\n        </p>\n      </li>\n      <li>\n        <p>\n          <strong class=\"underline\">Survival.</strong> Sections 3.1, 3.8, 3.9, 4, 7 and any other provision of this\n          Agreement that expressly or by its nature continues after termination, shall survive termination of this\n          Agreement.\n        </p>\n      </li>\n    </ol>\n  </li>\n</ol>\n\n{{signature_block}}\n\n<p class=\"text-center break-before\">\n  <strong>SCHEDULE A – SPECIFIC PRIVACY AND SECURITY MEASURES</strong>\n</p>\n\n<p>\n  Organization must, in relation to each Site\n</p>\n\n<ol type=\"a\">\n  <li>\n    secure all workstations and printers at the Site to prevent any viewing of PharmaNet Data by persons other than\n    Authorized Users;\n  </li>\n  <li>\n    <p>\n      implement all privacy and security measures specified in the following documents published by the Province, as\n      amended from time to time:\n    </p>\n\n    <ol type=\"i\">\n      <li>\n        <p>\n          the PharmaNet Professional and Software Conformance Standards\n        </p>\n\n        <a href=\"https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards\"\n           target=\"_blank\"\n           rel=\"noopener noreferrer\">\n          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards\n        </a>\n      </li>\n      <li>\n        <p>\n          Office of the Chief Information Officer: \"Submission for Technical Security Standard and High Level\n          Architecture for Wireless Local Area Network Connectivity\"\n        </p>\n\n        <a href=\"https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet\"\n           target=\"_blank\"\n           rel=\"noopener noreferrer\">\n          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet\n        </a>\n      </li>\n    </ol>\n  </li>\n  <li>\n    ensure that a qualified technical support person is engaged to provide security support for the Site. This person\n    should be familiar with the Site’s network configurations, hardware and software, including all SSO-Provided\n    Technology and Associated Technology, and should be capable of understanding and adhering to the standards set\n    forth in this Agreement and Schedule. Note that any such qualified technical support person must not be permitted\n    by Organization to access or use PharmaNet in any manner, unless otherwise permitted under this Agreement;\n  </li>\n  <li>\n    establish and maintain documented privacy policies that detail how Organization will meet its privacy obligations\n    in relation to the Site;\n  </li>\n  <li>\n    establish breach reporting and response processes in relation to the Site;\n  </li>\n  <li>\n    detail expectations for privacy protection in contracts and service agreements as applicable at the Site;\n  </li>\n  <li>\n    regularly review the administrative, physical and technological safeguards at the Site;\n  </li>\n  <li>\n    establish and maintain a program for monitoring PharmaNet use at the Site, including by making appropriate\n    monitoring and reporting mechanisms available to Authorized Users for this purpose.\n  </li>\n</ol>\n");
        }
    }
}
