using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddedPaperSubmissionEmailTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "EmailType", "ModifiedDate", "Template", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 17, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 17, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "Your request for PharmaNet access has been approved and recorded in PRIME. When it is possible for you to do so, you must enrol in PRIME using your mobile BC Services Card. <br> <br> <strong> Your temporary GPID is @Model.GPID. </strong> <br> <br> The first time you log into PRIME you should be asked if you have previously received permission to access PharmaNet via an offline process. If you do not see this prompt, please stop your enrollment and contact PRIMESupport@@gov.bc.ca.", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 17);
        }
    }
}
