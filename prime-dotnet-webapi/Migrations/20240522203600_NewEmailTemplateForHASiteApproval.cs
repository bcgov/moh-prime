using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class NewEmailTemplateForHASiteApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "Description", "EmailType", "ModifiedDate", "Recipient", "Subject", "Template", "TemplateName", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 23, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "The email will be triggered when a HA site is approved.", 23, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "To: HLTH.HnetConnection@gov.bc.ca", "[{siteId}] HA Site Registration Approved", "Hello, <p><p>@Model.DoingBusinessAs (@Model.HealthAuthority) with SiteID @Model.Pec has been approved by the Ministry of Health for PharmaNet access. Please notify the PharmaNet software vendor (@Model.Vendor) for this site and complete any remaining tasks to activate the site.</p><p>Please connect by phone or email if you have any questions. <br/><br/>Thank you, <br/><br/>PRIME Support team <br/>1-844-397-7463<br/> PRIMESupport@gov.bc.ca", "HA Site Approval", new DateTimeOffset(new DateTime(2023, 10, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 23);
        }
    }
}
