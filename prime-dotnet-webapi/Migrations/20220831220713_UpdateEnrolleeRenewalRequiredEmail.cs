using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateEnrolleeRenewalRequiredEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 8,
                column: "Template",
                value: "@{ var renewalDate = Model.RenewalDate.Date.ToShortDateString(); } <p>Dear @Model.EnrolleeName,</p><p>It is a requirement, to retain access to PharmaNet, that your PRIME information remains current. PharmaNet users must renew their PRIME enrolment every year.</p><p>To continue to use PharmaNet, please renew your enrolment information by @renewalDate. This can be done quickly:</p><ol><li>Log in to PRIME <a href='@Model.PrimeUrl'>@Model.PrimeUrl</a>.</li><li>Click <b>Renew or Update Information</b> (top of screen).</li><li>On the PRIME Profile screen, review your information.<ol style='list-style-type:lower-alpha;'><li>Use the pencil icon to the right to edit sections that are out of date. Click <b>Continue</b> at the bottom of updated screens to save changes.</li><li>Once changes are saved, <b>certify and submit</b> at the bottom of the PRIME Profile page.</li></ol></li><li>If instructed to go on, click <b>Continue</b></li><li>Most renewals will be approved automatically*. If you are notified that your renewal is approved, go to the next page to review and accept the PharmaNet user terms of access. This step completes renewal. Note that the terms of access may have changed, so please read carefully.</li><li>You will next be prompted to share your renewal approval with the person or team in your workplace who setsup PharmaNet accounts. You do this by entering their email address(es). <b>Only share the renewal approval if:</b><ol style='list-style-type:lower-alpha;'><li>You changed workplaces or care setting (new clinic, health authority, etc.) since you last updated your account, oryou updated your account previously and at that time did not share the approval notification with your PharmaNet administrator, and/or</li><li>You previously enrolled as an RN, RPN, LPN or midwife, and have since been issued a PharmaNet ID by BCCNM. Your access type may have changed, and the people in your workplace who set up PharmaNet access need to know this</li></ol></li></ol><p>If your renewal is sent for review, you will be notified when you can complete the remaining steps.</p><p>Thank you for renewing your PRIME enrolment,</p><p>PRIME Support</p><p>1-844-397-7463</p>");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 8,
                column: "Template",
                value: "@{ var renewalDate = Model.RenewalDate.Date.ToShortDateString(); } <p>Dear @Model.EnrolleeName,</p> <p> To continue to use PharmaNet, please renew your enrolment information by @renewalDate. </p> <p> It is a requirement of your access to PharmaNet that your PRIME information remains current. PharmaNet users must renew their PRIME enrolment every year. </p> <p> Renewal can be done quickly: </p> <ol><li>Log in to PRIME @Model.PrimeUrl</li><li>Review your information and update where needed</li><li>Submit your enrolment</li></ol> <p> Most users will be processed automatically. If you see an approval notice, go to the next page to review and accept the PharmaNet user terms of access. This step completes renewal. You will be prompted to share your approval email. You do not need to share it unless: </p> <ul><li>You changed workplaces or care setting (new clinic, health authority, etc.) since you last updated your account (or you updated your account and at that time did not share the approval notification with your new PharmaNet administrator), and/or</li><li>You originally enrolled in PRIME before February 1, 2022 as a nurse (LPN, RN, or RPN), pharmacy technician or midwife</li></ul> <p> If your renewal is sent for review, you will either receive an email from the PRIME team or a notice from the PRIME application when you can complete the remaining steps. </p> <p> Thank you for renewing your PRIME enrolment! </p> <p> PRIME Support </p> <p> 1-844-397-7463 </p>");
        }
    }
}
