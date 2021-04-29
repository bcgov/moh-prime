using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class HALikeCareSettingWithJobTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthAuthorityCode",
                table: "OboSite",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "OboSite",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OboSite_HealthAuthorityCode",
                table: "OboSite",
                column: "HealthAuthorityCode");

            migrationBuilder.AddForeignKey(
                name: "FK_OboSite_HealthAuthorityLookup_HealthAuthorityCode",
                table: "OboSite",
                column: "HealthAuthorityCode",
                principalTable: "HealthAuthorityLookup",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            // Update the Job Titles of all Obo Sites of an Enrollee
            // to a comma-separated list of the Enrollee's existing Jobs (from old schema)
            migrationBuilder.Sql(@"
                do
                $$
                declare
                    rec record;
                begin
                    for rec in
                        select j.""EnrolleeId"", string_agg(j.""Title"" , ', ') AS job_list
                        FROM   ""Job"" j
                        GROUP  BY j.""EnrolleeId""
                    loop
                        update ""OboSite""
                        set ""JobTitle"" = rec.job_list
                        where ""EnrolleeId"" = rec.""EnrolleeId"";
                    end loop;
                end;
                $$
            ");

            // Update the HealthAuthorityCode of all HealthAuthority Obo Sites of an Enrollee
            // to the largest valued(completely arbitrary) HealthAuthorityCode of the Enrollee's HealthAuthority associations
            migrationBuilder.Sql(@"
                do
                $$
                declare
                    rec record;
                begin
                    for rec in
                        select eha.""EnrolleeId"", eha.""HealthAuthorityCode""
                        FROM   ""EnrolleeHealthAuthority"" eha
                        order by eha.""EnrolleeId"" asc, eha.""HealthAuthorityCode"" asc
                    loop
                        update ""OboSite""
                        set ""HealthAuthorityCode"" = rec.""HealthAuthorityCode""
                        where ""CareSettingCode"" = 1
                            and ""EnrolleeId"" = rec.""EnrolleeId"";
                    end loop;
                end;
                $$
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OboSite_HealthAuthorityLookup_HealthAuthorityCode",
                table: "OboSite");

            migrationBuilder.DropIndex(
                name: "IX_OboSite_HealthAuthorityCode",
                table: "OboSite");

            migrationBuilder.DropColumn(
                name: "HealthAuthorityCode",
                table: "OboSite");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "OboSite");
        }
    }
}
