using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class RenamedEmailPhoneFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactEmail",
                table: "Enrollee",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ContactPhone",
                table: "Enrollee",
                newName: "SmsPhone");

            migrationBuilder.RenameColumn(
                name: "VoiceExtension",
                table: "Enrollee",
                newName: "PhoneExtension");

            migrationBuilder.RenameColumn(
                name: "VoicePhone",
                table: "Enrollee",
                newName: "Phone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Enrollee",
                newName: "ContactEmail");

            migrationBuilder.RenameColumn(
                name: "SmsPhone",
                table: "Enrollee",
                newName: "ContactPhone");

            migrationBuilder.RenameColumn(
                name: "PhoneExtension",
                table: "Enrollee",
                newName: "VoiceExtension");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Enrollee",
                newName: "VoicePhone");
        }
    }
}
