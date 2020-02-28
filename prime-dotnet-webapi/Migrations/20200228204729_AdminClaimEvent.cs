using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AdminClaimEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BusinessEventTypeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { (short)4, new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Admin Claim", new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessEventTypeLookup",
                keyColumn: "Code",
                keyValue: (short)4);
        }
    }
}
