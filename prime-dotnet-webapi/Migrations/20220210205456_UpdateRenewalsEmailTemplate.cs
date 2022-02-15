using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class UpdateRenewalsEmailTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 8,
                column: "Template",
                value: "@{ var renewalDate = Model.RenewalDate.Date.ToShortDateString(); }Dear @Model.EnrolleeName, <br> <br> It is a requirement of PharmaNet that your PRIME information remains current. To continue to use PharmaNet, renew your enrolment information by @renewalDate. This can be done quickly:<br> <br><ul><li>Log in to PRIME <a href=\"@Model.PrimeUrl\">@Model.PrimeUrl</a></li><li>Review your information and update where needed</li><li>Re-read and accept the PharmaNet user terms of access</li><li>Submit your enrolment</li></ul>In some cases, you will need to share your PRIME enrolment email with the person who sets up PharmaNet accounts at your workplace (PharmaNet administrator), as you did when you first enrolled.<br> <br>Share your approval notification with your PharmaNet administrator if:<br> <br><ul><li>You changed workplaces or care setting (new clinic, health authority, etc.) since you last updated your account (or you updated and did not share the approval notification with your new PharmaNet administrator)</a></li><li>You originally enrolled in PRIME before February 1, 2022 as a nurse (LPN, RN, or RPN), pharmacy technician or midwife</li></ul>Thank you, <br> <br>PRIME Support <br>1-844-397-7463<br> <br><a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>");

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 10,
                column: "Template",
                value: "Dear @Model.EnrolleeName,<br> <br>You did not renew your PRIME enrolment in time. PRIME will instruct your PharmaNet software vendor to de-activate your account. You may not use PharmaNet without being enrolled in PRIME. Any access will be recorded as unauthorized.<br> <br><strong>You can re-enrol in PRIME</strong> if you need PharmaNet to care for patients.<br> <br>Thank you,<br> <br>PRIME Support <br>1-844-397-7463<br> <br><a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 8,
                column: "Template",
                value: "@{ var renewalDate = Model.RenewalDate.Date.ToShortDateString(); } Dear @Model.EnrolleeName, <br> <br> Your enrolment for PharmaNet access will expire on @renewalDate. In order to continue to use PharmaNet, you must renew your enrolment information. <br> Click here to visit PRIME. <a href=\"@Model.PrimeUrl\">@Model.PrimeUrl</a>");

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 10,
                column: "Template",
                value: "Dear @Model.EnrolleeName, <br> <br> Your enrolment has not been renewed. PRIME will be notifying your PharmaNet software vendor to deactivate your account.");
        }
    }
}
