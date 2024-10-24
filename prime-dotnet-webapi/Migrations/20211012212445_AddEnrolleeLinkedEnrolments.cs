﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddEnrolleeLinkedEnrolments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrolleeLinkedEnrolment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    PaperEnrolleeId = table.Column<int>(nullable: true),
                    UserProvidedGpid = table.Column<string>(nullable: true),
                    EnrolmentLinkDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeLinkedEnrolment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeLinkedEnrolment_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeLinkedEnrolment_Enrollee_PaperEnrolleeId",
                        column: x => x.PaperEnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_EnrolleeLinkedEnrolment_EnrolleeId",
                table: "EnrolleeLinkedEnrolment",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeLinkedEnrolment_PaperEnrolleeId",
                table: "EnrolleeLinkedEnrolment",
                column: "PaperEnrolleeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolleeLinkedEnrolment");

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
