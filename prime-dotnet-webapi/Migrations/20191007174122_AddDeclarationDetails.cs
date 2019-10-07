using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddDeclarationDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HasConvictionDetails",
                table: "Enrolment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HasDisciplinaryActionDetails",
                table: "Enrolment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HasPharmaNetSuspendedDetails",
                table: "Enrolment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HasRegistrationSuspendedDetails",
                table: "Enrolment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasConvictionDetails",
                table: "Enrolment");

            migrationBuilder.DropColumn(
                name: "HasDisciplinaryActionDetails",
                table: "Enrolment");

            migrationBuilder.DropColumn(
                name: "HasPharmaNetSuspendedDetails",
                table: "Enrolment");

            migrationBuilder.DropColumn(
                name: "HasRegistrationSuspendedDetails",
                table: "Enrolment");
        }
    }
}
