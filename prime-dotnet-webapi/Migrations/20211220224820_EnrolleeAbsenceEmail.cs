using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class EnrolleeAbsenceEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTimestamp",
                table: "EnrolleeAbsence",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "EmailType", "ModifiedDate", "Template", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[] { 19, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 19, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "To Whom it may concern, <br> <br> Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ratione placeat necessitatibus adipisci dicta doloribus. Ratione inventore aperiam nobis consequuntur ab, cum, numquam praesentium magnam commodi quasi in voluptates enim repellat!", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTimestamp",
                table: "EnrolleeAbsence",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }
    }
}
