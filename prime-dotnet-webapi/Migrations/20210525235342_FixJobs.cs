using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class FixJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // See PR#1209 for original version of this migration
            migrationBuilder.Sql(@"
                update ""OboSite"" o
                set ""JobTitle"" = rec.job_list
                from (
                    select j.""EnrolleeId"", string_agg(j.""Title"" , ', ') AS job_list
                    FROM   ""Job"" j
                    GROUP  BY j.""EnrolleeId""
                ) rec
                where o.""EnrolleeId"" = rec.""EnrolleeId"";
            ");

            migrationBuilder.Sql(@"
                update ""OboSite"" o
                set ""HealthAuthorityCode"" = rec.""HealthAuthorityCode""
                from (
                    select eha.""EnrolleeId"", eha.""HealthAuthorityCode""
                    FROM   ""EnrolleeHealthAuthority"" eha
                    order by eha.""EnrolleeId"" asc, eha.""HealthAuthorityCode"" asc
                ) rec
                where o.""CareSettingCode"" = 1 and o.""EnrolleeId"" = rec.""EnrolleeId"";
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
