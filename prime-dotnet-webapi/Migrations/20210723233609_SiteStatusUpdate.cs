using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class SiteStatusUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // change the old status Approved 3 to Editable 1
            migrationBuilder.Sql(@"
                    UPDATE public.""SiteStatus"" SET ""StatusType"" = 1 WHERE ""StatusType"" = 3;
            ");

            // change the old status Locked 4 to 3
            migrationBuilder.Sql(@"
                    UPDATE public.""SiteStatus"" SET ""StatusType"" = 3 WHERE ""StatusType"" = 4;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // revert the Locked 4 -> 3
            migrationBuilder.Sql(@"
                    UPDATE public.""SiteStatus"" SET ""StatusType"" = 4 WHERE ""StatusType"" = 3;
            ");

            // revert the Approved 3 to Editable 1
            migrationBuilder.Sql(@"
                    UPDATE public.""SiteStatus"" ss SET ""StatusType"" = 3
                    FROM public.""Site"" s
                    WHERE ss.""StatusType"" = 1 AND ss.""SiteId"" = s.""Id"" AND s.""ApprovedDate"" IS NOT NULL;
            ");
        }
    }
}
