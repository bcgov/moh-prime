using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class ForceRenewal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpiryReason",
                table: "Agreement",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "EmailType", "ModifiedDate", "Template", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 20, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 20, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "Hello @Model.EnrolleeName, <br> <br> You are receiving this message as your PharmaNet user status has changed from on-behalf-of user to independent user following changes to legislation governing access to PharmaNet. <br> <br> Being an independent PharmaNet user means you will now access PharmaNet as yourself instead of on behalf of another practitioner. <br> <br> Log back in to PRIME by @Model.RenewalDate.ToString(\"d MMMM yyyy\") to confirm your profile information and review and accept the user terms of access for independent users. These will automatically be presented to you. This is a requirement for you to maintain access to PharmaNet, as the new terms of access are quite different from those you may have accepted earlier. <br> <br> For information about PRIME, visit the <a href=\"@Model.PrimeUrl\">PRIME web page</a>.  If you have questions or difficulties using PRIME, please contact us at the email address below.<br> <br> Thank you, <br> <br> PRIME Support<br><a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 21, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 21, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "Hello @Model.EnrolleeName, <br> <br> You are receiving this message as your PharmaNet user status has changed from on-behalf-of user to independent user following changes to legislation governing access to PharmaNet. <br> <br> Being an independent user of PharmaNet means you now will access PharmaNet as yourself instead of on behalf of another practitioner. <br> <br> <b>This is your last day</b> to log back in to PRIME to confirm your profile information and review and accept the new user terms of access for independent users. These will automatically be presented to you. This is a requirement for you to maintain access to PharmaNet. <br> <br> For information about PRIME, visit the <a href=\"@Model.PrimeUrl\">PRIME web page</a>.  If you have questions or difficulties using PRIME, please contact us at the email address below.<br> <br> Thank you, <br> <br> PRIME Support<br><a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMEsupport@gov.bc.ca</a>", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DropColumn(
                name: "ExpiryReason",
                table: "Agreement");
        }
    }
}
