using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollegeLookup",
                columns: table => new
                {
                    Code = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Enrollee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    PreferredFirstName = table.Column<string>(nullable: true),
                    PreferredMiddleName = table.Column<string>(nullable: true),
                    PreferredLastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    ContactEmail = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    VoicePhone = table.Column<string>(nullable: true),
                    VoiceExtension = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobNameLookup",
                columns: table => new
                {
                    Code = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobNameLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "LicenseLookup",
                columns: table => new
                {
                    Code = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationNameLookup",
                columns: table => new
                {
                    Code = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationNameLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationTypeLookup",
                columns: table => new
                {
                    Code = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationTypeLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "PracticeLookup",
                columns: table => new
                {
                    Code = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnrolleeId = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Postal = table.Column<string>(nullable: true),
                    AddressType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Enrollee_EnrolleeId1",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrolment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnrolleeId = table.Column<int>(nullable: false),
                    AppliedDate = table.Column<DateTime>(nullable: false),
                    Approved = table.Column<bool>(nullable: true),
                    ApprovedReason = table.Column<string>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    HasCertification = table.Column<bool>(nullable: true),
                    IsDeviceProvider = table.Column<bool>(nullable: true),
                    DeviceProviderNumber = table.Column<string>(nullable: true),
                    IsInsulinPumpProvider = table.Column<bool>(nullable: true),
                    IsAccessingPharmaNetOnBehalfOf = table.Column<bool>(nullable: true),
                    HasConviction = table.Column<bool>(nullable: true),
                    HasRegistrationSuspended = table.Column<bool>(nullable: true),
                    HasDisciplinaryAction = table.Column<bool>(nullable: true),
                    HasPharmaNetSuspended = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrolment_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnrolmentId = table.Column<int>(nullable: false),
                    CollegeCode = table.Column<short>(nullable: false),
                    LicenseNumber = table.Column<string>(nullable: false),
                    LicenseCode = table.Column<short>(nullable: false),
                    RenewalDate = table.Column<DateTime>(nullable: false),
                    PracticeCode = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certification_CollegeLookup_CollegeCode",
                        column: x => x.CollegeCode,
                        principalTable: "CollegeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certification_Enrolment_EnrolmentId",
                        column: x => x.EnrolmentId,
                        principalTable: "Enrolment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certification_LicenseLookup_LicenseCode",
                        column: x => x.LicenseCode,
                        principalTable: "LicenseLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certification_PracticeLookup_PracticeCode",
                        column: x => x.PracticeCode,
                        principalTable: "PracticeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnrolmentId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Job_Enrolment_EnrolmentId",
                        column: x => x.EnrolmentId,
                        principalTable: "Enrolment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnrolmentId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OrganizationTypeCode = table.Column<short>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organization_Enrolment_EnrolmentId",
                        column: x => x.EnrolmentId,
                        principalTable: "Enrolment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Organization_OrganizationTypeLookup_OrganizationTypeCode",
                        column: x => x.OrganizationTypeCode,
                        principalTable: "OrganizationTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CollegeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { (short)1, "College of Physicians and Surgeons of BC (CPSBC)" },
                    { (short)2, "College of Pharmacists of BC (CPBC)" },
                    { (short)3, "College of Registered Nurses of BC (CRNBC)" },
                    { (short)4, "None" }
                });

            migrationBuilder.InsertData(
                table: "JobNameLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { (short)1, "Medical Office Assistant" },
                    { (short)2, "Midwife" },
                    { (short)3, "Nurse (not nurse practitioner)" },
                    { (short)4, "Pharmacy Assistant" },
                    { (short)5, "Pharmacy Technician" },
                    { (short)6, "Registration Clerk" },
                    { (short)7, "Ward Clerk" },
                    { (short)8, "Other" }
                });

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { (short)5, "Temporary Registered Nurse" },
                    { (short)4, "Registered Nurse" },
                    { (short)1, "Full - General" },
                    { (short)2, "Full - Pharmacist" },
                    { (short)3, "Full - Specialty" }
                });

            migrationBuilder.InsertData(
                table: "OrganizationNameLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { (short)1, "Vancouver Island Health" },
                    { (short)2, "Shoppers Drug Mart" }
                });

            migrationBuilder.InsertData(
                table: "OrganizationTypeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { (short)1, "Health Authority" },
                    { (short)2, "Pharmacy" }
                });

            migrationBuilder.InsertData(
                table: "PracticeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { (short)3, "Sexually Transmitted Infections (STI)" },
                    { (short)1, "Remote Practice" },
                    { (short)2, "Reproductive Care" },
                    { (short)4, "None" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeId_AddressType",
                table: "Address",
                columns: new[] { "EnrolleeId", "AddressType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_EnrolleeId",
                table: "Address",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_Certification_CollegeCode",
                table: "Certification",
                column: "CollegeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Certification_EnrolmentId",
                table: "Certification",
                column: "EnrolmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Certification_LicenseCode",
                table: "Certification",
                column: "LicenseCode");

            migrationBuilder.CreateIndex(
                name: "IX_Certification_PracticeCode",
                table: "Certification",
                column: "PracticeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolment_EnrolleeId",
                table: "Enrolment",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_EnrolmentId",
                table: "Job",
                column: "EnrolmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_EnrolmentId",
                table: "Organization",
                column: "EnrolmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationTypeCode",
                table: "Organization",
                column: "OrganizationTypeCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Certification");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "JobNameLookup");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "OrganizationNameLookup");

            migrationBuilder.DropTable(
                name: "CollegeLookup");

            migrationBuilder.DropTable(
                name: "LicenseLookup");

            migrationBuilder.DropTable(
                name: "PracticeLookup");

            migrationBuilder.DropTable(
                name: "Enrolment");

            migrationBuilder.DropTable(
                name: "OrganizationTypeLookup");

            migrationBuilder.DropTable(
                name: "Enrollee");
        }
    }
}
