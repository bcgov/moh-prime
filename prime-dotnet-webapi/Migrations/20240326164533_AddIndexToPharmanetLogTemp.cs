using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddIndexToPharmanetLogTemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PharmanetTransactionLogTemp_PharmacyId",
                table: "PharmanetTransactionLogTemp",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmanetTransactionLogTemp_TransactionId",
                table: "PharmanetTransactionLogTemp",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmanetTransactionLogTemp_TxDateTime",
                table: "PharmanetTransactionLogTemp",
                column: "TxDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_PharmanetTransactionLogTemp_UserId",
                table: "PharmanetTransactionLogTemp",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PharmanetTransactionLogTemp_PharmacyId",
                table: "PharmanetTransactionLogTemp");

            migrationBuilder.DropIndex(
                name: "IX_PharmanetTransactionLogTemp_TransactionId",
                table: "PharmanetTransactionLogTemp");

            migrationBuilder.DropIndex(
                name: "IX_PharmanetTransactionLogTemp_TxDateTime",
                table: "PharmanetTransactionLogTemp");

            migrationBuilder.DropIndex(
                name: "IX_PharmanetTransactionLogTemp_UserId",
                table: "PharmanetTransactionLogTemp");
        }
    }
}
