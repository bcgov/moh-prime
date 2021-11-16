using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdatedDviceProviderFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PharmanetTransactionLog_TransactionId",
                table: "PharmanetTransactionLog");

            migrationBuilder.DropColumn(
                name: "IsInsulinPumpProvider",
                table: "Enrollee");

            migrationBuilder.RenameColumn(
                name: "DeviceProviderNumber",
                table: "Enrollee",
                newName: "DeviceProviderIdentifier");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "Name",
                value: "College License, Practitioner ID, or Device Provider ID not in PharmaNet table");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 8,
                column: "Name",
                value: "Device Provider");

            migrationBuilder.CreateIndex(
                name: "IX_PharmanetTransactionLog_TransactionId",
                table: "PharmanetTransactionLog",
                column: "TransactionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PharmanetTransactionLog_TransactionId",
                table: "PharmanetTransactionLog");

            migrationBuilder.RenameColumn(
                name: "DeviceProviderIdentifier",
                table: "Enrollee",
                newName: "DeviceProviderNumber");

            migrationBuilder.AddColumn<bool>(
                name: "IsInsulinPumpProvider",
                table: "Enrollee",
                type: "boolean",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "Name",
                value: "College License or Practitioner ID not in PharmaNet table");

            migrationBuilder.UpdateData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 8,
                column: "Name",
                value: "Insulin Pump Provider");

            migrationBuilder.CreateIndex(
                name: "IX_PharmanetTransactionLog_TransactionId",
                table: "PharmanetTransactionLog",
                column: "TransactionId");
        }
    }
}
