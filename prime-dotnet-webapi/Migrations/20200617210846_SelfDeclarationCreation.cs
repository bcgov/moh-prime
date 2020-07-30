using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class SelfDeclarationCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelfDeclarationTypeLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfDeclarationTypeLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "SelfDeclaration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    SelfDeclarationTypeCode = table.Column<int>(nullable: false),
                    SelfDeclarationDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfDeclaration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelfDeclaration_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelfDeclaration_SelfDeclarationTypeLookup_SelfDeclarationTy~",
                        column: x => x.SelfDeclarationTypeCode,
                        principalTable: "SelfDeclarationTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelfDeclarationDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    DocumentGuid = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    UploadedDate = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    SelfDeclarationTypeCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfDeclarationDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelfDeclarationDocument_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelfDeclarationDocument_SelfDeclarationTypeLookup_SelfDecla~",
                        column: x => x.SelfDeclarationTypeCode,
                        principalTable: "SelfDeclarationTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SelfDeclarationTypeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Has Conviction", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Has Registration Suspended", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Has Disciplinary Action", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), "Has PharmaNet Suspended", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclaration_EnrolleeId",
                table: "SelfDeclaration",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclaration_SelfDeclarationTypeCode",
                table: "SelfDeclaration",
                column: "SelfDeclarationTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclarationDocument_EnrolleeId",
                table: "SelfDeclarationDocument",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclarationDocument_SelfDeclarationTypeCode",
                table: "SelfDeclarationDocument",
                column: "SelfDeclarationTypeCode");

            migrationBuilder.Sql(@"
                INSERT INTO ""SelfDeclaration""
                SELECT
                    nextval('""SelfDeclaration_Id_seq""') as ""Id"",
                    e.""CreatedUserId"",
                    e.""CreatedTimeStamp"",
                    e.""CreatedUserId"" as ""UpdatedUserId"",
                    current_timestamp as ""UpdatedTimeStamp"",
                    e.""Id"" as ""EnrolleeId"",
                    1 as ""SelfDeclarationTypeCode"",
                    e.""HasConvictionDetails"" as ""SelfDeclarationDetails""
                FROM ""Enrollee"" e
                WHERE e.""HasConviction"" = true;
            ");

            migrationBuilder.Sql(@"
                INSERT INTO ""SelfDeclaration""
                SELECT
                    nextval('""SelfDeclaration_Id_seq""') as ""Id"",
                    e.""CreatedUserId"",
                    e.""CreatedTimeStamp"",
                    e.""CreatedUserId"" as ""UpdatedUserId"",
                    current_timestamp as ""UpdatedTimeStamp"",
                    e.""Id"" as ""EnrolleeId"",
                    2 as ""SelfDeclarationTypeCode"",
                    e.""HasRegistrationSuspendedDetails"" as ""SelfDeclarationDetails""
                FROM ""Enrollee"" e
                WHERE e.""HasRegistrationSuspended"" = true;
            ");

            migrationBuilder.Sql(@"
                INSERT INTO ""SelfDeclaration""
                SELECT
                    nextval('""SelfDeclaration_Id_seq""') as ""Id"",
                    e.""CreatedUserId"",
                    e.""CreatedTimeStamp"",
                    e.""CreatedUserId"" as ""UpdatedUserId"",
                    current_timestamp as ""UpdatedTimeStamp"",
                    e.""Id"" as ""EnrolleeId"",
                    3 as ""SelfDeclarationTypeCode"",
                    e.""HasDisciplinaryActionDetails"" as ""SelfDeclarationDetails""
                FROM ""Enrollee"" e
                WHERE e.""HasDisciplinaryAction"" = true;
            ");

            migrationBuilder.Sql(@"
                INSERT INTO ""SelfDeclaration""
                SELECT
                    nextval('""SelfDeclaration_Id_seq""') as ""Id"",
                    e.""CreatedUserId"",
                    e.""CreatedTimeStamp"",
                    e.""CreatedUserId"" as ""UpdatedUserId"",
                    current_timestamp as ""UpdatedTimeStamp"",
                    e.""Id"" as ""EnrolleeId"",
                    4 as ""SelfDeclarationTypeCode"",
                    e.""HasPharmaNetSuspendedDetails"" as ""SelfDeclarationDetails""
                FROM ""Enrollee"" e
                WHERE e.""HasPharmaNetSuspended"" = true;
            ");

            migrationBuilder.DropColumn(
                name: "HasConviction",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "HasConvictionDetails",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "HasDisciplinaryAction",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "HasDisciplinaryActionDetails",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "HasPharmaNetSuspended",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "HasPharmaNetSuspendedDetails",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "HasRegistrationSuspended",
                table: "Enrollee");

            migrationBuilder.DropColumn(
                name: "HasRegistrationSuspendedDetails",
                table: "Enrollee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelfDeclaration");

            migrationBuilder.DropTable(
                name: "SelfDeclarationDocument");

            migrationBuilder.DropTable(
                name: "SelfDeclarationTypeLookup");

            migrationBuilder.AddColumn<bool>(
                name: "HasConviction",
                table: "Enrollee",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HasConvictionDetails",
                table: "Enrollee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasDisciplinaryAction",
                table: "Enrollee",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HasDisciplinaryActionDetails",
                table: "Enrollee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasPharmaNetSuspended",
                table: "Enrollee",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HasPharmaNetSuspendedDetails",
                table: "Enrollee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasRegistrationSuspended",
                table: "Enrollee",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HasRegistrationSuspendedDetails",
                table: "Enrollee",
                type: "text",
                nullable: true);
        }
    }
}
