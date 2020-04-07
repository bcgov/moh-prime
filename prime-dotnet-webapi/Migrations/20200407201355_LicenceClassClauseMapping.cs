using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class LicenceClassClauseMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTimeStamp",
                table: "AccessTermLicenseClassClause",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "AccessTermLicenseClassClause",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedTimeStamp",
                table: "AccessTermLicenseClassClause",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "AccessTermLicenseClassClause",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "LicenseClassClauseMapping",
                columns: table => new
                {
                    LicenseCode = table.Column<int>(nullable: false),
                    OrganizatonTypeCode = table.Column<int>(nullable: false),
                    LicenseClassClauseId = table.Column<int>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseClassClauseMapping", x => new { x.LicenseCode, x.OrganizatonTypeCode, x.LicenseClassClauseId });
                    table.ForeignKey(
                        name: "FK_LicenseClassClauseMapping_LicenseClassClause_LicenseClassCl~",
                        column: x => x.LicenseClassClauseId,
                        principalTable: "LicenseClassClause",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicenseClassClauseMapping_LicenseLookup_LicenseCode",
                        column: x => x.LicenseCode,
                        principalTable: "LicenseLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicenseClassClauseMapping_OrganizationTypeLookup_Organizato~",
                        column: x => x.OrganizatonTypeCode,
                        principalTable: "OrganizationTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "GlobalClause",
                keyColumn: "Id",
                keyValue: 1,
                column: "Clause",
                value: "");

            migrationBuilder.UpdateData(
                table: "LicenseClassClause",
                keyColumn: "Id",
                keyValue: 1,
                column: "Clause",
                value: "");

            migrationBuilder.UpdateData(
                table: "LicenseClassClause",
                keyColumn: "Id",
                keyValue: 2,
                column: "Clause",
                value: "");

            migrationBuilder.InsertData(
                table: "LicenseClassClauseMapping",
                columns: new[] { "LicenseCode", "OrganizatonTypeCode", "LicenseClassClauseId", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, 2, 1, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 1, 2, 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 1, 3, 1, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 1, 3, 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LicenseClassClauseMapping_LicenseClassClauseId",
                table: "LicenseClassClauseMapping",
                column: "LicenseClassClauseId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseClassClauseMapping_OrganizatonTypeCode",
                table: "LicenseClassClauseMapping",
                column: "OrganizatonTypeCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicenseClassClauseMapping");

            migrationBuilder.DropColumn(
                name: "CreatedTimeStamp",
                table: "AccessTermLicenseClassClause");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "AccessTermLicenseClassClause");

            migrationBuilder.DropColumn(
                name: "UpdatedTimeStamp",
                table: "AccessTermLicenseClassClause");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "AccessTermLicenseClassClause");

            migrationBuilder.UpdateData(
                table: "GlobalClause",
                keyColumn: "Id",
                keyValue: 1,
                column: "Clause",
                value: "Global clause lorem, ipsum dolor sit amet consectetur adipisicing elit. Modi nihil corporis, ex totam, eos sapiente quam, sit ea iure consequatur neque harum architecto debitis adipisci molestiae fuga sed nam vitae.");

            migrationBuilder.UpdateData(
                table: "LicenseClassClause",
                keyColumn: "Id",
                keyValue: 1,
                column: "Clause",
                value: "License class clause 1 Consectetur adipisicing elit. Doloremque sit, rerum assumenda sed facere quam vel soluta suscipit esse neque quod, pariatur ea excepturi atque delectus voluptatum, modi obcaecati aliquid!");

            migrationBuilder.UpdateData(
                table: "LicenseClassClause",
                keyColumn: "Id",
                keyValue: 2,
                column: "Clause",
                value: "License class clause 2 Rerum assumenda sed facere quam vel soluta suscipit esse neque quod, pariatur ea excepturi atque delectus voluptatum, modi obcaecati aliquid!");
        }
    }
}
