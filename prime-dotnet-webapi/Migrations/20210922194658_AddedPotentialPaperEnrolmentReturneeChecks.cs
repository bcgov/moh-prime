using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddedPotentialPaperEnrolmentReturneeChecks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrolleeLinkedEnrolments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    PaperEnrolleeId = table.Column<int>(nullable: false),
                    UserProvidedGpid = table.Column<string>(nullable: true),
                    EnrolmentCreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeLinkedEnrolments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeLinkedEnrolments_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeLinkedEnrolments_Enrollee_PaperEnrolleeId",
                        column: x => x.PaperEnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StatusReasonLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 19, "PRIME enrolment does not match paper enrollee record" },
                    { 20, "Possible match with paper enrolment" },
                    { 21, "Unable to link enrollee to paper enrolment" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeLinkedEnrolments_EnrolleeId",
                table: "EnrolleeLinkedEnrolments",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeLinkedEnrolments_PaperEnrolleeId",
                table: "EnrolleeLinkedEnrolments",
                column: "PaperEnrolleeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolleeLinkedEnrolments");

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 21);
        }
    }
}
