using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class AddContactPhoneExtension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneExtension",
                table: "Contact",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneExtension",
                table: "Contact");
        }
    }
}
