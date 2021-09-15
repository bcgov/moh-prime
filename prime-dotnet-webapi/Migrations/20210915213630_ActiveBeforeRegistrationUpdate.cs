using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class ActiveBeforeRegistrationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 16,
                column: "Template",
                value: "Thank you for registering your site (SiteID: @Model.Pec) in PRIME. If you need to update any site information in PRIME, you may log in at any time using your mobile BC Services Card. If you have any questions, please phone 1 - 844 - 397 - 7463 or email PRIMESupport@@gov.bc.ca. Thank you.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 16,
                column: "Template",
                value: @"Thank you for registering your site (SiteID: @Model.Pec) in PRIME.
 If you need to update any site information in PRIME, you may log in at any time using your mobile BC Services Card.If you have any questions, please phone 1 - 844 - 397 - 7463 or email PRIMESupport @gov.bc.ca.
 Thank you.");
        }
    }
}
