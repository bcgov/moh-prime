using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateEffectiveDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 5,
                column: "EffectiveDate",
                value: new DateTimeOffset(new DateTime(2022, 12, 17, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 6,
                column: "EffectiveDate",
                value: new DateTimeOffset(new DateTime(2022, 12, 17, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 7,
                column: "EffectiveDate",
                value: new DateTimeOffset(new DateTime(2022, 12, 17, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 8,
                column: "EffectiveDate",
                value: new DateTimeOffset(new DateTime(2022, 12, 17, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            //PRIME-2301 populate the SelfDeclarationCompletedDate for enrollees that have not accepted TOA yet
            migrationBuilder.Sql(@"
                UPDATE ""Enrollee""
                SET ""SelfDeclarationCompletedDate"" = ""UpdatedTimeStamp""
                WHERE ""SelfDeclarationCompletedDate"" is null and ""ProfileCompleted"";
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 5,
                column: "EffectiveDate",
                value: new DateTimeOffset(new DateTime(2022, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 6,
                column: "EffectiveDate",
                value: new DateTimeOffset(new DateTime(2022, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 7,
                column: "EffectiveDate",
                value: new DateTimeOffset(new DateTime(2022, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 8,
                column: "EffectiveDate",
                value: new DateTimeOffset(new DateTime(2022, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
