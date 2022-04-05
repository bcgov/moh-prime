using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddEnrolleeTransactionLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Processed",
                table: "PharmanetTransactionLog",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "EnrolleeTransactionLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnrolleeId = table.Column<int>(type: "integer", nullable: false),
                    PharmanetTransactionLogId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeTransactionLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeTransactionLog_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeTransactionLog_PharmanetTransactionLog_PharmanetTra~",
                        column: x => x.PharmanetTransactionLogId,
                        principalTable: "PharmanetTransactionLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeTransactionLog_EnrolleeId",
                table: "EnrolleeTransactionLog",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeTransactionLog_PharmanetTransactionLogId",
                table: "EnrolleeTransactionLog",
                column: "PharmanetTransactionLogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolleeTransactionLog");

            migrationBuilder.DropColumn(
                name: "Processed",
                table: "PharmanetTransactionLog");
        }
    }
}
