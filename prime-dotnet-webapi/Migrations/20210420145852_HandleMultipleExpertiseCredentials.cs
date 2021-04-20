using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class HandleMultipleExpertiseCredentials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE ""PlrProvider"" ALTER COLUMN ""Expertise"" TYPE text[] USING ARRAY[""Expertise""];");

            migrationBuilder.Sql(@"ALTER TABLE ""PlrProvider"" ALTER COLUMN ""Credentials"" TYPE text[] USING ARRAY[""Credentials""];");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Expertise",
                table: "PlrProvider",
                type: "text",
                nullable: true,
                oldClrType: typeof(string[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Credentials",
                table: "PlrProvider",
                type: "text",
                nullable: true,
                oldClrType: typeof(string[]),
                oldNullable: true);
        }
    }
}
