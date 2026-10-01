using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prime.Migrations
{
    /// <inheritdoc />
    public partial class UnlicensedStudentNurseChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentTypeLookup",
                columns: table => new
                {
                    Code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTypeLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "UnlicensedStudent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnrolleeId = table.Column<int>(type: "integer", nullable: false),
                    StudentTypeCode = table.Column<int>(type: "integer", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnlicensedStudent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnlicensedStudent_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnlicensedStudent_StudentTypeLookup_StudentTypeCode",
                        column: x => x.StudentTypeCode,
                        principalTable: "StudentTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StatusReasonLookup",
                columns: new[] { "Code", "Name" },
                values: new object[] { 24, "Enrollee is a unlicensed student" });

            migrationBuilder.InsertData(
                table: "StudentTypeLookup",
                columns: new[] { "Code", "Name", "Weight" },
                values: new object[,]
                {
                    { 1, "Student Nurse Practitioner", 10 },
                    { 2, "Student Registered Nurse", 20 },
                    { 3, "Student Registered Psychiatric Nurse", 30 },
                    { 4, "Student Licensed Practical Nurse", 40 },
                    { 5, "Student Midwife", 50 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnlicensedStudent_EnrolleeId",
                table: "UnlicensedStudent",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnlicensedStudent_StudentTypeCode",
                table: "UnlicensedStudent",
                column: "StudentTypeCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnlicensedStudent");

            migrationBuilder.DropTable(
                name: "StudentTypeLookup");

            migrationBuilder.DeleteData(
                table: "StatusReasonLookup",
                keyColumn: "Code",
                keyValue: 24);
        }
    }
}
