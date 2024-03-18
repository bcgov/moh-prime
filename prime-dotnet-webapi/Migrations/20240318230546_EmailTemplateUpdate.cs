using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class EmailTemplateUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PharmanetTransactionLogTemp");

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ModifiedDate", "Template", "UpdatedTimeStamp" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 3, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Hello,<p><p>The Ministry of Health has been notified that you require remote access to PharmaNet at:<br/> <br/> Organization name: @Model.OrganizationName <br/> Site address: @Model.SiteStreetAddress, @Model.SiteCity <br/> <br/>To access PharmaNet remotely, you must enrol in PRIME and indicate that you require remote access. If you have already enrolled in PRIME, you must log into PRIME and add remote access for this clinic to your profile. <p><p>**Remote access is only available for practitioners on an exceptional basis, to care for patients of a clinic that has identified the practitioner in the clinic's PRIME site registration. Remote access means you are physically located outside the premises of an approved PharmaNet site. <span class=\"text-danger\">You must always be physically located in BC when using PharmaNet, even if approved for remote access.</span><p>Remote access to PharmaNet when working for a health authority is managed by that health authority, outside of PRIME. Please contact your health authority.</p> <p>If you do not require remote access to the site identified above, please advise the site so they can remove you from their remote user list in PRIME.</p><p>Once you have logged into to PRIME:</p><p> - If you are enrolling for the first time, click the toggle to request remote access if you require it. </ p >< p > -If you are updating your existig PRIME profile, click the<strong>Edit Remote Access</ strong > button or pencil icon for that section, then click the toggle to request remote access </ p >< p > -Click Save and Continue.Refer to < a href =\"https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/pharmacare/pharmanet-bc-s-drug-information-network/prime/prime-user-guides#update\">update information or renew enrolment</a> for instructions. </p><p>You can enrol or update your profile at <a href=\"@Model.PrimeUrl\">@Model.PrimeUrl</a>.</p> Please connect by email if you have any questions. <br/><br/>Thank you, <br/><br/>PRIME Support team <br/> PRIMESupport@gov.bc.ca</br> <a href=\"https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/pharmacare/pharmanet-bc-s-drug-information-network/prime/prime-user-guides\">PRIME user guides</a>", new DateTimeOffset(new DateTime(2024, 3, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ModifiedDate", "Template", "UpdatedTimeStamp" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 3, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "@{ var renewalDate = Model.RenewalDate.Date.ToShortDateString(); } <p>Hello  @Model.EnrolleeName,</p><p>  If you still need PharmaNet access you must renew your PRIME enrolment annually. </p><p>Please renew by @renewalDate.</p><ol><li>Log in to PRIME <a href='@Model.PrimeUrl'>@Model.PrimeUrl</a>.</li><li>Click <b>Access Individual Enrolment</b>.</li><li>On the PRIME Profile screen, review your information.<ol style='list-style-type:lower-alpha;'><li>Select the edit button or pencil icon to revise sections that are out of date. Click <strong>Continue</strong> at the bottom of updated screens to save changes.</li><li>Once changes are saved, check the <strong>\"I certify\"</strong> box and click the <strong>Submit Enrolment</strong> button.</li><li>If instructed to go on, click <b>Continue</b>, then go to the next screen to review and accept the PharmaNet user terms of access.  The terms of access may have changed, so please read carefully.</li></ol>This step completes your renewal and turns off notices until your next renewal cycle.</li> <li>If your renewal is sent for review, you will either be contacted by the PRIME Support team or notified to log in to PRIME to complete the remaining steps.</li><li> You may need to share your PRIME approval email with the person or team in your workplace who sets up PharmaNet accounts if something has changed since you last updated your account; for example: name change, access type, care setting, or college license information. You do this by entering their email address(es) in the line for the relevant care setting on the next steps to get PharmaNet screen and clicking the send button. <ol style='list-style-type:lower-alpha;'> </li></ol></li></ol><p>Please connect by phone or email if you have any questions. <br/>Thank you for renewing your PRIME enrolment.</p> <br/>PRIME Support team <br/>1-844-397-7463<br/> PRIMESupport@gov.bc.ca <p><a href=\"https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/pharmacare/pharmanet-bc-s-drug-information-network/prime/prime-user-guides\">PRIME user guides</a>", new DateTimeOffset(new DateTime(2024, 3, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PharmanetTransactionLogTemp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CollegePrefix = table.Column<string>(type: "text", nullable: true),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LocationIpAddress = table.Column<string>(type: "text", nullable: true),
                    PharmacyId = table.Column<string>(type: "text", nullable: true),
                    PractitionerId = table.Column<string>(type: "text", nullable: true),
                    ProviderSoftwareId = table.Column<string>(type: "text", nullable: true),
                    ProviderSoftwareVersion = table.Column<string>(type: "text", nullable: true),
                    SourceIpAddress = table.Column<string>(type: "text", nullable: true),
                    TransactionId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionOutcome = table.Column<string>(type: "text", nullable: true),
                    TransactionSubType = table.Column<string>(type: "text", nullable: true),
                    TransactionType = table.Column<string>(type: "text", nullable: true),
                    TxDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmanetTransactionLogTemp", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ModifiedDate", "Template", "UpdatedTimeStamp" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "Hello,<p><p>The Ministry of Health has been notified that you require remote access to PharmaNet at:<br/> <br/> Organization name: @Model.OrganizationName <br/> Site address: @Model.SiteStreetAddress, @Model.SiteCity <br/> <br/>To access PharmaNet remotely, you must enrol in PRIME and indicate that you require remote access. If you have already enrolled in PRIME, you must log into PRIME and add remote access to your profile. Refer to <a href=\"https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/pharmacare/pharmanet-bc-s-drug-information-network/prime/prime-user-guides\">PRIME user guides</a> for instructions. </p><p>You can enrol or update your profile at <a href=\"@Model.PrimeUrl\">@Model.PrimeUrl</a>.</p> Please connect by phone or email if you have any questions. <br/><br/>Thank you, <br/><br/>PRIME Support team <br/>1-844-397-7463<br/> PRIMESupport@gov.bc.ca", new DateTimeOffset(new DateTime(2023, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ModifiedDate", "Template", "UpdatedTimeStamp" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "@{ var renewalDate = Model.RenewalDate.Date.ToShortDateString(); } <p>Hello  @Model.EnrolleeName,</p><p>It is time to renew your PRIME enrolment. If you still need PharmaNet to care for patients, you must ensure that your PRIME profile is current. Update/ renew your profile  and sign the terms of access every year. </p><p>Please renew by @renewalDate.</p><ol><li>Log in to PRIME <a href='@Model.PrimeUrl'>@Model.PrimeUrl</a>.</li><li>Click <b>Renew or Update Information</b> (top of screen).</li><li>On the PRIME Profile screen, review your information.<ol style='list-style-type:lower-alpha;'><li>Select the edit button to revise sections that are out of date. Click <strong>Continue</strong> at the bottom of updated screens to save changes.</li><li>Once changes are saved, <strong> check the \"I certify\" box and click the submit button</strong>.</li></ol></li><li>If instructed to go on, click <b>Continue.</b></li><li>If you are notified that your renewal is approved, go to the next screen to review and accept the PharmaNet user terms of access. This step completes your renewal. The terms of access may have changed, so please read carefully.</li> If your renewal is sent for review, you will either be contacted by the PRIME Support team or notified to log in to PRIME to complete the remaining steps.<li> You will be prompted to share your renewal approval with the person or team in your workplace who sets up PharmaNet accounts if you changed workplaces or care setting (new clinic, health authority, etc.) since you last updated your account, or you did not share the approval notification with your PharmaNet administrator earlier. You do this by entering their email address(es) and clicking the send button. <ol style='list-style-type:lower-alpha;'> </li></ol></li></ol><p>Please connect by phone or email if you have any questions. <br/>Thank you for renewing your PRIME enrolment.</p> </p> <br/><br/>PRIME Support team <br/>1-844-397-7463<br/> PRIMESupport@gov.bc.ca", new DateTimeOffset(new DateTime(2023, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
