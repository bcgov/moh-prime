using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AdaptationsForMoHKeyCloak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Party",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Enrollee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Admin",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Party_Username",
                table: "Party",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_Username",
                table: "Enrollee",
                column: "Username",
                unique: true);

            //
            // Setup data in new column based on existing data
            //

            migrationBuilder.Sql(@"UPDATE ""Party""
                SET ""Username"" = CONCAT(""HPDID"", '@bcsc')
                WHERE ""HPDID"" IS NOT NULL;");

            migrationBuilder.Sql(@"UPDATE ""Enrollee""
                SET ""Username"" = CONCAT(""HPDID"", '@bcsc')
                WHERE ""HPDID"" IS NOT NULL
                    AND ""GPID"" NOT LIKE 'NABCSC%' OR ""GPID"" IS NULL;");

            migrationBuilder.Sql(@"UPDATE ""Admin""
                SET ""Username"" = ""IDIR"";");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Party_Username",
                table: "Party");

            migrationBuilder.DropIndex(
                name: "IX_Enrollee_Username",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Admin");
        }
    }
}
