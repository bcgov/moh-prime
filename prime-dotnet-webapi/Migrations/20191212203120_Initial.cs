using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollegeLookup",
                columns: table => new
                {
                    Code = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Prefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "CountryLookup",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Enrollee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    LicensePlate = table.Column<string>(maxLength: 20, nullable: true),
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
                    DeviceProviderNumber = table.Column<string>(maxLength: 5, nullable: true),
                    IsInsulinPumpProvider = table.Column<bool>(nullable: true),
                    HasConviction = table.Column<bool>(nullable: true),
                    HasConvictionDetails = table.Column<string>(nullable: true),
                    HasRegistrationSuspended = table.Column<bool>(nullable: true),
                    HasRegistrationSuspendedDetails = table.Column<string>(nullable: true),
                    HasDisciplinaryAction = table.Column<bool>(nullable: true),
                    HasDisciplinaryActionDetails = table.Column<string>(nullable: true),
                    HasPharmaNetSuspended = table.Column<bool>(nullable: true),
                    HasPharmaNetSuspendedDetails = table.Column<string>(nullable: true),
                    ProfileCompleted = table.Column<bool>(nullable: false)
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
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationTypeLookup",
                columns: table => new
                {
                    Code = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
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
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "StatusReasonLookup",
                columns: table => new
                {
                    Code = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusReasonLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ProvinceLookup",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    CountryCode = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvinceLookup", x => x.Code);
                    table.ForeignKey(
                        name: "FK_ProvinceLookup_CountryLookup_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "CountryLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessAgreementNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EnrolleeId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    NoteDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessAgreementNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessAgreementNotes_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdjudicatorNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EnrolleeId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: false),
                    NoteDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjudicatorNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdjudicatorNotes_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrolmentCertificateAccessToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentCertificateAccessToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolmentCertificateAccessToken_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrolmentCertificateNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EnrolleeId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    NoteDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentCertificateNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolmentCertificateNotes_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Job_Enrollee_EnrolleeId",
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
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    OrganizationTypeCode = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organization_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Organization_OrganizationTypeLookup_OrganizationTypeCode",
                        column: x => x.OrganizationTypeCode,
                        principalTable: "OrganizationTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    CollegeCode = table.Column<short>(nullable: false),
                    LicenseNumber = table.Column<string>(maxLength: 5, nullable: false),
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
                        name: "FK_Certification_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
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
                name: "EnrolmentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    StatusCode = table.Column<short>(nullable: false),
                    StatusDate = table.Column<DateTime>(nullable: false),
                    PharmaNetStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatuses_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatuses_StatusLookup_StatusCode",
                        column: x => x.StatusCode,
                        principalTable: "StatusLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    CountryCode = table.Column<string>(nullable: true),
                    ProvinceCode = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Street2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Postal = table.Column<string>(nullable: true),
                    AddressType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_CountryLookup_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "CountryLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_ProvinceLookup_ProvinceCode",
                        column: x => x.ProvinceCode,
                        principalTable: "ProvinceLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
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
                name: "EnrolmentStatusReasons",
                columns: table => new
                {
                    EnrolmentStatusId = table.Column<int>(nullable: false),
                    StatusCode = table.Column<short>(nullable: false),
                    StatusReasonCode = table.Column<short>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentStatusReasons", x => new { x.EnrolmentStatusId, x.StatusCode, x.StatusReasonCode });
                    table.ForeignKey(
                        name: "FK_EnrolmentStatusReasons_EnrolmentStatuses_EnrolmentStatusId",
                        column: x => x.EnrolmentStatusId,
                        principalTable: "EnrolmentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatusReasons_StatusReasonLookup_StatusReasonCode",
                        column: x => x.StatusReasonCode,
                        principalTable: "StatusReasonLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CollegeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "Prefix", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "College of Physicians and Surgeons of BC (CPSBC)", "91", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "College of Pharmacists of BC (CPBC)", "P1", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "BC College of Nursing Professionals (BCCNP)", "96", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CountryLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Canada", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "United States", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "JobNameLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Medical Office Assistant", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Pharmacy Assistant", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Registration Clerk", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Ward Clerk", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)41, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Practicing Registered Psychiatric Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)40, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Employed Student Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)39, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Registered Nurse (Emergency)", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)38, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Registered Nurse (Special Event)", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)37, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Non-Practicing Licensed Graduate Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)32, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Practicing Registered Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)34, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Non-Practicing Registered Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)33, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional Registered Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)42, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional Registered Psychiatric Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)31, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Non-Practicing Pharmacy Technician", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)36, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional Licensed Graduate Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)43, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Non-Practicing Registered Psychiatric Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)48, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional Nurse Practitioner", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)45, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Registered Psychiatric Nurse (Emergency)", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)46, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Employed Student Psychiatric Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)47, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Practicing Nurse Practitioner", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)30, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Non-Practicing Pharmacist", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)49, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Non-practicing Nurse Practitioner", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)50, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Nurse Practitioner (Special Event)", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)51, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Nurse Practitioner (Emergency)", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)52, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Practicing Licensed Practical Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)53, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional Licensed Practical Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)54, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Non-Practicing Licensed Practical Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)55, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Licensed Practical Nurse (Emergency)", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)56, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Licensed Practical Nurse (Special Event)", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)44, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Registered Psychiatric Nurse (Special Event)", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)29, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Pharmacy Technician", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)35, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Practicing Licensed Graduate Nurse", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)27, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Temporary Pharmacist", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Full - Family", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Full - Specialty", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)28, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Student Pharmacist", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Osteopathic", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional - Family", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)6, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Provisional - Speciality", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)7, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Academic", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)8, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Conditional - Practice Limitations", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)9, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Conditional - Practice Setting", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)10, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Conditional - Disciplined", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)11, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Educational - Medical Student", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)12, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Educational - Postgraduate Resident", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)13, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Educational - Postgraduate Resident Elective", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Special", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)15, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Educational - Postgraduate Trainee", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)16, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Clinical Observership", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)17, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Visitor", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)18, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Emergency - Family", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)19, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Emergency - Specialty", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)20, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Retired - Life ", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)21, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Temporarily Inactive", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)14, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Educational - Postgraduate Fellow", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)22, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Surgical Assistant", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)23, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Administrative", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)24, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Assessment", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)25, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Full Pharmacist", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)26, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Limited Pharmacist", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "OrganizationTypeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)4, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Community Pharmacy", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Community Practice", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Primary Care Network", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Community Health Practice Access to PharmaNet (ComPAP)", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Health Authority", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "PracticeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Remote Practice", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Reproductive Health - STI", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Reproductive health - Contraceptive Management", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "First Call", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "StatusLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)6, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Declined Access Agreement", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Accepted Access Agreement", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Declined", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Submitted", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "In Progress", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Adjudicated/Approved", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "StatusReasonLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)7, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Self Declaration", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Automatic", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Manual", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Name Discrepancy", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)4, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Not in PharmaNet", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)5, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Insulin Pump Provider", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)6, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Licence Class", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)8, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Contact address or Identity Address Out of British Columbia", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)2, (short)27, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)29, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)28, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)3, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)26, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)25, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)24, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)23, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)22, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)21, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)20, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)19, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)30, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)18, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)16, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)15, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)14, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)13, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)12, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)11, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)10, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)9, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)8, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)7, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)6, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)17, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)2, (short)31, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)32, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)33, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)1, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)2, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)56, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)55, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)54, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)53, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)52, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)51, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)50, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)49, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)48, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)47, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)46, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)45, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)44, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)43, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)42, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)41, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)40, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)39, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)38, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)37, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)36, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)35, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)34, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)5, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)1, (short)4, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "CollegePractice",
                columns: new[] { "CollegeCode", "PracticeCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)3, (short)4, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)1, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)2, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { (short)3, (short)3, new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "ProvinceLookup",
                columns: new[] { "Code", "CountryCode", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { "WY", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Wyoming", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "WI", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Wisconsin", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "WV", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "West Virginia", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "WA", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Washington", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "VA", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Virginia", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "VI", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Virgin Islands, U.S.", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "UT", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Utah", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "IL", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Illinois", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ID", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Idaho", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "HI", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Hawaii", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "GU", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Guam", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "GA", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Georgia", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "FL", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Florida", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "DC", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "District of Columbia", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "DE", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Delaware", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "CT", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Connecticut", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "CO", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Colorado", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "CA", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "California", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AR", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Arkansas", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AZ", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Arizona", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "IN", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Indiana", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AS", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "American Samoa", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AL", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Alabama", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "YT", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Yukon", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NU", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Nunavut", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NT", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Northwest Territories", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "SK", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Saskatchewan", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "QC", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Quebec", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "PE", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Prince Edward Island", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ON", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Ontario", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NS", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Nova Scotia", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NL", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Newfoundland and Labrador", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NB", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "New Brunswick", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MB", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Manitoba", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "BC", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "British Columbia", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AK", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Alaska", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "IA", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Iowa", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "KS", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Kansas", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "KY", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Kentucky", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "UM", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "United States Minor Outlying Islands", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "TX", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Texas", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "TN", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Tennessee", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "SD", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "South Dakota", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "SC", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "South Carolina", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "RI", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Rhode Island", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "PR", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Puerto Rico", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "PA", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Pennsylvania", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "OR", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Oregon", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "OK", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Oklahoma", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "OH", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Ohio", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MP", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Northern Mariana Islands", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ND", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "North Dakota", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NC", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "North Carolina", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NY", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "New York", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NM", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "New Mexico", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NJ", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "New Jersey", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NH", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "New Hampshire", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NV", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Nevada", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "NE", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Nebraska", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MT", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Montana", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MO", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Missouri", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MS", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Mississippi", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MN", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Minnesota", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MI", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Michigan", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MA", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Massachusetts", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "MD", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Maryland", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "ME", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Maine", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "LA", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Louisiana", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "VT", "US", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Vermont", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") },
                    { "AB", "CA", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000"), "Alberta", new DateTime(2019, 12, 12, 12, 31, 19, 221, DateTimeKind.Local).AddTicks(6670), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessAgreementNotes_EnrolleeId",
                table: "AccessAgreementNotes",
                column: "EnrolleeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryCode",
                table: "Address",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ProvinceCode",
                table: "Address",
                column: "ProvinceCode");

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
                name: "IX_AdjudicatorNotes_EnrolleeId",
                table: "AdjudicatorNotes",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_Certification_CollegeCode",
                table: "Certification",
                column: "CollegeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Certification_EnrolleeId",
                table: "Certification",
                column: "EnrolleeId");

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
                name: "IX_Enrollee_UserId",
                table: "Enrollee",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentCertificateAccessToken_EnrolleeId",
                table: "EnrolmentCertificateAccessToken",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentCertificateNotes_EnrolleeId",
                table: "EnrolmentCertificateNotes",
                column: "EnrolleeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatuses_EnrolleeId",
                table: "EnrolmentStatuses",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatuses_StatusCode",
                table: "EnrolmentStatuses",
                column: "StatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatusReasons_StatusReasonCode",
                table: "EnrolmentStatusReasons",
                column: "StatusReasonCode");

            migrationBuilder.CreateIndex(
                name: "IX_Job_EnrolleeId",
                table: "Job",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_EnrolleeId",
                table: "Organization",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_OrganizationTypeCode",
                table: "Organization",
                column: "OrganizationTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceLookup_CountryCode",
                table: "ProvinceLookup",
                column: "CountryCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessAgreementNotes");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "AdjudicatorNotes");

            migrationBuilder.DropTable(
                name: "Certification");

            migrationBuilder.DropTable(
                name: "CollegeLicense");

            migrationBuilder.DropTable(
                name: "CollegePractice");

            migrationBuilder.DropTable(
                name: "EnrolmentCertificateAccessToken");

            migrationBuilder.DropTable(
                name: "EnrolmentCertificateNotes");

            migrationBuilder.DropTable(
                name: "EnrolmentStatusReasons");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "JobNameLookup");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "ProvinceLookup");

            migrationBuilder.DropTable(
                name: "LicenseLookup");

            migrationBuilder.DropTable(
                name: "CollegeLookup");

            migrationBuilder.DropTable(
                name: "PracticeLookup");

            migrationBuilder.DropTable(
                name: "EnrolmentStatuses");

            migrationBuilder.DropTable(
                name: "StatusReasonLookup");

            migrationBuilder.DropTable(
                name: "OrganizationTypeLookup");

            migrationBuilder.DropTable(
                name: "CountryLookup");

            migrationBuilder.DropTable(
                name: "Enrollee");

            migrationBuilder.DropTable(
                name: "StatusLookup");
        }
    }
}
