using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class SiteLastLogMaterializedView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = new StringBuilder();
            sql.AppendLine(" CREATE MATERIALIZED VIEW SiteLastLog AS");
            sql.AppendLine(" select \"PharmacyId\", max(\"TxDateTime\") as \"LastLogDate\"");
            sql.AppendLine(" from \"PharmanetTransactionLog\" ptl");
            sql.AppendLine(" group by \"PharmacyId\"");
            migrationBuilder.Sql(sql.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP MATERIALIZED VIEW SiteLastLog;");
        }
    }
}
