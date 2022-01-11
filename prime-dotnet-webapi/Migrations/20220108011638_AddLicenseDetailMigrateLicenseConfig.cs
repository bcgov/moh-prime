using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddLicenseDetailMigrateLicenseConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicensedToProvideCare",
                table: "LicenseLookup");

            migrationBuilder.DropColumn(
                name: "Manual",
                table: "LicenseLookup");

            migrationBuilder.DropColumn(
                name: "NamedInImReg",
                table: "LicenseLookup");

            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "LicenseLookup");

            migrationBuilder.DropColumn(
                name: "PrescriberIdType",
                table: "LicenseLookup");

            migrationBuilder.DropColumn(
                name: "Validate",
                table: "LicenseLookup");

            migrationBuilder.CreateTable(
                name: "LicenseDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LicenseCode = table.Column<int>(type: "integer", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Prefix = table.Column<string>(type: "text", nullable: true),
                    Manual = table.Column<bool>(type: "boolean", nullable: false),
                    Validate = table.Column<bool>(type: "boolean", nullable: false),
                    NamedInImReg = table.Column<bool>(type: "boolean", nullable: false),
                    LicensedToProvideCare = table.Column<bool>(type: "boolean", nullable: false),
                    PrescriberIdType = table.Column<int>(type: "integer", nullable: true),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseDetail_LicenseLookup_LicenseCode",
                        column: x => x.LicenseCode,
                        principalTable: "LicenseLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LicenseDetail",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "LicenseCode", "LicensedToProvideCare", "Manual", "NamedInImReg", "Prefix", "PrescriberIdType", "UpdatedTimeStamp", "UpdatedUserId", "Validate" },
                values: new object[,]
                {
                    { 29, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, true, false, false, "T9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 31, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 31, false, true, false, "T9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 68, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 68, true, true, false, "T9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 47, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 47, true, false, true, "96", 2, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 48, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 48, true, true, true, "96", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 51, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 51, true, false, true, "96", 2, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 49, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 49, false, true, true, "96", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 32, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 32, true, false, false, "R9", 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 33, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 33, true, false, false, "R9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 39, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 39, true, false, false, "R9", 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 34, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 34, false, true, false, "R9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 40, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, true, false, false, "R9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 35, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 35, true, true, false, "R9", 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 30, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, false, true, true, "P1", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 36, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 36, true, true, false, "R9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 41, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 41, true, false, false, "Y9", 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 42, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 42, true, false, false, "Y9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 45, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, true, false, false, "Y9", 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 43, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 43, false, true, false, "Y9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 46, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 46, true, false, false, "Y9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 52, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 52, true, false, false, "L9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 53, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 53, true, false, false, "L9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 55, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 55, true, false, false, "L9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 54, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 54, false, true, false, "L9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 60, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, true, false, false, "98", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 61, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 61, true, false, false, "98", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 62, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 62, true, false, false, "98", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 63, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 63, true, true, false, "98", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 37, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 37, false, false, false, "R9", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 27, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, true, false, true, "P1", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 28, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, true, false, false, "P1", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 26, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 26, true, false, true, "P1", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 2, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 5, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 6, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 9, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, true, true, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 8, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, true, true, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 10, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, true, true, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 22, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 16, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 7, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, true, true, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 4, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 3, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, true, true, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 17, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, true, true, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 12, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 13, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 14, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 15, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 11, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, true, false, false, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 23, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, false, true, false, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 20, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, false, true, false, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 24, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, true, true, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 18, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 19, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, true, false, true, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 21, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, false, true, false, "91", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 59, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 59, true, true, false, "93", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 65, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 65, true, true, false, "93", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 66, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 66, true, true, false, "93", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 67, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 67, true, true, false, "93", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 25, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, true, false, true, "P1", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { 69, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 69, true, true, false, "98", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false },
                    { 64, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 64, false, true, false, "", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LicenseDetail_LicenseCode",
                table: "LicenseDetail",
                column: "LicenseCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicenseDetail");

            migrationBuilder.AddColumn<bool>(
                name: "LicensedToProvideCare",
                table: "LicenseLookup",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Manual",
                table: "LicenseLookup",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NamedInImReg",
                table: "LicenseLookup",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "LicenseLookup",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrescriberIdType",
                table: "LicenseLookup",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Validate",
                table: "LicenseLookup",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 1,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 2,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 3,
                columns: new[] { "LicensedToProvideCare", "Manual", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 4,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 5,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 6,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 7,
                columns: new[] { "LicensedToProvideCare", "Manual", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 8,
                columns: new[] { "LicensedToProvideCare", "Manual", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 9,
                columns: new[] { "LicensedToProvideCare", "Manual", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 10,
                columns: new[] { "LicensedToProvideCare", "Manual", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 11,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "91" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 12,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 13,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 14,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 15,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 16,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 17,
                columns: new[] { "LicensedToProvideCare", "Manual", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 18,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 19,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 20,
                columns: new[] { "Manual", "Prefix", "Validate" },
                values: new object[] { true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 21,
                columns: new[] { "Manual", "Prefix", "Validate" },
                values: new object[] { true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 22,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 23,
                columns: new[] { "Manual", "Prefix", "Validate" },
                values: new object[] { true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 24,
                columns: new[] { "LicensedToProvideCare", "Manual", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, true, "91", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 25,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "P1", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 26,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "P1", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 27,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "P1", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 28,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "P1" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 29,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "T9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 30,
                columns: new[] { "Manual", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "P1", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 31,
                columns: new[] { "Manual", "Prefix" },
                values: new object[] { true, "T9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 32,
                columns: new[] { "LicensedToProvideCare", "Prefix", "PrescriberIdType", "Validate" },
                values: new object[] { true, "R9", 1, true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 33,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "R9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 34,
                columns: new[] { "Manual", "Prefix" },
                values: new object[] { true, "R9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 35,
                columns: new[] { "LicensedToProvideCare", "Manual", "Prefix", "PrescriberIdType" },
                values: new object[] { true, true, "R9", 1 });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 36,
                columns: new[] { "LicensedToProvideCare", "Manual", "Prefix" },
                values: new object[] { true, true, "R9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 37,
                columns: new[] { "Prefix", "Validate" },
                values: new object[] { "R9", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 39,
                columns: new[] { "LicensedToProvideCare", "Prefix", "PrescriberIdType", "Validate" },
                values: new object[] { true, "R9", 1, true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 40,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "R9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 41,
                columns: new[] { "LicensedToProvideCare", "Prefix", "PrescriberIdType", "Validate" },
                values: new object[] { true, "Y9", 1, true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 42,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "Y9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 43,
                columns: new[] { "Manual", "Prefix" },
                values: new object[] { true, "Y9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 45,
                columns: new[] { "LicensedToProvideCare", "Prefix", "PrescriberIdType", "Validate" },
                values: new object[] { true, "Y9", 1, true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 46,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "Y9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 47,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "PrescriberIdType", "Validate" },
                values: new object[] { true, true, "96", 2, true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 48,
                columns: new[] { "LicensedToProvideCare", "Manual", "NamedInImReg", "Prefix" },
                values: new object[] { true, true, true, "96" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 49,
                columns: new[] { "Manual", "NamedInImReg", "Prefix", "Validate" },
                values: new object[] { true, true, "96", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 51,
                columns: new[] { "LicensedToProvideCare", "NamedInImReg", "Prefix", "PrescriberIdType", "Validate" },
                values: new object[] { true, true, "96", 2, true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 52,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "L9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 53,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "L9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 54,
                columns: new[] { "Manual", "Prefix" },
                values: new object[] { true, "L9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 55,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "L9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 59,
                columns: new[] { "LicensedToProvideCare", "Manual", "Prefix", "Validate" },
                values: new object[] { true, true, "93", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 60,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "98" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 61,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "98" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 62,
                columns: new[] { "LicensedToProvideCare", "Prefix" },
                values: new object[] { true, "98" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 63,
                columns: new[] { "LicensedToProvideCare", "Manual", "Prefix" },
                values: new object[] { true, true, "98" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 64,
                columns: new[] { "Manual", "Prefix" },
                values: new object[] { true, "" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 65,
                columns: new[] { "LicensedToProvideCare", "Manual", "Prefix", "Validate" },
                values: new object[] { true, true, "93", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 66,
                columns: new[] { "LicensedToProvideCare", "Manual", "Prefix", "Validate" },
                values: new object[] { true, true, "93", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 67,
                columns: new[] { "LicensedToProvideCare", "Manual", "Prefix", "Validate" },
                values: new object[] { true, true, "93", true });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 68,
                columns: new[] { "LicensedToProvideCare", "Manual", "Prefix" },
                values: new object[] { true, true, "T9" });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 69,
                columns: new[] { "LicensedToProvideCare", "Manual", "Prefix" },
                values: new object[] { true, true, "98" });

            migrationBuilder.CreateIndex(
                name: "IX_Job_EnrolleeId",
                table: "Job",
                column: "EnrolleeId");
        }
    }
}
