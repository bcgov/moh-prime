using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class AddPharmanetTransactionLogArchive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PharmanetTransactionLogArchive",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TransactionId = table.Column<long>(type: "bigint", nullable: false),
                    TxDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    LocationIpAddress = table.Column<string>(type: "text", nullable: true),
                    SourceIpAddress = table.Column<string>(type: "text", nullable: true),
                    PharmacyId = table.Column<string>(type: "text", nullable: true),
                    TransactionType = table.Column<string>(type: "text", nullable: true),
                    TransactionSubType = table.Column<string>(type: "text", nullable: true),
                    PractitionerId = table.Column<string>(type: "text", nullable: true),
                    CollegePrefix = table.Column<string>(type: "text", nullable: true),
                    TransactionOutcome = table.Column<string>(type: "text", nullable: true),
                    ProviderSoftwareId = table.Column<string>(type: "text", nullable: true),
                    ProviderSoftwareVersion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmanetTransactionLogArchive", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PharmanetTransactionLogArchive");
        }
    }
}
