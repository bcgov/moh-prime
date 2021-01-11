using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class Audit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTimeStamp",
                table: "PartyEnrolment",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "PartyEnrolment",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedTimeStamp",
                table: "PartyEnrolment",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "PartyEnrolment",
                nullable: true);


            migrationBuilder.Sql(@"
                update public.""PartyEnrolment"" set
                ""CreatedTimeStamp"" = p.""CreatedTimeStamp"",
                ""CreatedUserId"" = p.""CreatedUserId"",
                ""UpdatedTimeStamp"" = p.""UpdatedTimeStamp"",
                ""UpdatedUserId"" = p.""UpdatedUserId""
                from public.""Party"" p
                where p.""Id"" = ""PartyId""
            ");


            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedTimeStamp",
                table: "PartyEnrolment",
                nullable: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedUserId",
                table: "PartyEnrolment",
                nullable: false);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedTimeStamp",
                table: "PartyEnrolment",
                nullable: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedUserId",
                table: "PartyEnrolment",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTimeStamp",
                table: "PartyEnrolment");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "PartyEnrolment");

            migrationBuilder.DropColumn(
                name: "UpdatedTimeStamp",
                table: "PartyEnrolment");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "PartyEnrolment");
        }
    }
}
