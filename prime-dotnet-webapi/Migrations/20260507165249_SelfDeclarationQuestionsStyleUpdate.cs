using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class SelfDeclarationQuestionsStyleUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 7,
                column: "Text",
                value: "Has an employer ever <u>disciplined you</u>, or <u>terminated your employment</u>, for <u>a matter involving an “unlawful or improper action”</u>?  Has a contract for your services ever been terminated <u>for a matter involving an “unlawful or improper action”</u>?");

            migrationBuilder.UpdateData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 8,
                column: "Text",
                value: "Has your access to <u>PharmaNet</u> or <u>any other health information system</u>, whether or not electronic and whether or not in British Columbia or another jurisdiction, been <u>suspended or cancelled for a matter involving an “unlawful or improper action”</u>?");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 7,
                column: "Text",
                value: "Has an employer ever disciplined you, or terminated your employment, for <u>a matter involving an “unlawful or improper action”</u>?  Has a contract for your services ever been terminated <u>for a matter involving an “unlawful or improper action”</u>?");

            migrationBuilder.UpdateData(
                table: "SelfDeclarationVersion",
                keyColumn: "Id",
                keyValue: 8,
                column: "Text",
                value: "Has your access to <u>PharmaNet</u> or <u>any other health information system</u>, whether or not electronic and whether or not in British Columbia or another jurisdiction, been suspended or cancelled <u>for a matter involving an “unlawful or improper action”</u>?");
        }
    }
}
