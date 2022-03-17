using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class ExpireOBOPharmTechTOAApril1st : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    UPDATE public.""Agreement""
                    SET ""ExpiryDate"" = '2022-04-30 00:00:00.000 -0700'
                    FROM (
	                    SELECT ""Id""
                        FROM (
                            SELECT c.""EnrolleeId""
                            FROM public.""Certification"" c
                            JOIN (
                                SELECT c.""EnrolleeId"", COUNT(c.""EnrolleeId"") as ""num""
                                FROM public.""Certification"" c
                                GROUP BY    c.""EnrolleeId""
                            ) x on c.""EnrolleeId"" = x.""EnrolleeId""
                            WHERE x.""num"" = 1 and c.""LicenseCode"" = 29
                        ) a
                        JOIN (
                            SELECT a1.""Id"", a1.""EnrolleeId""
                            FROM public.""Agreement"" a1
                            JOIN public.""AgreementVersion"" av on av.""Id"" = a1.""AgreementVersionId""
                            LEFT OUTER JOIN ""Agreement"" a2
                                ON (a1.""EnrolleeId"" = a2.""EnrolleeId"" AND a1.""AcceptedDate"" < a2.""AcceptedDate"")
                            WHERE a2.""EnrolleeId"" IS null
                                AND av.""AgreementType"" = 6
                                AND a1.""ExpiryDate"" is not null
                        ) b on a.""EnrolleeId"" = b.""EnrolleeId""
                    ) AS subquery
                    WHERE public.""Agreement"".""Id"" = subquery.""Id"";
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
