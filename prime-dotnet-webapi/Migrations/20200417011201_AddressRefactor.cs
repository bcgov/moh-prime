using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddressRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MailingAddressId",
                table: "Enrollee",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhysicalAddressId",
                table: "Enrollee",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
                UPDATE ""Enrollee""
                SET ""PhysicalAddressId"" = ""Address"".""Id""
                FROM ""Address""
                WHERE ""Enrollee"".""Id"" = ""Address"".""EnrolleeId"" and ""Address"".""AddressType"" = 1;

                UPDATE ""Enrollee""
                SET ""MailingAddressId"" = ""Address"".""Id""
                FROM ""Address""
                WHERE ""Enrollee"".""Id"" = ""Address"".""EnrolleeId"" and ""Address"".""AddressType"" = 2;
            ");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Enrollee_EnrolleeId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Enrollee_EnrolleeId1",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_EnrolleeId_AddressType",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_EnrolleeId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "EnrolleeId",
                table: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_MailingAddressId",
                table: "Enrollee",
                column: "MailingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_PhysicalAddressId",
                table: "Enrollee",
                column: "PhysicalAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Address_MailingAddressId",
                table: "Enrollee",
                column: "MailingAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollee_Address_PhysicalAddressId",
                table: "Enrollee",
                column: "PhysicalAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Address_MailingAddressId",
                table: "Enrollee");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollee_Address_PhysicalAddressId",
                table: "Enrollee");

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_MailingAddressId",
                table: "Enrollee");

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_PhysicalAddressId",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "MailingAddressId",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "PhysicalAddressId",
                table: "Enrollee");

            migrationBuilder.AddColumn<int>(
                name: "EnrolleeId",
                table: "Address",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeId_AddressType",
                table: "Address",
                columns: new[] { "EnrolleeId", "AddressType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_EnrolleeId",
                table: "Address",
                column: "EnrolleeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Enrollee_EnrolleeId",
                table: "Address",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Enrollee_EnrolleeId1",
                table: "Address",
                column: "EnrolleeId",
                principalTable: "Enrollee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
