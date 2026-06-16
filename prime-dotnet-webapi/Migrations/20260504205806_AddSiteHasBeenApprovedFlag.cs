using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class AddSiteHasBeenApprovedFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenApproved",
                table: "Site",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql(@"UPDATE ""Site"" s
                SET ""HasBeenApproved"" = true
                WHERE s.""Id"" in ( SELECT DISTINCT ""SiteId"" FROM ""BusinessEvent"" e WHERE e.""Description"" = 'Site Approved')
                OR s.""ApprovedDate"" IS NOT NULL;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenApproved",
                table: "Site");
        }
    }
}
