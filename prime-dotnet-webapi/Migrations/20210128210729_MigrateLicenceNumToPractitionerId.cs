using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class MigrateLicenceNumToPractitionerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.Sql(@"
                UPDATE ""Certification"" c
                SET ""PractitionerId"" = c.""LicenseNumber""
                FROM ""LicenseLookup"" l
                WHERE l.""Code"" = c.""LicenseCode""
                AND l.""Validate"" = TRUE
                AND c.""PractitionerId"" isnull
                AND LENGTH(c.""LicenseNumber"") = 5
                AND c.""LicenseNumber"" ~ '^[0-9]*$';
            ");
            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
