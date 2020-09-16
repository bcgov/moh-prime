using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateDevProvOrgTypeCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE ""OrganizationTypeLookup""
                SET ""Code"" = '4'
                WHERE ""Name"" = 'Device Provider';
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE ""OrganizationTypeLookup""
                SET ""Code"" = '5'
                WHERE ""Name"" = 'Device Provider';
            ");
        }
    }
}
