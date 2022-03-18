using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateRenewalEmailTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 8,
                column: "Template",
                value: "Hello @Model.EnrolleeName,<br><br><p>Your annual renewal of enrolment for access to PharmaNet does not appear to have been completed.</p><br><p>Once a year, you must return to PRIME, update your information if needed, and sign the terms of access again.</p><br><p>You must complete the renewal to retain your access to PharmaNet.</p><br><p>Please email <a href=\"mailto:PRIMESupport@gov.bc.ca\" target=\"_top\">PRIMESupport@gov.bc.ca</a> identifying which situation below applies to you, and the results of taking any recommended action:</p><ol><li><p>I am not able to use the BC Services Card app to log in to PRIME.</p><p>Action: Contact BC Services Card Help Desk:</p><ul><li>1-888-356-2741 (Canada and USA toll-free)</li><li>604-660-2355 (Lower Mainland or outside Canada and USA)</li></ul></li><li><p>I have not signed my PharmaNet Terms of Access this year.</p><p>Action: Log in to your profile in PRIME and review and sign your terms of access.</p>	</li>	<li><p>I’m not able to complete my enrolment because of an issue in PRIME.</p><p>Action: With your email, include a screenshot of the issue in PRIME.</p></li><li><p>I completed my enrolment, but PRIME says it is under review.</p><p>Action: You will hear from PRIME Support with any questions, or receive a notice from PRIME when it is time to complete the next step. Let us know in your email when you completed your enrolment.</p></li><li><p>I no longer require access to PharmaNet</p><p>Action: Log in to your profile in PRIME, and indicate an absence from PharmaNet.</p><ul><li>On the PRIME Profile page, select Absence Management from the menu on the lefthand side of the page.</li><li>In the “Enter a date range” box, use the calendar icon to enter the first day you no longer require access to PharmaNet. The date can be in the past, but do not enter a date earlier than the last time you used PharmaNet.</li><li>Leave the end date blank if you don’t intend to use PharmaNet again. If you know a date when you will return to using PharmaNet, enter as the end date.</li><li>	Click Submit.</li><li>Enter the email address of the person who sets up PharmaNet user accounts at your clinic/pharmacy/facility/HA, and share your approval. PRIME will notify them of the date(s) when you will not be using PharmaNet.</li></ul></li><li><p>Other</p><p>Action: Explain fully in your email.</p>	</li></ol><p>	Your access to PharmaNet may be deactivated if we don’t hear from you within 4 calendar days.</p><p>If that happens and you require access, please contact <a href=\"mailto:PRIMESupport@gov.bc.ca\" target=\"_top\">PRIMESupport@gov.bc.ca</a> for instructions.</p><br><p>Thank you,</p><br>PRIME Support");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 8,
                column: "Template",
                value: "@{ var renewalDate = Model.RenewalDate.Date.ToShortDateString(); }Dear @Model.EnrolleeName, <br> <br> It is a requirement of PharmaNet that your PRIME information remains current. To continue to use PharmaNet, renew your enrolment information by @renewalDate. This can be done quickly:<br> <br><ul><li>Log in to PRIME <a href=\"@Model.PrimeUrl\">@Model.PrimeUrl</a></li><li>Review your information and update where needed</li><li>Re-read and accept the PharmaNet user terms of access</li><li>Submit your enrolment</li></ul>In some cases, you will need to share your PRIME enrolment email with the person who sets up PharmaNet accounts at your workplace (PharmaNet administrator), as you did when you first enrolled.<br> <br>Share your approval notification with your PharmaNet administrator if:<br> <br><ul><li>You changed workplaces or care setting (new clinic, health authority, etc.) since you last updated your account (or you updated and did not share the approval notification with your new PharmaNet administrator)</a></li><li>You originally enrolled in PRIME before February 1, 2022 as a nurse (LPN, RN, or RPN), pharmacy technician or midwife</li></ul>Thank you, <br> <br>PRIME Support <br>1-844-397-7463<br> <br><a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>");
        }
    }
}
