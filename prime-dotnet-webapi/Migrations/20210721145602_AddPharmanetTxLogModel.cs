using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddPharmanetTxLogModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PharmanetTransactionLog",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    TransactionId = table.Column<long>(nullable: false),
                    TxDateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    PharmacyId = table.Column<string>(nullable: true),
                    TransactionType = table.Column<string>(nullable: true),
                    TransactionSubType = table.Column<string>(nullable: true),
                    PractitionerId = table.Column<string>(nullable: true),
                    CollegePrefix = table.Column<string>(nullable: true),
                    TransactionOutcome = table.Column<string>(nullable: true),
                    ProviderSoftwareId = table.Column<string>(nullable: true),
                    ProviderSoftwareVersion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmanetTransactionLog", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PharmanetTransactionLog_PharmacyId",
                table: "PharmanetTransactionLog",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmanetTransactionLog_TxDateTime",
                table: "PharmanetTransactionLog",
                column: "TxDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_PharmanetTransactionLog_UserId",
                table: "PharmanetTransactionLog",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PharmanetTransactionLog");
        }
    }
}
