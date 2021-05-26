using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class FixJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // See PR#1209 for original version of this migration
            migrationBuilder.Sql(@"
                update ""OboSite""
                set ""JobTitle"" = (rec.""job_list"")
                from (
                    select j.""EnrolleeId"", string_agg(j.""Title"" , ', ') AS job_list
                    FROM   ""Job"" j
                    GROUP  BY j.""EnrolleeId""
                ) rec
                where ""EnrolleeId"" = rec.""EnrolleeId"";
            ");

            migrationBuilder.Sql(@"
                update ""OboSite""
                set ""HealthAuthorityCode"" = rec.""HealthAuthorityCode""
                from (
                    select eha.""EnrolleeId"", eha.""HealthAuthorityCode""
                    FROM   ""EnrolleeHealthAuthority"" eha
                    order by eha.""EnrolleeId"" asc, eha.""HealthAuthorityCode"" asc
                ) rec
                where ""CareSettingCode"" = 1 and ""EnrolleeId"" = rec.""EnrolleeId"";
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
