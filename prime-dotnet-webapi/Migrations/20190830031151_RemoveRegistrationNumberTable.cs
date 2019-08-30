using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace prime.Migrations
{
    public partial class RemoveRegistrationNumberTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PharmacistRegistrationNumber",
                schema: "public");

            migrationBuilder.DropColumn(
                name: "Content",
                schema: "public",
                table: "Application");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                schema: "public",
                table: "Application",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PharmacistRegistrationNumber",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Number = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacistRegistrationNumber", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "PharmacistRegistrationNumber",
                columns: new[] { "Id", "Number" },
                values: new object[,]
                {
                    { 1, "A0000" },
                    { 2, "A0001" },
                    { 3, "B0000" },
                    { 4, "B0001" }
                });
        }
    }
}
