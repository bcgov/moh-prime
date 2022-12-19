using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class SelfDeclarationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortingNumber",
                table: "SelfDeclarationTypeLookup",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SelfDeclarationVersionId",
                table: "SelfDeclaration",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SelfDeclarationCompletedDate",
                table: "Enrollee",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SelfDeclarationVersion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SelfDeclarationTypeCode = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    EffectiveDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfDeclarationVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelfDeclarationVersion_SelfDeclarationTypeLookup_SelfDeclar~",
                        column: x => x.SelfDeclarationTypeCode,
                        principalTable: "SelfDeclarationTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 19,
                column: "Template",
                value: "This is an automated generated email from PRIME. <br> <br> @(Model.FirstName + \" \" +  Model.LastName + \" is going to be absent \") @if (Model.End.HasValue) {@(\"from \" + Model.Start.ToShortDateString() + \" to \" + Model.End.Value.ToShortDateString() + \".  Please consider deactivating the user during this period. Any access during this period by the user will be considered as an unauthorized access.\")} else {@(\"indefinitely, starting \" + Model.Start.ToShortDateString() + \". Please deactivate the user on the start date. Any access during this period by the user will be considered as an unauthorized access.\")}");

            migrationBuilder.UpdateData(
                table: "SelfDeclarationTypeLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "SortingNumber",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SelfDeclarationTypeLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "SortingNumber",
                value: 2);

            migrationBuilder.UpdateData(
                table: "SelfDeclarationTypeLookup",
                keyColumn: "Code",
                keyValue: 3,
                columns: new[] { "Name", "SortingNumber" },
                values: new object[] { "Has PharmaNet Suspended", 3 });

            migrationBuilder.UpdateData(
                table: "SelfDeclarationTypeLookup",
                keyColumn: "Code",
                keyValue: 4,
                columns: new[] { "Name", "SortingNumber" },
                values: new object[] { "Has Disciplinary Action", 4 });

            migrationBuilder.InsertData(
                table: "SelfDeclarationVersion",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "SelfDeclarationTypeCode", "Text", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 2, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, "Are you, or have you ever been, the subject of an order or a conviction under legislation in any jurisdiction for a matter that involved improper access to, collection, use, or disclosure or retention of personal information?", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 2, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, "Are you, or have you ever been, subject to any limits, conditions or prohibitions imposed as a result of disciplinary actions taken by a governing body of a health profession in any jurisdiction, that involved improper access to, collection, use, or disclosure or retention of personal information?", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 2, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4, "Have you ever been disciplined or fired by an employer, or had a contract for your services terminated, for a matter that involved improper access to, collection, use, or disclosure or retention of personal information?", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 2, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3, "Have you ever had your access to PharmaNet or any other health information system, whether or not electronic,  an electronic health record system, electronic medical record system, pharmacy or laboratory record system, or any similar health information system, in any jurisdiction, suspended or cancelled for a matter that involved improper access to, collection, use, or disclosure or retention of personal information?", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2022, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, "Have you ever been the subject of <u>an order</u> or <u>conviction</u> in British Columbia or any other jurisdiction <u>for a matter involving an “unlawful or improper action”</u>?", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2022, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, "Are you, or have you ever been, subject to the imposition, whether by order or with consent, of <u>prohibitions, limits or conditions on your practice of a health profession:</u> <ol style='list-style-type: lower-alpha;' class='mb-0'><li>in British Columbia, under the Health Professions Act or the Pharmacy Operations and Drug Scheduling Act, or</li><li>in any other jurisdiction, by a body that regulates a health profession in that jurisdiction</li></ol><u>for a matter involving an “unlawful or improper action”</u>?", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2022, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4, "Has an employer ever disciplined you, or terminated your employment, for <u>a matter involving an “unlawful or improper action”</u>?  Has a contract for your services ever been terminated <u>for a matter involving an “unlawful or improper action”</u>?", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2022, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3, "Has your access to <u>PharmaNet</u> or <u>any other health information system</u>, whether or not electronic and whether or not in British Columbia or another jurisdiction, been suspended or cancelled <u>for a matter involving an “unlawful or improper action”</u>?", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclaration_SelfDeclarationVersionId",
                table: "SelfDeclaration",
                column: "SelfDeclarationVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclarationVersion_SelfDeclarationTypeCode",
                table: "SelfDeclarationVersion",
                column: "SelfDeclarationTypeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_SelfDeclaration_SelfDeclarationVersion_SelfDeclarationVersi~",
                table: "SelfDeclaration",
                column: "SelfDeclarationVersionId",
                principalTable: "SelfDeclarationVersion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);


            //PRIME-2301 populate the SelfDeclarationVersionId for the existing record
            migrationBuilder.Sql(@"
                UPDATE public.""SelfDeclaration""
                SET ""SelfDeclarationVersionId"" = ""SelfDeclarationTypeCode""
                WHERE ""SelfDeclarationVersionId"" is null;
            ");

            //PRIME-2301 populate the SelfDeclarationCompletedDate from existing agreement record for existing record
            migrationBuilder.Sql(@"
                UPDATE ""Enrollee"" e
                SET ""SelfDeclarationCompletedDate"" = (select max(a.""AcceptedDate"") from ""Agreement"" a where a.""EnrolleeId"" = e.""Id"")
                WHERE ""SelfDeclarationCompletedDate"" is null;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelfDeclaration_SelfDeclarationVersion_SelfDeclarationVersi~",
                table: "SelfDeclaration");

            migrationBuilder.DropTable(
                name: "SelfDeclarationVersion");

            migrationBuilder.DropIndex(
                name: "IX_SelfDeclaration_SelfDeclarationVersionId",
                table: "SelfDeclaration");

            migrationBuilder.DropColumn(
                name: "SortingNumber",
                table: "SelfDeclarationTypeLookup");

            migrationBuilder.DropColumn(
                name: "SelfDeclarationVersionId",
                table: "SelfDeclaration");

            migrationBuilder.DropColumn(
                name: "SelfDeclarationCompletedDate",
                table: "Enrollee");

            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 19,
                column: "Template",
                value: "This is an automated generated email from PRIME. <br> <br> @(Model.FirstName + \" \" +  Model.LastName + \" is going to be absent \") @if (Model.End.HasValue) {@(\"from \" + Model.Start.ToShortDateString() + \" to \" + Model.End.Value.ToShortDateString() + \".  Please consider deactivating the user during this period. Any access during this period by the user will be considered as an unauthorized access.\")} else {@(\"indefinitely, starting \" + Model.Start.ToShortDateString() + \". Please deactivate the user on the start date. Any access during this period by the user will be considered as an unauthorized access.\")}");

            migrationBuilder.UpdateData(
                table: "SelfDeclarationTypeLookup",
                keyColumn: "Code",
                keyValue: 3,
                column: "Name",
                value: "Has Disciplinary Action");

            migrationBuilder.UpdateData(
                table: "SelfDeclarationTypeLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "Name",
                value: "Has PharmaNet Suspended");
        }
    }
}
