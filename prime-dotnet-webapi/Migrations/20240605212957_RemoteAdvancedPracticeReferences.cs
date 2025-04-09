using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class RemoteAdvancedPracticeReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE ""Certification""
                SET ""PracticeCode"" = null;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
