using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationClaimDenailEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "Description", "EmailType", "ModifiedDate", "Recipient", "Subject", "Template", "TemplateName", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 24, new DateTimeOffset(new DateTime(2026, 6, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "The email will be triggered when PRIME admin deny a organization claim.", 24, new DateTimeOffset(new DateTime(2026, 6, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "To: New SA", "Organization Claim was Denied", "Hello, <p><p>Your claim of the organization @Model.OrganizationName, which the site with SiteID/PEC @Model.ProvidedSiteId is part of, has been denied. Please contact PRIMESupport@gov.bc.ca for further information. <p><p> Thank you, <p><p> PRIME Support Team<br/>1-844-397-7463<br/> PRIMESupport@gov.bc.ca", "Organization Claim Denial Notification", new DateTimeOffset(new DateTime(2026, 6, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 24);
        }
    }
}
