using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddSigningAuthToPartyFieldOnAgreement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Agreement_OnlyOneForeignKey",
                table: "Agreement");

            migrationBuilder.Sql(@"
                    update ""Agreement"" a
                    set ""PartyId"" = subquery.""SigningAuthorityId""
                    from(
                        select a.""OrganizationId"", o.""SigningAuthorityId""
                        from ""Agreement"" a
                        join ""Organization"" o on o.""Id"" = a.""OrganizationId""
                        where a.""OrganizationId"" is not null
                    ) as subquery
                    where a.""OrganizationId"" = subquery.""OrganizationId"";
            ");

            migrationBuilder.CreateCheckConstraint(
                name: "CHK_Agreement_OrganizationHasSigningAuth",
                table: "Agreement",
                sql: "((\"OrganizationId\" is null) or (\"PartyId\" is not null))");

            migrationBuilder.CreateCheckConstraint(
                name: "CHK_Agreement_EitherPartyOrEnrollee",
                table: "Agreement",
                sql: @"( CASE WHEN ""EnrolleeId"" IS NULL THEN 0 ELSE 1 END
                     + CASE WHEN ""PartyId"" IS NULL THEN 0 ELSE 1 END) = 1");

            migrationBuilder.AddColumn<bool>(
                name: "PendingTransfer",
                table: "Organization",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Agreement_OrganizationHasSigningAuth",
                table: "Agreement");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Agreement_EitherPartyOrEnrollee",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "PendingTransfer",
                table: "Organization");

            migrationBuilder.CreateCheckConstraint(
                name: "CHK_Agreement_OnlyOneForeignKey",
                table: "Agreement",
                sql: @"( CASE WHEN ""EnrolleeId"" IS NULL THEN 0 ELSE 1 END
                     + CASE WHEN ""OrganizationId"" IS NULL THEN 0 ELSE 1 END
                     + CASE WHEN ""PartyId"" IS NULL THEN 0 ELSE 1 END) = 1");
        }
    }
}
