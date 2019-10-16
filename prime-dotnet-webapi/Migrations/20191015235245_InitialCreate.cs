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
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
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
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
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
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
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
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
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
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
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
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
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
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
                    UpdatedTimeStamp = table.Column<DateTime>(nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EnrolleeId = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Postal = table.Column<string>(nullable: true),
                    AddressType = table.Column<int>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
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
                    DeviceProviderNumber = table.Column<string>(nullable: true),
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
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
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
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
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
                name: "Certification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EnrolmentId = table.Column<int>(nullable: false),
                    CollegeCode = table.Column<short>(nullable: false),
                    LicenseNumber = table.Column<string>(nullable: false),
                    LicenseCode = table.Column<short>(nullable: false),
                    RenewalDate = table.Column<DateTime>(nullable: false),
                    PracticeCode = table.Column<short>(nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
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
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EnrolmentId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
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
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: true),
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
                    { (short)1, new DateTime(2019, 10, 15, 16, 52, 45, 237, DateTimeKind.Local).AddTicks(7626), Guid.Empty, "College of Physicians and Surgeons of BC (CPSBC)", "91", new DateTime(2019, 10, 15, 16, 52, 45, 241, DateTimeKind.Local).AddTicks(9857), Guid.Empty },
                    { (short)2, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(200), Guid.Empty, "College of Pharmacists of BC (CPBC)", "P1", new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(213), Guid.Empty },
                    { (short)3, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(223), Guid.Empty, "College of Registered Nurses of BC (CRNBC)", "96", new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(226), Guid.Empty },
                    { (short)4, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(229), Guid.Empty, "None", null, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(231), Guid.Empty }
                });

            migrationBuilder.InsertData(
                table: "JobNameLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9846), Guid.Empty, "Medical Office Assistant", new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9865), Guid.Empty },
                    { (short)2, new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9887), Guid.Empty, "Midwife", new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9889), Guid.Empty },
                    { (short)3, new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9892), Guid.Empty, "Nurse (not nurse practitioner)", new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9895), Guid.Empty },
                    { (short)4, new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9898), Guid.Empty, "Pharmacy Assistant", new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9900), Guid.Empty },
                    { (short)5, new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9903), Guid.Empty, "Pharmacy Technician", new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9906), Guid.Empty },
                    { (short)6, new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9908), Guid.Empty, "Registration Clerk", new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9911), Guid.Empty },
                    { (short)7, new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9913), Guid.Empty, "Ward Clerk", new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9916), Guid.Empty },
                    { (short)8, new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9918), Guid.Empty, "Other", new DateTime(2019, 10, 15, 16, 52, 45, 243, DateTimeKind.Local).AddTicks(9921), Guid.Empty }
                });

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)5, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(3042), Guid.Empty, "Temporary Registered Nurse", new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(3044), Guid.Empty },
                    { (short)4, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(3036), Guid.Empty, "Registered Nurse", new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(3039), Guid.Empty },
                    { (short)1, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(2998), Guid.Empty, "Full - General", new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(3007), Guid.Empty },
                    { (short)2, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(3025), Guid.Empty, "Full - Pharmacist", new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(3028), Guid.Empty },
                    { (short)3, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(3031), Guid.Empty, "Full - Specialty", new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(3034), Guid.Empty }
                });

            migrationBuilder.InsertData(
                table: "OrganizationNameLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 10, 15, 16, 52, 45, 245, DateTimeKind.Local).AddTicks(7223), Guid.Empty, "Vancouver Island Health", new DateTime(2019, 10, 15, 16, 52, 45, 245, DateTimeKind.Local).AddTicks(7261), Guid.Empty },
                    { (short)2, new DateTime(2019, 10, 15, 16, 52, 45, 245, DateTimeKind.Local).AddTicks(7298), Guid.Empty, "Shoppers Drug Mart", new DateTime(2019, 10, 15, 16, 52, 45, 245, DateTimeKind.Local).AddTicks(7303), Guid.Empty }
                });

            migrationBuilder.InsertData(
                table: "OrganizationTypeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)1, new DateTime(2019, 10, 15, 16, 52, 45, 245, DateTimeKind.Local).AddTicks(9444), Guid.Empty, "Health Authority", new DateTime(2019, 10, 15, 16, 52, 45, 245, DateTimeKind.Local).AddTicks(9458), Guid.Empty },
                    { (short)2, new DateTime(2019, 10, 15, 16, 52, 45, 245, DateTimeKind.Local).AddTicks(9487), Guid.Empty, "Pharmacy", new DateTime(2019, 10, 15, 16, 52, 45, 245, DateTimeKind.Local).AddTicks(9491), Guid.Empty }
                });

            migrationBuilder.InsertData(
                table: "PracticeLookup",
                columns: new[] { "Code", "CreatedTimeStamp", "CreatedUserId", "Name", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)3, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(6069), Guid.Empty, "Sexually Transmitted Infections (STI)", new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(6071), Guid.Empty },
                    { (short)1, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(6034), Guid.Empty, "Remote Practice", new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(6044), Guid.Empty },
                    { (short)2, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(6063), Guid.Empty, "Reproductive Care", new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(6066), Guid.Empty },
                    { (short)4, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(6074), Guid.Empty, "None", new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(6077), Guid.Empty }
                });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode", "CreatedTimeStamp", "CreatedUserId", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { (short)3, (short)1, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(4086), Guid.Empty, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(4089), Guid.Empty },
                    { (short)1, (short)2, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(4045), Guid.Empty, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(4052), Guid.Empty },
                    { (short)1, (short)3, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(4069), Guid.Empty, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(4072), Guid.Empty },
                    { (short)2, (short)4, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(4075), Guid.Empty, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(4078), Guid.Empty },
                    { (short)2, (short)5, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(4080), Guid.Empty, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(4083), Guid.Empty },
                    { (short)3, (short)5, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(4091), Guid.Empty, new DateTime(2019, 10, 15, 16, 52, 45, 242, DateTimeKind.Local).AddTicks(4094), Guid.Empty }
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
                name: "CollegeLicense");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "JobNameLookup");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "OrganizationNameLookup");

            migrationBuilder.DropTable(
                name: "PracticeLookup");

            migrationBuilder.DropTable(
                name: "CollegeLookup");

            migrationBuilder.DropTable(
                name: "LicenseLookup");

            migrationBuilder.DropTable(
                name: "Enrolment");

            migrationBuilder.DropTable(
                name: "OrganizationTypeLookup");

            migrationBuilder.DropTable(
                name: "Enrollee");
        }
    }
}
