using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class RefinePlrProviderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address2Line1",
                table: "PlrProvider");

            migrationBuilder.DropColumn(
                name: "Address2Line2",
                table: "PlrProvider");

            migrationBuilder.DropColumn(
                name: "Address2Line3",
                table: "PlrProvider");

            migrationBuilder.DropColumn(
                name: "Address2StartDate",
                table: "PlrProvider");

            migrationBuilder.DropColumn(
                name: "City2",
                table: "PlrProvider");

            migrationBuilder.DropColumn(
                name: "Country2",
                table: "PlrProvider");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "PlrProvider");

            migrationBuilder.DropColumn(
                name: "PostalCode2",
                table: "PlrProvider");

            migrationBuilder.DropColumn(
                name: "Province2",
                table: "PlrProvider");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StatusStartDate",
                table: "PlrProvider",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StatusExpiryDate",
                table: "PlrProvider",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "PlrProvider",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Address1StartDate",
                table: "PlrProvider",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConditionEndDate",
                table: "PlrProvider",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConditionStartDate",
                table: "PlrProvider",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpn",
                table: "PlrProvider",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConditionEndDate",
                table: "PlrProvider");

            migrationBuilder.DropColumn(
                name: "ConditionStartDate",
                table: "PlrProvider");

            migrationBuilder.DropColumn(
                name: "Cpn",
                table: "PlrProvider");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StatusStartDate",
                table: "PlrProvider",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StatusExpiryDate",
                table: "PlrProvider",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "PlrProvider",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Address1StartDate",
                table: "PlrProvider",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2Line1",
                table: "PlrProvider",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2Line2",
                table: "PlrProvider",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2Line3",
                table: "PlrProvider",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Address2StartDate",
                table: "PlrProvider",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "City2",
                table: "PlrProvider",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country2",
                table: "PlrProvider",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "PlrProvider",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode2",
                table: "PlrProvider",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province2",
                table: "PlrProvider",
                type: "text",
                nullable: true);
        }
    }
}
