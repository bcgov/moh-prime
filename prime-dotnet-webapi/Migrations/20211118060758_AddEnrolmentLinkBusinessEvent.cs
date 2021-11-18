using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddEnrolmentLinkBusinessEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BusinessEventTypeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[] { 10, "Paper Enrolment Link" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessEventTypeLookup",
                keyColumn: "Code",
                keyValue: 10);
        }
    }
}
