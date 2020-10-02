using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class MigrateOrganizationsToNewAgreement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Electronically Signed Org Agreements
            migrationBuilder.Sql(@"
                INSERT INTO ""Agreement""
                SELECT
                    nextval('""AccessTerm_Id_seq""') as ""Id"",
                    o.""CreatedUserId"",
                    o.""CreatedTimeStamp"",
                    o.""UpdatedUserId"",
                    current_timestamp as ""UpdatedTimeStamp"",
                    null as ""EnrolleeId"",
                    11 as ""AgreementVersionId"",
                    null as ""LimitsConditionsCaluseId"",
                    o.""AcceptedAgreementDate"" as ""CreatedDate"",
                    o.""AcceptedAgreementDate"" as ""AcceptedDate"",
                    null as ""ExpiryDate"",
                    o.""Id"" as ""OrganizationId"",
                    null as ""PartyId""
                FROM ""Organization"" as o
                WHERE o.""AcceptedAgreementDate"" IS NOT null
                    AND (
                        SELECT Count(*)
                        FROM ""SignedAgreementDocument"" sad
                        WHERE sad.""OrganizationId"" = o.""Id"")
                    = 0;
            ");

            // Uploaded Signed Org Agreement Documents
            migrationBuilder.Sql(@"
                WITH inserted AS(
                    INSERT INTO ""Agreement""
                    SELECT
                        nextval('""AccessTerm_Id_seq""') as ""Id"",
                        o.""CreatedUserId"",
                        o.""CreatedTimeStamp"",
                        o.""UpdatedUserId"",
                        current_timestamp as ""UpdatedTimeStamp"",
                        null as ""EnrolleeId"",
                        11 as ""AgreementVersionId"",
                        null as ""LimitsConditionsCaluseId"",
                        o.""AcceptedAgreementDate"" as ""CreatedDate"",
                        o.""AcceptedAgreementDate"" as ""AcceptedDate"",
                        null as ""ExpiryDate"",
                        o.""Id"" as ""OrganizationId"",
                        null as ""PartyId""
                    FROM ""Organization"" as o
                    WHERE o.""AcceptedAgreementDate"" IS NOT null
                        AND (
                            SELECT Count(*)
                            FROM ""SignedAgreementDocument"" sad
                            WHERE sad.""OrganizationId"" = o.""Id"")
                        != 0
                    RETURNING ""Id"", ""OrganizationId""
                )
                UPDATE ""SignedAgreementDocument"" sad
                SET ""AgreementId"" = (SELECT inserted.""Id"" FROM inserted WHERE inserted.""OrganizationId"" = sad.""OrganizationId"");
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
