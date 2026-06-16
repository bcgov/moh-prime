using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class SelfDeclarationQuestionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SelfDeclarationVersion",
                columns: new[] { "Id", "CareSettingCodeStr", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "SelfDeclarationTypeCode", "Text", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 10, "1,2,3,4", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2026, 4, 9, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, "Are you, or have you ever been, subject to the imposition, whether by order or with consent, of <u>prohibitions, limits or conditions on your practice of a health profession:</u> <ol style='list-style-type: lower-alpha;' class='mb-0'><li>in British Columbia, under the Health Professions and Occupations Act, the Health Professions Act or the Pharmacy Operations and Drug Scheduling Act, or</li><li>in any other jurisdiction, by a body that regulates a health profession in that jurisdiction</li></ol><u>for a matter involving an “unlawful or improper action”</u>?", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
