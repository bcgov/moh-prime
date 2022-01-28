using System;
using Microsoft.EntityFrameworkCore.Migrations;

using Prime.Models;

namespace Prime.Migrations
{
    public partial class AddPharmacyTechRuNoOboToa : Migration
    {
        private static readonly DateTimeOffset SEEDING_DATE = DateTimeOffset.Parse("2019-09-16 -7:00");

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AgreementVersion",
                columns: new[] { "CreatedUserId", "CreatedTimeStamp", "UpdatedUserId", "UpdatedTimeStamp", "Text", "AgreementType", "EffectiveDate" },
                values: new object[] { Guid.Empty, SEEDING_DATE, Guid.Empty, SEEDING_DATE,
                    @"TOA TEMPLATE CONTENT",
                    (int) AgreementType.PharmacyTechnicianTOA, SEEDING_DATE });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AgreementVersion",
                keyColumn: "Id",
                keyValue: 18);
        }
    }
}
