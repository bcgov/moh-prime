using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class PharmOrgAgreement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AgreementVersion",
                columns: new[] { "Id", "AgreementType", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "Text", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 12, 5, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<p class=""text-center"">
  This Agreement is made the {{day}} day of {{month}}, {{year}}
</p>

<h1>---- PLACEHOLDER TEXT ----</h1>
", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
