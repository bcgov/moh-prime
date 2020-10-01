using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class MigrateOldOrgAgreements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //(OrganizationId, AgreementVersionId, CreatedDate, AcceptedDate, ExpiryDate)
            // nextval('""Agreement_Id_seq""') as ""Id"",
            migrationBuilder.Sql(@"
                INSERT INTO ""Agreement""
                SELECT
                    o.""Id"" as ""OrganizationId"",
                    o.""CreatedUserId"",
                    o.""CreatedTimeStamp"",
                    o.""UpdatedUserId"",
                    current_timestamp as o.""UpdatedTimeStamp"",
                    null as ""EnrolleeId"",
                    o.""AcceptedAgreementDate"" as ""CreatedDate"",
                    o.""AcceptedAgreementDate"" as ""AcceptedDate""
                FROM ""Organization"" as o
                WHERE o.""AcceptedAgreementDate"" != null;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
