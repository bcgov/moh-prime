using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddedPaperSubmissionTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PharmanetTransactionLog_TransactionId",
                table: "PharmanetTransactionLog");

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 11,
                column: "Template",
                value: "<p> Your site registration has been approved. The site must now be set up and activated in PharmaNet. Your PharmaNet software vendor will be notified when the site has been activated, and you will hear from them when you can start to use PharmaNet. </p> <p> Individuals who will be accessing PharmaNet at your site should enrol in PRIME now if they have not already done so. For more information, please visit <a href=\"https://www.gov.bc.ca/pharmanet/PRIME\" target=\"_blank\">https://www.gov.bc.ca/pharmanet/PRIME</a>. [for private community practice only: If you have registered any physicians or nurse practitioners for remote access to PharmaNet, they must enroll in PRIME before they use remote access, which they can do here: <a href=\"https://pharmanetenrolment.gov.bc.ca\" target=\"_blank\">https://pharmanetenrolment.gov.bc.ca</a>. You must not permit remote use of PharmaNet until these users are approved in PRIME.] </p> <p> If you have any questions or concerns, please phone 1-844-397-7463 or email <a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>. </p> <p> Thank you. </p>");

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 12,
                column: "Template",
                value: "<p> The site you registered in PRIME, @Model.DoingBusinessAs, has been approved by the Ministry of Health. Your SiteID is @Model.Pec. </p> <p> Health Insurance BC has been notified of the site’s approval and will contact your software vendor. Your vendor will complete any remaining setup for your site and may contact you or the PharmaNet Administrator at your site. </p> <p> If you need to update any information in PRIME regarding your site, you may log in at any time using your mobile BC Services Card. If you have any questions or concerns, please phone 1-844-397-7463 or email <a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>. </p> <p> Thank you. </p>");

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 16,
                column: "Template",
                value: "Thank you for registering your site (SiteID: @Model.Pec) in PRIME. If you need to update any site information in PRIME, you may log in at any time using your mobile BC Services Card. If you have any questions, please phone 1 - 844 - 397 - 7463 or email <a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>. Thank you.");

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "EmailType", "ModifiedDate", "Template", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 17, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 17, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "Your request for PharmaNet access has been approved and recorded in PRIME. When it is possible for you to do so, you must enrol in PRIME using your mobile BC Services Card. <br> <br> <strong> Your temporary GPID is @Model.GPID. </strong> <br> <br> The first time you log into PRIME you should be asked if you have previously received permission to access PharmaNet via an offline process. If you do not see this prompt, please stop your enrollment and contact <a href=\"mailto:PRIMEsupport@gov.bc.ca", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.CreateIndex(
                name: "IX_PharmanetTransactionLog_TransactionId",
                table: "PharmanetTransactionLog",
                column: "TransactionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PharmanetTransactionLog_TransactionId",
                table: "PharmanetTransactionLog");

            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 11,
                column: "Template",
                value: "<p> Your site registration has been approved. The site must now be set up and activated in PharmaNet. Your PharmaNet software vendor will be notified when the site has been activated, and you will hear from them when you can start to use PharmaNet. </p> <p> Individuals who will be accessing PharmaNet at your site should enrol in PRIME now if they have not already done so. For more information, please visit <a href=\"https://www.gov.bc.ca/pharmanet/PRIME\" target=\"_blank\">https://www.gov.bc.ca/pharmanet/PRIME</a>. [for private community practice only: If you have registered any physicians or nurse practitioners for remote access to PharmaNet, they must enroll in PRIME before they use remote access, which they can do here: <a href=\"https://pharmanetenrolment.gov.bc.ca\" target=\"_blank\">https://pharmanetenrolment.gov.bc.ca</a>. You must not permit remote use of PharmaNet until these users are approved in PRIME.] </p> <p> If you have any questions or concerns, please phone 1-844-397-7463 or email <a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMESupport@gov.bc.ca</a>. </p> <p> Thank you. </p>");

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 12,
                column: "Template",
                value: "<p> The site you registered in PRIME, @Model.DoingBusinessAs, has been approved by the Ministry of Health. Your SiteID is @Model.Pec. </p> <p> Health Insurance BC has been notified of the site’s approval and will contact your software vendor. Your vendor will complete any remaining setup for your site and may contact you or the PharmaNet Administrator at your site. </p> <p> If you need to update any information in PRIME regarding your site, you may log in at any time using your mobile BC Services Card. If you have any questions or concerns, please phone 1-844-397-7463 or email PRIMESupport@gov.bc.ca. </p> <p> Thank you. </p>");

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 16,
                column: "Template",
                value: "Thank you for registering your site (SiteID: @Model.Pec) in PRIME. If you need to update any site information in PRIME, you may log in at any time using your mobile BC Services Card. If you have any questions, please phone 1 - 844 - 397 - 7463 or email PRIMESupport@@gov.bc.ca. Thank you.");

            migrationBuilder.CreateIndex(
                name: "IX_PharmanetTransactionLog_TransactionId",
                table: "PharmanetTransactionLog",
                column: "TransactionId");
        }
    }
}
