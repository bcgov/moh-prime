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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Prefix = table.Column<string>(nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<Guid>(nullable: false),
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
                    VoiceExtension = table.Column<string>(nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "StatusLookup",
                columns: table => new
                {
                    Code = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EnrolleeId = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Postal = table.Column<string>(nullable: true),
                    AddressType = table.Column<int>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EnrolleeId = table.Column<int>(nullable: false),
                    AppliedDate = table.Column<DateTime>(nullable: false),
                    Approved = table.Column<bool>(nullable: true),
                    ApprovedReason = table.Column<string>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    HasCertification = table.Column<bool>(nullable: true),
                    IsDeviceProvider = table.Column<bool>(nullable: true),
                    DeviceProviderNumber = table.Column<string>(maxLength: 5, nullable: true),
                    IsInsulinPumpProvider = table.Column<bool>(nullable: true),
                    IsAccessingPharmaNetOnBehalfOf = table.Column<bool>(nullable: true),
                    HasConviction = table.Column<bool>(nullable: true),
                    HasConvictionDetails = table.Column<string>(nullable: true),
                    HasRegistrationSuspended = table.Column<bool>(nullable: true),
                    HasRegistrationSuspendedDetails = table.Column<string>(nullable: true),
                    HasDisciplinaryAction = table.Column<bool>(nullable: true),
                    HasDisciplinaryActionDetails = table.Column<string>(nullable: true),
                    HasPharmaNetSuspended = table.Column<bool>(nullable: true),
                    HasPharmaNetSuspendedDetails = table.Column<string>(nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
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
                name: "CollegeLicense",
                columns: table => new
                {
                    CollegeCode = table.Column<short>(nullable: false),
                    LicenseCode = table.Column<short>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeLicense", x => new { x.CollegeCode, x.LicenseCode });
                    table.ForeignKey(
                        name: "FK_CollegeLicense_CollegeLookup_CollegeCode",
                        column: x => x.CollegeCode,
                        principalTable: "CollegeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollegeLicense_LicenseLookup_LicenseCode",
                        column: x => x.LicenseCode,
                        principalTable: "LicenseLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollegePractice",
                columns: table => new
                {
                    CollegeCode = table.Column<short>(nullable: false),
                    PracticeCode = table.Column<short>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegePractice", x => new { x.CollegeCode, x.PracticeCode });
                    table.ForeignKey(
                        name: "FK_CollegePractice_CollegeLookup_CollegeCode",
                        column: x => x.CollegeCode,
                        principalTable: "CollegeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollegePractice_PracticeLookup_PracticeCode",
                        column: x => x.PracticeCode,
                        principalTable: "PracticeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EnrolmentId = table.Column<int>(nullable: false),
                    CollegeCode = table.Column<short>(nullable: false),
                    LicenseNumber = table.Column<string>(maxLength: 5, nullable: false),
                    LicenseCode = table.Column<short>(nullable: false),
                    RenewalDate = table.Column<DateTime>(nullable: false),
                    PracticeCode = table.Column<short>(nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
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
                name: "EnrolmentStatus",
                columns: table => new
                {
                    EnrolmentId = table.Column<int>(nullable: false),
                    StatusCode = table.Column<short>(nullable: false),
                    StatusDate = table.Column<DateTime>(nullable: false),
                    IsCurrent = table.Column<bool>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentStatus", x => new { x.EnrolmentId, x.StatusCode });
                    table.ForeignKey(
                        name: "FK_EnrolmentStatus_Enrolment_EnrolmentId",
                        column: x => x.EnrolmentId,
                        principalTable: "Enrolment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatus_StatusLookup_StatusCode",
                        column: x => x.StatusCode,
                        principalTable: "StatusLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EnrolmentId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EnrolmentId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OrganizationTypeCode = table.Column<short>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
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
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "Prefix", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "College of Physicians and Surgeons of BC (CPSBC)", "91", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "College of Pharmacists of BC (CPBC)", "P1", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "College of Registered Nurses of BC (CRNBC)", "96", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "None", null, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "JobNameLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Medical Office Assistant", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Midwife", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Nurse (not nurse practitioner)", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Pharmacy Assistant", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Pharmacy Technician", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)6, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Registration Clerk", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)7, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Ward Clerk", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)8, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Other", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)5, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Registered Nurse", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Registered Nurse", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Full - Specialty", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Full - Pharmacist", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Full - General", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "OrganizationNameLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Vancouver Island Health", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Shoppers Drug Mart", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "OrganizationTypeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Health Authority", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Pharmacy", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "PracticeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Remote Practice", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Reproductive Care", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Sexually Transmitted Infections (STI)", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "None", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "StatusLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "In Progress", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Submitted", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Approved", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Denied", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), "Accepted", new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)3, (short)1, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)2, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)3, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)4, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)5, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)5, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CollegePractice",
                columns: new[] { "CollegeCode", "PracticeCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, (short)4, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)3, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)3, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)3, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)2, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)1, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)2, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)4, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)1, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)1, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)2, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)4, new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 10, 17, 13, 11, 39, 787, DateTimeKind.Local).AddTicks(498), new Guid("00000000-0000-0000-0000-000000000000") }
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
                name: "IX_CollegeLicense_LicenseCode",
                table: "CollegeLicense",
                column: "LicenseCode");

            migrationBuilder.CreateIndex(
                name: "IX_CollegePractice_PracticeCode",
                table: "CollegePractice",
                column: "PracticeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolment_EnrolleeId",
                table: "Enrolment",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatus_StatusCode",
                table: "EnrolmentStatus",
                column: "StatusCode");

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
                name: "CollegeLicense");

            migrationBuilder.DropTable(
                name: "CollegePractice");

            migrationBuilder.DropTable(
                name: "EnrolmentStatus");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "JobNameLookup");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "OrganizationNameLookup");

            migrationBuilder.DropTable(
                name: "LicenseLookup");

            migrationBuilder.DropTable(
                name: "CollegeLookup");

            migrationBuilder.DropTable(
                name: "PracticeLookup");

            migrationBuilder.DropTable(
                name: "StatusLookup");

            migrationBuilder.DropTable(
                name: "Enrolment");

            migrationBuilder.DropTable(
                name: "OrganizationTypeLookup");

            migrationBuilder.DropTable(
                name: "Enrollee");
        }
    }
}
