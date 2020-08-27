using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AgreementView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE VIEW public.""NewestAgreements""
                AS
                SELECT DISTINCT ON(""Discriminator"")
	                ""Id""
                FROM public.""Agreement""
                ORDER BY ""Discriminator"", ""EffectiveDate"" DESC
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW public.""NewestAgreements""
            ");
        }
    }
}
