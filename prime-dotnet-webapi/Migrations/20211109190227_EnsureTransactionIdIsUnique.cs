using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class EnsureTransactionIdIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PharmanetTransactionLog_TransactionId",
                table: "PharmanetTransactionLog");

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

            migrationBuilder.CreateIndex(
                name: "IX_PharmanetTransactionLog_TransactionId",
                table: "PharmanetTransactionLog",
                column: "TransactionId");
        }
    }
}
