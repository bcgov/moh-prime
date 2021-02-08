using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddPrescriberIdMandatorytoLicence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrescriberIdMandatory",
                table: "LicenseLookup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EnrolleeNotification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    AdminId = table.Column<int>(nullable: false),
                    AssigneeId = table.Column<int>(nullable: false),
                    EnrolleeNoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeNotification_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeNotification_Admin_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeNotification_EnrolleeNote_EnrolleeNoteId",
                        column: x => x.EnrolleeNoteId,
                        principalTable: "EnrolleeNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteNotification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    AdminId = table.Column<int>(nullable: false),
                    AssigneeId = table.Column<int>(nullable: false),
                    SiteRegistrationNoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteNotification_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteNotification_Admin_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteNotification_SiteRegistrationNote_SiteRegistrationNoteId",
                        column: x => x.SiteRegistrationNoteId,
                        principalTable: "SiteRegistrationNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 1,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 2,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 3,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 4,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 5,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 6,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 7,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 8,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 9,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 10,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 11,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 12,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 13,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 14,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 15,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 16,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 17,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 18,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 19,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 20,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 21,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 22,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 23,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 24,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 25,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 26,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 27,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 28,
                columns: new[] { "NamedInImReg", "PrescriberIdMandatory", "Validate" },
                values: new object[] { false, 1, false });

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 29,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 30,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 31,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 32,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 33,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 34,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 35,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 36,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 37,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 38,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 39,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 40,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 41,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 42,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 43,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 44,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 45,
                column: "PrescriberIdMandatory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 46,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 47,
                column: "PrescriberIdMandatory",
                value: 3);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 48,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 49,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 50,
                column: "PrescriberIdMandatory",
                value: 3);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 51,
                column: "PrescriberIdMandatory",
                value: 3);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 52,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 53,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 54,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 55,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 56,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 57,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 58,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 59,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 60,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 61,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 62,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 63,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 64,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 65,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 66,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 67,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 68,
                column: "PrescriberIdMandatory",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeNotification_AdminId",
                table: "EnrolleeNotification",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeNotification_AssigneeId",
                table: "EnrolleeNotification",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeNotification_EnrolleeNoteId",
                table: "EnrolleeNotification",
                column: "EnrolleeNoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SiteNotification_AdminId",
                table: "SiteNotification",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteNotification_AssigneeId",
                table: "SiteNotification",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteNotification_SiteRegistrationNoteId",
                table: "SiteNotification",
                column: "SiteRegistrationNoteId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolleeNotification");

            migrationBuilder.DropTable(
                name: "SiteNotification");

            migrationBuilder.DropColumn(
                name: "PrescriberIdMandatory",
                table: "LicenseLookup");

            migrationBuilder.UpdateData(
                table: "LicenseLookup",
                keyColumn: "Code",
                keyValue: 28,
                columns: new[] { "NamedInImReg", "Validate" },
                values: new object[] { true, true });
        }
    }
}
