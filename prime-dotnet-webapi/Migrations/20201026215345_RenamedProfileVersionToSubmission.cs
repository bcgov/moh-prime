using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class RenamedProfileVersionToSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "EnrolleeProfileVersion",
                newName: "Submission");

            migrationBuilder.AddColumn<int>(
                name: "AgreementType",
                table: "Submission",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgreementType",
                table: "Submission");

            migrationBuilder.RenameTable(
                name: "Submission",
                newName: "EnrolleeProfileVersion");
        }
    }
}
