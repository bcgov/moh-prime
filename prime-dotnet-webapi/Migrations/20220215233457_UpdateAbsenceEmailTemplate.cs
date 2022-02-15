using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateAbsenceEmailTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 19,
                column: "Template",
                value: "This is an automated generated email from PRIME. <br> <br> @(Model.FirstName + \" \" +  Model.LastName + \" is going to be absent \") @if (Model.End.HasValue) {@(\"from \" + Model.Start.ToShortDateString() + \" to \" + Model.End.Value.ToShortDateString() + \".  Please consider deactivating the user during this period. Any access during this period by the user will be considered as an unauthorized access.\")} else {@(\"indefinitely, starting \" + Model.Start.ToShortDateString() + \". Please deactivate the user on the start date. Any access during this period by the user will be considered as an unauthorized access.\")}");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 19,
                column: "Template",
                value: "To Whom it may concern, <br> <br> @(Model.FirstName + \" \" +  Model.LastName + \" is going to be absent \") @if (Model.End.HasValue) {@(\"from \" + Model.Start.ToShortDateString() + \" to \" + Model.End.Value.ToShortDateString() + \".  Please consider deactivating the user during this period. Any access during this period by the user will be considered as an unauthorized access.\")} else {@(\"indefinitely, starting \" + Model.Start.ToShortDateString() + \". Please deactivate the user on the start date. Any access during this period by the user will be considered as an unauthorized access.\")}");
        }
    }
}
