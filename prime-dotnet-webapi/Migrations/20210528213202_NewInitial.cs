using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    // The old migrations were deleted and comprssed into this NewInitial migration in PR#1367
    public partial class NewInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    IDIR = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgreementVersion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    AgreementType = table.Column<int>(nullable: false),
                    EffectiveDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementVersion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    BannerType = table.Column<int>(nullable: false),
                    BannerLocationCode = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    StartTimestamp = table.Column<DateTime>(nullable: false),
                    EndTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessEventTypeLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessEventTypeLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "CareSettingLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareSettingLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "CollegeLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
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
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Credential",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    ConnectionId = table.Column<string>(nullable: true),
                    SchemaId = table.Column<string>(nullable: true),
                    CredentialExchangeId = table.Column<string>(nullable: true),
                    CredentialDefinitionId = table.Column<string>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    Base64QRCode = table.Column<string>(nullable: true),
                    AcceptedCredentialDate = table.Column<DateTimeOffset>(nullable: true),
                    RevokedCredentialDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credential", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentAccessToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    DocumentGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAccessToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    SendType = table.Column<string>(nullable: true),
                    MsgId = table.Column<Guid>(nullable: true),
                    SentTo = table.Column<string>(nullable: true),
                    Cc = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    DateSent = table.Column<DateTimeOffset>(nullable: true),
                    LatestStatus = table.Column<string>(nullable: true),
                    StatusMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Template = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: false),
                    EmailType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacilityLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "HealthAuthorityLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthAuthorityLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "IdentifierTypeLookup",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentifierTypeLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "JobNameLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
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
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Weight = table.Column<int>(nullable: false),
                    Prefix = table.Column<string>(nullable: true),
                    Manual = table.Column<bool>(nullable: false),
                    Validate = table.Column<bool>(nullable: false),
                    NamedInImReg = table.Column<bool>(nullable: false),
                    LicensedToProvideCare = table.Column<bool>(nullable: false),
                    PrescriberIdType = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "LimitsConditionsClause",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    EffectiveDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LimitsConditionsClause", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Party",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    HPDID = table.Column<string>(maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    GivenNames = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    PreferredFirstName = table.Column<string>(nullable: true),
                    PreferredMiddleName = table.Column<string>(nullable: true),
                    PreferredLastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    JobRoleTitle = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PhoneExtension = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    SMSPhone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlrProvider",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Ipc = table.Column<string>(nullable: true),
                    IdentifierType = table.Column<string>(nullable: true),
                    CollegeId = table.Column<string>(nullable: true),
                    ProviderRoleType = table.Column<string>(nullable: true),
                    MspId = table.Column<string>(nullable: true),
                    NamePrefix = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    ThirdName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Suffix = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    StatusCode = table.Column<string>(nullable: true),
                    StatusReasonCode = table.Column<string>(nullable: true),
                    StatusStartDate = table.Column<DateTime>(nullable: false),
                    StatusExpiryDate = table.Column<DateTime>(nullable: false),
                    Expertise = table.Column<string>(nullable: true),
                    Languages = table.Column<string>(nullable: true),
                    Address1Line1 = table.Column<string>(nullable: true),
                    Address1Line2 = table.Column<string>(nullable: true),
                    Address1Line3 = table.Column<string>(nullable: true),
                    City1 = table.Column<string>(nullable: true),
                    Province1 = table.Column<string>(nullable: true),
                    Country1 = table.Column<string>(nullable: true),
                    PostalCode1 = table.Column<string>(nullable: true),
                    Address1StartDate = table.Column<DateTime>(nullable: false),
                    Address2Line1 = table.Column<string>(nullable: true),
                    Address2Line2 = table.Column<string>(nullable: true),
                    Address2Line3 = table.Column<string>(nullable: true),
                    City2 = table.Column<string>(nullable: true),
                    Province2 = table.Column<string>(nullable: true),
                    Country2 = table.Column<string>(nullable: true),
                    PostalCode2 = table.Column<string>(nullable: true),
                    Address2StartDate = table.Column<DateTime>(nullable: false),
                    Credentials = table.Column<string>(nullable: true),
                    TelephoneAreaCode = table.Column<string>(nullable: true),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    FaxAreaCode = table.Column<string>(nullable: true),
                    FaxNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ConditionCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlrProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracticeLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "PreApprovedRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    PartyType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreApprovedRegistration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivilegeTypeLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivilegeTypeLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "SelfDeclarationTypeLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfDeclarationTypeLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "StatusLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusReasonLookup", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Enrollee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    GPID = table.Column<string>(maxLength: 20, nullable: true),
                    HPDID = table.Column<string>(maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    GivenNames = table.Column<string>(nullable: false),
                    PreferredFirstName = table.Column<string>(nullable: true),
                    PreferredMiddleName = table.Column<string>(nullable: true),
                    PreferredLastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    SmsPhone = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PhoneExtension = table.Column<string>(nullable: true),
                    DeviceProviderNumber = table.Column<string>(nullable: true),
                    IsInsulinPumpProvider = table.Column<bool>(nullable: true),
                    AdjudicatorId = table.Column<int>(nullable: true),
                    ProfileCompleted = table.Column<bool>(nullable: false),
                    AlwaysManual = table.Column<bool>(nullable: false),
                    IdentityAssuranceLevel = table.Column<int>(nullable: false),
                    IdentityProvider = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollee_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CareSettingCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorLookup", x => x.Code);
                    table.ForeignKey(
                        name: "FK_VendorLookup_CareSettingLookup_CareSettingCode",
                        column: x => x.CareSettingCode,
                        principalTable: "CareSettingLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProvinceLookup",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
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
                name: "CollegeLicense",
                columns: table => new
                {
                    CollegeCode = table.Column<int>(nullable: false),
                    LicenseCode = table.Column<int>(nullable: false)
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
                name: "AuthorizedUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    PartyId = table.Column<int>(nullable: false),
                    EmploymentIdentifier = table.Column<string>(nullable: true),
                    HealthAuthorityCode = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorizedUsers_HealthAuthorityLookup_HealthAuthorityCode",
                        column: x => x.HealthAuthorityCode,
                        principalTable: "HealthAuthorityLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorizedUsers_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GisEnrolment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    PartyId = table.Column<int>(nullable: false),
                    LdapUsername = table.Column<string>(nullable: true),
                    LdapLoginSuccessDate = table.Column<DateTimeOffset>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    SubmittedDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GisEnrolment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GisEnrolment_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    RegistrationId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DoingBusinessAs = table.Column<string>(nullable: true),
                    Completed = table.Column<bool>(nullable: false),
                    SubmittedDate = table.Column<DateTimeOffset>(nullable: true),
                    SigningAuthorityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organization_Party_SigningAuthorityId",
                        column: x => x.SigningAuthorityId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartyEnrolment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    PartyId = table.Column<int>(nullable: false),
                    PartyType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyEnrolment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyEnrolment_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollegePractice",
                columns: table => new
                {
                    CollegeCode = table.Column<int>(nullable: false),
                    PracticeCode = table.Column<int>(nullable: false)
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
                name: "PrivilegeGroupLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrivilegeTypeCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivilegeGroupLookup", x => x.Code);
                    table.ForeignKey(
                        name: "FK_PrivilegeGroupLookup_PrivilegeTypeLookup_PrivilegeTypeCode",
                        column: x => x.PrivilegeTypeCode,
                        principalTable: "PrivilegeTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessAgreementNote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    AdjudicatorId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: false),
                    NoteDate = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessAgreementNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessAgreementNote_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessAgreementNote_Enrollee_EnrolleeId",
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
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    CollegeCode = table.Column<int>(nullable: false),
                    LicenseCode = table.Column<int>(nullable: false),
                    LicenseNumber = table.Column<string>(nullable: false),
                    PractitionerId = table.Column<string>(nullable: true),
                    RenewalDate = table.Column<DateTimeOffset>(nullable: false),
                    PracticeCode = table.Column<int>(nullable: true)
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
                name: "EnrolleeAdjudicationDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    DocumentGuid = table.Column<Guid>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    UploadedDate = table.Column<DateTimeOffset>(nullable: false),
                    AdjudicatorId = table.Column<int>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeAdjudicationDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeAdjudicationDocument_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeAdjudicationDocument_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrolleeCareSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    CareSettingCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeCareSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeCareSetting_CareSettingLookup_CareSettingCode",
                        column: x => x.CareSettingCode,
                        principalTable: "CareSettingLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeCareSetting_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrolleeCredential",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    CredentialId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeCredential", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeCredential_Credential_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "Credential",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeCredential_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrolleeHealthAuthority",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    HealthAuthorityCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeHealthAuthority", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeHealthAuthority_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeHealthAuthority_HealthAuthorityLookup_HealthAuthori~",
                        column: x => x.HealthAuthorityCode,
                        principalTable: "HealthAuthorityLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrolleeNote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    AdjudicatorId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: false),
                    NoteDate = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeNote_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeNote_Enrollee_EnrolleeId",
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
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    Expires = table.Column<DateTimeOffset>(nullable: false),
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
                name: "EnrolmentStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    StatusCode = table.Column<int>(nullable: false),
                    StatusDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatus_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
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
                name: "IdentificationDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    DocumentGuid = table.Column<Guid>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    UploadedDate = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentificationDocument_Enrollee_EnrolleeId",
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
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
                name: "SelfDeclaration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    SelfDeclarationTypeCode = table.Column<int>(nullable: false),
                    SelfDeclarationDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfDeclaration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelfDeclaration_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelfDeclaration_SelfDeclarationTypeLookup_SelfDeclarationTy~",
                        column: x => x.SelfDeclarationTypeCode,
                        principalTable: "SelfDeclarationTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelfDeclarationDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    DocumentGuid = table.Column<Guid>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    UploadedDate = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    SelfDeclarationTypeCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfDeclarationDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelfDeclarationDocument_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelfDeclarationDocument_SelfDeclarationTypeLookup_SelfDecla~",
                        column: x => x.SelfDeclarationTypeCode,
                        principalTable: "SelfDeclarationTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    ProfileSnapshot = table.Column<string>(type: "json", nullable: false),
                    AgreementType = table.Column<int>(nullable: true),
                    RequestedRemoteAccess = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    Confirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submission_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Agreement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: true),
                    PartyId = table.Column<int>(nullable: true),
                    AgreementVersionId = table.Column<int>(nullable: false),
                    LimitsConditionsClauseId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    AcceptedDate = table.Column<DateTimeOffset>(nullable: true),
                    ExpiryDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreement", x => x.Id);
                    table.CheckConstraint("CHK_Agreement_OnlyOneForeignKey", @"( CASE WHEN ""EnrolleeId"" IS NULL THEN 0 ELSE 1 END
                     + CASE WHEN ""OrganizationId"" IS NULL THEN 0 ELSE 1 END
                     + CASE WHEN ""PartyId"" IS NULL THEN 0 ELSE 1 END) = 1");
                    table.ForeignKey(
                        name: "FK_Agreement_AgreementVersion_AgreementVersionId",
                        column: x => x.AgreementVersionId,
                        principalTable: "AgreementVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agreement_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agreement_LimitsConditionsClause_LimitsConditionsClauseId",
                        column: x => x.LimitsConditionsClauseId,
                        principalTable: "LimitsConditionsClause",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agreement_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agreement_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Privilege",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PrivilegeGroupCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privilege", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Privilege_PrivilegeGroupLookup_PrivilegeGroupCode",
                        column: x => x.PrivilegeGroupCode,
                        principalTable: "PrivilegeGroupLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "EnrolmentStatusReason",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolmentStatusId = table.Column<int>(nullable: false),
                    StatusReasonCode = table.Column<int>(nullable: false),
                    ReasonNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentStatusReason", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatusReason_EnrolmentStatus_EnrolmentStatusId",
                        column: x => x.EnrolmentStatusId,
                        principalTable: "EnrolmentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatusReason_StatusReasonLookup_StatusReasonCode",
                        column: x => x.StatusReasonCode,
                        principalTable: "StatusReasonLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrolmentStatusReference",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolmentStatusId = table.Column<int>(nullable: false),
                    AdjudicatorNoteId = table.Column<int>(nullable: true),
                    AdminId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentStatusReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatusReference_EnrolleeNote_AdjudicatorNoteId",
                        column: x => x.AdjudicatorNoteId,
                        principalTable: "EnrolleeNote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatusReference_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrolmentStatusReference_EnrolmentStatus_EnrolmentStatusId",
                        column: x => x.EnrolmentStatusId,
                        principalTable: "EnrolmentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    JobRoleTitle = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    SMSPhone = table.Column<string>(nullable: true),
                    PhysicalAddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Address_PhysicalAddressId",
                        column: x => x.PhysicalAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnrolleeAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeAddress_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeAddress_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OboSite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    CareSettingCode = table.Column<int>(nullable: false),
                    HealthAuthorityCode = table.Column<int>(nullable: true),
                    SiteName = table.Column<string>(nullable: true),
                    PEC = table.Column<string>(nullable: true),
                    FacilityName = table.Column<string>(nullable: true),
                    JobTitle = table.Column<string>(nullable: false),
                    PhysicalAddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OboSite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OboSite_CareSettingLookup_CareSettingCode",
                        column: x => x.CareSettingCode,
                        principalTable: "CareSettingLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OboSite_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OboSite_HealthAuthorityLookup_HealthAuthorityCode",
                        column: x => x.HealthAuthorityCode,
                        principalTable: "HealthAuthorityLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OboSite_Address_PhysicalAddressId",
                        column: x => x.PhysicalAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartyAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    PartyId = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyAddress_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyAddress_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemoteAccessLocation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    InternetProvider = table.Column<string>(nullable: false),
                    PhysicalAddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteAccessLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemoteAccessLocation_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RemoteAccessLocation_Address_PhysicalAddressId",
                        column: x => x.PhysicalAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SignedAgreementDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    DocumentGuid = table.Column<Guid>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    UploadedDate = table.Column<DateTimeOffset>(nullable: false),
                    AgreementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignedAgreementDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignedAgreementDocument_Agreement_AgreementId",
                        column: x => x.AgreementId,
                        principalTable: "Agreement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedPrivilege",
                columns: table => new
                {
                    EnrolleeId = table.Column<int>(nullable: false),
                    PrivilegeId = table.Column<int>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedPrivilege", x => new { x.PrivilegeId, x.EnrolleeId });
                    table.ForeignKey(
                        name: "FK_AssignedPrivilege_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedPrivilege_Privilege_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "Privilege",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefaultPrivilege",
                columns: table => new
                {
                    LicenseCode = table.Column<int>(nullable: false),
                    PrivilegeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultPrivilege", x => new { x.PrivilegeId, x.LicenseCode });
                    table.ForeignKey(
                        name: "FK_DefaultPrivilege_LicenseLookup_LicenseCode",
                        column: x => x.LicenseCode,
                        principalTable: "LicenseLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DefaultPrivilege_Privilege_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "Privilege",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    PhysicalAddressId = table.Column<int>(nullable: true),
                    AdministratorPharmaNetId = table.Column<int>(nullable: true),
                    PrivacyOfficerId = table.Column<int>(nullable: true),
                    TechnicalSupportId = table.Column<int>(nullable: true),
                    ProvisionerId = table.Column<int>(nullable: true),
                    CareSettingCode = table.Column<int>(nullable: true),
                    PEC = table.Column<string>(nullable: true),
                    DoingBusinessAs = table.Column<string>(nullable: true),
                    Completed = table.Column<bool>(nullable: false),
                    SubmittedDate = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ApprovedDate = table.Column<DateTimeOffset>(nullable: true),
                    AdjudicatorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Site_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Site_Contact_AdministratorPharmaNetId",
                        column: x => x.AdministratorPharmaNetId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Site_CareSettingLookup_CareSettingCode",
                        column: x => x.CareSettingCode,
                        principalTable: "CareSettingLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Site_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Site_Address_PhysicalAddressId",
                        column: x => x.PhysicalAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Site_Contact_PrivacyOfficerId",
                        column: x => x.PrivacyOfficerId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Site_Party_ProvisionerId",
                        column: x => x.ProvisionerId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Site_Contact_TechnicalSupportId",
                        column: x => x.TechnicalSupportId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessDay_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessEvent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: true),
                    AdminId = table.Column<int>(nullable: true),
                    PartyId = table.Column<int>(nullable: true),
                    SiteId = table.Column<int>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: true),
                    BusinessEventTypeCode = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EventDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessEvent_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusinessEvent_BusinessEventTypeLookup_BusinessEventTypeCode",
                        column: x => x.BusinessEventTypeCode,
                        principalTable: "BusinessEventTypeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessEvent_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusinessEvent_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusinessEvent_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BusinessEvent_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessLicence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    DeferredLicenceReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessLicence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessLicence_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemoteAccessSite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    SiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteAccessSite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemoteAccessSite_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RemoteAccessSite_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemoteUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemoteUser_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteAdjudicationDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    DocumentGuid = table.Column<Guid>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    UploadedDate = table.Column<DateTimeOffset>(nullable: false),
                    AdjudicatorId = table.Column<int>(nullable: false),
                    SiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAdjudicationDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteAdjudicationDocument_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteAdjudicationDocument_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteRegistrationNote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    AdjudicatorId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: false),
                    NoteDate = table.Column<DateTimeOffset>(nullable: false),
                    SiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteRegistrationNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteRegistrationNote_Admin_AdjudicatorId",
                        column: x => x.AdjudicatorId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteRegistrationNote_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteRegistrationReviewDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    DocumentGuid = table.Column<Guid>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    UploadedDate = table.Column<DateTimeOffset>(nullable: false),
                    SiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteRegistrationReviewDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteRegistrationReviewDocument_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteVendor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    VendorCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteVendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteVendor_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteVendor_VendorLookup_VendorCode",
                        column: x => x.VendorCode,
                        principalTable: "VendorLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessLicenceDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    DocumentGuid = table.Column<Guid>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    UploadedDate = table.Column<DateTimeOffset>(nullable: false),
                    BusinessLicenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessLicenceDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessLicenceDocument_BusinessLicence_BusinessLicenceId",
                        column: x => x.BusinessLicenceId,
                        principalTable: "BusinessLicence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrolleeRemoteUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EnrolleeId = table.Column<int>(nullable: false),
                    RemoteUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolleeRemoteUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrolleeRemoteUser_Enrollee_EnrolleeId",
                        column: x => x.EnrolleeId,
                        principalTable: "Enrollee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolleeRemoteUser_RemoteUser_RemoteUserId",
                        column: x => x.RemoteUserId,
                        principalTable: "RemoteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemoteUserCertification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    RemoteUserId = table.Column<int>(nullable: false),
                    CollegeCode = table.Column<int>(nullable: false),
                    LicenseNumber = table.Column<string>(nullable: false),
                    LicenseCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteUserCertification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemoteUserCertification_CollegeLookup_CollegeCode",
                        column: x => x.CollegeCode,
                        principalTable: "CollegeLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RemoteUserCertification_LicenseLookup_LicenseCode",
                        column: x => x.LicenseCode,
                        principalTable: "LicenseLookup",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RemoteUserCertification_RemoteUser_RemoteUserId",
                        column: x => x.RemoteUserId,
                        principalTable: "RemoteUser",
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

            migrationBuilder.InsertData(
                table: "AgreementVersion",
                columns: new[] { "Id", "AgreementType", "CreatedTimeStamp", "CreatedUserId", "EffectiveDate", "Text", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<h1>PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER</h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <ol>
                  <li>

                    <p class=""bold underline"">
                      On Behalf of User Access
                    </p>

                    <p class=""bold"">
                      You represent and warrant to the Province that:
                    </p>

                    <ol type=""a"">
                      <li>
                        your employment duties in relation to a Practitioner require you to access PharmaNet (and PharmaNet Data) to
                        support the Practitioner’s delivery of Direct Patient Care;
                      </li>
                      <li>
                        you are directly supervised by a Practitioner who has been granted access to PharmaNet by the Province; and
                      </li>
                      <li>
                        all information provided by you in connection with your application for PharmaNet access, including all
                        information submitted through PRIME, is true and correct.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      Definitions
                    </p>

                    <p class=""bold"">
                      In these terms, capitalized terms will have the following meanings:
                    </p>

                    <ul class=""list-unstyled"">
                      <li>
                        <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                        services to an individual to whom a Practitioner provides direct patient care in the context of their Practice.
                      </li>
                      <li>
                        <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the
                        following website (or such other website as may be specified by the Province from time to time for this
                        purpose):

                        <br><br>

                        <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                      </li>
                      <li>
                        <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information Management
                        Regulation.
                      </li>
                      <li>
                        <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record or
                        information in the custody, control or possession of you or a Practitioner that was obtained through access to
                        PharmaNet by anyone.
                      </li>
                      <li>
                        <strong>“Practice”</strong> means a Practitioner’s practice of their health profession.
                      </li>
                      <li>
                        <strong>“Practitioner”</strong> means a health professional regulated under the Health Professions Act who
                        supervises your access and use of PharmaNet and who has been granted access to PharmaNet by the Province.
                      </li>
                      <li>
                        <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for, and
                        manage, their access to PharmaNet, and through which users are granted access by the Province.
                      </li>
                      <li>
                        <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                        Minister of Health.
                      </li>
                    </ul>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      Terms of Access to PharmaNet
                    </p>

                    <p class=""bold"">
                      You must:
                    </p>

                    <ol type=""a"">
                      <li>
                        access and use PharmaNet and PharmaNet Data only to support Direct Patient Care delivered by the Practitioner to
                        the individuals whose PharmaNet Data you are accessing;
                      </li>
                      <li>
                        only access PharmaNet as permitted by law and directed by the Practitioner;
                      </li>
                      <li>
                        maintain all PharmaNet Data, whether accessed on PharmaNet or otherwise disclosed to you in any manner, in
                        strict confidence;
                      </li>
                      <li>
                        maintain the security of PharmaNet, and any applications, connections, or networks used to access PharmaNet;
                      </li>
                      <li>
                        complete all training required by the Practice’s PharmaNet software vendor and the Province before accessing
                        PharmaNet;
                      </li>
                      <li>
                        notify the Province if you have any reason to suspect that PharmaNet, or any PharmaNet Data, is or has been
                        accessed or used inappropriately by any person.
                      </li>
                    </ol>

                    <p class=""bold"">
                      You must not:
                    </p>

                    <ol type=""a""
                        start=""7"">
                      <li>
                        disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and directed
                        by the Practitioner;
                      </li>
                      <li>
                        permit any person to use any user IDs, passwords or credentials provided to you to access PharmaNet;
                      </li>
                      <li>
                        reveal, share or compromise any user IDs, passwords or credentials for PharmaNet;
                      </li>
                      <li>
                        use, or attempt to use, the user IDs, passwords or credentials of any other person to access PharmaNet;
                      </li>
                      <li>
                        take any action that might compromise the integrity of PharmaNet, its information, or the provincial drug plan,
                        such as altering information or submitting false information;
                      </li>
                      <li>
                        test the security related to PharmaNet;
                      </li>
                      <li>
                        attempt to access PharmaNet from any location other than the approved Practice site of the Practitioner,
                        including by VPN or other remote access technology, unless that VPN or remote access technology has first been
                        approved by the Province in writing for use at the Practice.
                      </li>
                    </ol>

                    <p>
                      Your access to PharmaNet and use of PharmaNet Data are governed by the Pharmaceutical Services Act and you must
                      comply with all your duties under that Act.
                    </p>

                    <p>
                      The Province may, in writing and from time to time, set further limits and conditions in respect of PharmaNet,
                      either for you or for the Practitioner(s), and that you must comply with any such further limits and conditions.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      How to Notify the Province
                    </p>

                    <p>
                      Notice to the Province may be sent in writing to:
                    </p>

                    <address>
                      Director, Information and PharmaNet Development<br>
                      Ministry of Health<br>
                      PO Box 9652, STN PROV GOVT<br>
                      Victoria, BC V8W 9P4<br>

                      <br>

                      <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                    </address>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      Province may modify these terms
                    </p>

                    <p>
                      The Province may amend these terms, including this section, at any time in its sole discretion:
                    </p>

                    <ol type=""i"">
                      <li>
                        by written notice to you, in which case the amendment will become effective upon the later of (A) the date
                        notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified by the
                        Province, if any; or
                      </li>
                      <li>
                        by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will specify
                        the effective date of the amendment, which date will be at least thirty (30) days after the date that the
                        PharmaCare Newsletter containing the notice is first published.
                      </li>
                    </ol>

                    <p>
                      If you do not agree with any amendment for which notice has been provided by the Province in accordance with (i)
                      or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
                      PharmaNet.
                    </p>

                    <p>
                      Any written notice to you under (i) above will be in writing and delivered by the Province to you using any of the
                      contact mechanisms identified by you in PRIME, including by mail to a specified postal address, email to a
                      specified email address or text message to a specified cell phone number. You may be required to click a URL link
                      or log into PRIME to receive the contents of any such notice.
                    </p>

                  </li>
                  {$lcPlaceholder}
                  <li>

                    <p class=""bold underline"">
                      Governing Law
                    </p>

                    <p>
                      These terms will be governed by and will be construed and interpreted in accordance with the laws of British
                      Columbia and the laws of Canada applicable therein.
                    </p>

                    <p>
                      Unless otherwise specified, a reference to a statute or regulation by name means the statute or regulation of
                      British Columbia of that name, as amended or replaced from time to time, and includes any enactment made under the
                      authority of that statute or regulation.
                    </p>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 16, 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -8, 0, 0, 0)), @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the &quot;Agreement&quot;). Please read them carefully.
                </p>

                <ol>
                  <li>

                    <p class=""bold underline"">
                      BACKGROUND
                    </p>

                    <p>
                      The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links B.C.
                      pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered into
                      PharmaNet.
                    </p>

                    <p>
                      The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet is to
                      enhance patient care by providing timely and relevant information to persons involved in the provision of direct
                      patient care.
                    </p>

                    <p class=""bold underline"">
                      PharmaNet contains highly sensitive confidential information, including Personal Information and the proprietary
                      and confidential information of third-party licensors to the Province, and it is in the public interest to ensure
                      that appropriate measures are in place to protect the confidentiality of all such information. All access to and
                      use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INTERPRETATION
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will have the
                          meanings given below:
                        </p>

                        <ul>
                          <li>
                            <strong>&quot;Act&quot;</strong> means the <em>Pharmaceutical Services Act</em>.
                          </li>
                          <li>
                            <strong>&quot;Approved Practice Site&quot;</strong> means the physical site at which you provide Direct
                            Patient Care and which is approved by the Province for PharmaNet access. For greater certainty,
                            &quot;Approved Practice Site&quot; does not include a location from which remote access to PharmaNet takes
                            place;
                          </li>
                          <li>
                            <strong>&quot;Approved SSO&quot;</strong> means a software support organization approved by the Province
                            that provides you with the information technology software and/or services through which you and
                            On-Behalf-of Users access PharmaNet.
                          </li>
                          <li>

                            <p>
                              <strong>&quot;Conformance Standards&quot;</strong> means the following documents published by the
                              Province, as amended from time to time:
                            </p>

                            <ol type=""i"">
                              <li>
                                PharmaNet Professional and Software Conformance Standards; and
                              </li>
                              <li>
                                Office of the Chief Information Officer: &quot;Submission for Technical Security Standard and High Level
                                Architecture for Wireless Local Area Network Connectivity&quot;.
                              </li>
                            </ol>

                          </li>
                          <li>
                            <strong>&quot;Direct Patient Care&quot;</strong> means, for the purposes of this Agreement, the provision of
                            health services to an individual to whom you provide direct patient care in the context of your Practice.
                          </li>
                          <li>
                            <strong>&quot;Information Management Regulation&quot;</strong> means the Information Management Regulation,
                            B.C. Reg. 74/2015.
                          </li>
                          <li>
                            <strong>&quot;On-Behalf-of User&quot;</strong> means a member of your staff who (i) requires access to
                            PharmaNet to carry out duties in relation to your Practice; (ii) is authorized by you to access PharmaNet
                            on your behalf; and (iii) has been granted access to PharmaNet by the Province.
                          </li>
                          <li>
                            <strong>&quot;Personal Information&quot;</strong> means all recorded information that is about an
                            identifiable individual or is defined as, or deemed to be, &quot;personal information&quot; or
                            &quot;personal health information&quot; pursuant to any Privacy Laws.
                          </li>
                          <li>
                            <strong>&quot;PharmaCare Newsletter&quot;</strong> means the PharmaCare newsletter published by the Province
                            on the following website (or such other website as may be specified by the Province from time to time for
                            this purpose):

                            <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">
                              www.gov.bc.ca/pharmacarenewsletter
                            </a>
                          </li>
                          <li>
                            <strong>&quot;PharmaNet&quot;</strong> means PharmaNet as continued under section 2 of the Information
                            Management Regulation.
                          </li>
                          <li>
                            <strong>&quot;PharmaNet Data&quot;</strong> includes any record or information contained in PharmaNet and
                            any record or information in the custody, control or possession of you or an On-Behalf-of User that was
                            obtained through your or an On-Behalf-of User’s access to PharmaNet.
                          </li>
                          <li>
                            <strong>&quot;Practice&quot;</strong> means your practice of the health profession regulated under the
                            <em>Health Professions Act</em>, or your practice as an enrolled device provider under the Provider
                            Regulation, B.C. Reg. 222/2014, as identified by you through PRIME.
                          </li>
                          <li>
                            <strong>&quot;PRIME&quot;</strong> means the online service provided by the Province that allows users to
                            apply for, and manage, their access to PharmaNet, and through which users are granted access by the
                            Province.
                          </li>
                          <li>
                            <strong>&quot;Privacy Laws&quot;</strong> means the Act, the <em>Freedom of Information and Protection of
                            Privacy Act</em>, the <em>Personal Information Protection Act</em>, and any other statutory or legal
                            obligations of privacy owed by you or the Province, whether arising under statute, by contract or at common
                            law.
                          </li>
                          <li>
                            <strong>&quot;Province&quot;</strong> means Her Majesty the Queen in Right of British Columbia, as
                            represented by the Minister of Health.
                          </li>
                          <li>
                            <strong>&quot;Professional College&quot;</strong> is the regulatory body governing your Practice.
                          </li>
                          <li>

                            <p>
                              <strong>&quot;Unauthorized Person&quot;</strong> means any person other than:
                            </p>

                            <ol type=""i"">
                              <li>
                                you,
                              </li>
                              <li>
                                an On-Behalf-of User, or
                              </li>
                              <li>
                                a representative of an Approved SSO that is accessing PharmaNet for technical support purposes in
                                accordance with section 6 of the <em>Information Management Regulation</em>.
                              </li>
                            </ol>

                          </li>
                        </ul>

                      </li>
                      <li>
                        <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or regulation by
                        name means the statute or regulation of British Columbia of that name, as amended or replaced from time to time,
                        and includes any enactment made under the authority of that statute or regulation.
                      </li>
                      <li>

                        <p>
                          <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this Agreement:
                        </p>

                        <ol type=""i"">
                          <li>
                            a provision in the body of this Agreement will prevail over any conflicting provision in any further
                            limits or conditions communicated to you in writing by the Province, unless the conflicting provision
                            expressly states otherwise; and
                          </li>
                          <li>
                            a provision referred to in (i) above will prevail over any conflicting provision in the Conformance
                            Standards.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      APPLICATION OF LEGISLATION
                    </p>

                    <p>
                      You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act and all
                      Privacy Laws applicable to PharmaNet and PharmaNet Data.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
                    </p>

                    <p>
                      You acknowledge that:
                    </p>

                    <ol type=""a"">
                      <li>
                        PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
                        Province under the authority of the Act;
                      </li>
                      <li>
                        specific provisions of the Act, including the <em>Information Management Regulation</em> and sections 24, 25 and
                        29 of the Act, apply directly to you and to On-Behalf-of Users as a result; and
                      </li>
                      <li>
                        this Agreement documents limits and conditions, set by the minister in writing, that the Act requires you to
                        comply with.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCESS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
                        compliance with the limits and conditions set out in section 5(b) below and otherwise in this Agreement. The
                        Province may from time to time, at its discretion, amend or change the scope of your access privileges to
                        PharmaNet as privacy, security, business and clinical practice requirements change. In such circumstances, the
                        Province will use reasonable efforts to notify you of such changes.
                      </li>
                      <li>

                        <p>
                          <strong>Limits and Conditions of Access.</strong> The following limits and conditions apply to your access to
                          PharmaNet:
                        </p>

                        <ol type=""i"">
                          <li>
                            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, for so
                            long as you are a registrant in good standing with the Professional College and your registration permits
                            you to deliver Direct Patient Care requiring access to PharmaNet;
                          </li>
                          <li>
                            unless (iii) below applies, you will only access PharmaNet at the Approved Practice Site, and using only the
                            technologies and applications approved by the Province.
                          </li>
                          <li>
                            <p>
                              you may only access PharmaNet using remote access technology if all of the following conditions are met:
                            </p>

                            <ol>
                              <li>
                                the remote access technology used at the Approved Practice Site has been specifically approved in
                                writing by the Province,
                              </li>
                              <li>
                                the requirements of the Province’s Policy for Remote Access to PharmaNet
                                (<a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards"">https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards</a>) are met,
                              </li>
                              <li>
                                your Approved Practice Site has registered you with the Province for remote access at the Approved
                                Practice Site,
                              </li>
                              <li>
                                you have applied to the Province for remote access at the Approved Practice Site and the Province has
                                approved that application in writing, and
                              </li>
                              <li>
                                you are physically located in British Columbia at the time of any such remote access.
                              </li>
                            </ol>
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access takes
                            place at the Approved Practice Site and the access is in relation to patients for whom you will be providing
                            Direct Patient Care at the Approved Practice Site requiring the access to PharmaNet;
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of Users do not access PharmaNet using VPN or other remote access
                            technology
                          </li>
                          <li>
                            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
                            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
                          </li>
                          <li>
                            you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of market
                            research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet Data, for the
                            purpose of market research;
                          </li>
                          <li>
                            subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure that
                            On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of Direct Patient
                            Care, including for the purposes of deidentification or aggregation, quality improvement, evaluation, health
                            care planning, surveillance, research or other secondary uses;
                          </li>
                          <li>
                            you will not permit any Unauthorized Person to access PharmaNet, and you will take all reasonable measures
                            to ensure that no Unauthorized Person can access PharmaNet;
                          </li>
                          <li>
                            you will complete any training program(s) that your Approved SSO makes available to you in relation to
                            PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
                          </li>
                          <li>
                            you will immediately notify the Province when you or an On-Behalf-of User no longer require access to
                            PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave of absence
                            from your staff, or where the On-Behalf-of User’s access-related duties in relation to the Practice have
                            changed;
                          </li>
                          <li>
                            you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional limits or
                            conditions applicable to you, as may be communicated to you by the Province in writing;
                          </li>
                          <li>
                            you represent and warrant that all information provided by you in connection with your application for
                            PharmaNet access, including through PRIME, is true and correct.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this Agreement
                        for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
                        PharmaNet Data.
                      </li>
                      <li>

                        <p>
                          <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable measures to
                          safeguard Personal Information, including any Personal Information in the PharmaNet Data while it is in the
                          custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
                        </p>

                        <ol type=""i"">
                          <li>
                            take all reasonable steps to ensure the physical security of Personal Information, generally and as required
                            by Privacy Laws;
                          </li>
                          <li>
                            secure all workstations and printers in a protected area in the Practice to prevent viewing of PharmaNet
                            Data by Unauthorized Persons;
                          </li>
                          <li>
                            ensure separate access credential (such as user name and password) for each On-Behalf-of User, and prohibit
                            sharing or other multiple use of your access credential, or an On-Behalf-of User’s access credential, for
                            access to PharmaNet;
                          </li>
                          <li>
                            secure any workstations used to access PharmaNet and all devices, codes or passwords that enable access to
                            PharmaNet by yourself or any On-Behalf-of User;
                          </li>
                          <li>
                            take such other privacy and security measures as the Province may reasonably require from time-to-time.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Conformance Standards.</strong> You will comply with, and will ensure On-Behalf-of Users comply with,
                        the rules specified in the Conformance Standards when accessing and recording information in PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLOSURE, STORAGE, AND ACCESS REQUESTS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper files or
                        any electronic system, unless such storage or retention is required for record keeping in accordance with
                        Professional College requirements and in connection with your provision of Direct Patient Care and otherwise
                        is in compliance with the Conformance Standards. You will not modify any records retained in accordance with
                        this section other than as may be expressly authorized in the Conformance Standards. For clarity, you may
                        annotate a discrete record provided that the discrete record is not itself modified other than as expressly
                        authorized in the Conformance Standards.
                      </li>
                      <li>
                        <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with section
                        6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the purpose of
                        monitoring your own Practice.
                      </li>
                      <li>
                        <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do not,
                        disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient Care or is
                        otherwise authorized under section 24(1) of the Act.
                      </li>
                      <li>
                        <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of Users do
                        not, disclose PharmaNet Data for the purpose of market research.
                      </li>
                      <li>
                        <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in accordance
                        with section 6(a) of this Agreement, you will not provide to patients any copies of records containing PharmaNet
                        Data or &quot;print outs&quot; produced directly from PharmaNet, and will refer any requests for access to such
                        records or &quot;print outs&quot; to the Province.
                      </li>
                      <li>
                        <strong>Responding to Requests to Correct a Record contained in PharmaNet.</strong> If you receive a request for
                        correction of any record or information contained in PharmaNet, you will refer the request to the Province.
                      </li>
                      <li>
                        <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the Province if
                        you receive any order, demand or request compelling, or threatening to compel, disclosure of records contained
                        in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For greater
                        certainty, the foregoing requires that you notify the Province only with respect to any access requests or
                        demands for records contained in PharmaNet, and not records retained by you in accordance with section 6(a) of
                        this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCURACY
                    </p>

                    <p>
                      You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of User
                      in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material inaccuracy or
                      error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it if
                      necessary, and notify the Province of the inaccuracy or error and any steps taken.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INVESTIGATIONS, AUDITS, AND REPORTING
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations conducted by
                        the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
                        Agreement, including providing access upon request to your facilities, data management systems, books, records
                        and personnel for the purposes of such audit or investigation.
                      </li>
                      <li>
                        <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province may
                        report any material breach of this Agreement to your Professional College or to the Information and Privacy
                        Commissioner of British Columbia.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE OF NON COMPLIANCE AND DUTY TO INVESTIGATE
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this Agreement,
                        and will take all reasonable steps to prevent recurrences of any such breaches, including taking any steps
                        necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of User’s
                        access rights.
                      </li>
                      <li>

                        <p>
                          <strong>Non Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
                        </p>

                        <ol type=""i"">
                          <li>
                            you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User will be unable
                            to comply with the terms of this Agreement in any respect, or
                          </li>
                          <li>
                            you have knowledge of any circumstances, incidents or events which have or may jeopardize the security,
                            confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person, to access
                            PharmaNet.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      TERM OF AGREEMENT, SUSPENSION & TERMINATION
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet by the
                        Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or (e)
                        below.
                      </li>
                      <li>
                        <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written notice to
                        the Province.
                      </li>
                      <li>
                        <strong>Suspension or Termination of PharmaNet access.</strong> If the Province suspends or terminates your
                        right to access PharmaNet under the <em>Information Management Regulation</em>, this Agreement will
                        automatically terminate as of the date of such suspension or termination.
                      </li>
                      <li>
                        <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate this
                        Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to you if
                        you or an On-Behalf-of User fail to comply with any provision of this Agreement.
                      </li>
                      <li>
                        <strong>Termination by operation of the Information Management Regulation.</strong> This Agreement will
                        terminate automatically if your access to PharmaNet ends by operation of section 18 of the
                        <em>Information Management Regulation</em>.
                      </li>
                      <li>
                        <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may suspend your
                        account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the Province’s
                        policies. Please contact the Province immediately if your account has been suspended for inactivity but you
                        still require access to PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and PharmaNet
                        Data is solely at your own risk. All such access and information is provided on an &quot;as is&quot; and
                        &quot;as available&quot; basis without warranty or condition of any kind. The Province does not warrant the
                        accuracy, completeness or reliability of the PharmaNet Data or the availability of PharmaNet, or that access
                        to or the operation of PharmaNet will function without error, failure or interruption.
                      </li>
                      <li>
                        <strong>You are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
                        you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or acting
                        upon such information. The clinical or other information disclosed to you or an On-Behalf-of User pursuant to
                        this Agreement is in no way intended to be a substitute for professional judgment.
                      </li>
                      <li>
                        <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the Province
                        for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or PharmaNet
                        Data.
                      </li>
                      <li>
                        <strong>You Must Indemnify the Province if You Cause a Loss or Claim.</strong> You agree to indemnify and save
                        harmless the Province, and the Province’s employees and agents  (each an <strong>""Indemnified Person""</strong>)
                        from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified Person may
                        sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are based
                        upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
                        On-Behalf-of User, in connection with this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another method of
                          delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to be
                          effective, must be in writing and emailed or mailed to:
                        </p>

                        <address>
                          Director, Information and PharmaNet Development<br>
                          Ministry of Health<br>
                          PO Box 9652, STN PROV GOVT<br>
                          Victoria, BC V8W 9P4<br>

                          <br>

                          <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                        </address>

                      </li>
                      <li>
                        <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will be in
                        writing and delivered by the Province to you using any of the contact mechanisms identified by you in PRIME,
                        including by mail to a specified postal address, email to a specified email address or text message to the
                        specified cell phone number. You may be required to click a URL link or log into PRIME to receive the content
                        of any such notice.
                      </li>
                      <li>
                        <strong>Deemed receipt.</strong> Any written communication from a party, if personally delivered or sent
                        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent
                        by mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays)
                        after the date the notice was sent.
                      </li>
                      <li>
                        <strong>Substitute contact information.</strong> You may notify the Province of a substitute contact mechanism
                        by updating your contact information in PRIME.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      GENERAL
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and is
                          severable from any other covenant, and if any of them are held by a court, or other decision-maker, to be
                          invalid, this Agreement will be interpreted as if such provisions were not included.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Survival.</strong> Sections 3, 4, 5(b)(vii) (viii), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any other
                          provision of this Agreement that expressly or by its nature continues after termination, shall survive
                          termination of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and interpreted in
                          accordance with the laws of British Columbia and the laws of Canada applicable therein.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be assigned
                          without the prior written approval of the Province.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any provision of
                          this Agreement by you is not a waiver of its right subsequently to insist on performance of that or any other
                          provision of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Province may modify this Agreement.</strong> The Province may amend this Agreement, including this
                          section, at any time in its sole discretion:
                        </p>

                        <ol type=""i"">
                          <li>
                            by written notice to you, in which case the amendment will become effective upon the later of (A) the
                            date notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
                            by the Province, if any; or
                          </li>
                          <li>
                            by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                            specify the effective date of the amendment, which date will be at least 30 (thirty) days after the date
                            that the PharmaCare Newsletter containing the notice is first published.
                          </li>
                        </ol>

                        <p>
                          If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment described in
                          (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this Agreement will be
                          deemed to have been so amended as of the effective date. If you do not agree with any amendment for which
                          notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly (and in any
                          event before the effective date) cease all access or use of PharmaNet by yourself and all On-Behalf-of Users,
                          and take the steps necessary to terminate this Agreement in accordance with section 10.
                        </p>

                      </li>
                    </ol>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 15, 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -8, 0, 0, 0)), @"<h1>
                  PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER<br>
                </h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <p class=""bold underline"">
                  On Behalf-of-User Access
                </p>

                <ol>
                  <li>
                    <p>
                      You represent and warrant to the Province that:
                    </p>

                    <ol type=""a"">
                      <li>
                        your employment duties in relation to a Practitioner require you to access PharmaNet (and PharmaNet Data) to
                        support the Practitioner’s delivery of Direct Patient Care;
                      </li>
                      <li>
                        you are directly supervised by a Practitioner who has been granted access to PharmaNet by the Province; and
                      </li>
                      <li>
                        all information provided by you in connection with your application for PharmaNet access, including all
                        information submitted through PRIME, is true and correct.
                      </li>
                    </ol>

                  </li>
                </ol>

                <p class=""bold underline"">
                  Definitions
                </p>

                <ol start=""2"">
                  <li>
                    <p>
                      In these terms, capitalized terms will have the following meanings:
                    </p>

                    <ul class=""list-unstyled"">
                      <li>
                        <strong>“Approved Practice Site”</strong> means the physical site at which a Practitioner provides Direct
                        Patient Care and which is approved by the Province for PharmaNet access. For greater certainty, “Approved
                        Practice Site” does not include a location from which remote access to PharmaNet takes place.
                      </li>
                      <li>
                        <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                        services to an individual to whom a Practitioner provides direct patient care in the context of their Practice.
                      </li>
                      <li>
                        <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the
                        following website (or such other website as may be specified by the Province from time to time for this
                        purpose):

                        <br><br>

                        <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                      </li>
                      <li>
                        <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the <em>Information Management
                        Regulation</em>, B.C. Reg. 74/2015.
                      </li>
                      <li>
                        <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record or
                        information in the custody, control or possession of you or a Practitioner that was obtained through access to
                        PharmaNet by anyone.
                      </li>
                      <li>
                        <strong>“Practice”</strong> means a Practitioner’s practice of their health profession.
                      </li>
                      <li>
                        <strong>“Practitioner”</strong> means a health professional regulated under the <em>Health Professions Act</em>,
                        or an enrolled device provide under the <em>Provider Regulation</em> B.C. Reg. 222/2014, who supervises your
                        access to and use of PharmaNet and who has been granted access to PharmaNet by the Province.
                      </li>
                      <li>
                        <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for, and
                        manage, their access to PharmaNet, and through which users are granted access by the Province.
                      </li>
                      <li>
                        <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                        Minister of Health.
                      </li>
                    </ul>

                  </li>
                  <li>
                    Unless otherwise specified, a reference to a statute or regulation by name means the statute or regulation of
                    British Columbia of that name, as amended or replaced from time to time, and includes any enactment made under the
                    authority of that statute or regulation.
                  </li>
                </ol>

                <p class=""bold underline"">
                  Terms of Access to PharmaNet
                </p>

                <ol start=""4"">
                  <li>

                    <p>
                      You must:
                    </p>

                    <ol type=""a"">
                      <li>
                        access and use PharmaNet and PharmaNet Data only at the Approved Practice Site of a Practitioner;
                      </li>
                      <li>
                        access and use PharmaNet and PharmaNet Data only to support Direct Patient Care delivered by a Practitioner to
                        the individuals whose PharmaNet Data you are accessing, and only if the Practitioner is or will be delivering
                        Direct Patient Care requiring that access to those individuals at the same Approved Practice Site at which the
                        access occurs;
                      </li>
                      <li>
                        only access PharmaNet as permitted by law and directed by a Practitioner;
                      </li>
                      <li>
                        maintain all PharmaNet Data, whether accessed on PharmaNet or otherwise disclosed to you in any manner, in
                        strict confidence;
                      </li>
                      <li>
                        maintain the security of PharmaNet, and any applications, connections, or networks used to access PharmaNet;
                      </li>
                      <li>
                        complete all training required by the Approved Practice Site’s PharmaNet software vendor and the Province before
                        accessing PharmaNet;
                      </li>
                      <li>
                        notify the Province if you have any reason to suspect that PharmaNet, or any PharmaNet Data, is or has been
                        accessed or used inappropriately by any person.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p>
                      You must not:
                    </p>

                    <ol type=""a"">
                      <li>
                        disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and
                        directed by a Practitioner;
                      </li>
                      <li>
                        permit any person to use any user IDs, passwords or credentials provided to you to access PharmaNet;
                      </li>
                      <li>
                        reveal, share or compromise any user IDs, passwords or credentials for PharmaNet;
                      </li>
                      <li>
                        use, or attempt to use, the user IDs, passwords or credentials of any other person to access PharmaNet;
                      </li>
                      <li>
                        take any action that might compromise the integrity of PharmaNet, its information, or the provincial drug plan,
                        such as altering information or submitting false information;
                      </li>
                      <li>
                        test the security related to PharmaNet;
                      </li>
                      <li>
                        attempt to access PharmaNet from any location other than the Approved Practice Site of a Practitioner,
                        including by VPN or other remote access technology;
                      </li>
                      <li>
                        access PharmaNet unless the access is for the purpose of supporting a Practitioner in providing Direct
                        Patient Care to a patient at the same Approved Practice Site at which your access occurs.
                      </li>
                    </ol>
                  </li>
                </ol>
                <ol start=""6"">
                  <li>
                    Your access to PharmaNet and use of PharmaNet Data are governed by the <em>Pharmaceutical Services Act</em> and you
                    must comply with all your duties under that Act.
                  </li>
                  <li>
                    The Province may, in writing and from time to time, set further limits and conditions in respect of PharmaNet,
                    either for you or for the Practitioner(s), and that you must comply with any such further limits and conditions.
                  </li>
                </ol>

                <p class=""bold underline"">
                  How to Notify the Province
                </p>

                <ol start=""8"">
                  <li>

                    <p>
                      Notice to the Province may be sent in writing to:
                    </p>

                    <address>
                      Director, Information and PharmaNet Development<br>
                      Ministry of Health<br>
                      PO Box 9652, STN PROV GOVT<br>
                      Victoria, BC V8W 9P4<br>

                      <br>

                      <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                    </address>

                  </li>
                </ol>

                <p class=""bold underline"">
                  Province May Modify These Terms
                </p>

                <ol start=""9"">
                  <li>
                    <p>
                      The Province may amend these terms, including this section, at any time in its sole discretion:
                    </p>

                    <ol type=""i"">
                      <li>
                        by written notice to you, in which case the amendment will become effective upon the later of (A) the date
                        notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified by the
                        Province, if any; or
                      </li>
                      <li>
                        by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will specify
                        the effective date of the amendment, which date will be at least thirty (30) days after the date that the
                        PharmaCare Newsletter containing the notice is first published.
                      </li>
                    </ol>

                    <p>
                      If you do not agree with any amendment for which notice has been provided by the Province in accordance with (i)
                      or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
                      PharmaNet.
                    </p>

                    <p>
                      Any written notice to you under (i) above will be in writing and delivered by the Province to you using any of the
                      contact mechanisms identified by you in PRIME, including by mail to a specified postal address, email to a
                      specified email address or text message to a specified cell phone number. You may be required to click a URL link
                      or log into PRIME to receive the contents of any such notice.
                    </p>

                  </li>
                </ol>

                <p class=""bold underline"">
                  Governing Law
                </p>

                <ol start=""10"">
                  <li>

                    <p>
                      These terms will be governed by and will be construed and interpreted in accordance with the laws of British
                      Columbia and the laws of Canada applicable therein.
                    </p>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 14, 6, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<h1>
                  PHARMANET TERMS OF ACCESS FOR PHARMACY OR DEVICE PROVIDER ON-BEHALF-OF USER
                </h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <p class=""bold underline"">
                  On-Behalf-of User Access
                </p>

                <ol>
                  <li>
                    <p>
                      You represent and warrant to the Province that:
                    </p>

                    <ol type=""a"">
                      <li>
                        your employment duties in relation to a Practitioner require you to access PharmaNet (and PharmaNet Data) to
                        support the Practitioner’s delivery of Direct Patient Care;
                      </li>
                      <li>
                        you are directly supervised by a Practitioner, who has been granted access to PharmaNet by the Province; and
                      </li>
                      <li>
                        all information provided by you in connection with your application for PharmaNet access, including all
                        information submitted through PRIME, is true and correct.
                      </li>
                    </ol>

                  </li>
                </ol>

                <p class=""bold underline"">
                  Definitions
                </p>

                <ol start=""2"">
                  <li>

                    <p>
                      In these terms, capitalized terms will have the following meanings:
                    </p>

                    <ul class=""list-unstyled"">
                      <li>
                        <strong>“Approved Practice Site”</strong> means the physical site at which a Practitioner provides Direct
                        Patient Care and which is approved by the Province for PharmaNet access. For greater certainty, “Approved
                        Practice Site” does not include a location from which remote access to PharmaNet takes place.
                      </li>
                      <li>
                        <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                        services to an individual to whom a Practitioner provides direct patient care in the context of their Practice.
                      </li>
                      <li>
                        <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the
                        following website (or such other website as may be specified by the Province from time to time for this
                        purpose):

                        <br><br>

                        <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">http://www.gov.bc.ca/pharmacarenewsletter</a>
                      </li>
                      <li>
                        <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the <em>Information Management
                        Regulation, B.C</em>. Reg. 74/2015.
                      </li>
                      <li>
                        <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record or
                        information in the custody, control or possession of you or a Practitioner that was obtained through access to
                        PharmaNet by anyone.
                      </li>
                      <li>
                        <strong>“Practice”</strong> means a Practitioner’s practice of their health profession.
                      </li>
                      <li>
                        <strong>“Practitioner”</strong> means a health professional regulated under the <em>Health Professions Act</em>,
                        or an
                        enrolled device provider under the <em>Provider Regulation</em>, B.C. Reg. 222/2014,who supervises your access
                        to and use of PharmaNet and who has been granted access to PharmaNet by the Province.
                      </li>
                      <li>
                        <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for, and
                        manage, their access to PharmaNet, and through which users are granted access by the Province.
                      </li>
                      <li>
                        <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                        Minister of Health.
                      </li>
                    </ul>

                  </li>
                  <li>
                    Unless otherwise specified, a reference to a statute or regulation by name means the statute or regulation of
                    British Columbia of that name, as amended or replaced from time to time, and includes any enactment made under the
                    authority of that statute or regulation.
                  </li>
                </ol>

                <p class=""bold underline"">
                  Terms of Access to PharmaNet
                </p>

                <ol start=""4"">
                  <li>

                    <p>
                      You must:
                    </p>

                    <ol type=""a"">
                      <li>
                        access and use PharmaNet and PharmaNet Data only at the Approved Practice Site of a Practitioner;
                      </li>
                      <li>
                        access and use PharmaNet and PharmaNet Data only to support Direct Patient Care delivered by a Practitioner to
                        the individuals whose PharmaNet Data you are accessing, and only if the Practitioner is or will be delivering
                        Direct Patient Care requiring that access to those individuals at the same Approved Practice Site at which the
                        access occurs;
                      </li>
                      <li>
                        only access PharmaNet as permitted by law and directed by a Practitioner;
                      </li>
                      <li>
                        maintain all PharmaNet Data, whether accessed on PharmaNet or otherwise disclosed to you in any manner, in
                        strict confidence;
                      </li>
                      <li>
                        maintain the security of PharmaNet, and any applications, connections, or networks used to access PharmaNet;
                      </li>
                      <li>
                        complete all training required by the Approved Practice Site’s PharmaNet software vendor and the Province before
                        accessing PharmaNet;
                      </li>
                      <li>
                        notify the Province if you have any reason to suspect that PharmaNet, or any PharmaNet Data, is or has been
                        accessed or used inappropriately by any person.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p>
                      You must not:
                    </p>

                    <ol type=""a"">
                      <li>
                        disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and directed
                        by a Practitioner;
                      </li>
                      <li>
                        permit any person to use any user IDs, passwords or credentials provided to you to access PharmaNet;
                      </li>
                      <li>
                        reveal, share or compromise any user IDs, passwords or credentials for PharmaNet;
                      </li>
                      <li>
                        use, or attempt to use, the user IDs, passwords or credentials of any other person to access PharmaNet;
                      </li>
                      <li>
                        take any action that might compromise the integrity of PharmaNet, its information, or the provincial drug plan,
                        such as altering information or submitting false information;
                      </li>
                      <li>
                        test the security related to PharmaNet;
                      </li>
                      <li>
                        attempt to access PharmaNet from any location other than the Approved Practice Site of a Practitioner, including
                        by VPN or other remote access technology;
                      </li>
                      <li>
                        access PharmaNet unless the access is for the purpose of supporting a Practitioner in providing Direct Patient
                        Care to a patient at the same Approved Practice Site at which your access occurs;
                      </li>
                      <li>
                        use PharmaNet to submit claims to PharmaCare or a third-party insurer unless directed to do so by a Practitioner
                        at an Approved Practice Site that is enrolled as a provider or device provider under the
                        <em>Provider Regulation</em>, B.C. Reg. 222/2014.
                      </li>
                    </ol>
                  </li>
                </ol>
                <ol start=""6"">
                  <li>
                    Your access to PharmaNet and use of PharmaNet Data are governed by the Pharmaceutical Services Act and you must
                    comply with all your duties under that Act.
                  </li>
                  <li>
                    The Province may, in writing and from time to time, set further limits and conditions in respect of PharmaNet,
                    either for you or for the Practitioner(s), and that you must comply with any such further limits and conditions.
                  </li>
                </ol>

                <p class=""bold underline"">
                  How to Notify the Province
                </p>

                <ol start=""8"">
                  <li>

                    <p>
                      Notice to the Province may be sent in writing to:
                    </p>

                    <address>
                      Director, Information and PharmaNet Innovation<br>
                      Ministry of Health<br>
                      PO Box 9652, STN PROV GOVT<br>
                      Victoria, BC V8W 9P4<br>

                      <br>

                      <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                    </address>
                  </li>
                </ol>

                <p class=""bold underline"">
                  Province May Modify These Terms
                </p>

                <ol start=""9"">
                  <li>

                    <p>
                      The Province may amend these terms, including this section, at any time in its sole discretion:
                    </p>

                    <ol type=""i"">
                      <li>
                        by written notice to you, in which case the amendment will become effective upon the later of (A) the date
                        notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified by the
                        Province, if any; or
                      </li>
                      <li>
                        by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will specify
                        the effective date of the amendment, which date will be at least thirty (30) days after the date that the
                        PharmaCare Newsletter containing the notice is first published.
                      </li>
                    </ol>

                    <p>
                      If you do not agree with any amendment for which notice has been provided by the Province in accordance with (i)
                      or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
                      PharmaNet.
                    </p>

                    <p>
                      If you do not agree with any amendment for which notice has been provided by the Province in accordance with (i)
                      or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
                      PharmaNet.
                    </p>
                  </li>
                </ol>

                <p class=""bold underline"">
                  Governing Law
                </p>

                <ol start=""10"">
                  <li>

                    <p>
                      These terms will be governed by and will be construed and interpreted in accordance with the laws of British
                      Columbia and the laws of Canada applicable therein.
                    </p>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, 1, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <h1>PHARMANET USER TERMS OF ACCESS FOR PHARMACISTS</h1>

                <ol>
                  <li>

                    <p class=""bold underline"">
                      BACKGROUND
                    </p>

                    <p>
                      The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links B.C.
                      pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered into
                      PharmaNet.
                    </p>

                    <p>
                      The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet is to
                      enhance patient care by providing timely and relevant information to persons involved in the provision of direct
                      patient care.
                    </p>

                    <p class=""bold"">
                      PharmaNet contains highly sensitive confidential information, including Personal Information and the proprietary
                      and confidential information of third-party licensors to the Province, and it is in the public interest to ensure
                      that appropriate measures are in place to protect the confidentiality of all such information. All access to and
                      use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INTERPRETATION
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will have the
                          meanings given below:
                        </p>

                        <ul class=""list-unstyled"">
                          <li>
                            <strong>“Act”</strong> means the <em>Pharmaceutical Services Act</em>.
                          </li>
                          <li>
                            <strong>“Approved Practice Site”</strong> means the physical site at which you provide Direct Patient Care
                            and which is approved by the Province for PharmaNet access.
                          </li>
                          <li>
                            <strong>“Approved SSO”</strong> means a software support organization approved by the Province that provides
                            you with the information technology software and/or services through which you and On-Behalf-of Users access
                            PharmaNet.
                          </li>
                          <li>
                            <strong>“Claim”</strong> means a claim made under the Act for payment in respect of a benefit under the Act.
                          </li>
                          <li>

                            <p>
                              <strong>“Conformance Standards”</strong> means the following documents published by the Province, as
                              amended from time to time:
                            </p>

                            <ol type=""i"">
                              <li>
                                PharmaNet Professional and Software Conformance Standards<br>
                                <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards"" target=""_blank"" rel=""noopener noreferrer"">
                                  https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards
                                </a>; and
                              </li>
                              <li>
                                Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                                Architecture for Wireless Local Area Network Connectivity”.
                                <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet"" target=""_blank"" rel=""noopener noreferrer"">
                                  https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet
                                </a>
                              </li>
                            </ol>

                          </li>
                          <li>
                            <strong>“Device Provider”</strong> means a person enrolled under section 11 of the Act in the class of
                            provider known as “device provider”.
                          </li>
                          <li>
                            <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                            services to an individual to whom you provide direct patient care in the context of your Practice.
                          </li>
                          <li>
                            <strong>“Information Management Regulation”</strong> means the <em>Information Management Regulation</em>,
                            B.C. Reg.
                            74/2015.
                          </li>
                          <li>
                            <strong>“On-Behalf-of User”</strong> means a member of your staff who (i) requires access to PharmaNet to
                            carry out duties in relation to your Practice; (ii) is authorized by you to access PharmaNet on your behalf;
                            and (iii) has been granted access to PharmaNet by the Province.
                          </li>
                          <li>
                            <strong>“Personal Information”</strong> means all recorded information that is about an identifiable
                            individual or is defined as, or deemed to be, “personal information” or “personal health information”
                            pursuant to any Privacy Laws.
                          </li>
                          <li>
                            <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the
                            following website (or such other website as may be specified by the Province from time to time for this
                            purpose):

                            <br><br>

                            <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                          </li>
                          <li>
                            <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information Management
                            Regulation.
                          </li>
                          <li>
                            <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record
                            or information in the custody, control or possession of you or an On-Behalf-of User that was obtained
                            through your or an On-Behalf-of User’s access to PharmaNet.
                          </li>
                          <li>
                            <strong>“Practice”</strong> means your practice of the health profession regulated under the <em>Health
                                      Professions Act</em>, or your practice as a Device Provider, as identified by you through PRIME
                            or another mechanism provided by the Province.
                          </li>
                          <li>
                            <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for,
                            and manage, their access to PharmaNet, and through which users are granted access by the Province.
                          </li>
                          <li>
                            <strong>“Privacy Laws”</strong> means the Act, the
                            <em>Freedom of Information and Protection of Privacy Act</em>, the Personal Information Protection Act, and
                            any other statutory or legal obligations of privacy owed by you or the Province, whether arising under
                            statute, by contract or at common law.
                          </li>
                          <li>
                            <strong>“Provider”</strong> means a person enrolled under section 11 of the Act for the purpose of receiving
                            payment for providing benefits.
                          </li>
                          <li>
                            <strong>“Provider Regulation”</strong> means the <em>Provider Regulation</em>, B.C. Reg. 222/2014.
                          </li>
                          <li>
                            <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                            Minister of Health.
                          </li>
                          <li>
                            <strong>“Professional College”</strong> is the regulatory body governing your Practice.
                          </li>
                          <li>
                            <p>
                              <strong>“Unauthorized Person”</strong> means any person other than:
                            </p>

                            <ol type=""i"">
                              <li>
                                you,
                              </li>
                              <li>
                                an On-Behalf-of User, or
                              </li>
                              <li>
                                a representative of an Approved SSO that is accessing PharmaNet for technical support purposes in
                                accordance with section 6 of the <em>Information Management Regulation</em>.
                              </li>
                            </ol>

                          </li>
                        </ul>

                      </li>
                      <li>
                        <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or regulation by
                        name means the statute or regulation of British Columbia of that name, as amended or replaced from time to time,
                        and includes any enactment made under the authority of that statute or regulation.
                      </li>
                      <li>

                        <p>
                          <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this Agreement:
                        </p>

                        <ol type=""i"">
                          <li>
                            a provision in the body of this Agreement will prevail over any conflicting provision in any further limits
                            or conditions communicated to you in writing by the Province, unless the conflicting provision expressly
                            states otherwise; and
                          </li>
                          <li>
                            a provision referred to in (i) above will prevail over any conflicting provision in the Conformance
                            Standards.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      APPLICATION OF LEGISLATION
                    </p>

                    <p>
                      You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act, the
                      Information Management Regulation and all Privacy Laws applicable to PharmaNet and PharmaNet Data.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
                    </p>

                    <p>
                      You acknowledge that:
                    </p>

                    <ol type=""a"">
                      <li>
                        PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
                        Province under the authority of the Act;
                      </li>
                      <li>
                        specific provisions of the Act (including but not limited to sections 24, 25 and 29) and the Information
                        Management Regulation apply directly to you and to On-Behalf-of Users as a result; and
                      </li>
                      <li>
                        this Agreement documents limits and conditions, set by the minister in writing, that the Act requires you to
                        comply with.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCESS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
                        compliance with the limits and conditions set out in this Agreement. The Province may from time to time, at its
                        discretion, amend or change the scope of your access privileges to PharmaNet as privacy, security, business and
                        clinical practice requirements change. In such circumstances, the Province will use reasonable efforts to notify
                        you of such changes.
                      </li>
                      <li>

                        <p>
                          <strong>Requirements for Access.</strong> The following requirements apply to your access to PharmaNet:
                        </p>

                        <ol type=""i"">
                          <li>
                            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, for so
                            long as you are a registrant in good standing with the Professional College and your registration permits
                            you to deliver Direct Patient Care requiring access to PharmaNet or, in the case of access as a Device
                            Provider, for so long as you are enrolled as a Device Provider;
                          </li>
                          <li>
                            you will only access PharmaNet: at the Approved Practice Site, and using only the technologies and
                            applications approved by the Province;
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access takes
                            place at the Approved Practice Site and the access is required in relation to patients for whom you will be
                            providing Direct Patient Care at the Approved Practice Site;
                          </li>
                          <li>
                            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
                            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
                          </li>
                          <li>
                            you will not submit Claims on PharmaNet other than from an Approved Practice Site in respect of which a
                            person is enrolled as a Provider, and you will ensure that On-Behalf-of Users submit Claims on PharmaNet
                            only from an Approved Practice Site in respect of which a person is enrolled as a Provider;
                          </li>
                          <li>
                            you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of market
                            research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet Data, for the
                            purpose of market research;
                          </li>
                          <li>
                            subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure that
                            On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of Direct Patient
                            Care, including for the purposes of quality improvement, evaluation, health care planning, surveillance,
                            research or other secondary uses;
                          </li>
                          <li>
                            you will not permit any Unauthorized Person to access PharmaNet, and you will take all reasonable measures
                            to ensure that no Unauthorized Person can access PharmaNet;
                          </li>
                          <li>
                            you will complete any training program(s) that your Approved SSO makes available to you in relation to
                            PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
                          </li>
                          <li>
                            you will immediately notify the Province when you or an On-Behalf-of User no longer require access to
                            PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave of absence
                            from your staff, or where the On-Behalf-of User’s access-related duties in relation to the Practice have
                            changed;
                          </li>
                          <li>
                            you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional limits or
                            conditions applicable to you, as may be communicated to you by the Province in writing;
                          </li>
                          <li>
                            you represent and warrant that all information provided by you in connection with your application for
                            PharmaNet access, including through PRIME, is true and correct.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this Agreement
                        for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
                        PharmaNet Data.
                      </li>
                      <li>

                        <p>
                          <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable measures to
                          safeguard Personal Information, including any Personal Information in PharmaNet Data while it is in the
                          custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
                        </p>

                        <ol type=""i"">
                          <li>
                            take all reasonable steps to ensure the physical security of Personal Information, generally and as
                            required by Privacy Laws;
                          </li>
                          <li>
                            secure all workstations and printers in a protected area in the Approved Practice Site to prevent
                            viewing of PharmaNet Data by Unauthorized Persons;
                          </li>
                          <li>
                            ensure separate access credential (such as user name and password) for each On-Behalf-of User, and
                            prohibit sharing or other multiple use of your access credential, or an On-Behalf-of User’s access
                            credential, for access to PharmaNet;
                          </li>
                          <li>
                            secure any workstations used to access PharmaNet and all devices, codes or passwords that enable access
                            to PharmaNet by yourself or any On-Behalf-of User;
                          </li>
                          <li>
                            take such other privacy and security measures as the Province may reasonably require from time-to-time.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Conformance Standards.</strong> You will comply with, and will ensure On-Behalf-of Users comply with,
                        the rules specified in the Conformance Standards when accessing and recording information in PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLOSURE, STORAGE, AND ACCESS REQUESTS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper files or
                        any electronic system, unless such storage or retention is required for record keeping in accordance with the
                        Act, the Provider Regulation, and Professional College requirements and in connection with your provision of
                        Direct Patient Care and otherwise is in compliance with the Conformance Standards. You will not modify any
                        records retained in accordance with this section other than as may be expressly authorized in the Conformance
                        Standards. For clarity, you may annotate a discrete record provided that the discrete record is not itself
                        modified other than as expressly authorized in the Conformance Standards.
                      </li>
                      <li>
                        <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with section
                        6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the purpose of
                        monitoring your own Practice.
                      </li>
                      <li>
                        <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do not,
                        disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient Care or is
                        otherwise authorized under section 24(1) of the Act.
                      </li>
                      <li>
                        <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of Users do
                        not, disclose PharmaNet Data for the purpose of market research.
                      </li>
                      <li>
                        <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in accordance
                        with section 6(a) of this Agreement, you will not provide to patients any copies of records containing PharmaNet
                        Data or “print outs” produced directly from PharmaNet, and will refer any requests for access to such records or
                        “print outs” to the Province.
                      </li>
                      <li>
                        <strong>Responding to Requests to Correct a Record Contained in PharmaNet.</strong> If you receive a request for
                        correction of any record or information contained in PharmaNet, you will refer the request to the Province.
                      </li>
                      <li>
                        <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the Province if
                        you receive any order, demand or request compelling, or threatening to compel, disclosure of records contained
                        in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For greater
                        certainty, the foregoing requires that you notify the Province only with respect to any access requests or
                        demands for records contained in PharmaNet, and not records retained by you in accordance with section 6(a) of
                        this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCURACY
                    </p>

                    <p>
                      You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of User
                      in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material inaccuracy or
                      error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it if
                      necessary, and notify the Province of the inaccuracy or error and any steps taken.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INVESTIGATIONS, AUDITS, AND REPORTING
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations conducted by
                        the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
                        Agreement, including providing access upon request to your facilities, data management systems, books, records
                        and personnel for the purposes of such audit or investigation.
                      </li>
                      <li>
                        <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province may
                        report any material breach of this Agreement to your Professional College or to the Information and Privacy
                        Commissioner of British Columbia.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE OF NON-COMPLIANCE AND DUTY TO INVESTIGATE
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this Agreement,
                        and will take all reasonable steps to prevent recurrences of any such breaches, including taking any steps
                        necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of User’s
                        access rights.
                      </li>
                      <li>

                        <p>
                          <strong>Non-Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
                        </p>

                        <ol type=""i"">
                          <li>
                            you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User will be unable
                            to comply with the terms of this Agreement in any respect, or
                          </li>
                          <li>
                            you have knowledge of any circumstances, incidents or events which have or may jeopardize the security,
                            confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person, to access
                            PharmaNet.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      TERM OF AGREEMENT, SUSPENSION & TERMINATION
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet by the
                        Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or (e)
                        below.
                      </li>
                      <li>
                        <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written notice to
                        the Province.
                      </li>
                      <li>
                        <strong>Suspension or Termination of PharmaNet Access.</strong> If the Province suspends or terminates your
                        right, or an On-Behalf-of User’s right, to access PharmaNet under the
                        <em>Information Management Regulation</em>, the Province may also terminate this Agreement at any time
                        thereafter upon written notice to you.
                      </li>
                      <li>
                        <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate this
                        Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to you if
                        you or an On-Behalf-of User fail to comply with any provision of this Agreement.
                      </li>
                      <li>
                        <strong>Termination by Operation of the Information Management Regulation.</strong> This Agreement will
                        terminate automatically if your access to PharmaNet ends by operation of section 18 of the
                        <em>Information Management Regulation</em>.
                      </li>
                      <li>
                        <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may suspend your
                        account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the Province’s
                        policies. Please contact the Province immediately if your account has been suspended for inactivity but you
                        still require access to PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and PharmaNet
                        Data is solely at your own risk. All such access and information is provided on an “as is” and “as available”
                        basis without warranty or condition of any kind. The Province does not warrant the accuracy, completeness or
                        reliability of the PharmaNet Data or the availability of PharmaNet, or that access to or the operation of
                        PharmaNet will function without error, failure or interruption.
                      </li>
                      <li>
                        <strong>You Are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
                        you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or acting
                        upon such information. The clinical or other information disclosed to you or an On-Behalf-of User pursuant to
                        this Agreement is in no way intended to be a substitute for professional judgment.
                      </li>
                      <li>
                        <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the Province
                        for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or PharmaNet
                        Data.
                      </li>
                      <li>
                        <strong>You Must Indemnify the Province If You Cause a Loss or Claim.</strong> You agree to indemnify and save
                        harmless the Province, and the Province’s employees and agents (each an <strong>""Indemnified Person""</strong>)
                        from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified Person may
                        sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are based
                        upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
                        On-Behalf-of User, in connection with this Agreement or in connection with access to PharmaNet by you or an
                        On-Behalf-of User.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another method of
                          delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to be
                          effective, must be in writing and emailed or mailed to:
                        </p>

                        <address>
                          Director, Information and PharmaNet Innovation<br>
                          Ministry of Health<br>
                          PO Box 9652, STN PROV GOVT<br>
                          Victoria, BC V8W 9P4<br>

                          <br>

                          <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                        </address>

                      </li>
                      <li>
                        <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will be in
                        writing and delivered by the Province to you using any of the contact mechanisms identified by you in PRIME,
                        including by mail to a specified postal address, email to a specified email address or text message to the
                        specified cell phone number. You may be required to click a URL link or log into PRIME to receive the content of
                        any such notice.
                      </li>
                      <li>
                        <strong>Deemed Receipt.</strong> Any written communication from a party, if personally delivered or sent
                        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent by
                        mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays) after
                        the date the notice was sent.
                      </li>
                      <li>
                        <strong>Substitute Contact Information.</strong> You may notify the Province of a substitute contact mechanism
                        by updating your contact information in PRIME.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      GENERAL
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and is
                          severable from any other covenant, and if any of them are held by a court, or other decision-maker, to be
                          invalid, this Agreement will be interpreted as if such provisions were not included.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Survival.</strong> Sections 3, 4, 5(b)(vi) (vii), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any other
                          provision of this Agreement that expressly or by its nature continues after termination, shall survive
                          termination of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and interpreted in
                          accordance with the laws of British Columbia and the laws of Canada applicable therein.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be assigned
                          without the prior written approval of the Province.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any provision of
                          this Agreement by you is not a waiver of its right subsequently to insist on performance of that or any other
                          provision of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Province May Modify this Agreement.</strong> The Province may amend this Agreement, including this
                          section, at any time in its sole discretion:
                        </p>

                        <ol type=""i"">
                          <li>
                            by written notice to you, in which case the amendment will become effective upon the later of (A) the
                            date notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
                            by the Province, if any; or
                          </li>
                          <li>
                            by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                            specify the effective date of the amendment, which date will be at least 30 (thirty) days after the date
                            that the PharmaCare Newsletter containing the notice is first published.
                          </li>
                        </ol>

                        <p>
                          If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment described in
                          (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this Agreement will be
                          deemed to have been so amended as of the effective date. If you do not agree with any amendment for which
                          notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly (and in any
                          event before the effective date) cease all access or use of PharmaNet by yourself and all On-Behalf-of Users,
                          and take the steps necessary to terminate this Agreement in accordance with section 10.
                        </p>

                      </li>
                    </ol>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, 5, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<p class=""text-center"">
                  This Agreement is made the {{day}} day of {{month}}, {{year}}
                </p>

                <h1>---- PLACEHOLDER TEXT ----</h1>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, 4, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<p class=""text-center"">
                  This Agreement is made the {{day}} day of {{month}}, {{year}}
                </p>

                <h1>ORGANIZATION AGREEMENT FOR PHARMANET USE</h1>

                <p>
                  This Organization Agreement for PharmaNet Use (the &quot;Agreement&quot;) is executed by {{organizationName}}
                  (&quot;Organization&quot;) for the benefit of HER MAJESTY THE QUEEN IN RIGHT OF THE PROVINCE OF BRITISH COLUMBIA, as
                  represented by the Minister of Health (the &quot;Province&quot;).
                </p>

                <p>
                  <strong>WHEREAS:</strong>
                </p>

                <ol type=""A"">
                  <li>
                    The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links
                    pharmacies to a central data system. Every prescription dispensed in community pharmacies in British
                    Columbia is entered into PharmaNet.
                  </li>
                  <li>
                    PharmaNet contains highly sensitive confidential information, including personal information, and it is in
                    the public interest to ensure that appropriate measures are in place to protect the confidentiality and
                    integrity of such information. All access to and use of PharmaNet and PharmaNet Data is subject to the
                    Act and other applicable law.
                  </li>
                  <li>
                    The Province permits Authorized Users to access PharmaNet to provide health services to, or to facilitate
                    the care of, the individual whose personal information is being accessed.
                  </li>
                  <li>
                    This Agreement sets out the terms by which Organization may permit Authorized Users to access PharmaNet
                    at the Site(s) operated by Organization.
                  </li>
                </ol>

                <p>
                  <strong>NOW THEREFORE</strong> Organization makes this Agreement knowing that the Province will rely on it
                  in permitting access to and use of PharmaNet from Sites operated by Organization. Organization conclusively
                  acknowledges that reliance by the Province on this Agreement is in every respect justifiable and that it
                  received fair and valuable consideration for this Agreement, the receipt and adequacy of which is hereby
                  acknowledged. Organization hereby agrees as follows:
                </p>

                <p class=""text-center"">
                  <strong>ARTICLE 1 – INTERPRETATION</strong>
                </p>

                <ol type=""1""
                    start=""1""
                    class=""decimal"">
                  <li>
                    <ol type=""1"">
                      <li>
                        <p>
                          In this Agreement, unless the context otherwise requires, the following definitions will apply:
                        </p>

                        <ol type=""a"">
                          <li>
                            <strong>&quot;Act&quot;</strong> means the <em>Pharmaceutical Services Act</em>;
                          </li>
                          <li>
                            <strong>&quot;Approved SSO&quot;</strong> means, in relation to a Site, the software support organization
                            identified in section 1 of the Site Request that provides Organization with the SSO-Provided
                            Technology used at the Site;
                          </li>
                          <li>
                            <strong>&quot;Associated Technology&quot;</strong> means, in relation to a Site, any information technology
                            hardware, software or services used at the Site, other than the SSO-Provided Technology, that is
                            in any way used in connection with Site Access or any PharmaNet Data;
                          </li>
                          <li>
                            <p>
                              <strong>&quot;Authorized User&quot;</strong> means an individual who is granted access to PharmaNet by the
                              Province and who is:
                            </p>

                            <ol type=""i"">
                              <li>
                                an employee or independent contractor of Organization, or
                              </li>
                              <li>
                                if Organization is an individual, the Organization;
                              </li>
                            </ol>
                          </li>
                          <li>
                            <strong>&quot;Information Management Regulation&quot;</strong> means the
                            <em>Information Management Regulation</em>,
                            B.C. Reg. 74/2015;
                          </li>
                          <li>
                            <strong>&quot;On-Behalf-Of User&quot;</strong> means an Authorized User described in subsection 4 (5) of the
                            <em>Information Management Regulation</em> who acts on behalf of a Regulated User when accessing
                            PharmaNet;
                          </li>
                          <li>
                            &quot;PharmaNet&quot; means PharmaNet as continued under section 2 of the
                            <em>Information Management Regulation</em>;
                          </li>
                          <li>
                            <strong>&quot;PharmaNet Data&quot;</strong> includes any records or information contained in PharmaNet and
                            any records
                            or information in the custody, control or possession of Organization or any Authorized User as the result of
                            any Site Access;
                          </li>
                          <li>
                            <strong>&quot;Regulated User&quot;</strong> means an Authorized User described in subsections 4 (2) to (4)
                            of the
                            <em>Information Management Regulation</em>;
                          </li>
                          <li>
                            <strong>&quot;Signing Authority&quot;</strong> means the individual identified by Organization as the
                            &quot;Signing Authority&quot;
                            for a Site, with the associated contact information, as set out in section 2 of the Site Request;
                          </li>
                          <li>
                            <p>
                              &quot;Site&quot; means a premises operated by Organization and located in British Columbia that:
                            </p>

                            <ol type=""i"">
                              <li>
                                is the subject of a Site Request submitted to the Province, and
                              </li>
                              <li>
                                has been approved for Site Access by the Province in writing
                              </li>
                            </ol>

                            <p class=""underline"">
                              For greater certainty, &quot;Site&quot; does not include a location from which remote access to PharmaNet
                              takes place;
                            </p>
                          </li>
                          <li>
                            <strong>&quot;Site Access&quot;</strong> means any access to or use of PharmaNet at a Site or remotely as
                            permitted
                            by the Province;
                          </li>
                          <li>
                            <strong>&quot;Site Request&quot;</strong> means, in relation to a Site, the information contained in the
                            PharmaNet access
                            request form submitted to the Province by the Organization, requesting PharmaNet access at the Site, as such
                            information is updated by the Organization from time to time in accordance with section 2.2;
                          </li>
                          <li>
                            <strong>&quot;SSO-Provided Technology&quot;</strong> means any information technology hardware, software or
                            services
                            provided to Organization by an Approved SSO for the purpose of Site Access;
                          </li>
                        </ol>
                      </li>
                      <li>
                        Unless otherwise specified, a reference to a statute or regulation by name means a statute or regulation of
                        British
                        Columbia of that name, as amended or replaced from time to time, and includes any enactments made under the
                        authority
                        of that statute or regulation.
                      </li>
                      <li>
                        <p>
                          The following are the Schedules attached to and incorporated into this Agreement:
                        </p>

                        <ul>
                          <li>
                            Schedule A – Specific Privacy and Security Measures
                          </li>
                        </ul>
                      </li>
                      <li>
                        The main body of this Agreement, the Schedules, and any documents incorporated by reference into this Agreement
                        are to
                        be interpreted so that all of the provisions are given as full effect as possible. In the event of a conflict,
                        unless
                        expressly stated to the contrary the main body of the Agreement will prevail over the Schedules, which will
                        prevail
                        over any document incorporated by reference.
                      </li>
                    </ol>
                  </li>
                </ol>

                <p class=""text-center"">
                  <strong>ARTICLE 2 – REPRESENTATIONS AND WARRANTIES</strong>
                </p>

                <ol type=""1""
                    start=""2""
                    class=""decimal"">
                  <li>
                    <ol type=""1"">
                      <li>
                        <p>
                          Organization represents and warrants to the Province, as of the date of this
                          Agreement and throughout its term, that:
                        </p>

                        <ol type=""a"">
                          <li>
                            the information contained in the Site Request for each Site is true and correct;
                          </li>
                          <li>
                            <p>
                              if Organization is not an individual:
                            </p>

                            <ol type=""i"">
                              <li>
                                Organization has the power and capacity to enter into this Agreement and to comply with its terms;
                              </li>
                              <li>
                                all necessary corporate or other proceedings have been taken to authorize the execution and delivery
                                of this Agreement by, or on behalf of, Organization; and
                              </li>
                              <li>
                                this Agreement has been legally and properly executed by, or on behalf of, the Organization and is
                                legally binding upon and enforceable against Organization in accordance with its terms.
                              </li>
                            </ol>
                          </li>
                        </ol>
                      </li>
                      <li>
                        Organization must immediately notify the Province of any change to the information contained in a Site Request,
                        including any change to a Site’s status, location, normal operating hours, Approved SSO, or the name and contact
                        information of the Signing Authority or any of the other specific roles set out in the Site Request. Such
                        notices
                        must be submitted to the Province in the form and manner directed by the Province in its published instructions
                        regarding the submission of updated Site Request information, as such instructions may be updated from time to
                        time by the Province.
                      </li>
                    </ol>
                  </li>
                </ol>

                <p class=""text-center"">
                  <strong>ARTICLE 3 – SITE ACCESS REQUIREMENTS</strong>
                </p>

                <ol type=""1""
                    start=""3""
                    class=""decimal"">
                  <li>
                    <ol type=""1"">
                      <li>
                        Organization must comply with the Act and all applicable law.
                      </li>
                      <li>
                        Organization must submit a Site Request to the Province for each physical location where it intends to provide
                        Site
                        Access, and must only provide Site Access from Sites approved in writing by the Province. For greater certainty,
                        a
                        Site Request is not required for each physical location from which remote access, as permitted under section
                        3.6,
                        may occur, but Organization must provide, with the Site Request, a list of the locations from which remote
                        access
                        may occur, and ensure this list remains current for the term of this agreement.
                      </li>
                      <li>
                        Organization must only provide Site Access using SSO-Provided Technology. For the purposes of remote access,
                        Organization must ensure that technology used meets the requirements of Schedule A.
                      </li>
                      <li>
                        Unless otherwise authorized by the Province in writing, Organization must at all times use the secure network or
                        security technology that the Province certifies or makes available to Organization for the purpose of Site
                        Access.
                        The use of any such network or technology by Organization may be subject to terms and conditions of use,
                        including
                        acceptable use policies, established by the Province and communicated to Organization from time to time in
                        writing.
                      </li>
                      <li>
                        <p>
                          Organization must only make Site Access available to the following individuals:
                        </p>

                        <ol type=""a"">
                          <li>
                            Authorized Users when they are physically located at a Site, and, in the case of an On-Behalf-of-User
                            accessing
                            personal information of a patient on behalf of a Regulated User, only if the Regulated User will be
                            delivering
                            care to that patient at the same Site at which the access to personal information occurs;
                          </li>
                          <li>
                            Representatives of an Approved SSO for technical support purposes, in accordance with section 6 of the
                            <em>Information Management Regulation</em>.
                          </li>
                        </ol>
                      </li>
                      <li>
                        Despite section 3.5(a), Organization may make Site Access available to Regulated Users who are physically
                        located in
                        British Columbia and remotely connected to a Site using a VPN or other remote access technology specifically
                        approved
                        by the Province in writing for the Site.
                      </li>
                      <li>
                        <p>
                          Organization must ensure that Authorized Users with Site Access:
                        </p>

                        <ol type=""a"">
                          <li>
                            only access PharmaNet to the extent necessary to provide health services to, or facilitate the care of, the
                            individual whose personal information is being accessed;
                          </li>
                          <li>
                            first complete any mandatory training program(s) that the Site’s Approved SSO or the Province makes
                            available
                            in relation to PharmaNet;
                          </li>
                          <li>
                            access PharmaNet using their own separate login identifications and credentials, and do not share or have
                            multiple use of any such login identifications and credentials;
                          </li>
                          <li>
                            secure all devices, codes and credentials that enable access to PharmaNet against unauthorized use; and
                          </li>
                          <li>
                            in the case of remote access, comply with the policies of the Province relating to remote access to
                            PharmaNet.
                          </li>
                        </ol>
                      </li>
                      <li>
                        If notified by the Province that an Authorized User’s access to PharmaNet has been suspended or revoked,
                        Organization
                        will immediately take any local measures necessary to remove the Authorized User’s Site Access. Organization
                        will
                        only restore Site Access to a previously suspended or revoked Authorized User upon the Province’s specific
                        written
                        direction.
                      </li>
                      <li>
                        <p>
                          For the purposes of this section:
                        </p>

                        <ol type=""a"">
                          <li>
                            <strong>&quot;Responsible Authorized User&quot;</strong> means, in relation to any PharmaNet Data, the
                            Regulated User by whom,
                            or on whose behalf, that data was obtained from PharmaNet; and
                          </li>
                          <li>
                            <strong>&quot;Use&quot;</strong> includes to collect, access, retain, use, de-identify, and disclose.
                          </li>
                        </ol>

                        <p>
                          The PharmaNet Data disclosed under this Agreement is disclosed by the Province solely for the Use of the
                          Responsible
                          User to whom it is disclosed.
                        </p>

                        <p>
                          Organization must not Use any PharmaNet Data, or permit any third party to Use PharmaNet Data, unless the
                          Responsible
                          User has authorized such Use and it is otherwise permitted under the Act, applicable law, and the limits and
                          conditions imposed by the Province on the Responsible User.
                        </p>
                      </li>
                      <li>
                        <p>
                          Organization must make all reasonable arrangements to protect PharmaNet Data against such risks as
                          unauthorized access,
                          collection, use, modification, retention, disclosure or disposal, including by:
                        </p>

                        <ol type=""a"">
                          <li>
                            taking all reasonable physical, technical and operational measures necessary to ensure Site Access operates
                            in
                            accordance with sections 3.1 to 3.9 above, and
                          </li>
                          <li>
                            complying with the requirements of Schedule A.
                          </li>
                        </ol>
                      </li>
                    </ol>
                  </li>
                </ol>

                <p class=""text-center"">
                  <strong>ARTICLE 4 – NON-COMPLIANCE AND INVESTIGATIONS</strong>
                </p>

                <ol type=""1""
                    start=""4""
                    class=""decimal"">
                  <li>
                    <ol type=""1"">
                      <li>
                        Organization must promptly notify the Province, and provide particulars, if Organization does not comply, or
                        anticipates
                        that it will be unable to comply, with the terms of this Agreement, or if Organization has knowledge of any
                        circumstances,
                        incidents or events which have or may jeopardize the security, confidentiality or integrity of PharmaNet,
                        including any
                        attempt by any person to gain unauthorized access to PharmaNet or the networks or equipment used to connect to
                        PharmaNet
                        or convey PharmaNet Data.
                      </li>
                      <li>
                        Organization must immediately investigate any suspected breaches of this Agreement and take all reasonable steps
                        to prevent
                        recurrences of any such breaches.
                      </li>
                      <li>
                        Organization must cooperate with any audits or investigations conducted by the Province (including any
                        independent auditor
                        appointed by the Province) regarding compliance with this Agreement, including by providing access upon request
                        to a Site
                        and any associated facilities, networks, equipment, systems, books, records and personnel for the purposes of
                        such audit
                        or investigation.
                      </li>
                    </ol>
                  </li>
                </ol>

                <p class=""text-center"">
                  <strong>ARTICLE 5 – SITE TERMINATION</strong>
                </p>

                <ol type=""1""
                    start=""5""
                    class=""decimal"">
                  <li>
                    <ol type=""1"">
                      <li>
                        <p>
                          The Province may terminate all Site Access at a Site immediately, upon notice to the Signing Authority for the
                          Site, if:
                        </p>

                        <ol type=""a"">
                          <li>
                            the Approved SSO for the Site is no longer approved by the Province to provide information technology
                            hardware, software,
                            or service in connection with PharmaNet, or
                          </li>
                          <li>
                            the Province determines that the SSO-Provided Technology or Associated Technology in use at the Site, or any
                            component
                            thereof, is obsolete, unsupported, or otherwise poses an unacceptable security risk to PharmaNet,
                          </li>
                        </ol>

                        <p>
                          and the Organization is unable or unwilling to remedy the problem within a timeframe acceptable to the
                          Province.
                        </p>
                      </li>
                      <li>
                        As a security precaution, the Province may suspend Site Access at a Site after a period of inactivity. If Site
                        Access at a
                        Site remains inactive for a period of 90 days or more, the Province may, immediately upon notice to the Signing
                        Authority
                        for the Site, terminate all further Site Access at the Site.
                      </li>
                      <li>
                        Organization must prevent all further Site Access at a Site immediately upon the Province’s termination, in
                        accordance with
                        this Article 5, of Site Access at the Site.
                      </li>
                    </ol>
                  </li>
                </ol>

                <p class=""text-center"">
                  <strong>ARTICLE 6 – TERM AND TERMINATION</strong>
                </p>

                <ol type=""1""
                    start=""6""
                    class=""decimal"">
                  <li>
                    <ol type=""1"">
                      <li>
                        The term of this Agreement begins on the date first noted above and continues until it is terminated
                        in accordance with this Article 6.
                      </li>
                      <li>
                        Organization may terminate this Agreement at any time on notice to the Province.
                      </li>
                      <li>
                        The Province may terminate this Agreement immediately upon notice to Organization if Organization fails to
                        comply with any
                        provision of this Agreement.
                      </li>
                      <li>
                        The Province may terminate this Agreement immediately upon notice to Organization in the event Organization no
                        longer operates
                        any Sites where Site Access is permitted.
                      </li>
                      <li>
                        The Province may terminate this Agreement for any reason upon two (2) months advance notice to Organization.
                      </li>
                      <li>
                        Organization must prevent any further Site Access immediately upon termination of this Agreement.
                      </li>
                    </ol>
                  </li>
                </ol>

                <p class=""text-center"">
                  <strong>ARTICLE 7 – DISCLAIMER AND INDEMNITY</strong>
                </p>

                <ol type=""1""
                    start=""7""
                    class=""decimal"">
                  <li>
                    <ol type=""1"">
                      <li>
                        The PharmaNet access and PharmaNet Data provided under this Agreement are provided &quot;as is&quot; without
                        warranty of any kind,
                        whether express or implied. All implied warranties, including, without limitation, implied warranties of
                        merchantability,
                        fitness for a particular purpose, and non-infringement, are hereby expressly disclaimed. The Province does not
                        warrant
                        the accuracy, completeness or reliability of the PharmaNet Data or the availability of PharmaNet, or that access
                        to or
                        the operation of PharmaNet will function without error, failure or interruption.
                      </li>
                      <li>
                        Under no circumstances will the Province be liable to any person or business entity for any direct, indirect,
                        special,
                        incidental, consequential, or other damages based on any use of PharmaNet or the PharmaNet Data, including
                        without
                        limitation any lost profits, business interruption, or loss of programs or information, even if the Province has
                        been specifically advised of the possibility of such damages.
                      </li>

                      <li>
                        Organization must indemnify and save harmless the Province, and the Province’s employees and agents (each an
                        <strong>""Indemnified Person""</strong>) from any losses, claims, damages, actions, causes of action, costs and
                        expenses that an Indemnified Person may sustain, incur, suffer or be put to at any time, either before or after
                        this Agreement ends, which are based upon, arise out of or occur directly or indirectly by reason of any act
                        or omission by Organization, or by any Authorized User at the Site, in connection with this Agreement.
                      </li>
                    </ol>
                  </li>
                </ol>

                <p class=""text-center"">
                  <strong>ARTICLE 8 – GENERAL</strong>
                </p>

                <ol type=""1""
                    start=""8""
                    class=""decimal"">
                  <li>
                    <ol type=""1"">
                      <li>
                        <p>
                          <strong class=""underline"">Notice.</strong> Except where this Agreement expressly provides for another method
                          of delivery, any notice to be given to the Province must be in writing and emailed or mailed to:
                        </p>

                        <address>
                          Director, Information and PharmaNet Innovation<br>
                          Ministry of Health<br>
                          PO Box 9652, STN PROV GOVT<br>
                          Victoria, BC V8W 9P4<br>

                          <br>

                          <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                        </address>

                        <p>
                          Any notice to be given to a Signing Authority or the Organization will be in writing and emailed, mailed,
                          faxed
                          or text messaged to the Signing Authority (in the case of notice to a Signing Authority) or all Signing
                          Authorities (in the case of notice to the Organization). A Signing Authority may be required to click a
                          URL link or to log in to the Province’s &quot;PRIME&quot; system to receive the content of any such notice.
                        </p>

                        <p>
                          Any written notice from a party, if sent electronically, will be deemed to have been received 24 hours after
                          the
                          time the notice was sent, or, if sent by mail, will be deemed to have been received 3 days (excluding
                          Saturdays,
                          Sundays and statutory holidays) after the date the notice was sent.
                        </p>
                      </li>
                      <li>
                        <strong class=""underline"">Waiver.</strong> The failure of the Province at any time to insist on performance of
                        any
                        provision of this Agreement by Organization is not a waiver of its right subsequently to insist on performance
                        of
                        that or any other provision of this Agreement. A waiver of any provision or breach of this Agreement is
                        effective
                        only if it is writing and signed by, or on behalf of, the waiving party.
                      </li>
                      <li>
                        <p>
                          <strong class=""underline"">Modification.</strong> No modification to this Agreement is effective unless it is
                          in writing and signed
                          by, or on behalf of, the parties.
                        </p>

                        <p>
                          Notwithstanding the foregoing, the Province may amend this Agreement, including the Schedules and this
                          section,
                          at any time in its sole discretion, by written notice to Organization, in which case the amendment will become
                          effective upon the later of: (i) the date notice of the amendment is delivered to Organization; and (ii) the
                          effective date of the amendment specified by the Province. The Province will make reasonable efforts to
                          provide
                          at least thirty (30) days advance notice of any such amendment, subject to any determination by the Province
                          that a shorter notice period is necessary due to changes in the Act, applicable law or applicable policies of
                          the Province, or is necessary to maintain privacy and security in relation to PharmaNet or PharmaNet Data.
                        </p>

                        <p>
                          If Organization does not agree with any amendment for which notice has been provided by the Province in
                          accordance with this section, Organization must promptly (and in any event prior to the effective date)
                          cease Site Access at all Sites and take the steps necessary to terminate this Agreement in accordance
                          with Article 6.
                        </p>
                      </li>
                      <li>
                        <p>
                          <strong class=""underline"">Governing Law.</strong> This Agreement will be governed by and will be construed
                          and interpreted in accordance with the laws of British Columbia and the laws of Canada applicable therein.
                        </p>
                      </li>
                    </ol>
                  </li>
                </ol>

                <p class=""text-center"">
                  <strong>SCHEDULE A – SPECIFIC PRIVACY AND SECURITY MEASURES</strong>
                </p>

                <p>
                  Organization must, in relation to each Site and in relation to Remote Access:
                </p>

                <ol type=""a"">
                  <li>
                    secure all workstations and printers at the Site to prevent any viewing of PharmaNet Data by persons other
                    than Authorized Users;
                  </li>
                  <li>
                    <p>
                      implement all privacy and security measures specified in the following documents published by the Province, as
                      amended from time to time:
                    </p>

                    <ol type=""i"">
                      <li>
                        <p>
                          the PharmaNet Professional and Software Conformance Standards
                        </p>

                        <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards""
                           target=""_blank""
                           rel=""noopener noreferrer"">
                          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards
                        </a>
                      </li>
                      <li>
                        <p>
                          Office of the Chief Information Officer: &quot;Submission for Technical Security Standard and High Level
                          Architecture for Wireless Local Area Network Connectivity&quot;
                        </p>

                        <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet""
                           target=""_blank""
                           rel=""noopener noreferrer"">
                          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet
                        </a>
                      </li>
                      <li>
                        <p>
                          Policy for Secure Remote Access to PharmaNet
                        </p>

                        <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards""
                           target=""_blank""
                           rel=""noopener noreferrer"">
                          https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards
                        </a>
                      </li>
                    </ol>
                  </li>
                  <li>
                    ensure that a qualified technical support person is engaged to provide security support for the Site. This person
                    should be familiar with the Site’s network configurations, hardware and software, including all SSO-Provided
                    Technology
                    and Associated Technology, and should be capable of understanding and adhering to the standards set forth in this
                    Agreement and Schedule. Note that any such qualified technical support person must not be permitted by Organization
                    to access or use PharmaNet in any manner, unless otherwise permitted under this Agreement;
                  </li>
                  <li>
                    establish and maintain documented privacy policies that detail how Organization will meet its privacy obligations
                    in relation to the Site;
                  </li>
                  <li>
                    establish breach reporting and response processes in relation to the Site;
                  </li>
                  <li>
                    detail expectations for privacy protection in contracts and service agreements as applicable at the Site;
                  </li>
                  <li>
                    regularly review the administrative, physical and technological safeguards at the Site;
                  </li>
                  <li>
                    establish and maintain a program for monitoring PharmaNet use at the Site, including by making appropriate
                    monitoring
                    and reporting mechanisms available to Authorized Users for this purpose.
                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, 1, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<h1>PHARMANET COMMUNITY PHARMACIST TERMS OF ACCESS</h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <ol>
                  <li>

                    <p class=""bold underline"">
                      BACKGROUND
                    </p>

                    <p>
                      The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links B.C.
                      pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered into
                      PharmaNet.
                    </p>

                    <p>
                      The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet is to
                      enhance patient care by providing timely and relevant information to persons involved in the provision of direct
                      patient care.
                    </p>

                    <p class=""bold"">
                      PharmaNet contains highly sensitive confidential information, including Personal Information and the proprietary
                      and confidential information of third-party licensors to the Province, and it is in the public interest to ensure
                      that appropriate measures are in place to protect the confidentiality of all such information. All access to and
                      use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INTERPRETATION
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will have the
                          meanings given below:
                        </p>

                        <ul class=""list-unstyled"">
                          <li>
                            <strong>“Act”</strong> means the <em>Pharmaceutical Services Act</em>.
                          </li>
                          <li>
                            <strong>“Approved Practice Site”</strong> means the physical site at which you provide Direct Patient Care
                            and which is approved by the Province for PharmaNet access.
                            <span class=""underline"">For greater certainty, “Approved Practice Site” does not include a location from which remote access to PharmaNet takes place;</span>
                          </li>
                          <li>
                            <strong>“Approved SSO”</strong> means a software support organization approved by the Province that provides
                            you with the information technology software and/or services through which you and On-Behalf-of Users access
                            PharmaNet.
                          </li>
                          <li>
                            <strong>“Claim”</strong> means a claim made under the Act for payment in respect of a benefit under the Act.
                          </li>
                          <li>

                            <p>
                              <strong>“Conformance Standards”</strong> means the following documents published by the Province, as
                              amended from time to time:
                            </p>

                            <ol type=""i"">
                              <li>
                                PharmaNet Professional and Software Conformance Standards<br>
                                <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards"" target=""_blank"" rel=""noopener noreferrer"">
                                  https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards
                                </a>;
                              </li>
                              <li>
                                Policy for Secure Remote Access to PharmaNet
                                <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards"" target=""_blank"" rel=""noopener noreferrer"">
                                  https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards
                                </a>; and
                              </li>
                              <li>
                                iii. Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                                Architecture for Wireless Local Area Network Connectivity”.
                              </li>
                            </ol>

                          </li>
                          <li>
                            <strong>“Device Provider”</strong> means a person enrolled under section 11 of the Act in the class of
                            provider known as “device provider”.
                          </li>
                          <li>
                            <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                            services to an individual to whom you provide direct patient care in the context of your Practice.
                          </li>
                          <li>
                            <strong>“Information Management Regulation”</strong> means the <em>Information Management Regulation</em>,
                            B.C. Reg.
                            74/2015.
                          </li>
                          <li>
                            <strong>“On-Behalf-of User”</strong> means a member of your staff who (i) requires access to PharmaNet to
                            carry out duties in relation to your Practice; (ii) is authorized by you to access PharmaNet on your behalf;
                            and (iii) has been granted access to PharmaNet by the Province.
                          </li>
                          <li>
                            <strong>“Personal Information”</strong> means all recorded information that is about an identifiable
                            individual or is defined as, or deemed to be, “personal information” or “personal health information”
                            pursuant to any Privacy Laws.
                          </li>
                          <li>
                            <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the
                            following website (or such other website as may be specified by the Province from time to time for this
                            purpose):

                            <br><br>

                            <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                          </li>
                          <li>
                            <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information Management
                            Regulation.
                          </li>
                          <li>
                            <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record
                            or information in the custody, control or possession of you or an On-Behalf-of User that was obtained
                            through your or an On-Behalf-of User’s access to PharmaNet.
                          </li>
                          <li>
                            <strong>“Practice”</strong> means your practice of the health profession regulated under the <em>Health
                                      Professions Act</em>, or your practice as a Device Provider, as identified by you through PRIME
                            or another mechanism provided by the Province.
                          </li>
                          <li>
                            <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for,
                            and manage, their access to PharmaNet, and through which users are granted access by the Province.
                          </li>
                          <li>
                            <strong>“Privacy Laws”</strong> means the Act, the
                            <em>Freedom of Information and Protection of Privacy Act</em>, the Personal Information Protection Act, and
                            any other statutory or legal obligations of privacy owed by you or the Province, whether arising under
                            statute, by contract or at common law.
                          </li>
                          <li>
                            <strong>“Provider”</strong> means a person enrolled under section 11 of the Act for the purpose of receiving
                            payment for providing benefits.
                          </li>
                          <li>
                            <strong>“Provider Regulation”</strong> means the <em>Provider Regulation</em>, B.C. Reg. 222/2014.
                          </li>
                          <li>
                            <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                            Minister of Health.
                          </li>
                          <li>
                            <strong>“Professional College”</strong> is the regulatory body governing your Practice.
                          </li>
                          <li>
                            <p>
                              <strong>“Unauthorized Person”</strong> means any person other than:
                            </p>

                            <ol type=""i"">
                              <li>
                                you,
                              </li>
                              <li>
                                an On-Behalf-of User, or
                              </li>
                              <li>
                                a representative of an Approved SSO that is accessing PharmaNet for technical support purposes in
                                accordance with section 6 of the <em>Information Management Regulation</em>.
                              </li>
                            </ol>

                          </li>
                        </ul>

                      </li>
                      <li>
                        <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or regulation by
                        name means the statute or regulation of British Columbia of that name, as amended or replaced from time to time,
                        and includes any enactment made under the authority of that statute or regulation.
                      </li>
                      <li>

                        <p>
                          <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this Agreement:
                        </p>

                        <ol type=""i"">
                          <li>
                            i. a provision in the body of this Agreement will prevail over any conflicting provision in any further
                            limits or conditions communicated to you in writing by the Province, unless the conflicting provision
                            expressly states otherwise; and a provision referred to in (i) above will prevail over any conflicting
                            provision in the Conformance Standards.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      APPLICATION OF LEGISLATION
                    </p>

                    <p>
                      You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act, the
                      Information Management Regulation and all Privacy Laws applicable to PharmaNet and PharmaNet Data.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
                    </p>

                    <p>
                      You acknowledge that:
                    </p>

                    <ol type=""a"">
                      <li>
                        a. PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
                        Province under the authority of the Act;
                      </li>
                      <li>
                        b. specific provisions of the Act (including but not limited to sections 24, 25 and 29) and the Information
                        Management Regulation apply directly to you and to On-Behalf-of Users as a result; and
                      </li>
                      <li>
                        c. this Agreement documents limits and conditions, set by the minister in writing, that the Act requires you to
                        comply with.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCESS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
                        compliance with the limits and conditions set out in this Agreement. The Province may from time to time, at its
                        discretion, amend or change the scope of your access privileges to PharmaNet as privacy, security, business and
                        clinical practice requirements change. In such circumstances, the Province will use reasonable efforts to notify
                        you of such changes.
                      </li>
                      <li>

                        <p>
                          <strong>Requirements for Access.</strong> The following requirements apply to your access to PharmaNet:
                        </p>

                        <ol type=""i"">
                          <li>
                            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, for so
                            long as you are a registrant in good standing with the Professional College and your registration permits
                            you to deliver Direct Patient Care requiring access to PharmaNet or, in the case of access as a Device
                            Provider, for so long as you are enrolled as a Device Provider;
                          </li>
                          <li>
                            <p>you will only access PharmaNet:</p>

                            <ul>
                              <li>
                                at the Approved Practice Site, and using only the technologies and applications approved by the
                                Province; or
                              </li>
                              <li>
                                • using a VPN or similar remote access technology, if you are physically located in British Columbia and
                                remotely connected to the Approved Practice Site using a VPN or other remote access technology
                                specifically approved by the Province in writing for the Approved Practice Site.
                              </li>
                            </ul>
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access
                            takes place at the Approved Practice Site and the access is required in relation to patients for whom you
                            will be providing Direct Patient Care at the Approved Practice Site;
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of Users do not access PharmaNet using VPN or other remote access
                            technology;
                          </li>
                          <li>
                            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will
                            ensure that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct
                            Patient Care;
                          </li>
                          <li>
                            you will not submit Claims on PharmaNet other than from an Approved Practice Site in respect of which a
                            person is enrolled as a Provider, and you will ensure that On-Behalf-of Users submit Claims on PharmaNet
                            only from an Approved Practice Site in respect of which a person is enrolled as a Provider;
                          </li>
                          <li>
                            you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of market
                            research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet Data, for the
                            purpose of market research;
                          </li>
                          <li>
                            subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure that
                            On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of Direct Patient
                            Care, including for the purposes of quality improvement, evaluation, health care planning, surveillance,
                            research or other secondary uses;
                          </li>
                          <li>
                            you will not permit any Unauthorized Person to access PharmaNet, and you will take all reasonable
                            measures to ensure that no Unauthorized Person can access PharmaNet;
                          </li>
                          <li>
                            you will complete any training program(s) that your Approved SSO makes available to you in relation to
                            PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
                          </li>
                          <li>
                            you will immediately notify the Province when you or an On-Behalf-of User no longer require access to
                            PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave of absence
                            from your staff, or where the On-Behalf-of User’s access-related duties in relation to the Practice have
                            changed;
                          </li>
                          <li>
                            you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional limits or
                            conditions applicable to you, as may be communicated to you by the Province in writing;
                          </li>
                          <li>
                            you represent and warrant that all information provided by you in connection with your application for
                            PharmaNet access, including through PRIME, is true and correct.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this Agreement
                        for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
                        PharmaNet Data.
                      </li>
                      <li>

                        <p>
                          <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable measures to
                          safeguard Personal Information, including any Personal Information in PharmaNet Data while it is in the
                          custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
                        </p>

                        <ol type=""i"">
                          <li>
                            take all reasonable steps to ensure the physical security of Personal Information, generally and as
                            required by Privacy Laws;
                          </li>
                          <li>
                            secure all workstations and printers in a protected area in the Approved Practice Site to prevent
                            viewing of PharmaNet Data by Unauthorized Persons;
                          </li>
                          <li>
                            ensure separate access credential (such as user name and password) for each On-Behalf-of User, and
                            prohibit sharing or other multiple use of your access credential, or an On-Behalf-of User’s access
                            credential, for access to PharmaNet;
                          </li>
                          <li>
                            secure any workstations used to access PharmaNet and all devices, codes or passwords that enable access
                            to PharmaNet by yourself or any On-Behalf-of User;
                          </li>
                          <li>
                            take such other privacy and security measures as the Province may reasonably require from time-to-time.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Conformance Standards.</strong> You will comply with, and will ensure On-Behalf-of Users comply with,
                        the rules specified in the Conformance Standards when accessing and recording information in PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLOSURE, STORAGE, AND ACCESS REQUESTS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper files or
                        any electronic system, unless such storage or retention is required for record keeping in accordance with the
                        Act, the Provider Regulation, and Professional College requirements and in connection with your provision of
                        Direct Patient Care and otherwise is in compliance with the Conformance Standards. You will not modify any
                        records retained in accordance with this section other than as may be expressly authorized in the Conformance
                        Standards. For clarity, you may annotate a discrete record provided that the discrete record is not itself
                        modified other than as expressly authorized in the Conformance Standards.
                      </li>
                      <li>
                        <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with section
                        6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the purpose of
                        monitoring your own Practice.
                      </li>
                      <li>
                        <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do not,
                        disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient Care or is
                        otherwise authorized under section 24(1) of the Act.
                      </li>
                      <li>
                        <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of Users do
                        not, disclose PharmaNet Data for the purpose of market research.
                      </li>
                      <li>
                        <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in accordance
                        with section 6(a) of this Agreement, you will not provide to patients any copies of records containing PharmaNet
                        Data or “print outs” produced directly from PharmaNet, and will refer any requests for access to such records or
                        “print outs” to the Province.
                      </li>
                      <li>
                        <strong>Responding to Requests to Correct a Record contained in PharmaNet.</strong> If you receive a request for
                        correction of any record or information contained in PharmaNet, you will refer the request to the Province.
                      </li>
                      <li>
                        <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the Province if
                        you receive any order, demand or request compelling, or threatening to compel, disclosure of records contained
                        in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For greater
                        certainty, the foregoing requires that you notify the Province only with respect to any access requests or
                        demands for records contained in PharmaNet, and not records retained by you in accordance with section 6(a) of
                        this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCURACY
                    </p>

                    <p>
                      You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of User
                      in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material inaccuracy or
                      error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it if
                      necessary, and notify the Province of the inaccuracy or error and any steps taken.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INVESTIGATIONS, AUDITS, AND REPORTING
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations conducted by
                        the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
                        Agreement, including providing access upon request to your facilities, data management systems, books, records
                        and personnel for the purposes of such audit or investigation.
                      </li>
                      <li>
                        <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province may
                        report any material breach of this Agreement to your Professional College or to the Information and Privacy
                        Commissioner of British Columbia.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE OF NON COMPLIANCE AND DUTY TO INVESTIGATE
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this Agreement,
                        and will take all reasonable steps to prevent recurrences of any such breaches, including taking any steps
                        necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of User’s
                        access rights.
                      </li>
                      <li>

                        <p>
                          <strong>Non Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
                        </p>

                        <ol type=""i"">
                          <li>
                            you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User will be unable
                            to comply with the terms of this Agreement in any respect, or
                          </li>
                          <li>
                            you have knowledge of any circumstances, incidents or events which have or may jeopardize the security,
                            confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person, to access
                            PharmaNet.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      TERM OF AGREEMENT, SUSPENSION & TERMINATION
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet by the
                        Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or (e)
                        below.
                      </li>
                      <li>
                        <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written notice to
                        the Province.
                      </li>
                      <li>
                        <strong>Suspension or Termination of PharmaNet access.</strong> If the Province suspends or terminates your
                        right, or an On-Behalf-of User’s right, to access PharmaNet under the
                        <em>Information Management Regulation</em>, the Province may also terminate this Agreement at any time
                        thereafter upon written notice to you.
                      </li>
                      <li>
                        <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate this
                        Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to you if
                        you or an On-Behalf-of User fail to comply with any provision of this Agreement.
                      </li>
                      <li>
                        <strong>Termination by operation of the Information Management Regulation.</strong> This Agreement will
                        terminate automatically if your access to PharmaNet ends by operation of section 18 of the
                        <em>Information Management Regulation</em>.
                      </li>
                      <li>
                        <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may suspend your
                        account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the Province’s
                        policies. Please contact the Province immediately if your account has been suspended for inactivity but you
                        still require access to PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and PharmaNet
                        Data is solely at your own risk. All such access and information is provided on an “as is” and “as available”
                        basis without warranty or condition of any kind. The Province does not warrant the accuracy, completeness or
                        reliability of the PharmaNet Data or the availability of PharmaNet, or that access to or the operation of
                        PharmaNet will function without error, failure or interruption.
                      </li>
                      <li>
                        <strong>You are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
                        you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or acting
                        upon such information. The clinical or other information disclosed to you or an On-Behalf-of User pursuant to
                        this Agreement is in no way intended to be a substitute for professional judgment.
                      </li>
                      <li>
                        <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the Province
                        for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or PharmaNet
                        Data.
                      </li>
                      <li>
                        <strong>You Must Indemnify the Province if You Cause a Loss or Claim.</strong> You agree to indemnify and save
                        harmless the Province, and the Province’s employees and agents (each an <strong>""Indemnified Person""</strong>)
                        from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified Person may
                        sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are based
                        upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
                        On-Behalf-of User, in connection with this Agreement or in connection with access to PharmaNet by you or an
                        On-Behalf-of User.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another method of
                          delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to be
                          effective, must be in writing and emailed or mailed to:
                        </p>

                        <address>
                          Director, Information and PharmaNet Innovation<br>
                          Ministry of Health<br>
                          PO Box 9652, STN PROV GOVT<br>
                          Victoria, BC V8W 9P4<br>

                          <br>

                          <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                        </address>

                      </li>
                      <li>
                        <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will be in
                        writing and delivered by the Province to you using any of the contact mechanisms identified by you in PRIME,
                        including by mail to a specified postal address, email to a specified email address or text message to the
                        specified cell phone number. You may be required to click a URL link or log into PRIME to receive the content of
                        any such notice.
                      </li>
                      <li>
                        <strong>Deemed receipt.</strong> Any written communication from a party, if personally delivered or sent
                        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent by
                        mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays) after
                        the date the notice was sent.
                      </li>
                      <li>
                        <strong>Substitute contact information.</strong> You may notify the Province of a substitute contact mechanism
                        by updating your contact information in PRIME.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      GENERAL
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and is
                          severable from any other covenant, and if any of them are held by a court, or other decision-maker, to be
                          invalid, this Agreement will be interpreted as if such provisions were not included.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Survival.</strong> Sections 3, 4, 5(b)(vii) (viii), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any other
                          provision of this Agreement that expressly or by its nature continues after termination, shall survive
                          termination of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and interpreted in
                          accordance with the laws of British Columbia and the laws of Canada applicable therein.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be assigned
                          without the prior written approval of the Province.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any provision of
                          this Agreement by you is not a waiver of its right subsequently to insist on performance of that or any other
                          provision of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Province may modify this Agreement.</strong> The Province may amend this Agreement, including this
                          section, at any time in its sole discretion:
                        </p>

                        <ol type=""i"">
                          <li>
                            by written notice to you, in which case the amendment will become effective upon the later of (A) the
                            date notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
                            by the Province, if any; or
                          </li>
                          <li>
                            by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                            specify the effective date of the amendment, which date will be at least 30 (thirty) days after the date
                            that the PharmaCare Newsletter containing the notice is first published.
                          </li>
                        </ol>

                        <p>
                          If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment described in
                          (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this Agreement will be
                          deemed to have been so amended as of the effective date. If you do not agree with any amendment for which
                          notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly (and in any
                          event before the effective date) cease all access or use of PharmaNet by yourself and all On-Behalf-of Users,
                          and take the steps necessary to terminate this Agreement in accordance with section 10.
                        </p>

                      </li>
                    </ol>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 10, 1, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <h1>PHARMANET USER TERMS OF ACCESS FOR PHARMACISTS</h1>

                <ol>
                  <li>

                    <p class=""bold underline"">
                      BACKGROUND
                    </p>

                    <p>
                      The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links B.C.
                      pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered into
                      PharmaNet.
                    </p>

                    <p>
                      The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet is to
                      enhance patient care by providing timely and relevant information to persons involved in the provision of direct
                      patient care.
                    </p>

                    <p class=""bold"">
                      PharmaNet contains highly sensitive confidential information, including Personal Information and the proprietary
                      and confidential information of third-party licensors to the Province, and it is in the public interest to ensure
                      that appropriate measures are in place to protect the confidentiality of all such information. All access to and
                      use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INTERPRETATION
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will have the
                          meanings given below:
                        </p>

                        <ul class=""list-unstyled"">
                          <li>
                            <strong>“Act”</strong> means the <em>Pharmaceutical Services Act</em>.
                          </li>
                          <li>
                            <strong>“Approved Practice Site”</strong> means the physical site at which you provide Direct Patient Care
                            and which is approved by the Province for PharmaNet access.
                          </li>
                          <li>
                            <strong>“Approved SSO”</strong> means a software support organization approved by the Province that provides
                            you with the information technology software and/or services through which you and On-Behalf-of Users access
                            PharmaNet.
                          </li>
                          <li>
                            <strong>“Claim”</strong> means a claim made under the Act for payment in respect of a benefit under the Act.
                          </li>
                          <li>

                            <p>
                              <strong>“Conformance Standards”</strong> means the following documents published by the Province, as
                              amended from time to time:
                            </p>

                            <ol type=""i"">
                              <li>
                                PharmaNet Professional and Software Conformance Standards<br>
                                <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards"" target=""_blank"" rel=""noopener noreferrer"">
                                  https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/software/conformance-standards
                                </a>; and
                              </li>
                              <li>
                                Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                                Architecture for Wireless Local Area Network Connectivity”.
                                <a href=""https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet"" target=""_blank"" rel=""noopener noreferrer"">
                                  https://www2.gov.bc.ca/gov/content/health/practitioner-professional-resources/system-access/requirements-for-wireless-access-to-pharmanet
                                </a>
                              </li>
                            </ol>

                          </li>
                          <li>
                            <strong>“Device Provider”</strong> means a person enrolled under section 11 of the Act in the class of
                            provider known as “device provider”.
                          </li>
                          <li>
                            <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                            services to an individual to whom you provide direct patient care in the context of your Practice.
                          </li>
                          <li>
                            <strong>“Information Management Regulation”</strong> means the <em>Information Management Regulation</em>,
                            B.C. Reg.
                            74/2015.
                          </li>
                          <li>
                            <strong>“On-Behalf-of User”</strong> means a member of your staff who (i) requires access to PharmaNet to
                            carry out duties in relation to your Practice; (ii) is authorized by you to access PharmaNet on your behalf;
                            and (iii) has been granted access to PharmaNet by the Province.
                          </li>
                          <li>
                            <strong>“Personal Information”</strong> means all recorded information that is about an identifiable
                            individual or is defined as, or deemed to be, “personal information” or “personal health information”
                            pursuant to any Privacy Laws.
                          </li>
                          <li>
                            <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the
                            following website (or such other website as may be specified by the Province from time to time for this
                            purpose):

                            <br><br>

                            <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                          </li>
                          <li>
                            <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information Management
                            Regulation.
                          </li>
                          <li>
                            <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record
                            or information in the custody, control or possession of you or an On-Behalf-of User that was obtained
                            through your or an On-Behalf-of User’s access to PharmaNet.
                          </li>
                          <li>
                            <strong>“Practice”</strong> means your practice of the health profession regulated under the <em>Health
                                      Professions Act</em>, or your practice as a Device Provider, as identified by you through PRIME
                            or another mechanism provided by the Province.
                          </li>
                          <li>
                            <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for,
                            and manage, their access to PharmaNet, and through which users are granted access by the Province.
                          </li>
                          <li>
                            <strong>“Privacy Laws”</strong> means the Act, the
                            <em>Freedom of Information and Protection of Privacy Act</em>, the Personal Information Protection Act, and
                            any other statutory or legal obligations of privacy owed by you or the Province, whether arising under
                            statute, by contract or at common law.
                          </li>
                          <li>
                            <strong>“Provider”</strong> means a person enrolled under section 11 of the Act for the purpose of receiving
                            payment for providing benefits.
                          </li>
                          <li>
                            <strong>“Provider Regulation”</strong> means the <em>Provider Regulation</em>, B.C. Reg. 222/2014.
                          </li>
                          <li>
                            <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                            Minister of Health.
                          </li>
                          <li>
                            <strong>“Professional College”</strong> is the regulatory body governing your Practice.
                          </li>
                          <li>
                            <p>
                              <strong>“Unauthorized Person”</strong> means any person other than:
                            </p>

                            <ol type=""i"">
                              <li>
                                you,
                              </li>
                              <li>
                                an On-Behalf-of User, or
                              </li>
                              <li>
                                a representative of an Approved SSO that is accessing PharmaNet for technical support purposes in
                                accordance with section 6 of the <em>Information Management Regulation</em>.
                              </li>
                            </ol>

                          </li>
                        </ul>

                      </li>
                      <li>
                        <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or regulation by
                        name means the statute or regulation of British Columbia of that name, as amended or replaced from time to time,
                        and includes any enactment made under the authority of that statute or regulation.
                      </li>
                      <li>

                        <p>
                          <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this Agreement:
                        </p>

                        <ol type=""i"">
                          <li>
                            a provision in the body of this Agreement will prevail over any conflicting provision in any further limits
                            or conditions communicated to you in writing by the Province, unless the conflicting provision expressly
                            states otherwise; and
                          </li>
                          <li>
                            a provision referred to in (i) above will prevail over any conflicting provision in the Conformance
                            Standards.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      APPLICATION OF LEGISLATION
                    </p>

                    <p>
                      You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act, the
                      Information Management Regulation and all Privacy Laws applicable to PharmaNet and PharmaNet Data.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
                    </p>

                    <p>
                      You acknowledge that:
                    </p>

                    <ol type=""a"">
                      <li>
                        a. PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
                        Province under the authority of the Act;
                      </li>
                      <li>
                        b. specific provisions of the Act (including but not limited to sections 24, 25 and 29) and the Information
                        Management Regulation apply directly to you and to On-Behalf-of Users as a result; and
                      </li>
                      <li>
                        c. this Agreement documents limits and conditions, set by the minister in writing, that the Act requires you to
                        comply with.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCESS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
                        compliance with the limits and conditions set out in this Agreement. The Province may from time to time, at its
                        discretion, amend or change the scope of your access privileges to PharmaNet as privacy, security, business and
                        clinical practice requirements change. In such circumstances, the Province will use reasonable efforts to notify
                        you of such changes.
                      </li>
                      <li>

                        <p>
                          <strong>Requirements for Access.</strong> The following requirements apply to your access to PharmaNet:
                        </p>

                        <ol type=""i"">
                          <li>
                            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, for so
                            long as you are a registrant in good standing with the Professional College and your registration permits
                            you to deliver Direct Patient Care requiring access to PharmaNet or, in the case of access as a Device
                            Provider, for so long as you are enrolled as a Device Provider;
                          </li>
                          <li>
                            you will only access PharmaNet: at the Approved Practice Site, and using only the technologies and
                            applications approved by the Province;
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access takes
                            place at the Approved Practice Site and the access is required in relation to patients for whom you will be
                            providing Direct Patient Care at the Approved Practice Site;
                          </li>
                          <li>
                            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
                            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
                          </li>
                          <li>
                            you will not submit Claims on PharmaNet other than from an Approved Practice Site in respect of which a
                            person is enrolled as a Provider, and you will ensure that On-Behalf-of Users submit Claims on PharmaNet
                            only from an Approved Practice Site in respect of which a person is enrolled as a Provider;
                          </li>
                          <li>
                            you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of market
                            research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet Data, for the
                            purpose of market research;
                          </li>
                          <li>
                            subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure that
                            On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of Direct Patient
                            Care, including for the purposes of quality improvement, evaluation, health care planning, surveillance,
                            research or other secondary uses;
                          </li>
                          <li>
                            you will not permit any Unauthorized Person to access PharmaNet, and you will take all reasonable measures
                            to ensure that no Unauthorized Person can access PharmaNet;
                          </li>
                          <li>
                            you will complete any training program(s) that your Approved SSO makes available to you in relation to
                            PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
                          </li>
                          <li>
                            you will immediately notify the Province when you or an On-Behalf-of User no longer require access to
                            PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave of absence
                            from your staff, or where the On-Behalf-of User’s access-related duties in relation to the Practice have
                            changed;
                          </li>
                          <li>
                            you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional limits or
                            conditions applicable to you, as may be communicated to you by the Province in writing;
                          </li>
                          <li>
                            you represent and warrant that all information provided by you in connection with your application for
                            PharmaNet access, including through PRIME, is true and correct.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this Agreement
                        for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
                        PharmaNet Data.
                      </li>
                      <li>

                        <p>
                          <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable measures to
                          safeguard Personal Information, including any Personal Information in PharmaNet Data while it is in the
                          custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
                        </p>

                        <ol type=""i"">
                          <li>
                            take all reasonable steps to ensure the physical security of Personal Information, generally and as
                            required by Privacy Laws;
                          </li>
                          <li>
                            secure all workstations and printers in a protected area in the Approved Practice Site to prevent
                            viewing of PharmaNet Data by Unauthorized Persons;
                          </li>
                          <li>
                            ensure separate access credential (such as user name and password) for each On-Behalf-of User, and
                            prohibit sharing or other multiple use of your access credential, or an On-Behalf-of User’s access
                            credential, for access to PharmaNet;
                          </li>
                          <li>
                            secure any workstations used to access PharmaNet and all devices, codes or passwords that enable access
                            to PharmaNet by yourself or any On-Behalf-of User;
                          </li>
                          <li>
                            take such other privacy and security measures as the Province may reasonably require from time-to-time.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Conformance Standards.</strong> You will comply with, and will ensure On-Behalf-of Users comply with,
                        the rules specified in the Conformance Standards when accessing and recording information in PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLOSURE, STORAGE, AND ACCESS REQUESTS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper files or
                        any electronic system, unless such storage or retention is required for record keeping in accordance with the
                        Act, the Provider Regulation, and Professional College requirements and in connection with your provision of
                        Direct Patient Care and otherwise is in compliance with the Conformance Standards. You will not modify any
                        records retained in accordance with this section other than as may be expressly authorized in the Conformance
                        Standards. For clarity, you may annotate a discrete record provided that the discrete record is not itself
                        modified other than as expressly authorized in the Conformance Standards.
                      </li>
                      <li>
                        <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with section
                        6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the purpose of
                        monitoring your own Practice.
                      </li>
                      <li>
                        <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do not,
                        disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient Care or is
                        otherwise authorized under section 24(1) of the Act.
                      </li>
                      <li>
                        <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of Users do
                        not, disclose PharmaNet Data for the purpose of market research.
                      </li>
                      <li>
                        <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in accordance
                        with section 6(a) of this Agreement, you will not provide to patients any copies of records containing PharmaNet
                        Data or “print outs” produced directly from PharmaNet, and will refer any requests for access to such records or
                        “print outs” to the Province.
                      </li>
                      <li>
                        <strong>Responding to Requests to Correct a Record Contained in PharmaNet.</strong> If you receive a request for
                        correction of any record or information contained in PharmaNet, you will refer the request to the Province.
                      </li>
                      <li>
                        <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the Province if
                        you receive any order, demand or request compelling, or threatening to compel, disclosure of records contained
                        in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For greater
                        certainty, the foregoing requires that you notify the Province only with respect to any access requests or
                        demands for records contained in PharmaNet, and not records retained by you in accordance with section 6(a) of
                        this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCURACY
                    </p>

                    <p>
                      You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of User
                      in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material inaccuracy or
                      error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it if
                      necessary, and notify the Province of the inaccuracy or error and any steps taken.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INVESTIGATIONS, AUDITS, AND REPORTING
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations conducted by
                        the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
                        Agreement, including providing access upon request to your facilities, data management systems, books, records
                        and personnel for the purposes of such audit or investigation.
                      </li>
                      <li>
                        <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province may
                        report any material breach of this Agreement to your Professional College or to the Information and Privacy
                        Commissioner of British Columbia.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE OF NON-COMPLIANCE AND DUTY TO INVESTIGATE
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this Agreement,
                        and will take all reasonable steps to prevent recurrences of any such breaches, including taking any steps
                        necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of User’s
                        access rights.
                      </li>
                      <li>

                        <p>
                          <strong>Non-Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
                        </p>

                        <ol type=""i"">
                          <li>
                            you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User will be unable
                            to comply with the terms of this Agreement in any respect, or
                          </li>
                          <li>
                            you have knowledge of any circumstances, incidents or events which have or may jeopardize the security,
                            confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person, to access
                            PharmaNet.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      TERM OF AGREEMENT, SUSPENSION & TERMINATION
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet by the
                        Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or (e)
                        below.
                      </li>
                      <li>
                        <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written notice to
                        the Province.
                      </li>
                      <li>
                        <strong>Suspension or Termination of PharmaNet Access.</strong> If the Province suspends or terminates your
                        right, or an On-Behalf-of User’s right, to access PharmaNet under the
                        <em>Information Management Regulation</em>, the Province may also terminate this Agreement at any time
                        thereafter upon written notice to you.
                      </li>
                      <li>
                        <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate this
                        Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to you if
                        you or an On-Behalf-of User fail to comply with any provision of this Agreement.
                      </li>
                      <li>
                        <strong>Termination by Operation of the Information Management Regulation.</strong> This Agreement will
                        terminate automatically if your access to PharmaNet ends by operation of section 18 of the
                        <em>Information Management Regulation</em>.
                      </li>
                      <li>
                        <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may suspend your
                        account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the Province’s
                        policies. Please contact the Province immediately if your account has been suspended for inactivity but you
                        still require access to PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and PharmaNet
                        Data is solely at your own risk. All such access and information is provided on an “as is” and “as available”
                        basis without warranty or condition of any kind. The Province does not warrant the accuracy, completeness or
                        reliability of the PharmaNet Data or the availability of PharmaNet, or that access to or the operation of
                        PharmaNet will function without error, failure or interruption.
                      </li>
                      <li>
                        <strong>You Are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
                        you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or acting
                        upon such information. The clinical or other information disclosed to you or an On-Behalf-of User pursuant to
                        this Agreement is in no way intended to be a substitute for professional judgment.
                      </li>
                      <li>
                        <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the Province
                        for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or PharmaNet
                        Data.
                      </li>
                      <li>
                        <strong>You Must Indemnify the Province If You Cause a Loss or Claim.</strong> You agree to indemnify and save
                        harmless the Province, and the Province’s employees and agents (each an <strong>""Indemnified Person""</strong>)
                        from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified Person may
                        sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are based
                        upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
                        On-Behalf-of User, in connection with this Agreement or in connection with access to PharmaNet by you or an
                        On-Behalf-of User.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another method of
                          delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to be
                          effective, must be in writing and emailed or mailed to:
                        </p>

                        <address>
                          Director, Information and PharmaNet Innovation<br>
                          Ministry of Health<br>
                          PO Box 9652, STN PROV GOVT<br>
                          Victoria, BC V8W 9P4<br>

                          <br>

                          <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                        </address>

                      </li>
                      <li>
                        <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will be in
                        writing and delivered by the Province to you using any of the contact mechanisms identified by you in PRIME,
                        including by mail to a specified postal address, email to a specified email address or text message to the
                        specified cell phone number. You may be required to click a URL link or log into PRIME to receive the content of
                        any such notice.
                      </li>
                      <li>
                        <strong>Deemed Receipt.</strong> Any written communication from a party, if personally delivered or sent
                        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent by
                        mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays) after
                        the date the notice was sent.
                      </li>
                      <li>
                        <strong>Substitute Contact Information.</strong> You may notify the Province of a substitute contact mechanism
                        by updating your contact information in PRIME.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      GENERAL
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and is
                          severable from any other covenant, and if any of them are held by a court, or other decision-maker, to be
                          invalid, this Agreement will be interpreted as if such provisions were not included.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Survival.</strong> Sections 3, 4, 5(b)(vi) (vii), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any other
                          provision of this Agreement that expressly or by its nature continues after termination, shall survive
                          termination of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and interpreted in
                          accordance with the laws of British Columbia and the laws of Canada applicable therein.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be assigned
                          without the prior written approval of the Province.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any provision of
                          this Agreement by you is not a waiver of its right subsequently to insist on performance of that or any other
                          provision of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Province May Modify this Agreement.</strong> The Province may amend this Agreement, including this
                          section, at any time in its sole discretion:
                        </p>

                        <ol type=""i"">
                          <li>
                            by written notice to you, in which case the amendment will become effective upon the later of (A) the
                            date notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
                            by the Province, if any; or
                          </li>
                          <li>
                            by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                            specify the effective date of the amendment, which date will be at least 30 (thirty) days after the date
                            that the PharmaCare Newsletter containing the notice is first published.
                          </li>
                        </ol>

                        <p>
                          If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment described in
                          (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this Agreement will be
                          deemed to have been so amended as of the effective date. If you do not agree with any amendment for which
                          notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly (and in any
                          event before the effective date) cease all access or use of PharmaNet by yourself and all On-Behalf-of Users,
                          and take the steps necessary to terminate this Agreement in accordance with section 10.
                        </p>

                      </li>
                    </ol>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<h1>
                  PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER<br>
                  with individual limits and conditions added
                </h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <p class=""bold underline"">
                  On Behalf-of-User Access
                </p>

                <ol>
                  <li>
                    <p>
                      You represent and warrant to the Province that:
                    </p>

                    <ol type=""a"">
                      <li>
                        your employment duties in relation to a Practitioner require you to access PharmaNet (and PharmaNet Data) to
                        support the Practitioner’s delivery of Direct Patient Care;
                      </li>
                      <li>
                        you are directly supervised by a Practitioner who has been granted access to PharmaNet by the Province; and
                      </li>
                      <li>
                        all information provided by you in connection with your application for PharmaNet access, including all
                        information submitted through PRIME, is true and correct.
                      </li>
                    </ol>

                  </li>
                </ol>

                <p class=""bold underline"">
                  Definitions
                </p>

                <ol start=""2"">
                  <li>
                    <p>
                      In these terms, capitalized terms will have the following meanings:
                    </p>

                    <ul class=""list-unstyled"">
                      <li>
                        <strong>“Approved Practice Site”</strong> means the physical site at which a Practitioner provides Direct
                        Patient Care and which is approved by the Province for PharmaNet access. For greater certainty, “Approved
                        Practice Site” does not include a location from which remote access to PharmaNet takes place.
                      </li>
                      <li>
                        <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                        services to an individual to whom a Practitioner provides direct patient care in the context of their Practice.
                      </li>
                      <li>
                        <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the
                        following website (or such other website as may be specified by the Province from time to time for this
                        purpose):

                        <br><br>

                        <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                      </li>
                      <li>
                        <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the <em>Information Management
                        Regulation</em>, B.C. Reg. 74/2015.
                      </li>
                      <li>
                        <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record or
                        information in the custody, control or possession of you or a Practitioner that was obtained through access to
                        PharmaNet by anyone.
                      </li>
                      <li>
                        <strong>“Practice”</strong> means a Practitioner’s practice of their health profession.
                      </li>
                      <li>
                        <strong>“Practitioner”</strong> means a health professional regulated under the <em>Health Professions Act</em>,
                        or an enrolled device provide under the <em>Provider Regulation</em> B.C. Reg. 222/2014, who supervises your
                        access to and use of PharmaNet and who has been granted access to PharmaNet by the Province.
                      </li>
                      <li>
                        <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for, and
                        manage, their access to PharmaNet, and through which users are granted access by the Province.
                      </li>
                      <li>
                        <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                        Minister of Health.
                      </li>
                    </ul>

                  </li>
                  <li>
                    Unless otherwise specified, a reference to a statute or regulation by name means the statute or regulation of
                    British Columbia of that name, as amended or replaced from time to time, and includes any enactment made under the
                    authority of that statute or regulation.
                  </li>
                </ol>

                <p class=""bold underline"">
                  Terms of Access to PharmaNet
                </p>

                <ol start=""4"">
                  <li>

                    <p>
                      You must:
                    </p>

                    <ol type=""a"">
                      <li>
                        access and use PharmaNet and PharmaNet Data only at the Approved Practice Site of a Practitioner;
                      </li>
                      <li>
                        access and use PharmaNet and PharmaNet Data only to support Direct Patient Care delivered by a Practitioner to
                        the individuals whose PharmaNet Data you are accessing, and only if the Practitioner is or will be delivering
                        Direct Patient Care requiring that access to those individuals at the same Approved Practice Site at which the
                        access occurs;
                      </li>
                      <li>
                        only access PharmaNet as permitted by law and directed by a Practitioner;
                      </li>
                      <li>
                        maintain all PharmaNet Data, whether accessed on PharmaNet or otherwise disclosed to you in any manner, in
                        strict confidence;
                      </li>
                      <li>
                        maintain the security of PharmaNet, and any applications, connections, or networks used to access PharmaNet;
                      </li>
                      <li>
                        complete all training required by the Approved Practice Site’s PharmaNet software vendor and the Province before
                        accessing PharmaNet;
                      </li>
                      <li>
                        notify the Province if you have any reason to suspect that PharmaNet, or any PharmaNet Data, is or has been
                        accessed or used inappropriately by any person.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p>
                      You must not:
                    </p>

                    <ol type=""a"">
                      <li>
                        disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and
                        directed by a Practitioner;
                      </li>
                      <li>
                        permit any person to use any user IDs, passwords or credentials provided to you to access PharmaNet;
                      </li>
                      <li>
                        reveal, share or compromise any user IDs, passwords or credentials for PharmaNet;
                      </li>
                      <li>
                        use, or attempt to use, the user IDs, passwords or credentials of any other person to access PharmaNet;
                      </li>
                      <li>
                        take any action that might compromise the integrity of PharmaNet, its information, or the provincial drug plan,
                        such as altering information or submitting false information;
                      </li>
                      <li>
                        test the security related to PharmaNet;
                      </li>
                      <li>
                        attempt to access PharmaNet from any location other than the Approved Practice Site of a Practitioner,
                        including by VPN or other remote access technology;
                      </li>
                      <li>
                        access PharmaNet unless the access is for the purpose of supporting a Practitioner in providing Direct
                        Patient Care to a patient at the same Approved Practice Site at which your access occurs.
                      </li>
                    </ol>
                  </li>
                </ol>
                <ol start=""6"">
                  <li>
                    Your access to PharmaNet and use of PharmaNet Data are governed by the <em>Pharmaceutical Services Act</em> and you
                    must comply with all your duties under that Act.
                  </li>
                  <li>
                    The Province may, in writing and from time to time, set further limits and conditions in respect of PharmaNet,
                    either for you or for the Practitioner(s), and that you must comply with any such further limits and conditions.
                  </li>
                </ol>

                <p class=""bold underline"">
                  How to Notify the Province
                </p>

                <ol start=""8"">
                  <li>

                    <p>
                      Notice to the Province may be sent in writing to:
                    </p>

                    <address>
                      Director, Information and PharmaNet Development<br>
                      Ministry of Health<br>
                      PO Box 9652, STN PROV GOVT<br>
                      Victoria, BC V8W 9P4<br>

                      <br>

                      <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                    </address>

                  </li>
                </ol>

                <p class=""bold underline"">
                  Province May Modify These Terms
                </p>

                <ol start=""9"">
                  <li>
                    <p>
                      The Province may amend these terms, including this section, at any time in its sole discretion:
                    </p>

                    <ol type=""i"">
                      <li>
                        by written notice to you, in which case the amendment will become effective upon the later of (A) the date
                        notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified by the
                        Province, if any; or
                      </li>
                      <li>
                        by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will specify
                        the effective date of the amendment, which date will be at least thirty (30) days after the date that the
                        PharmaCare Newsletter containing the notice is first published.
                      </li>
                    </ol>

                    <p>
                      If you do not agree with any amendment for which notice has been provided by the Province in accordance with (i)
                      or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
                      PharmaNet.
                    </p>

                    <p>
                      Any written notice to you under (i) above will be in writing and delivered by the Province to you using any of the
                      contact mechanisms identified by you in PRIME, including by mail to a specified postal address, email to a
                      specified email address or text message to a specified cell phone number. You may be required to click a URL link
                      or log into PRIME to receive the contents of any such notice.
                    </p>

                  </li>
                </ol>

                <p class=""bold underline"">
                  Governing Law
                </p>

                <ol start=""10"">
                  <li>

                    <p>
                      These terms will be governed by and will be construed and interpreted in accordance with the laws of British
                      Columbia and the laws of Canada applicable therein.
                    </p>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS: TEST OF INSERTION OF INDIVIDUAL L&C COPY</h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <ol>
                  <li>

                    <p class=""bold underline"">
                      BACKGROUND
                    </p>

                    <p>
                      The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links B.C.
                      pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered into
                      PharmaNet.
                    </p>

                    <p>
                      The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet is to
                      enhance patient care by providing timely and relevant information to persons involved in the provision of direct
                      patient care.
                    </p>

                    <p class=""bold underline"">
                      PharmaNet contains highly sensitive confidential information, including Personal Information and the proprietary
                      and confidential information of third-party licensors to the Province, and it is in the public interest to ensure
                      that appropriate measures are in place to protect the confidentiality of all such information. All access to and
                      use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INTERPRETATION
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will have the
                          meanings given below:
                        </p>

                        <ul class=""list-unstyled"">
                          <li>
                            <strong>“Act”</strong> means the <em>Pharmaceutical Services Act</em>.
                          </li>
                          <li>
                            <strong>“Approved Practice Site”</strong> means the physical site at which you provide Direct Patient Care
                            and which is approved by the Province for PharmaNet access.
                            <span class=""underline"">For greater certainty, “Approved Practice Site” does not include a location from which remote access to PharmaNet takes place;</span>
                          </li>
                          <li>
                            <strong>“Approved SSO”</strong> means a software support organization approved by the Province that provides
                            you with the information technology software and/or services through which you and On-Behalf-of Users access
                            PharmaNet.
                          </li>
                          <li>

                            <p>
                              <strong>“Conformance Standards”</strong> means the following documents published by the Province, as
                              amended from time to time:
                            </p>

                            <ol type=""i"">
                              <li>
                                PharmaNet Professional and Software Conformance Standards; and
                              </li>
                              <li>
                                Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                                Architecture for Wireless Local Area Network Connectivity”.
                              </li>
                            </ol>

                          </li>
                          <li>
                            <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                            services to an individual to whom you provide direct patient care in the context of your Practice.
                          </li>
                          <li>
                            <strong>“Information Management Regulation”</strong> means the Information Management Regulation, B.C. Reg.
                            74/2015.
                          </li>
                          <li>
                            <strong>“On-Behalf-of User”</strong> means a member of your staff who (i) requires access to PharmaNet to
                            carry out duties in relation to your Practice; (ii) is authorized by you to access PharmaNet on your behalf;
                            and (iii) has been granted access to PharmaNet by the Province.
                          </li>
                          <li>
                            <strong>“Personal Information”</strong> means all recorded information that is about an identifiable
                            individual or is defined as, or deemed to be, “personal information” or “personal health information”
                            pursuant to any Privacy Laws.
                          </li>
                          <li>
                            <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the
                            following website (or such other website as may be specified by the Province from time to time for this
                            purpose):

                            <br><br>

                            <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                          </li>
                          <li>
                            <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information Management
                            Regulation.
                          </li>
                          <li>
                            <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record
                            or information in the custody, control or possession of you or an On-Behalf-of User that was obtained
                            through your or an On-Behalf-of User’s access to PharmaNet.
                          </li>
                          <li>
                            <strong>“Practice”</strong> means your practice of the health profession regulated under the <em>Health
                            Professions Act</em>, or your practice as an enrolled device provider under the
                            <em>Provider Regulation</em>, B.C. Reg.222/2014, as identified by you through PRIME.
                          </li>
                          <li>
                            <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for,
                            and manage, their access to PharmaNet, and through which users are granted access by the Province.
                          </li>
                          <li>
                            <strong>“Privacy Laws”</strong> means the Act, the
                            <em>Freedom of Information and Protection of Privacy Act</em>,
                            the <em>Personal Information Protection Act</em>, and any other statutory or legal obligations of privacy
                            owed by you or the Province, whether arising under statute, by contract or at common law.
                          </li>
                          <li>
                            <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                            Minister of Health.
                          </li>
                          <li>
                            <strong>“Professional College”</strong> is the regulatory body governing your Practice.
                          </li>
                          <li>

                            <p>
                              <strong>“Unauthorized Person”</strong> means any person other than:
                            </p>

                            <ol type=""i"">
                              <li>
                                you,
                              </li>
                              <li>
                                an On-Behalf-of User, or
                              </li>
                              <li>
                                a representative of an Approved SSO that is accessing PharmaNet for technical support purposes in
                                accordance with section 6 of the <em>Information Management Regulation</em>.
                              </li>
                            </ol>

                          </li>
                        </ul>

                      </li>
                      <li>
                        <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or regulation by
                        name means the statute or regulation of British Columbia of that name, as amended or replaced from time to time,
                        and includes any enactment made under the authority of that statute or regulation.
                      </li>
                      <li>

                        <p>
                          <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this Agreement:
                        </p>

                        <ol type=""i"">
                          <li>
                            a provision in the body of this Agreement will prevail over any conflicting provision in any further
                            limits or conditions communicated to you in writing by the Province, unless the conflicting provision
                            expressly states otherwise; and
                          </li>
                          <li>
                            a provision referred to in (i) above will prevail over any conflicting provision in the Conformance
                            Standards.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      APPLICATION OF LEGISLATION
                    </p>

                    <p>
                      You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act and all
                      Privacy Laws applicable to PharmaNet and PharmaNet Data.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
                    </p>

                    <p>
                      You acknowledge that:
                    </p>

                    <ol type=""a"">
                      <li>
                        PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
                        Province under the authority of the Act;
                      </li>
                      <li>
                        specific provisions of the Act, including the <em>Information Management Regulation</em> and sections 24, 25 and
                        29 of the Act, apply directly to you and to On-Behalf-of Users as a result; and
                      </li>
                      <li>
                        this Agreement documents limits and conditions, set by the minister in writing, that the Act requires you to
                        comply with.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCESS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
                        compliance with the limits and conditions set out in section 5(b) below and otherwise in this Agreement. The
                        Province may from time to time, at its discretion, amend or change the scope of your access privileges to
                        PharmaNet as privacy, security, business and clinical practice requirements change. In such circumstances, the
                        Province will use reasonable efforts to notify you of such changes.
                      </li>
                      <li>

                        <p>
                          <strong>Limits and Conditions of Access.</strong> The following limits and conditions apply to your access to
                          PharmaNet:
                        </p>

                        <ol type=""i"">
                          <li>
                            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, for so
                            long as you are a registrant in good standing with the Professional College and your registration permits
                            you to deliver Direct Patient Care requiring access to PharmaNet;
                          </li>
                          <li>
                            <p>you will only access PharmaNet:</p>

                            <ul>
                              <li>
                                at the Approved Practice Site, and using only the technologies and applications approved by the
                                Province; or
                              </li>
                              <li>
                                using a VPN or similar remote access technology, if you are physically located in British Columbia and
                                remotely connected to the Approved Practice Site using a VPN or other remote access technology
                                specifically approved by the Province in writing for the Approved Practice Site.
                              </li>
                            </ul>
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access
                            takes place at the Approved Practice Site and the access is in relation to patients for whom you will be
                            providing Direct Patient Care at the Approved Practice Site requiring the access to PharmaNet;
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of Users do not access PharmaNet using VPN or other remote access
                            technology
                          </li>
                          <li>
                            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
                            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
                          </li>
                          <li>
                            you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of market
                            research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet Data, for the
                            purpose of market research;
                          </li>
                          <li>
                            subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure that
                            On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of Direct Patient
                            Care, including for the purposes of quality improvement, evaluation, health care planning, surveillance,
                            research or other secondary uses;
                          </li>
                          <li>
                            you will not permit any Unauthorized Person to access PharmaNet, and you will take all reasonable measures
                            to ensure that no Unauthorized Person can access PharmaNet;
                          </li>
                          <li>
                            you will complete any training program(s) that your Approved SSO makes available to you in relation to
                            PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
                          </li>
                          <li>
                            you will immediately notify the Province when you or an On-Behalf-of User no longer require access to
                            PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave of absence
                            from your staff, or where the On-Behalf-of User’s access-related duties in relation to the Practice have
                            changed;
                          </li>
                          <li>
                            you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional limits or
                            conditions applicable to you, as may be communicated to you by the Province in writing;
                          </li>
                          <li>
                            you represent and warrant that all information provided by you in connection with your application for
                            PharmaNet access, including through PRIME, is true and correct.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this Agreement
                        for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
                        PharmaNet Data.
                      </li>
                      <li>

                        <p>
                          <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable measures to
                          safeguard Personal Information, including any Personal Information in the PharmaNet Data while it is in the
                          custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
                        </p>

                        <ol type=""i"">
                          <li>
                            take all reasonable steps to ensure the physical security of Personal Information, generally and as
                            required by Privacy Laws;
                          </li>
                          <li>
                            secure all workstations and printers in a protected area in the Practice to prevent viewing of PharmaNet
                            Data by Unauthorized Persons;
                          </li>
                          <li>
                            ensure separate access credential (such as user name and password) for each On-Behalf-of User, and prohibit
                            sharing or other multiple use of your access credential, or an On-Behalf-of User’s access credential, for
                            access to PharmaNet;
                          </li>
                          <li>
                            secure any workstations used to access PharmaNet and all devices, codes or passwords that enable access to
                            PharmaNet by yourself or any On-Behalf-of User;
                          </li>
                          <li>
                            take such other privacy and security measures as the Province may reasonably require from time-to-time.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Conformance Standards.</strong> You will comply with, and will ensure On-Behalf-of
                        Users comply with, the rules specified in the Conformance Standards when accessing and recording information in
                        PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLOSURE, STORAGE, AND ACCESS REQUESTS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper files or
                        any electronic system, unless such storage or retention is required for record keeping in accordance with
                        Professional College requirements and in connection with your provision of Direct Patient Care and otherwise is
                        in compliance with the Conformance Standards. You will not modify any records retained in accordance with this
                        section other than as may be expressly authorized in the Conformance Standards. For clarity, you may annotate a
                        discrete record provided that the discrete record is not itself modified other than as expressly authorized in
                        the Conformance Standards.
                      </li>
                      <li>
                        <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with section
                        6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the purpose of
                        monitoring your own Practice.
                      </li>
                      <li>
                        <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do not,
                        disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient Care or is
                        otherwise authorized under section 24(1) of the Act.
                      </li>
                      <li>
                        <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of Users do
                        not, disclose PharmaNet Data for the purpose of market research.
                      </li>
                      <li>
                        <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in accordance
                        with section 6(a) of this Agreement, you will not provide to patients any copies of records containing PharmaNet
                        Data or “print outs” produced directly from PharmaNet, and will refer any requests for access to such records or
                        “print outs” to the Province.
                      </li>
                      <li>
                        <strong>Responding to Requests to Correct a Record contained in PharmaNet.</strong> If you receive a request for
                        correction of any record or information contained in PharmaNet, you will refer the request to the Province.
                      </li>
                      <li>
                        <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the Province if
                        you receive any order, demand or request compelling, or threatening to compel, disclosure of records contained
                        in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For greater
                        certainty, the foregoing requires that you notify the Province only with respect to any access requests or
                        demands for records contained in PharmaNet, and not records retained by you in accordance with section 6(a) of
                        this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCURACY
                    </p>

                    <p>
                      You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of User
                      in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material inaccuracy or
                      error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it if
                      necessary, and notify the Province of the inaccuracy or error and any steps taken.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INVESTIGATIONS, AUDITS, AND REPORTING
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations conducted by
                        the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
                        Agreement, including providing access upon request to your facilities, data management systems, books, records
                        and personnel for the purposes of such audit or investigation.
                      </li>
                      <li>
                        <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province may
                        report any material breach of this Agreement to your Professional College or to the Information and Privacy
                        Commissioner of British Columbia.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE OF NON COMPLIANCE AND DUTY TO INVESTIGATE
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this Agreement,
                        and will take all reasonable steps to prevent recurrences of any such breaches, including taking any steps
                        necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of User’s
                        access rights.
                      </li>
                      <li>

                        <p>
                          <strong>Non Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
                        </p>

                        <ol type=""i"">
                          <li>
                            you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User will be unable
                            to comply with the terms of this Agreement in any respect, or
                          </li>
                          <li>
                            you have knowledge of any circumstances, incidents or events which have or may jeopardize the security,
                            confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person, to access
                            PharmaNet.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      TERM OF AGREEMENT, SUSPENSION & TERMINATION
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet by the
                        Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or (e)
                        below.
                      </li>
                      <li>
                        <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written notice to
                        the Province.
                      </li>
                      <li>
                        <strong>Suspension or Termination of PharmaNet access.</strong> If the Province suspends or terminates your
                        right, or an On-Behalf-of User’s right, to access PharmaNet under the
                        <em>Information Management Regulation</em>, the Province may also terminate this Agreement at any time
                        thereafter upon written notice to you.
                      </li>
                      <li>
                        <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate this
                        Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to you if
                        you or an On-Behalf-of User fail to comply with any provision of this Agreement.
                      </li>
                      <li>
                        <strong>Termination by operation of the Information Management Regulation.</strong> This Agreement will
                        terminate automatically if your access to PharmaNet ends by operation of section 18 of the
                        <em>Information Management Regulation</em>.
                      </li>
                      <li>
                        <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may suspend your
                        account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the Province’s
                        policies. Please contact the Province immediately if your account has been suspended for inactivity but you
                        still require access to PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and PharmaNet
                        Data is solely at your own risk. All such access and information is provided on an “as is” and “as available”
                        basis without warranty or condition of any kind. The Province does not warrant the accuracy, completeness or
                        reliability of the PharmaNet Data or the availability of PharmaNet, or that access to or the operation of
                        PharmaNet will function without error, failure or interruption.
                      </li>
                      <li>
                        <strong>You are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
                        you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or acting
                        upon such information. The clinical or other information disclosed to you or an On-Behalf-of User pursuant to
                        this Agreement is in no way intended to be a substitute for professional judgment.
                      </li>
                      <li>
                        <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the Province
                        for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or PharmaNet
                        Data.
                      </li>
                      <li>
                        <strong>You Must Indemnify the Province if You Cause a Loss or Claim.</strong> You agree to indemnify and save
                        harmless the Province, and the Province’s employees and agents (each an <strong>""Indemnified Person""</strong>)
                        from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified Person may
                        sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are based
                        upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
                        On-Behalf-of User, in connection with this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another method of
                          delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to be
                          effective, must be in writing and emailed or mailed to:
                        </p>

                        <address>
                          Director, Information and PharmaNet Development<br>
                          Ministry of Health<br>
                          PO Box 9652, STN PROV GOVT<br>
                          Victoria, BC V8W 9P4<br>

                          <br>

                          <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                        </address>

                      </li>
                      <li>
                        <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will be in
                        writing and delivered by the Province to you using any of the contact mechanisms identified by you in PRIME,
                        including by mail to a specified postal address, email to a specified email address or text message to the
                        specified cell phone number. You may be required to click a URL link or log into PRIME to receive the content of
                        any such notice.
                      </li>
                      <li>
                        <strong>Deemed receipt.</strong> Any written communication from a party, if personally delivered or sent
                        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent by
                        mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays) after
                        the date the notice was sent.
                      </li>
                      <li>
                        <strong>Substitute contact information.</strong> You may notify the Province of a substitute contact mechanism
                        by updating your contact information in PRIME.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      GENERAL
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and is
                          severable from any other covenant, and if any of them are held by a court, or other decision-maker, to be
                          invalid, this Agreement will be interpreted as if such provisions were not included.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Survival.</strong> Sections 3, 4, 5(b)(vi) (vii), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any other
                          provision of this Agreement that expressly or by its nature continues after termination, shall survive
                          termination of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and interpreted in
                          accordance with the laws of British Columbia and the laws of Canada applicable therein.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be assigned
                          without the prior written approval of the Province.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any provision of
                          this Agreement by you is not a waiver of its right subsequently to insist on performance of that or any other
                          provision of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Province may modify this Agreement.</strong> The Province may amend this Agreement, including this
                          section, at any time in its sole discretion:
                        </p>

                        <ol type=""i"">
                          <li>
                            by written notice to you, in which case the amendment will become effective upon the later of (A) the
                            date notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
                            by the Province, if any; or
                          </li>
                          <li>
                            by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                            specify the effective date of the amendment, which date will be at least 30 (thirty) days after the date
                            that the PharmaCare Newsletter containing the notice is first published.
                          </li>
                        </ol>

                        <p>
                          If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment described in
                          (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this Agreement will be
                          deemed to have been so amended as of the effective date. If you do not agree with any amendment for which
                          notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly (and in any
                          event before the effective date) cease all access or use of PharmaNet by yourself and all On-Behalf-of Users,
                          and take the steps necessary to terminate this Agreement in accordance with section 10.
                        </p>

                      </li>
                    </ol>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<h1>PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER</h1>

                <p class=""bold"">
                    By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <ol>
                    <li>

                        <p class=""bold underline"">
                            On Behalf of User Access
                        </p>

                        <p class=""bold"">
                            You represent and warrant to the Province that:
                        </p>

                        <ol type=""a"">
                            <li>
                                your employment duties in relation to a Practitioner require you to access PharmaNet (and PharmaNet
                                Data) to
                                support the Practitioner’s delivery of Direct Patient Care;
                            </li>
                            <li>
                                you are directly supervised by a Practitioner who has been granted access to PharmaNet by the Province;
                                and
                            </li>
                            <li>
                                all information provided by you in connection with your application for PharmaNet access, including all
                                information submitted through PRIME, is true and correct.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            Definitions
                        </p>

                        <p class=""bold"">
                            In these terms, capitalized terms will have the following meanings:
                        </p>

                        <ul class=""list-unstyled"">
                            <li>
                                <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of
                                health
                                services to an individual to whom a Practitioner provides direct patient care in the context of their
                                Practice.
                            </li>
                            <li>
                                <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on
                                the
                                following website (or such other website as may be specified by the Province from time to time for this
                                purpose):

                                <br><br>

                                <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                            </li>
                            <li>
                                <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information Management
                                Regulation.
                            </li>
                            <li>
                                <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any
                                record or
                                information in the custody, control or possession of you or a Practitioner that was obtained through
                                access to
                                PharmaNet by anyone.
                            </li>
                            <li>
                                <strong>“Practice”</strong> means a Practitioner’s practice of their health profession.
                            </li>
                            <li>
                                <strong>“Practitioner”</strong> means a health professional regulated under the Health Professions Act
                                who
                                supervises your access and use of PharmaNet and who has been granted access to PharmaNet by the
                                Province.
                            </li>
                            <li>
                                <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply
                                for, and
                                manage, their access to PharmaNet, and through which users are granted access by the Province.
                            </li>
                            <li>
                                <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by
                                the
                                Minister of Health.
                            </li>
                        </ul>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            Terms of Access to PharmaNet
                        </p>

                        <p class=""bold"">
                            You must:
                        </p>

                        <ol type=""a"">
                            <li>
                                access and use PharmaNet and PharmaNet Data only to support Direct Patient Care delivered by the
                                Practitioner to
                                the individuals whose PharmaNet Data you are accessing;
                            </li>
                            <li>
                                only access PharmaNet as permitted by law and directed by the Practitioner;
                            </li>
                            <li>
                                maintain all PharmaNet Data, whether accessed on PharmaNet or otherwise disclosed to you in any manner,
                                in
                                strict confidence;
                            </li>
                            <li>
                                maintain the security of PharmaNet, and any applications, connections, or networks used to access
                                PharmaNet;
                            </li>
                            <li>
                                complete all training required by the Practice’s PharmaNet software vendor and the Province before
                                accessing
                                PharmaNet;
                            </li>
                            <li>
                                notify the Province if you have any reason to suspect that PharmaNet, or any PharmaNet Data, is or has
                                been
                                accessed or used inappropriately by any person.
                            </li>
                        </ol>

                        <p class=""bold"">
                            You must not:
                        </p>

                        <ol type=""a""
                            start=""7"">
                            <li>
                                disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and
                                directed
                                by the Practitioner;
                            </li>
                            <li>
                                permit any person to use any user IDs, passwords or credentials provided to you to access PharmaNet;
                            </li>
                            <li>
                                reveal, share or compromise any user IDs, passwords or credentials for PharmaNet;
                            </li>
                            <li>
                                use, or attempt to use, the user IDs, passwords or credentials of any other person to access PharmaNet;
                            </li>
                            <li>
                                take any action that might compromise the integrity of PharmaNet, its information, or the provincial
                                drug plan,
                                such as altering information or submitting false information;
                            </li>
                            <li>
                                test the security related to PharmaNet;
                            </li>
                            <li>
                                attempt to access PharmaNet from any location other than the approved Practice site of the
                                Practitioner, including by VPN or other remote access technology,
                                unless that VPN or remote access technology has first been approved by the Province in writing for use
                                at the Practice.
                                You must be physically located in BC whenever you use approved VPN or other approved remote access
                                technology to access PharmaNet.
                            </li>
                        </ol>

                        <p>
                            Your access to PharmaNet and use of PharmaNet Data are governed by the Pharmaceutical Services Act and you
                            must
                            comply with all your duties under that Act.
                        </p>

                        <p>
                            The Province may, in writing and from time to time, set further limits and conditions in respect of
                            PharmaNet,
                            either for you or for the Practitioner(s), and that you must comply with any such further limits and
                            conditions.
                        </p>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            How to Notify the Province
                        </p>

                        <p>
                            Notice to the Province may be sent in writing to:
                        </p>

                        <address>
                            Director, Information and PharmaNet Development<br>
                      Ministry of Health<br>
                      PO Box 9652, STN PROV GOVT<br>
                      Victoria, BC V8W 9P4<br>

                            <br>

                            <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                        </address>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            Province may modify these terms
                        </p>

                        <p>
                            The Province may amend these terms, including this section, at any time in its sole discretion:
                        </p>

                        <ol type=""i"">
                            <li>
                                by written notice to you, in which case the amendment will become effective upon the later of (A) the
                                date
                                notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
                                by the
                                Province, if any; or
                            </li>
                            <li>
                                by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                                specify
                                the effective date of the amendment, which date will be at least thirty (30) days after the date that
                                the
                                PharmaCare Newsletter containing the notice is first published.
                            </li>
                        </ol>

                        <p>
                            If you do not agree with any amendment for which notice has been provided by the Province in accordance with
                            (i)
                            or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
                            PharmaNet.
                        </p>

                        <p>
                            Any written notice to you under (i) above will be in writing and delivered by the Province to you using any
                            of the
                            contact mechanisms identified by you in PRIME, including by mail to a specified postal address, email to a
                            specified email address or text message to a specified cell phone number. You may be required to click a URL
                            link
                            or log into PRIME to receive the contents of any such notice.
                        </p>

                    </li>
                    {$lcPlaceholder}
                    <li>

                        <p class=""bold underline"">
                            Governing Law
                        </p>

                        <p>
                            These terms will be governed by and will be construed and interpreted in accordance with the laws of British
                            Columbia and the laws of Canada applicable therein.
                        </p>

                        <p>
                            Unless otherwise specified, a reference to a statute or regulation by name means the statute or regulation
                            of
                            British Columbia of that name, as amended or replaced from time to time, and includes any enactment made
                            under the
                            authority of that statute or regulation.
                        </p>

                    </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -8, 0, 0, 0)), @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

                <p class=""bold"">
                    By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <ol>
                    <li>

                        <p class=""bold underline"">
                            BACKGROUND
                        </p>

                        <p>
                            The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links
                            B.C.
                            pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered
                            into
                            PharmaNet.
                        </p>

                        <p>
                            The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet
                            is to
                            enhance patient care by providing timely and relevant information to persons involved in the provision of
                            direct
                            patient care.
                        </p>

                        <p class=""bold underline"">
                            PharmaNet contains highly sensitive confidential information, including Personal Information and the
                            proprietary
                            and confidential information of third-party licensors to the Province, and it is in the public interest to
                            ensure that appropriate measures are in place to protect the confidentiality of all such information. All
                            access
                            to and use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
                        </p>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            INTERPRETATION
                        </p>

                        <ol type=""a"">
                            <li>

                                <p>
                                    <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will
                                    have the
                                    meanings given below:
                                </p>

                                <ul class=""list-unstyled"">
                                    <li>
                                        <strong>“Act”</strong> means the Pharmaceutical Services Act.
                                    </li>
                                    <li>
                                        <strong>“Approved SSO”</strong> means a software support organization approved by the Province
                                        that provides
                                        you with the information technology software and/or services through which you and On-Behalf-of
                                        Users access
                                        PharmaNet.
                                    </li>
                                    <li>

                                        <p>
                                            <strong>“Conformance Standards”</strong> means the following documents published by the
                                            Province, as
                                            amended
                                            from time to time:
                                        </p>

                                        <ol type=""i"">
                                            <li>
                                                PharmaNet Professional and Software Conformance Standards; and
                                            </li>
                                            <li>
                                                Office of the Chief Information Officer: “Submission for Technical Security Standard and
                                                High Level
                                                Architecture for Wireless Local Area Network Connectivity”.
                                            </li>
                                        </ol>

                                    </li>
                                    <li>
                                        <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision
                                        of health
                                        services to an individual to whom you provide direct patient care in the context of your
                                        Practice.
                                    </li>
                                    <li>
                                        <strong>“Information Management Regulation”</strong> means the Information Management
                                        Regulation, B.C. Reg.
                                        74/2015.
                                    </li>
                                    <li>
                                        <strong>“On-Behalf-of User”</strong> means a member of your staff who (i) requires access to
                                        PharmaNet to
                                        carry out duties in relation to your Practice; and (ii) is authorized by you to access PharmaNet
                                        on your
                                        behalf; and (iii) has been granted access to PharmaNet by the Province.
                                    </li>
                                    <li>
                                        <strong>“Personal Information”</strong> means all recorded information that is about an
                                        identifiable
                                        individual or is defined as, or deemed to be, “personal information” or “personal health
                                        information”
                                        pursuant to any Privacy Laws.
                                    </li>
                                    <li>
                                        <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the
                                        Province on the
                                        following website (or such other website as may be specified by the Province from time to time
                                        for this
                                        purpose):

                                        <br><br>

                                        <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                                    </li>
                                    <li>
                                        <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information
                                        Management
                                        Regulation.
                                    </li>
                                    <li>
                                        <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and
                                        any record
                                        or information in the custody, control or possession of you or a On-Behalf-of User that was
                                        obtained through
                                        your or a On-Behalf-of User’s access to PharmaNet.
                                    </li>
                                    <li>
                                        <strong>“Practice”</strong> means your practice of the health profession regulated under the
                                        Health
                                        Professions Act and identified by you through PRIME.
                                    </li>
                                    <li>
                                        <strong>“PRIME”</strong> means the online service provided by the Province that allows users to
                                        apply for,
                                        and manage, their access to PharmaNet, and through which users are granted access by the
                                        Province.
                                    </li>
                                    <li>
                                        <strong>“Privacy Laws”</strong> means the Act, the Freedom of Information and Protection of
                                        Privacy Act, the
                                        Personal Information Protection Act, and any other statutory or legal obligations of privacy
                                        owed by you or
                                        the Province, whether arising under statute, by contract or at common law.
                                    </li>
                                    <li>
                                        <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as
                                        represented by the
                                        Minister of Health.
                                    </li>
                                    <li>
                                        <strong>“Professional College”</strong> is the regulatory body governing your Practice.
                                    </li>
                                    <li>

                                        <p>
                                            <strong>“Unauthorized Person”</strong> means any person other than:
                                        </p>

                                        <ol type=""i"">
                                            <li>
                                                you,
                                            </li>
                                            <li>
                                                an On-Behalf-of User, or
                                            </li>
                                            <li>
                                                a representative of an Approved SSO that is accessing PharmaNet for technical support
                                                purposes in
                                                accordance with section 6 of the Information Management Regulation.
                                            </li>
                                        </ol>

                                    </li>
                                </ul>

                            </li>
                            <li>
                                <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or
                                regulation by
                                name means the statute or regulation of British Columbia of that name, as amended or replaced from time
                                to time,
                                and includes any enactment made under the authority of that statute or regulation.
                            </li>
                            <li>

                                <p>
                                    <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this
                                    Agreement:
                                </p>

                                <ol type=""i"">
                                    <li>
                                        a provision in the body of this Agreement will prevail over any conflicting provision in any
                                        further limits
                                        or conditions communicated to you in writing by the Province, unless the conflicting provision
                                        expressly
                                        states otherwise; and
                                    </li>
                                    <li>
                                        a provision referred to in (i) above will prevail over any conflicting provision in the
                                        Conformance
                                        Standards.
                                    </li>
                                </ol>

                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            APPLICATION OF LEGISLATION
                        </p>

                        <p>
                            You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act and
                            all
                            Privacy Laws applicable to PharmaNet and PharmaNet Data.
                        </p>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
                        </p>

                        <p>
                            You acknowledge that:
                        </p>

                        <ol type=""a"">
                            <li>
                                PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by
                                the
                                Province under the authority of the Act;
                            </li>
                            <li>
                                specific provisions of the Act, including the Information Management Regulation and sections 24, 25 and
                                29 of
                                the Act, apply directly to you and to On-Behalf-of Users as a result; and
                            </li>
                            <li>
                                this Agreement documents limits and conditions, set by the minister in writing, that the Act requires
                                you to
                                comply with.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            ACCESS
                        </p>

                        <ol type=""a"">
                            <li>
                                <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
                                compliance with the limits and conditions set out in section 5(b) below and otherwise in this Agreement.
                                The
                                Province may from time to time, at its discretion, amend or change the scope of your access privileges
                                to
                                PharmaNet as privacy, security, business and clinical practice requirements change. In such
                                circumstances, the
                                Province will use reasonable efforts to notify you of such changes.
                            </li>
                            <li>

                                <p>
                                    <strong>Limits and Conditions of Access.</strong> The following limits and conditions apply to your
                                    access to
                                    PharmaNet:
                                </p>

                                <ol type=""i"">
                                    <li>
                                        you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access
                                        PharmaNet, for so
                                        long as you are a registrant in good standing with the Professional College and your licence
                                        permits you to
                                        deliver Direct Patient Care requiring access to PharmaNet;
                                    </li>
                                    <li>
                                        you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access
                                        PharmaNet, at a location approved by the Province, and using only the technologies and
                                        applications approved by the Province. For greater certainty, you must not access PharmaNet
                                        using a VPN or similar remote access technology to an approved location, unless that VPN or
                                        remote access technology has first been approved by the Province in writing for use at the
                                        Practice. You, or your On-Behalf-of Users, must be physically located in BC whenever you use VPN
                                        or similar remote access technology to access PharmaNet.
                                    </li>
                                    <li>
                                        you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you
                                        will ensure
                                        that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct
                                        Patient Care;
                                    </li>
                                    <li>
                                        you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of
                                        market
                                        research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet
                                        Data, for the
                                        purpose of market research;
                                    </li>
                                    <li>
                                        subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure
                                        that
                                        On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of
                                        Direct Patient
                                        Care, including for the purposes of quality improvement, evaluation, health care planning,
                                        surveillance,
                                        research or other secondary uses;
                                    </li>
                                    <li>
                                        you will not permit any Unauthorized Person to access PharmaNet, and you will take all
                                        reasonable measures
                                        to ensure that no Unauthorized Person can access PharmaNet;
                                    </li>
                                    <li>
                                        you will complete any training program(s) that your Approved SSO makes available to you in
                                        relation to
                                        PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
                                    </li>
                                    <li>
                                        you will immediately notify the Province when you or an On-Behalf-of User no longer require
                                        access to
                                        PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave
                                        of absence
                                        from your staff, or where the On-Behalf-of User’s access-related duties in relation to the
                                        Practice have
                                        changed;
                                    </li>
                                    <li>
                                        you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional
                                        limits or
                                        conditions applicable to you, as may be communicated to you by the Province in writing;
                                    </li>
                                    <li>
                                        you represent and warrant that all information provided by you in connection with your
                                        application for
                                        PharmaNet access, including through PRIME, is true and correct.
                                    </li>
                                </ol>

                            </li>
                            <li>
                                <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this
                                Agreement
                                for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
                                PharmaNet Data.
                            </li>
                            <li>

                                <p>
                                    <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable
                                    measures to
                                    safeguard Personal Information, including any Personal Information in the PharmaNet Data while it is
                                    in the
                                    custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
                                </p>

                                <ol type=""i"">
                                    <li>
                                        take all reasonable steps to ensure the physical security of Personal Information, generally and
                                        as required
                                        by Privacy Laws;
                                    </li>
                                    <li>
                                        secure all workstations and printers in a protected area in the Practice to prevent viewing of
                                        PharmaNet
                                        Data by Unauthorized Persons;
                                    </li>
                                    <li>
                                        ensure separate access credential (such as user name and password) for each On-Behalf-of User,
                                        and prohibit
                                        sharing or other multiple use of your access credential, or an On-Behalf-of User’s access
                                        credential, for
                                        access to PharmaNet;
                                    </li>
                                    <li>
                                        secure any workstations used to access PharmaNet and all devices, codes or passwords that enable
                                        access to
                                        PharmaNet by yourself or any On-Behalf-of User;
                                    </li>
                                    <li>
                                        take such other privacy and security measures as the Province may reasonably require from
                                        time-to-time.
                                    </li>
                                </ol>

                            </li>
                            <li>
                                <strong>Conformance Standards - Business Rules.</strong> You will comply with, and will ensure
                                On-Behalf-of
                                Users comply with, the business rules specified in the Conformance Standards when accessing and
                                recording
                                information in PharmaNet.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            DISCLOSURE, STORAGE, AND ACCESS REQUESTS
                        </p>

                        <ol type=""a"">
                            <li>
                                <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper
                                files or
                                any electronic system, unless such storage or retention is required for record keeping in accordance
                                with
                                Professional College requirements and in connection with your provision of Direct Patient Care and
                                otherwise is
                                in compliance with the Conformance Standards. You will not modify any records retained in accordance
                                with this
                                section other than as may be expressly authorized in the Conformance Standards. For clarity, you may
                                annotate a
                                discrete record provided that the discrete record is not itself modified other than as expressly
                                authorized in
                                the Conformance Standards.
                            </li>
                            <li>
                                <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with
                                section
                                6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the
                                purpose of
                                monitoring your own Practice.
                            </li>
                            <li>
                                <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do
                                not,
                                disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient
                                Care or is
                                otherwise authorized under section 24(1) of the Act.
                            </li>
                            <li>
                                <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of
                                Users do
                                not, disclose PharmaNet Data for the purpose of market research.
                            </li>
                            <li>
                                <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in
                                accordance
                                with section 6(a) of this Agreement, you will not provide to patients any copies of records containing
                                PharmaNet
                                Data or “print outs” produced directly from PharmaNet, and will refer any requests for access to such
                                records or
                                “print outs” to the Province.
                            </li>
                            <li>
                                <strong>Responding to Requests to Correct a Record contained in PharmaNet.</strong> If you receive a
                                request for
                                correction of any record or information contained in PharmaNet, you will refer the request to the
                                Province.
                            </li>
                            <li>
                                <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the
                                Province if
                                you receive any order, demand or request compelling, or threatening to compel, disclosure of records
                                contained
                                in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For
                                greater
                                certainty, the foregoing requires that you notify the Province only with respect to any access requests
                                or
                                demands for records contained in PharmaNet, and not records retained by you in accordance with section
                                6(a) of
                                this Agreement.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            ACCURACY
                        </p>

                        <p>
                            You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of
                            User
                            in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material
                            inaccuracy or
                            error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it
                            if
                            necessary, and notify the Province of the inaccuracy or error and any steps taken.
                        </p>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            INVESTIGATIONS, AUDITS, AND REPORTING
                        </p>

                        <ol type=""a"">
                            <li>
                                <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations
                                conducted by
                                the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
                                Agreement, including providing access upon request to your facilities, data management systems, books,
                                records
                                and personnel for the purposes of such audit or investigation.
                            </li>
                            <li>
                                <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province
                                may
                                report any material breach of this Agreement to your Professional College or to the Information and
                                Privacy
                                Commissioner of British Columbia.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            NOTICE OF NON COMPLIANCE AND DUTY TO INVESTIGATE
                        </p>

                        <ol type=""a"">
                            <li>
                                <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this
                                Agreement,
                                and will take all reasonable steps to prevent recurrences of any such breaches, including taking any
                                steps
                                necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of
                                User’s
                                access rights.
                            </li>
                            <li>

                                <p>
                                    <strong>Non Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
                                </p>

                                <ol type=""i"">
                                    <li>
                                        you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User
                                        will be unable
                                        to comply with the terms of this Agreement in any respect, or
                                    </li>
                                    <li>
                                        you have knowledge of any circumstances, incidents or events which have or may jeopardize the
                                        security,
                                        confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person,
                                        to access
                                        PharmaNet.
                                    </li>
                                </ol>

                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            TERM OF AGREEMENT, SUSPENSION & TERMINATION
                        </p>

                        <ol type=""a"">
                            <li>
                                <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet
                                by the
                                Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or
                                (e)
                                below.
                            </li>
                            <li>
                                <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written
                                notice to
                                the Province.
                            </li>
                            <li>
                                <strong>Suspension or Termination of PharmaNet access.</strong> If the Province suspends or terminates
                                your
                                right, or an On-Behalf-of User’s right, to access PharmaNet under the Information Management Regulation,
                                the
                                Province may also terminate this Agreement at any time thereafter upon written notice to you.
                            </li>
                            <li>
                                <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate
                                this
                                Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to
                                you if
                                you or an On-Behalf-of User fail to comply with any provision of this Agreement.
                            </li>
                            <li>
                                <strong>Termination by operation of the Information Management Regulation.</strong> This Agreement will
                                terminate automatically if your access to PharmaNet ends by operation of section 18 of the Information
                                Management Regulation.
                            </li>
                            <li>
                                <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may
                                suspend your
                                account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the
                                Province’s
                                policies. Please contact the Province immediately if your account has been suspended for inactivity but
                                you
                                still require access to PharmaNet.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
                        </p>

                        <ol type=""a"">
                            <li>
                                <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and
                                PharmaNet
                                Data is solely at your own risk. All such access and information is provided on an “as is” and “as
                                available”
                                basis without warranty or condition of any kind. The Province does not warrant the accuracy,
                                completeness or
                                reliability of the PharmaNet Data or the availability of PharmaNet, or that access to or the operation
                                of
                                PharmaNet will function without error, failure or interruption.
                            </li>
                            <li>
                                <strong>You are Responsible.</strong> You are responsible for verifying the accuracy of information
                                disclosed to
                                you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or
                                acting
                                upon such information. The clinical or other information disclosed to you or an On-Behalf-of User
                                pursuant to
                                this Agreement is in no way intended to be a substitute for professional judgment.
                            </li>
                            <li>
                                <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the
                                Province
                                for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or
                                PharmaNet
                                Data.
                            </li>
                            <li>
                                <strong>You Must Indemnify the Province if You Cause a Loss or Claim.</strong> You agree to indemnify
                                and save
                                harmless the Province, and the Province’s employees and agents (each an
                                <strong>""Indemnified Person""</strong>)
                                from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified
                                Person may
                                sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are
                                based
                                upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
                                On-Behalf-of User, in connection with this Agreement.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            NOTICE
                        </p>

                        <ol type=""a"">
                            <li>

                                <p>
                                    <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another
                                    method of
                                    delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to
                                    be
                                    effective,
                                    must be in writing and emailed or mailed to:
                                </p>

                                <address>
                                    Director, Information and PharmaNet Development<br>
                          Ministry of Health<br>
                          PO Box 9652, STN PROV GOVT<br>
                          Victoria, BC V8W 9P4<br>

                                    <br>

                                    <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                                </address>

                            </li>
                            <li>
                                <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will
                                be in
                                writing and delivered by the Province to you using any of the contact mechanisms identified by you in
                                PRIME,
                                including by mail to a specified postal address, email to a specified email address or text message to
                                the
                                specified cell phone number. You may be required to click a URL link or log into PRIME to receive the
                                content
                                of any such notice.
                            </li>
                            <li>
                                <strong>Deemed receipt.</strong> Any written communication from a party, if personally delivered or sent
                                electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if
                                sent
                                by mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory
                                holidays)
                                after the date the notice was sent.
                            </li>
                            <li>
                                <strong>Substitute contact information.</strong> You may notify the Province of a substitute contact
                                mechanism
                                by updating your contact information in PRIME.
                            </li>
                        </ol>

                    </li>
                    {$lcPlaceholder}
                    <li>

                        <p class=""bold underline"">
                            GENERAL
                        </p>

                        <ol type=""a"">
                            <li>

                                <p>
                                    <strong>Entire Agreement.</strong> This Agreement constitutes the entire agreement between the
                                    parties with
                                    respect to the subject matter of this agreement.
                                </p>

                            </li>
                            <li>

                                <p>
                                    <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and
                                    is
                                    severable from any other covenant, and if any of them are held by a court, or other decision-maker,
                                    to be
                                    invalid, this Agreement will be interpreted as if such provisions were not included.
                                </p>

                            </li>
                            <li>

                                <p>
                                    <strong>Survival.</strong> Sections 3, 4, 5(b)(iv) (v), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any
                                    other
                                    provision of this Agreement that expressly or by its nature continues after termination, shall
                                    survive
                                    termination of this Agreement.
                                </p>

                            </li>
                            <li>

                                <p>
                                    <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and
                                    interpreted in
                                    accordance with the laws of British Columbia and the laws of Canada applicable therein.
                                </p>

                            </li>
                            <li>

                                <p>
                                    <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be
                                    assigned
                                    without the prior written approval of the Province.
                                </p>

                            </li>
                            <li>

                                <p>
                                    <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any
                                    provision of
                                    this Agreement by you is not a waiver of its right subsequently to insist on performance of that or
                                    any other
                                    provision of this Agreement.
                                </p>

                            </li>
                            <li>

                                <p>
                                    <strong>Province may modify this Agreement.</strong> The Province may amend this Agreement,
                                    including this
                                    section, at any time in its sole discretion:
                                </p>

                                <ol type=""i"">
                                    <li>
                                        by written notice to you, in which case the amendment will become effective upon the later of
                                        (A) the date
                                        notice of the amendment is first delivered to you, or (B) the effective date of the amendment
                                        specified by
                                        the Province, if any; or
                                    </li>
                                    <li>
                                        by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the
                                        notice will
                                        specify the effective date of the amendment, which date will be at least 30 (thirty) days after
                                        the date
                                        that the PharmaCare Newsletter containing the notice is first published.
                                    </li>
                                </ol>

                                <p>
                                    If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment
                                    described in
                                    (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this
                                    Agreement will be
                                    deemed to have been so amended as of the effective date. If you do not agree with any amendment for
                                    which
                                    notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly
                                    (and in any
                                    event before the effective date) cease all access or use of PharmaNet by yourself and all
                                    On-Behalf-of Users,
                                    and take the steps necessary to terminate this Agreement in accordance with section 10.
                                </p>

                            </li>
                        </ol>

                    </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -8, 0, 0, 0)), @"<h1>PHARMANET TERMS OF ACCESS FOR ON-BEHALF-OF USER</h1>

                <p class=""bold"">
                    By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <ol>
                    <li>

                        <p class=""bold underline"">
                            On Behalf of User Access
                        </p>

                        <p class=""bold"">
                            You represent and warrant to the Province that:
                        </p>

                        <ol type=""a"">
                            <li>
                                your employment duties in relation to a Practitioner require you to access PharmaNet (and PharmaNet
                                Data) to
                                support the Practitioner’s delivery of Direct Patient Care;
                            </li>
                            <li>
                                you are directly supervised by a Practitioner who has been granted access to PharmaNet by the Province;
                                and
                            </li>
                            <li>
                                all information provided by you in connection with your application for PharmaNet access, including all
                                information submitted through PRIME, is true and correct.
                            </li>
                        </ol>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            Definitions
                        </p>

                        <p class=""bold"">
                            In these terms, capitalized terms will have the following meanings:
                        </p>

                        <ul class=""list-unstyled"">
                            <li>
                                <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of
                                health
                                services to an individual to whom a Practitioner provides direct patient care in the context of their
                                Practice.
                            </li>
                            <li>
                                <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on
                                the
                                following website (or such other website as may be specified by the Province from time to time for this
                                purpose):

                                <br><br>

                                <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                            </li>
                            <li>
                                <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information Management
                                Regulation.
                            </li>
                            <li>
                                <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any
                                record or
                                information in the custody, control or possession of you or a Practitioner that was obtained through
                                access to
                                PharmaNet by anyone.
                            </li>
                            <li>
                                <strong>“Practice”</strong> means a Practitioner’s practice of their health profession.
                            </li>
                            <li>
                                <strong>“Practitioner”</strong> means a health professional regulated under the Health Professions Act
                                who
                                supervises your access and use of PharmaNet and who has been granted access to PharmaNet by the
                                Province.
                            </li>
                            <li>
                                <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply
                                for, and
                                manage, their access to PharmaNet, and through which users are granted access by the Province.
                            </li>
                            <li>
                                <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by
                                the
                                Minister of Health.
                            </li>
                        </ul>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            Terms of Access to PharmaNet
                        </p>

                        <p class=""bold"">
                            You must:
                        </p>

                        <ol type=""a"">
                            <li>
                                access and use PharmaNet and PharmaNet Data only to support Direct Patient Care delivered by the
                                Practitioner to
                                the individuals whose PharmaNet Data you are accessing;
                            </li>
                            <li>
                                only access PharmaNet as permitted by law and directed by the Practitioner;
                            </li>
                            <li>
                                maintain all PharmaNet Data, whether accessed on PharmaNet or otherwise disclosed to you in any manner,
                                in
                                strict confidence;
                            </li>
                            <li>
                                maintain the security of PharmaNet, and any applications, connections, or networks used to access
                                PharmaNet;
                            </li>
                            <li>
                                complete all training required by the Practice’s PharmaNet software vendor and the Province before
                                accessing
                                PharmaNet;
                            </li>
                            <li>
                                notify the Province if you have any reason to suspect that PharmaNet, or any PharmaNet Data, is or has
                                been
                                accessed or used inappropriately by any person.
                            </li>
                        </ol>

                        <p class=""bold"">
                            You must not:
                        </p>

                        <ol type=""a""
                            start=""7"">
                            <li>
                                disclose PharmaNet Data for any purpose other than Direct Patient Care, except as permitted by law and
                                directed
                                by the Practitioner;
                            </li>
                            <li>
                                permit any person to use any user IDs, passwords or credentials provided to you to access PharmaNet;
                            </li>
                            <li>
                                reveal, share or compromise any user IDs, passwords or credentials for PharmaNet;
                            </li>
                            <li>
                                use, or attempt to use, the user IDs, passwords or credentials of any other person to access PharmaNet;
                            </li>
                            <li>
                                take any action that might compromise the integrity of PharmaNet, its information, or the provincial
                                drug plan,
                                such as altering information or submitting false information;
                            </li>
                            <li>
                                test the security related to PharmaNet;
                            </li>
                            <li>
                                you must not attempt to access PharmaNet from any location other than the approved Practice site of the
                                Practitioner, including by VPN or other remote access technology,
                                unless that VPN or remote access technology has first been approved by the Province in writing for use
                                at the Practice.
                                You must be physically located in BC whenever you use approved VPN or other approved remote access
                                technology to access PharmaNet.
                            </li>
                        </ol>

                        <p>
                            Your access to PharmaNet and use of PharmaNet Data are governed by the Pharmaceutical Services Act and you
                            must
                            comply with all your duties under that Act.
                        </p>

                        <p>
                            The Province may, in writing and from time to time, set further limits and conditions in respect of
                            PharmaNet,
                            either for you or for the Practitioner(s), and that you must comply with any such further limits and
                            conditions.
                        </p>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            How to Notify the Province
                        </p>

                        <p>
                            Notice to the Province may be sent in writing to:
                        </p>

                        <address>
                            Director, Information and PharmaNet Development<br>
                      Ministry of Health<br>
                      PO Box 9652, STN PROV GOVT<br>
                      Victoria, BC V8W 9P4<br>

                            <br>

                            <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                        </address>

                    </li>
                    <li>

                        <p class=""bold underline"">
                            Province may modify these terms
                        </p>

                        <p>
                            The Province may amend these terms, including this section, at any time in its sole discretion:
                        </p>

                        <ol type=""i"">
                            <li>
                                by written notice to you, in which case the amendment will become effective upon the later of (A) the
                                date
                                notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
                                by the
                                Province, if any; or
                            </li>
                            <li>
                                by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                                specify
                                the effective date of the amendment, which date will be at least thirty (30) days after the date that
                                the
                                PharmaCare Newsletter containing the notice is first published.
                            </li>
                        </ol>

                        <p>
                            If you do not agree with any amendment for which notice has been provided by the Province in accordance with
                            (i)
                            or (ii) above, you must promptly (and in any event before the effective date) cease all access or use of
                            PharmaNet.
                        </p>

                        <p>
                            Any written notice to you under (i) above will be in writing and delivered by the Province to you using any
                            of the
                            contact mechanisms identified by you in PRIME, including by mail to a specified postal address, email to a
                            specified email address or text message to a specified cell phone number. You may be required to click a URL
                            link
                            or log into PRIME to receive the contents of any such notice.
                        </p>

                    </li>
                    {$lcPlaceholder}
                    <li>

                        <p class=""bold underline"">
                            Governing Law
                        </p>

                        <p>
                            These terms will be governed by and will be construed and interpreted in accordance with the laws of British
                            Columbia and the laws of Canada applicable therein.
                        </p>

                        <p>
                            Unless otherwise specified, a reference to a statute or regulation by name means the statute or regulation
                            of
                            British Columbia of that name, as amended or replaced from time to time, and includes any enactment made
                            under the
                            authority of that statute or regulation.
                        </p>

                    </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <ol>
                  <li>

                    <p class=""bold underline"">
                      BACKGROUND
                    </p>

                    <p>
                      The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links B.C.
                      pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered into
                      PharmaNet.
                    </p>

                    <p>
                      The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet is to
                      enhance patient care by providing timely and relevant information to persons involved in the provision of direct
                      patient care.
                    </p>

                    <p class=""bold underline"">
                      PharmaNet contains highly sensitive confidential information, including Personal Information and the proprietary
                      and confidential information of third-party licensors to the Province, and it is in the public interest to
                      ensure that appropriate measures are in place to protect the confidentiality of all such information. All access
                      to and use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INTERPRETATION
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will have the
                          meanings given below:
                        </p>

                        <ul class=""list-unstyled"">
                          <li>
                            <strong>“Act”</strong> means the Pharmaceutical Services Act.
                          </li>
                          <li>
                            <strong>“Approved SSO”</strong> means a software support organization approved by the Province that provides
                            you with the information technology software and/or services through which you and On-Behalf-of Users access
                            PharmaNet.
                          </li>
                          <li>

                            <p>
                              <strong>“Conformance Standards”</strong> means the following documents published by the Province, as
                              amended
                              from time to time:
                            </p>

                            <ol type=""i"">
                              <li>
                                PharmaNet Professional and Software Conformance Standards; and
                              </li>
                              <li>
                                Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                                Architecture for Wireless Local Area Network Connectivity”.
                              </li>
                            </ol>

                          </li>
                          <li>
                            <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                            services to an individual to whom you provide direct patient care in the context of your Practice.
                          </li>
                          <li>
                            <strong>“Information Management Regulation”</strong> means the Information Management Regulation, B.C. Reg.
                            74/2015.
                          </li>
                          <li>
                            <strong>“On-Behalf-of User”</strong> means a member of your staff who (i) requires access to PharmaNet to
                            carry out duties in relation to your Practice; and (ii) is authorized by you to access PharmaNet on your
                            behalf; and (iii) has been granted access to PharmaNet by the Province.
                          </li>
                          <li>
                            <strong>“Personal Information”</strong> means all recorded information that is about an identifiable
                            individual or is defined as, or deemed to be, “personal information” or “personal health information”
                            pursuant to any Privacy Laws.
                          </li>
                          <li>
                            <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the
                            following website (or such other website as may be specified by the Province from time to time for this
                            purpose):

                            <br><br>

                            <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                          </li>
                          <li>
                            <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information Management
                            Regulation.
                          </li>
                          <li>
                            <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record
                            or information in the custody, control or possession of you or a On-Behalf-of User that was obtained through
                            your or a On-Behalf-of User’s access to PharmaNet.
                          </li>
                          <li>
                            <strong>“Practice”</strong> means your practice of the health profession regulated under the Health
                            Professions Act and identified by you through PRIME.
                          </li>
                          <li>
                            <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for,
                            and manage, their access to PharmaNet, and through which users are granted access by the Province.
                          </li>
                          <li>
                            <strong>“Privacy Laws”</strong> means the Act, the Freedom of Information and Protection of Privacy Act, the
                            Personal Information Protection Act, and any other statutory or legal obligations of privacy owed by you or
                            the Province, whether arising under statute, by contract or at common law.
                          </li>
                          <li>
                            <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                            Minister of Health.
                          </li>
                          <li>
                            <strong>“Professional College”</strong> is the regulatory body governing your Practice.
                          </li>
                          <li>

                            <p>
                              <strong>“Unauthorized Person”</strong> means any person other than:
                            </p>

                            <ol type=""i"">
                              <li>
                                you,
                              </li>
                              <li>
                                an On-Behalf-of User, or
                              </li>
                              <li>
                                a representative of an Approved SSO that is accessing PharmaNet for technical support purposes in
                                accordance with section 6 of the Information Management Regulation.
                              </li>
                            </ol>

                          </li>
                        </ul>

                      </li>
                      <li>
                        <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or regulation by
                        name means the statute or regulation of British Columbia of that name, as amended or replaced from time to time,
                        and includes any enactment made under the authority of that statute or regulation.
                      </li>
                      <li>

                        <p>
                          <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this Agreement:
                        </p>

                        <ol type=""i"">
                          <li>
                            a provision in the body of this Agreement will prevail over any conflicting provision in any further limits
                            or conditions communicated to you in writing by the Province, unless the conflicting provision expressly
                            states otherwise; and
                          </li>
                          <li>
                            a provision referred to in (i) above will prevail over any conflicting provision in the Conformance
                            Standards.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      APPLICATION OF LEGISLATION
                    </p>

                    <p>
                      You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act and all
                      Privacy Laws applicable to PharmaNet and PharmaNet Data.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
                    </p>

                    <p>
                      You acknowledge that:
                    </p>

                    <ol type=""a"">
                      <li>
                        PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
                        Province under the authority of the Act;
                      </li>
                      <li>
                        specific provisions of the Act, including the Information Management Regulation and sections 24, 25 and 29 of
                        the Act, apply directly to you and to On-Behalf-of Users as a result; and
                      </li>
                      <li>
                        this Agreement documents limits and conditions, set by the minister in writing, that the Act requires you to
                        comply with.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCESS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
                        compliance with the limits and conditions set out in section 5(b) below and otherwise in this Agreement. The
                        Province may from time to time, at its discretion, amend or change the scope of your access privileges to
                        PharmaNet as privacy, security, business and clinical practice requirements change. In such circumstances, the
                        Province will use reasonable efforts to notify you of such changes.
                      </li>
                      <li>

                        <p>
                          <strong>Limits and Conditions of Access.</strong> The following limits and conditions apply to your access to
                          PharmaNet:
                        </p>

                        <ol type=""i"">
                          <li>
                            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, for so
                            long as you are a registrant in good standing with the Professional College and your licence permits you to
                            deliver Direct Patient Care requiring access to PharmaNet;
                          </li>
                          <li>
                            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, at a
                            location approved by the Province, and using only the technologies and applications approved by the
                            Province. For greater certainty, you must not access PharmaNet using a VPN or similar remote access
                            technology to an approved location, unless that VPN or remote access technology has first been approved by
                            the Province in writing for use at the Practice;
                          </li>
                          <li>
                            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
                            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
                          </li>
                          <li>
                            you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of market
                            research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet Data, for the
                            purpose of market research;
                          </li>
                          <li>
                            subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure that
                            On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of Direct Patient
                            Care, including for the purposes of quality improvement, evaluation, health care planning, surveillance,
                            research or other secondary uses;
                          </li>
                          <li>
                            you will not permit any Unauthorized Person to access PharmaNet, and you will take all reasonable measures
                            to ensure that no Unauthorized Person can access PharmaNet;
                          </li>
                          <li>
                            you will complete any training program(s) that your Approved SSO makes available to you in relation to
                            PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
                          </li>
                          <li>
                            you will immediately notify the Province when you or an On-Behalf-of User no longer require access to
                            PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave of absence
                            from your staff, or where the On-Behalf-of User’s access-related duties in relation to the Practice have
                            changed;
                          </li>
                          <li>
                            you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional limits or
                            conditions applicable to you, as may be communicated to you by the Province in writing;
                          </li>
                          <li>
                            you represent and warrant that all information provided by you in connection with your application for
                            PharmaNet access, including through PRIME, is true and correct.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this Agreement
                        for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
                        PharmaNet Data.
                      </li>
                      <li>

                        <p>
                          <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable measures to
                          safeguard Personal Information, including any Personal Information in the PharmaNet Data while it is in the
                          custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
                        </p>

                        <ol type=""i"">
                          <li>
                            take all reasonable steps to ensure the physical security of Personal Information, generally and as required
                            by Privacy Laws;
                          </li>
                          <li>
                            secure all workstations and printers in a protected area in the Practice to prevent viewing of PharmaNet
                            Data by Unauthorized Persons;
                          </li>
                          <li>
                            ensure separate access credential (such as user name and password) for each On-Behalf-of User, and prohibit
                            sharing or other multiple use of your access credential, or an On-Behalf-of User’s access credential, for
                            access to PharmaNet;
                          </li>
                          <li>
                            secure any workstations used to access PharmaNet and all devices, codes or passwords that enable access to
                            PharmaNet by yourself or any On-Behalf-of User;
                          </li>
                          <li>
                            take such other privacy and security measures as the Province may reasonably require from time-to-time.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Conformance Standards - Business Rules.</strong> You will comply with, and will ensure On-Behalf-of
                        Users comply with, the business rules specified in the Conformance Standards when accessing and recording
                        information in PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLOSURE, STORAGE, AND ACCESS REQUESTS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper files or
                        any electronic system, unless such storage or retention is required for record keeping in accordance with
                        Professional College requirements and in connection with your provision of Direct Patient Care and otherwise is
                        in compliance with the Conformance Standards. You will not modify any records retained in accordance with this
                        section other than as may be expressly authorized in the Conformance Standards. For clarity, you may annotate a
                        discrete record provided that the discrete record is not itself modified other than as expressly authorized in
                        the Conformance Standards.
                      </li>
                      <li>
                        <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with section
                        6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the purpose of
                        monitoring your own Practice.
                      </li>
                      <li>
                        <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do not,
                        disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient Care or is
                        otherwise authorized under section 24(1) of the Act.
                      </li>
                      <li>
                        <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of Users do
                        not, disclose PharmaNet Data for the purpose of market research.
                      </li>
                      <li>
                        <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in accordance
                        with section 6(a) of this Agreement, you will not provide to patients any copies of records containing PharmaNet
                        Data or “print outs” produced directly from PharmaNet, and will refer any requests for access to such records or
                        “print outs” to the Province.
                      </li>
                      <li>
                        <strong>Responding to Requests to Correct a Record contained in PharmaNet.</strong> If you receive a request for
                        correction of any record or information contained in PharmaNet, you will refer the request to the Province.
                      </li>
                      <li>
                        <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the Province if
                        you receive any order, demand or request compelling, or threatening to compel, disclosure of records contained
                        in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For greater
                        certainty, the foregoing requires that you notify the Province only with respect to any access requests or
                        demands for records contained in PharmaNet, and not records retained by you in accordance with section 6(a) of
                        this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCURACY
                    </p>

                    <p>
                      You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of User
                      in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material inaccuracy or
                      error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it if
                      necessary, and notify the Province of the inaccuracy or error and any steps taken.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INVESTIGATIONS, AUDITS, AND REPORTING
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations conducted by
                        the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
                        Agreement, including providing access upon request to your facilities, data management systems, books, records
                        and personnel for the purposes of such audit or investigation.
                      </li>
                      <li>
                        <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province may
                        report any material breach of this Agreement to your Professional College or to the Information and Privacy
                        Commissioner of British Columbia.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE OF NON COMPLIANCE AND DUTY TO INVESTIGATE
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this Agreement,
                        and will take all reasonable steps to prevent recurrences of any such breaches, including taking any steps
                        necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of User’s
                        access rights.
                      </li>
                      <li>

                        <p>
                          <strong>Non Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
                        </p>

                        <ol type=""i"">
                          <li>
                            you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User will be unable
                            to comply with the terms of this Agreement in any respect, or
                          </li>
                          <li>
                            you have knowledge of any circumstances, incidents or events which have or may jeopardize the security,
                            confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person, to access
                            PharmaNet.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      TERM OF AGREEMENT, SUSPENSION & TERMINATION
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet by the
                        Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or (e)
                        below.
                      </li>
                      <li>
                        <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written notice to
                        the Province.
                      </li>
                      <li>
                        <strong>Suspension or Termination of PharmaNet access.</strong> If the Province suspends or terminates your
                        right, or an On-Behalf-of User’s right, to access PharmaNet under the Information Management Regulation, the
                        Province may also terminate this Agreement at any time thereafter upon written notice to you.
                      </li>
                      <li>
                        <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate this
                        Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to you if
                        you or an On-Behalf-of User fail to comply with any provision of this Agreement.
                      </li>
                      <li>
                        <strong>Termination by operation of the Information Management Regulation.</strong> This Agreement will
                        terminate automatically if your access to PharmaNet ends by operation of section 18 of the Information
                        Management Regulation.
                      </li>
                      <li>
                        <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may suspend your
                        account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the Province’s
                        policies. Please contact the Province immediately if your account has been suspended for inactivity but you
                        still require access to PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and PharmaNet
                        Data is solely at your own risk. All such access and information is provided on an “as is” and “as available”
                        basis without warranty or condition of any kind. The Province does not warrant the accuracy, completeness or
                        reliability of the PharmaNet Data or the availability of PharmaNet, or that access to or the operation of
                        PharmaNet will function without error, failure or interruption.
                      </li>
                      <li>
                        <strong>You are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
                        you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or acting
                        upon such information. The clinical or other information disclosed to you or an On-Behalf-of User pursuant to
                        this Agreement is in no way intended to be a substitute for professional judgment.
                      </li>
                      <li>
                        <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the Province
                        for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or PharmaNet
                        Data.
                      </li>
                      <li>
                        <strong>You Must Indemnify the Province if You Cause a Loss or Claim.</strong> You agree to indemnify and save
                        harmless the Province, and the Province’s employees and agents (each an <strong>""Indemnified Person""</strong>)
                        from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified Person may
                        sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are based
                        upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
                        On-Behalf-of User, in connection with this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another method of
                          delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to be
                          effective,
                          must be in writing and emailed or mailed to:
                        </p>

                        <address>
                          Director, Information and PharmaNet Development<br>
                          Ministry of Health<br>
                          PO Box 9652, STN PROV GOVT<br>
                          Victoria, BC V8W 9P4<br>

                          <br>

                          <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                        </address>

                      </li>
                      <li>
                        <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will be in
                        writing and delivered by the Province to you using any of the contact mechanisms identified by you in PRIME,
                        including by mail to a specified postal address, email to a specified email address or text message to the
                        specified cell phone number. You may be required to click a URL link or log into PRIME to receive the content
                        of any such notice.
                      </li>
                      <li>
                        <strong>Deemed receipt.</strong> Any written communication from a party, if personally delivered or sent
                        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent
                        by mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays)
                        after the date the notice was sent.
                      </li>
                      <li>
                        <strong>Substitute contact information.</strong> You may notify the Province of a substitute contact mechanism
                        by updating your contact information in PRIME.
                      </li>
                    </ol>

                  </li>
                  {$lcPlaceholder}
                  <li>

                    <p class=""bold underline"">
                      GENERAL
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Entire Agreement.</strong> This Agreement constitutes the entire agreement between the parties with
                          respect to the subject matter of this agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and is
                          severable from any other covenant, and if any of them are held by a court, or other decision-maker, to be
                          invalid, this Agreement will be interpreted as if such provisions were not included.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Survival.</strong> Sections 3, 4, 5(b)(iv) (v), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any other
                          provision of this Agreement that expressly or by its nature continues after termination, shall survive
                          termination of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and interpreted in
                          accordance with the laws of British Columbia and the laws of Canada applicable therein.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be assigned
                          without the prior written approval of the Province.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any provision of
                          this Agreement by you is not a waiver of its right subsequently to insist on performance of that or any other
                          provision of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Province may modify this Agreement.</strong> The Province may amend this Agreement, including this
                          section, at any time in its sole discretion:
                        </p>

                        <ol type=""i"">
                          <li>
                            by written notice to you, in which case the amendment will become effective upon the later of (A) the date
                            notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified by
                            the Province, if any; or
                          </li>
                          <li>
                            by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                            specify the effective date of the amendment, which date will be at least 30 (thirty) days after the date
                            that the PharmaCare Newsletter containing the notice is first published.
                          </li>
                        </ol>

                        <p>
                          If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment described in
                          (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this Agreement will be
                          deemed to have been so amended as of the effective date. If you do not agree with any amendment for which
                          notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly (and in any
                          event before the effective date) cease all access or use of PharmaNet by yourself and all On-Behalf-of Users,
                          and take the steps necessary to terminate this Agreement in accordance with section 10.
                        </p>

                      </li>
                    </ol>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(2020, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), @"<h1>PHARMANET REGULATED USER TERMS OF ACCESS</h1>

                <p class=""bold"">
                  By enrolling for PharmaNet access, you agree to the following terms (the “Agreement”). Please read them carefully.
                </p>

                <ol>
                  <li>

                    <p class=""bold underline"">
                      BACKGROUND
                    </p>

                    <p>
                      The Province owns and is responsible for the operation of PharmaNet, the province-wide network that links B.C.
                      pharmacies to a central data system. Every prescription dispensed in community pharmacies in B.C. is entered into
                      PharmaNet.
                    </p>

                    <p>
                      The purpose of providing you, and the On-Behalf-of Users whom you have authorized, with access to PharmaNet is to
                      enhance patient care by providing timely and relevant information to persons involved in the provision of direct
                      patient care.
                    </p>

                    <p class=""bold underline"">
                      PharmaNet contains highly sensitive confidential information, including Personal Information and the proprietary
                      and confidential information of third-party licensors to the Province, and it is in the public interest to ensure
                      that appropriate measures are in place to protect the confidentiality of all such information. All access to and
                      use of PharmaNet and PharmaNet Data is subject to the Act and Privacy Laws.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INTERPRETATION
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Definitions.</strong> Unless otherwise provided in this Agreement, capitalized terms will have the
                          meanings given below:
                        </p>

                        <ul class=""list-unstyled"">
                          <li>
                            <strong>“Act”</strong> means the <em>Pharmaceutical Services Act</em>.
                          </li>
                          <li>
                            <strong>“Approved Practice Site”</strong> means the physical site at which you provide Direct Patient Care
                            and which is approved by the Province for PharmaNet access.
                            <span class=""underline"">For greater certainty, “Approved Practice Site” does not include a location from which remote access to PharmaNet takes place;</span>
                          </li>
                          <li>
                            <strong>“Approved SSO”</strong> means a software support organization approved by the Province that provides
                            you with the information technology software and/or services through which you and On-Behalf-of Users access
                            PharmaNet.
                          </li>
                          <li>

                            <p>
                              <strong>“Conformance Standards”</strong> means the following documents published by the Province, as
                              amended from time to time:
                            </p>

                            <ol type=""i"">
                              <li>
                                PharmaNet Professional and Software Conformance Standards; and
                              </li>
                              <li>
                                Office of the Chief Information Officer: “Submission for Technical Security Standard and High Level
                                Architecture for Wireless Local Area Network Connectivity”.
                              </li>
                            </ol>

                          </li>
                          <li>
                            <strong>“Direct Patient Care”</strong> means, for the purposes of this Agreement, the provision of health
                            services to an individual to whom you provide direct patient care in the context of your Practice.
                          </li>
                          <li>
                            <strong>“Information Management Regulation”</strong> means the Information Management Regulation, B.C. Reg.
                            74/2015.
                          </li>
                          <li>
                            <strong>“On-Behalf-of User”</strong> means a member of your staff who (i) requires access to PharmaNet to
                            carry out duties in relation to your Practice; (ii) is authorized by you to access PharmaNet on your behalf;
                            and (iii) has been granted access to PharmaNet by the Province.
                          </li>
                          <li>
                            <strong>“Personal Information”</strong> means all recorded information that is about an identifiable
                            individual or is defined as, or deemed to be, “personal information” or “personal health information”
                            pursuant to any Privacy Laws.
                          </li>
                          <li>
                            <strong>“PharmaCare Newsletter”</strong> means the PharmaCare newsletter published by the Province on the
                            following website (or such other website as may be specified by the Province from time to time for this
                            purpose):

                            <br><br>

                            <a href=""http://www.gov.bc.ca/pharmacarenewsletter"" target=""_blank"" rel=""noopener noreferrer"">www.gov.bc.ca/pharmacarenewsletter</a>
                          </li>
                          <li>
                            <strong>“PharmaNet”</strong> means PharmaNet as continued under section 2 of the Information Management
                            Regulation.
                          </li>
                          <li>
                            <strong>“PharmaNet Data”</strong> includes any record or information contained in PharmaNet and any record
                            or information in the custody, control or possession of you or an On-Behalf-of User that was obtained
                            through your or an On-Behalf-of User’s access to PharmaNet.
                          </li>
                          <li>
                            <strong>“Practice”</strong> means your practice of the health profession regulated under the <em>Health
                            Professions Act</em>, or your practice as an enrolled device provider under the
                            <em>Provider Regulation</em>, B.C. Reg.222/2014, as identified by you through PRIME.
                          </li>
                          <li>
                            <strong>“PRIME”</strong> means the online service provided by the Province that allows users to apply for,
                            and manage, their access to PharmaNet, and through which users are granted access by the Province.
                          </li>
                          <li>
                            <strong>“Privacy Laws”</strong> means the Act, the
                            <em>Freedom of Information and Protection of Privacy Act</em>,
                            the <em>Personal Information Protection Act</em>, and any other statutory or legal obligations of privacy
                            owed by you or the Province, whether arising under statute, by contract or at common law.
                          </li>
                          <li>
                            <strong>“Province”</strong> means Her Majesty the Queen in Right of British Columbia, as represented by the
                            Minister of Health.
                          </li>
                          <li>
                            <strong>“Professional College”</strong> is the regulatory body governing your Practice.
                          </li>
                          <li>

                            <p>
                              <strong>“Unauthorized Person”</strong> means any person other than:
                            </p>

                            <ol type=""i"">
                              <li>
                                you,
                              </li>
                              <li>
                                an On-Behalf-of User, or
                              </li>
                              <li>
                                a representative of an Approved SSO that is accessing PharmaNet for technical support purposes in
                                accordance with section 6 of the <em>Information Management Regulation</em>.
                              </li>
                            </ol>

                          </li>
                        </ul>

                      </li>
                      <li>
                        <strong>Reference to Enactments.</strong> Unless otherwise specified, a reference to a statute or regulation by
                        name means the statute or regulation of British Columbia of that name, as amended or replaced from time to time,
                        and includes any enactment made under the authority of that statute or regulation.
                      </li>
                      <li>

                        <p>
                          <strong>Conflicting Provisions.</strong> In the event of a conflict among provisions of this Agreement:
                        </p>

                        <ol type=""i"">
                          <li>
                            a provision in the body of this Agreement will prevail over any conflicting provision in any further
                            limits or conditions communicated to you in writing by the Province, unless the conflicting provision
                            expressly states otherwise; and
                          </li>
                          <li>
                            a provision referred to in (i) above will prevail over any conflicting provision in the Conformance
                            Standards.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      APPLICATION OF LEGISLATION
                    </p>

                    <p>
                      You will strictly comply with, and you will ensure that On-Behalf-of Users strictly comply with, the Act and all
                      Privacy Laws applicable to PharmaNet and PharmaNet Data.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE THAT SPECIFIC PROVISIONS OF THE ACT APPLY DIRECTLY TO YOU
                    </p>

                    <p>
                      You acknowledge that:
                    </p>

                    <ol type=""a"">
                      <li>
                        PharmaNet Data accessed by you or an On-Behalf-of User pursuant to this Agreement is disclosed to you by the
                        Province under the authority of the Act;
                      </li>
                      <li>
                        specific provisions of the Act, including the <em>Information Management Regulation</em> and sections 24, 25 and
                        29 of the Act, apply directly to you and to On-Behalf-of Users as a result; and
                      </li>
                      <li>
                        this Agreement documents limits and conditions, set by the minister in writing, that the Act requires you to
                        comply with.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCESS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Grant of Access.</strong> The Province will provide you with access to PharmaNet subject to your
                        compliance with the limits and conditions set out in section 5(b) below and otherwise in this Agreement. The
                        Province may from time to time, at its discretion, amend or change the scope of your access privileges to
                        PharmaNet as privacy, security, business and clinical practice requirements change. In such circumstances, the
                        Province will use reasonable efforts to notify you of such changes.
                      </li>
                      <li>

                        <p>
                          <strong>Limits and Conditions of Access.</strong> The following limits and conditions apply to your access to
                          PharmaNet:
                        </p>

                        <ol type=""i"">
                          <li>
                            you will only access PharmaNet, and you will ensure that On-Behalf-of Users only access PharmaNet, for so
                            long as you are a registrant in good standing with the Professional College and your registration permits
                            you to deliver Direct Patient Care requiring access to PharmaNet;
                          </li>
                          <li>
                            <p>you will only access PharmaNet:</p>

                            <ul>
                              <li>
                                at the Approved Practice Site, and using only the technologies and applications approved by the
                                Province; or
                              </li>
                              <li>
                                using a VPN or similar remote access technology, if you are physically located in British Columbia and
                                remotely connected to the Approved Practice Site using a VPN or other remote access technology
                                specifically approved by the Province in writing for the Approved Practice Site.
                              </li>
                            </ul>
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of-Users do not access PharmaNet on your behalf unless the access
                            takes place at the Approved Practice Site and the access is in relation to patients for whom you will be
                            providing Direct Patient Care at the Approved Practice Site requiring the access to PharmaNet;
                          </li>
                          <li>
                            you must ensure that your On-Behalf-of Users do not access PharmaNet using VPN or other remote access
                            technology
                          </li>
                          <li>
                            you will only access PharmaNet as necessary for your provision of Direct Patient Care, and you will ensure
                            that On-Behalf-of Users only access PharmaNet as necessary to support your provision of Direct Patient Care;
                          </li>
                          <li>
                            you will not under any circumstances access PharmaNet, or use PharmaNet Data, for the purpose of market
                            research, and you will ensure that no On-Behalf-of Users access PharmaNet, or use PharmaNet Data, for the
                            purpose of market research;
                          </li>
                          <li>
                            subject to section 6(b) of this Agreement, you will not use PharmaNet Data, and you will ensure that
                            On-Behalf-of Users do not use PharmaNet Data, for any purpose other than your provision of Direct Patient
                            Care, including for the purposes of quality improvement, evaluation, health care planning, surveillance,
                            research or other secondary uses;
                          </li>
                          <li>
                            you will not permit any Unauthorized Person to access PharmaNet, and you will take all reasonable measures
                            to ensure that no Unauthorized Person can access PharmaNet;
                          </li>
                          <li>
                            you will complete any training program(s) that your Approved SSO makes available to you in relation to
                            PharmaNet, and you will ensure that all On-Behalf-of Users complete such training;
                          </li>
                          <li>
                            you will immediately notify the Province when you or an On-Behalf-of User no longer require access to
                            PharmaNet, including where the On-Behalf-of User ceases to be one of your staff or takes a leave of absence
                            from your staff, or where the On-Behalf-of User’s access-related duties in relation to the Practice have
                            changed;
                          </li>
                          <li>
                            you will comply with, and you will ensure that On-Behalf-of Users comply with, any additional limits or
                            conditions applicable to you, as may be communicated to you by the Province in writing;
                          </li>
                          <li>
                            you represent and warrant that all information provided by you in connection with your application for
                            PharmaNet access, including through PRIME, is true and correct.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Responsibility for On-Behalf-of Users.</strong> You agree that you are responsible under this Agreement
                        for all activities undertaken by On-Behalf-of Users in relation to their access to PharmaNet and use of
                        PharmaNet Data.
                      </li>
                      <li>

                        <p>
                          <strong>Privacy and Security Measures.</strong> You are responsible for taking all reasonable measures to
                          safeguard Personal Information, including any Personal Information in the PharmaNet Data while it is in the
                          custody, control or possession of yourself or an On-Behalf-of User. In particular, you will:
                        </p>

                        <ol type=""i"">
                          <li>
                            take all reasonable steps to ensure the physical security of Personal Information, generally and as
                            required by Privacy Laws;
                          </li>
                          <li>
                            secure all workstations and printers in a protected area in the Practice to prevent viewing of PharmaNet
                            Data by Unauthorized Persons;
                          </li>
                          <li>
                            ensure separate access credential (such as user name and password) for each On-Behalf-of User, and prohibit
                            sharing or other multiple use of your access credential, or an On-Behalf-of User’s access credential, for
                            access to PharmaNet;
                          </li>
                          <li>
                            secure any workstations used to access PharmaNet and all devices, codes or passwords that enable access to
                            PharmaNet by yourself or any On-Behalf-of User;
                          </li>
                          <li>
                            take such other privacy and security measures as the Province may reasonably require from time-to-time.
                          </li>
                        </ol>

                      </li>
                      <li>
                        <strong>Conformance Standards.</strong> You will comply with, and will ensure On-Behalf-of
                        Users comply with, the rules specified in the Conformance Standards when accessing and recording information in
                        PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLOSURE, STORAGE, AND ACCESS REQUESTS
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Retention of PharmaNet Data.</strong> You will not store or retain PharmaNet Data in any paper files or
                        any electronic system, unless such storage or retention is required for record keeping in accordance with
                        Professional College requirements and in connection with your provision of Direct Patient Care and otherwise is
                        in compliance with the Conformance Standards. You will not modify any records retained in accordance with this
                        section other than as may be expressly authorized in the Conformance Standards. For clarity, you may annotate a
                        discrete record provided that the discrete record is not itself modified other than as expressly authorized in
                        the Conformance Standards.
                      </li>
                      <li>
                        <strong>Use of Retained Records.</strong> You may use any records retained by you in accordance with section
                        6(a) of this Agreement for a purpose authorized under section 24(1) of the Act, including for the purpose of
                        monitoring your own Practice.
                      </li>
                      <li>
                        <strong>Disclosure to Third Parties.</strong> You will not, and will ensure that On-Behalf-of Users do not,
                        disclose PharmaNet Data to any Unauthorized Person, unless disclosure is required for Direct Patient Care or is
                        otherwise authorized under section 24(1) of the Act.
                      </li>
                      <li>
                        <strong>No Disclosure for Market Research.</strong> You will not, and will ensure that On-Behalf-of Users do
                        not, disclose PharmaNet Data for the purpose of market research.
                      </li>
                      <li>
                        <strong>Responding to Patient Access Requests.</strong> Aside from any records retained by you in accordance
                        with section 6(a) of this Agreement, you will not provide to patients any copies of records containing PharmaNet
                        Data or “print outs” produced directly from PharmaNet, and will refer any requests for access to such records or
                        “print outs” to the Province.
                      </li>
                      <li>
                        <strong>Responding to Requests to Correct a Record contained in PharmaNet.</strong> If you receive a request for
                        correction of any record or information contained in PharmaNet, you will refer the request to the Province.
                      </li>
                      <li>
                        <strong>Legal Demands for Records Contained in PharmaNet.</strong> You will immediately notify the Province if
                        you receive any order, demand or request compelling, or threatening to compel, disclosure of records contained
                        in PharmaNet. You will cooperate and consult with the Province in responding to any such demands. For greater
                        certainty, the foregoing requires that you notify the Province only with respect to any access requests or
                        demands for records contained in PharmaNet, and not records retained by you in accordance with section 6(a) of
                        this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      ACCURACY
                    </p>

                    <p>
                      You will make reasonable efforts to ensure that any Personal Information recorded by you or an On-Behalf-of User
                      in PharmaNet is accurate, complete and up to date. In the event that you become aware of a material inaccuracy or
                      error in such information, you will take reasonable steps to investigate the inaccuracy or error, correct it if
                      necessary, and notify the Province of the inaccuracy or error and any steps taken.
                    </p>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      INVESTIGATIONS, AUDITS, AND REPORTING
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Audits and Investigations.</strong> You will cooperate with any audits or investigations conducted by
                        the Province regarding your, or any On-Behalf-of User’s, compliance with the Act, Privacy Laws and this
                        Agreement, including providing access upon request to your facilities, data management systems, books, records
                        and personnel for the purposes of such audit or investigation.
                      </li>
                      <li>
                        <strong>Reports to College or Privacy Commissioner.</strong> You acknowledge and agree that the Province may
                        report any material breach of this Agreement to your Professional College or to the Information and Privacy
                        Commissioner of British Columbia.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE OF NON COMPLIANCE AND DUTY TO INVESTIGATE
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Duty to Investigate.</strong> You will investigate suspected breaches of the terms of this Agreement,
                        and will take all reasonable steps to prevent recurrences of any such breaches, including taking any steps
                        necessary to cooperate with the Province in ensuring the suspension or termination of an On-Behalf-of User’s
                        access rights.
                      </li>
                      <li>

                        <p>
                          <strong>Non Compliance.</strong> You will promptly notify the Province, and provide particulars, if:
                        </p>

                        <ol type=""i"">
                          <li>
                            you or an On-Behalf-of User do not comply, or you anticipate that you or a On-Behalf-of User will be unable
                            to comply with the terms of this Agreement in any respect, or
                          </li>
                          <li>
                            you have knowledge of any circumstances, incidents or events which have or may jeopardize the security,
                            confidentiality, or integrity of PharmaNet, including any unauthorized attempt, by any person, to access
                            PharmaNet.
                          </li>
                        </ol>

                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      TERM OF AGREEMENT, SUSPENSION & TERMINATION
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Term.</strong> The term of this Agreement begins on the date you are granted access to PharmaNet by the
                        Province and will continue until the date this Agreement is terminated under paragraph (b), (c), (d) or (e)
                        below.
                      </li>
                      <li>
                        <strong>Termination for Any Reason.</strong> You may terminate this Agreement at any time on written notice to
                        the Province.
                      </li>
                      <li>
                        <strong>Suspension or Termination of PharmaNet access.</strong> If the Province suspends or terminates your
                        right, or an On-Behalf-of User’s right, to access PharmaNet under the
                        <em>Information Management Regulation</em>, the Province may also terminate this Agreement at any time
                        thereafter upon written notice to you.
                      </li>
                      <li>
                        <strong>Termination for Breach.</strong> Notwithstanding paragraph (c) above, the Province may terminate this
                        Agreement, and any or all access to PharmaNet by you or an On-Behalf-of User, immediately upon notice to you if
                        you or an On-Behalf-of User fail to comply with any provision of this Agreement.
                      </li>
                      <li>
                        <strong>Termination by operation of the Information Management Regulation.</strong> This Agreement will
                        terminate automatically if your access to PharmaNet ends by operation of section 18 of the
                        <em>Information Management Regulation</em>.
                      </li>
                      <li>
                        <strong>Suspension of Account for Inactivity.</strong> As a security precaution, the Province may suspend your
                        account or an On-Behalf-of User’s account after a period of inactivity, in accordance with the Province’s
                        policies. Please contact the Province immediately if your account has been suspended for inactivity but you
                        still require access to PharmaNet.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      DISCLAIMER OF WARRANTY, LIMITATION OF LIABILITY AND INDEMNITY
                    </p>

                    <ol type=""a"">
                      <li>
                        <strong>Information Provided As Is.</strong> You acknowledge and agree that any use of PharmaNet and PharmaNet
                        Data is solely at your own risk. All such access and information is provided on an “as is” and “as available”
                        basis without warranty or condition of any kind. The Province does not warrant the accuracy, completeness or
                        reliability of the PharmaNet Data or the availability of PharmaNet, or that access to or the operation of
                        PharmaNet will function without error, failure or interruption.
                      </li>
                      <li>
                        <strong>You are Responsible.</strong> You are responsible for verifying the accuracy of information disclosed to
                        you as a result of your access to PharmaNet or otherwise pursuant to this Agreement before relying or acting
                        upon such information. The clinical or other information disclosed to you or an On-Behalf-of User pursuant to
                        this Agreement is in no way intended to be a substitute for professional judgment.
                      </li>
                      <li>
                        <strong>The Province Not Liable for Loss.</strong> No action may be brought by any person against the Province
                        for any loss or damage of any kind caused by any reason or purpose related to reliance on PharmaNet or PharmaNet
                        Data.
                      </li>
                      <li>
                        <strong>You Must Indemnify the Province if You Cause a Loss or Claim.</strong> You agree to indemnify and save
                        harmless the Province, and the Province’s employees and agents (each an <strong>""Indemnified Person""</strong>)
                        from any losses, claims, damages, actions, causes of action, costs and expenses that an Indemnified Person may
                        sustain, incur, suffer or be put to at any time, either before or after this Agreement ends, which are based
                        upon, arise out of or occur directly or indirectly by reason of any act or omission by you, or by any
                        On-Behalf-of User, in connection with this Agreement.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      NOTICE
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Notice to Province.</strong> Except where this Agreement expressly provides for another method of
                          delivery, any notice to be given by you to the Province that is contemplated by this Agreement, to be
                          effective, must be in writing and emailed or mailed to:
                        </p>

                        <address>
                          Director, Information and PharmaNet Development<br>
                          Ministry of Health<br>
                          PO Box 9652, STN PROV GOVT<br>
                          Victoria, BC V8W 9P4<br>

                          <br>

                          <a href=""mailto:PRIMESupport@gov.bc.ca"">PRIMESupport@gov.bc.ca</a>
                        </address>

                      </li>
                      <li>
                        <strong>Notice to You.</strong> Any notice to you to be delivered under the terms of this Agreement will be in
                        writing and delivered by the Province to you using any of the contact mechanisms identified by you in PRIME,
                        including by mail to a specified postal address, email to a specified email address or text message to the
                        specified cell phone number. You may be required to click a URL link or log into PRIME to receive the content of
                        any such notice.
                      </li>
                      <li>
                        <strong>Deemed receipt.</strong> Any written communication from a party, if personally delivered or sent
                        electronically, will be deemed to have been received 24 hours after the time the notice was sent, or, if sent by
                        mail, will be deemed to have been received 3 days (excluding Saturdays, Sundays and statutory holidays) after
                        the date the notice was sent.
                      </li>
                      <li>
                        <strong>Substitute contact information.</strong> You may notify the Province of a substitute contact mechanism
                        by updating your contact information in PRIME.
                      </li>
                    </ol>

                  </li>
                  <li>

                    <p class=""bold underline"">
                      GENERAL
                    </p>

                    <ol type=""a"">
                      <li>

                        <p>
                          <strong>Severability.</strong> Each provision in this Agreement constitutes a separate covenant and is
                          severable from any other covenant, and if any of them are held by a court, or other decision-maker, to be
                          invalid, this Agreement will be interpreted as if such provisions were not included.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Survival.</strong> Sections 3, 4, 5(b)(vi) (vii), 5(c), 5(d), 6(a)(b)(c)(d), 8, 9, 11, and any other
                          provision of this Agreement that expressly or by its nature continues after termination, shall survive
                          termination of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Governing Law.</strong> This Agreement will be governed by and will be construed and interpreted in
                          accordance with the laws of British Columbia and the laws of Canada applicable therein.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Assignment Restricted.</strong> Your rights and obligations under this Agreement may not be assigned
                          without the prior written approval of the Province.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Waiver.</strong> The failure of the Province at any time to insist on performance of any provision of
                          this Agreement by you is not a waiver of its right subsequently to insist on performance of that or any other
                          provision of this Agreement.
                        </p>

                      </li>
                      <li>

                        <p>
                          <strong>Province may modify this Agreement.</strong> The Province may amend this Agreement, including this
                          section, at any time in its sole discretion:
                        </p>

                        <ol type=""i"">
                          <li>
                            by written notice to you, in which case the amendment will become effective upon the later of (A) the
                            date notice of the amendment is first delivered to you, or (B) the effective date of the amendment specified
                            by the Province, if any; or
                          </li>
                          <li>
                            by publishing notice of any such amendment in the PharmaCare Newsletter, in which case the notice will
                            specify the effective date of the amendment, which date will be at least 30 (thirty) days after the date
                            that the PharmaCare Newsletter containing the notice is first published.
                          </li>
                        </ol>

                        <p>
                          If you or an On-Behalf-of User access or use PharmaNet after the effective date of an amendment described in
                          (i) or (ii) above, you will be deemed to have accepted the corresponding amendment, and this Agreement will be
                          deemed to have been so amended as of the effective date. If you do not agree with any amendment for which
                          notice has been provided by the Province in accordance with (i) or (ii) above, you must promptly (and in any
                          event before the effective date) cease all access or use of PharmaNet by yourself and all On-Behalf-of Users,
                          and take the steps necessary to terminate this Agreement in accordance with section 10.
                        </p>

                      </li>
                    </ol>

                  </li>
                </ol>
                ", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "BusinessEventTypeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 6, "Site" },
                    { 9, "Pharmanet API Call" },
                    { 7, "Admin View" },
                    { 5, "Enrollee" },
                    { 8, "Organization" },
                    { 3, "Note" },
                    { 2, "Email" },
                    { 1, "Status Change" },
                    { 4, "Admin Claim" }
                });

            migrationBuilder.InsertData(
                table: "CareSettingLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 1, "Health Authority" },
                    { 2, "Private Community Health Practice" },
                    { 3, "Community Pharmacy" },
                    { 4, "Device Provider" }
                });

            migrationBuilder.InsertData(
                table: "CollegeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 16, "College of Psychologists of BC" },
                    { 15, "College of Physical Therapists of BC" },
                    { 14, "College of Optometrists of BC" },
                    { 13, "College of Opticians of BC" },
                    { 12, "College of Occupational Therapists of BC" },
                    { 11, "College of Naturopathic Physicians of BC" },
                    { 10, "College of Massage Therapists of BC" },
                    { 9, "College of Dietitians of BC" },
                    { 5, "College of Dental Hygenists of BC" },
                    { 7, "College of Dental Surgeons of BC" },
                    { 6, "College of Dental Technicians of BC" },
                    { 4, "College of Chiropractors of BC" },
                    { 3, "BC College of Nurses and Midwives (BCCNM)" },
                    { 2, "College of Pharmacists of BC (CPBC)" },
                    { 1, "College of Physicians and Surgeons of BC (CPSBC)" },
                    { 17, "College of Speech and Hearing Health Professionals of BC" },
                    { 8, "College of Denturists of BC" },
                    { 18, "College of Traditional Chinese Medicine Practitioners and Acupuncturists of BC" }
                });

            migrationBuilder.InsertData(
                table: "CountryLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "US", "United States" },
                    { "CA", "Canada" }
                });

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatedUserId", "EmailType", "ModifiedDate", "Template", "UpdatedTimeStamp", "UpdatedUserId" },
                values: new object[,]
                {
                    { 10, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 10, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "Dear @Model.EnrolleeName, <br> <br> Your enrolment has not been renewed. PRIME will be notifying your PharmaNet software vendor to deactivate your account.", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 13, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 13, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "<p> A new PharmaNet site registration has been received. See the attached registration and organization agreement for more information. @if (!string.IsNullOrWhiteSpace(Model.Url)) { @(\"To access the Business Licence, click this\") <a href=\"@Model.Url\" target=\"_blank\">link</a>@(\".\") } </p>", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 12, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 12, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "<p> The site you registered in PRIME, @Model.DoingBusinessAs, has been approved by the Ministry of Health. Your SiteID is @Model.Pec. </p> <p> Health Insurance BC has been notified of the site’s approval and will contact your software vendor. Your vendor will complete any remaining setup for your site and may contact you or the PharmaNet Administrator at your site. </p> <p> If you need to update any information in PRIME regarding your site, you may log in at any time using your mobile BC Services Card. If you have any questions or concerns, please phone 1-844-397-7463 or email PRIMESupport@gov.bc.ca. </p> <p> Thank you. </p>", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 11, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 11, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "<p> Your site registration has been approved. The site must now be set up and activated in PharmaNet. Your PharmaNet software vendor will be notified when the site has been activated, and you will hear from them when you can start to use PharmaNet. </p> <p> Individuals who will be accessing PharmaNet at your site should enrol in PRIME now if they have not already done so. For more information, please visit <a href=\"https://www.gov.bc.ca/pharmanet/PRIME\" target=\"_blank\">https://www.gov.bc.ca/pharmanet/PRIME</a>. [for private community practice only: If you have registered any physicians or nurse practitioners for remote access to PharmaNet, they must enroll in PRIME before they use remote access, which they can do here: <a href=\"https://pharmanetenrolment.gov.bc.ca\" target=\"_blank\">https://pharmanetenrolment.gov.bc.ca</a>. You must not permit remote use of PharmaNet until these users are approved in PRIME.] </p> <p> If you have any questions or concerns, please phone 1-844-397-7463 or email <a href=\"mailto:PRIMEsupport@gov.bc.ca\" target=\"_top\">PRIMESupport@gov.bc.ca</a>. </p> <p> Thank you. </p>", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 8, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 8, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "@{ var renewalDate = Model.RenewalDate.Date.ToShortDateString(); } Dear @Model.EnrolleeName, <br> <br> Your enrolment for PharmaNet access will expire on @renewalDate. In order to continue to use PharmaNet, you must renew your enrolment information. <br> Click here to visit PRIME. <a href=\"@Model.PrimeUrl\">@Model.PrimeUrl</a>", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 7, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 7, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "<p>@Model.DoingBusinessAs with PEC/SiteID @Model.Pec has been approved by the Ministry of Health for PharmaNet access. Please notify the PharmaNet software vendor for this site and complete any remaining tasks to activate the site.</p><p>Thank you.</p><p>PRIME Support Team.</p>", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 9, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 9, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "<p> A user has uploaded business licence to their PharmaNet site registration. @if (!string.IsNullOrWhiteSpace(Model.Url)) { @(\"To access the Business Licence, click this\") <a href=\"@Model.Url\" target=\"_blank\">link</a>@(\".\") } </p>", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 5, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 5, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "The Ministry of Health has been notified that you require Remote Access at <br> Organization Name: @Model.OrganizationName <br> Site Address: @Model.SiteStreetAddress, @Model.SiteCity <br> <br> To complete your approval for Remote Access, please ensure you have indicated you require Remote Access on your profile at <a href=\"@Model.PrimeUrl\">@Model.PrimeUrl</a>.", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 4, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 4, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "To: PharmNet Access administrator <br> <br> @Model.EnrolleeFullName has been approved for <strong>Health Authority Access to PharmaNet.</strong> <br> They can now be set up with their PharmaNet Access account in your local software. You must include their <strong>Global PharmaNet ID (GPID)</strong> on their account profile. <br> You can access their GPID via this link here. <br> <a href=\"@Model.TokenUrl\">@Model.TokenUrl</a> <br> <strong>This link will expire after @Model.ExpiresInDays days</strong>.", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 3, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "To: Whom it may concern, <br> <br> @Model.EnrolleeFullName has been approved for <strong>Community Pharmacy Access to PharmaNet.</strong> They can now be set up with their PharmaNet Access account in your local software. You must include their <strong>Global PharmaNet ID (GPID)</strong> on their account profile. You can access their GPID via this link below. <br> <br> <a href=\"@Model.TokenUrl\">@Model.TokenUrl</a> <br> <strong>This link will expire after @Model.ExpiresInDays days</strong>. <br> <br> Thank you.", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 2, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "<style> .underline { text-decoration: underline; } .list-item-mb { margin-bottom: 0.75rem; } </style>To: Clinic Manager (person responsible for coordinating PharmaNet access):<br><br>@Model.EnrolleeFullName has been approved for Private Community Health Practice Access to PharmaNet.<br><br> <strong> To set up their access, you must forward this PRIME Enrolment Confirmation and the information specified below to your <span class=\"underline\">PharmaNet Software Vendor</span>. </strong> <br><br> <ol> <li class=\"list-item-mb\"> Name of medical clinic: </li> <li class=\"list-item-mb\"> Clinic address: </li> <li class=\"list-item-mb\"> Pharmacy equivalency code (PEC): <i>This is your PharmaNet site ID; ask your vendor if you don’t know it:</i> </li> <li class=\"list-item-mb\"> For <strong> physicians, pharmacists, and nurse practitioners:</strong> <br><br> College name and College ID of this user <em>(leave blank if this user is not a physician, pharmacist or nurse practitioner)</em> </li> <li class=\"list-item-mb\"> For users who work <strong>On Behalf Of</strong> a physician, pharmacist, or nurse practitioner: <br><br> College name(s) and College ID(s) of the physicians, pharmacists or nurse practitioners who this user will access PharmaNet on behalf of: <em>(leave this blank if this user is a pharmacist, nurse practitioner or physician)</em> </li> </ol> <a href=\"@Model.TokenUrl\">@Model.TokenUrl</a> <br> <strong>This link will expire after @Model.ExpiresInDays days.</strong> <br><br> Thank you,<br><br> PRIME<br><br> 1-844-397-7463<br><br> <a href='mailto:PRIMEsupport@gov.bc.ca' target='_top'>PRIMEsupport@gov.bc.ca</a>", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 1, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 1, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "<p> Your PRIME application status has changed since you last viewed it. Please click <a href=\"@Model.Url\">here</a> to log into PRIME and view your status. </p>", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 6, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000"), 6, new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), "@{ var pecText = string.IsNullOrWhiteSpace(Model.SitePec) ? \"Not Assigned\" : Model.SitePec; } <p> Notification: The list of Remote Practitioners at @Model.SiteStreetAddress of @Model.OrganizationName (PEC: @pecText) has been updated. The Remote Practitioners at this site are: </p> <h2 class=\"mb-2\">Remote Users</h2> @foreach (var name in Model.RemoteUserNames) { <div class=\"ml-2 mb-2\"> <h5>Name</h5> <span class=\"ml-2\">@name</span> </div> } <h2 class=\"mb-2\">Site Information</h2> <p> See the attached registration and organization agreement for more information. @if (!string.IsNullOrWhiteSpace(Model.DocumentUrl)) { @(\"To access the Business Licence, click this\") <a href=\"@Model.DocumentUrl\" target=\"_blank\">link</a>@(\".\") } </p>", new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "FacilityLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 4, "Out-patient pharmacy" },
                    { 1, "Acute/ambulatory care" },
                    { 2, "Long-term care" },
                    { 3, "In-patient pharmacy" },
                    { 5, "Outpatient or community-based clinic" }
                });

            migrationBuilder.InsertData(
                table: "HealthAuthorityLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 6, "Provincial Health Services Authority" },
                    { 4, "Island Health" },
                    { 5, "Fraser Health" },
                    { 2, "Interior Health" },
                    { 1, "Northern Health" },
                    { 3, "Vancouver Coastal Health" }
                });

            migrationBuilder.InsertData(
                table: "IdentifierTypeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "2.16.840.1.113883.4.477", "COUNID" },
                    { "2.16.840.1.113883.4.401", "PHTID" },
                    { "2.16.840.1.113883.4.439", "KNID" },
                    { "2.16.840.1.113883.4.433", "RMTID" },
                    { "2.16.840.1.113883.4.414", "PHYSIOID" },
                    { "2.16.840.1.113883.4.422", "CHIROID" },
                    { "2.16.840.1.113883.4.361", "SWID" },
                    { "2.16.840.1.113883.4.362", "PSYCHID" },
                    { "2.16.840.1.113883.4.364", "OTID" },
                    { "2.16.840.1.113883.4.363", "CCID" },
                    { "2.16.840.1.113883.3.40.2.6", "DENID" },
                    { "2.16.840.1.113883.3.40.2.4", "CPSID" },
                    { "2.16.840.1.113883.3.40.2.10", "LPNID" },
                    { "2.16.840.1.113883.3.40.2.18", "RMID" },
                    { "2.16.840.1.113883.4.454", "RACID" },
                    { "2.16.840.1.113883.3.40.2.14", "PHID" },
                    { "2.16.840.1.113883.4.608", "RPNRC" },
                    { "2.16.840.1.113883.3.40.2.20", "RNPID" },
                    { "2.16.840.1.113883.3.40.2.19", "RNID" },
                    { "2.16.840.1.113883.4.452", "MFTID" },
                    { "2.16.840.1.113883.4.429", "OPTID" },
                    { "2.16.840.1.113883.4.530", "RDID" }
                });

            migrationBuilder.InsertData(
                table: "JobNameLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 4, "Ward clerk" },
                    { 3, "Registration clerk" },
                    { 5, "Nursing unit assistant" },
                    { 1, "Medical office assistant" },
                    { 2, "Pharmacy assistant" }
                });

            migrationBuilder.InsertData(
                table: "LicenseLookup",
                columns: new[] { "Code", "LicensedToProvideCare", "Manual", "Name", "NamedInImReg", "Prefix", "PrescriberIdType", "Validate", "Weight" },
                values: new object[,]
                {
                    { 37, false, false, "Non-Practicing Licensed Graduate Nurse", false, "R9", null, true, 14 },
                    { 35, true, true, "Practicing Licensed Graduate Nurse", false, "R9", 1, false, 12 },
                    { 40, true, false, "Employed Student Nurse", false, "R9", null, false, 11 },
                    { 34, false, true, "Non-Practicing Registered Nurse", false, "R9", null, false, 10 },
                    { 39, true, false, "Temporary Registered Nurse (Emergency)", false, "R9", 1, true, 9 },
                    { 41, true, false, "Practicing Registered Psychiatric Nurse", false, "Y9", 1, true, 15 },
                    { 38, true, true, "Temporary Registered Nurse (Special Event)", false, "R9", 1, false, 8 },
                    { 32, true, false, "Practicing Registered Nurse", false, "R9", 1, true, 6 },
                    { 49, false, true, "Non-Practicing Nurse Practitioner", true, "96", null, true, 5 },
                    { 51, true, false, "Temporary Nurse Practitioner (Emergency)", true, "96", 2, true, 4 },
                    { 50, true, true, "Temporary Nurse Practitioner (Special Event)", true, "96", 2, true, 3 },
                    { 48, true, true, "Provisional Nurse Practitioner", true, "96", null, false, 2 },
                    { 47, true, false, "Practicing Nurse Practitioner", true, "96", 2, true, 1 },
                    { 33, true, false, "Provisional Registered Nurse", false, "R9", null, false, 7 },
                    { 68, true, true, "Temporary Pharmacy Technician", false, "T9", null, false, 8 },
                    { 42, true, false, "Provisional Registered Psychiatric Nurse", false, "Y9", null, false, 16 },
                    { 45, true, false, "Temporary Registered Psychiatric Nurse (Emergency)", false, "Y9", 1, true, 18 },
                    { 64, false, true, "Not Displayed", false, "", null, false, 1 },
                    { 63, true, true, "Non-Practising Midwife", false, "98", null, false, 31 },
                    { 62, true, false, "Temporary Midwife (Emergency)", false, "98", null, false, 30 },
                    { 61, true, false, "Provisional Midwife", false, "98", null, false, 29 },
                    { 60, true, false, "Practising Midwife", false, "98", null, false, 28 },
                    { 58, true, true, "Temporary Nurse Practitioner (time-limited)", true, "96", null, true, 27 },
                    { 44, true, true, "Temporary Registered Psychiatric Nurse (Special Event)", false, "Y9", 1, false, 17 },
                    { 57, false, true, "Non-Practicing Licensed Nurse Practitioner", true, "96", null, true, 26 },
                    { 56, true, true, "Temporary Licensed Practical Nurse (Special Event)", false, "L9", null, false, 24 },
                    { 55, true, false, "Temporary Licensed Practical Nurse (Emergency)", false, "L9", null, false, 23 },
                    { 53, true, false, "Provisional Licensed Practical Nurse", false, "L9", null, false, 22 },
                    { 52, true, false, "Practicing Licensed Practical Nurse", false, "L9", null, false, 21 },
                    { 46, true, false, "Employed Student Psychiatric Nurse", false, "Y9", null, false, 20 },
                    { 43, false, true, "Non-Practicing Registered Psychiatric Nurse", false, "Y9", null, false, 19 },
                    { 54, false, true, "Non-Practicing Licensed Practical Nurse", false, "L9", null, false, 25 },
                    { 31, false, true, "Non-Practicing Pharmacy Technician", false, "T9", null, false, 7 },
                    { 36, true, true, "Provisional Licensed Graduate Nurse", false, "R9", null, false, 13 },
                    { 30, false, true, "Non-Practicing Pharmacist", true, "P1", null, true, 5 },
                    { 12, true, false, "Educational - Postgraduate Resident", true, "91", null, true, 14 },
                    { 17, true, true, "Visitor", true, "91", null, true, 13 },
                    { 3, true, true, "Special", true, "91", null, true, 12 },
                    { 4, true, false, "Osteopathic", true, "91", null, true, 11 },
                    { 7, true, true, "Academic", true, "91", null, true, 10 },
                    { 16, true, false, "Clinical Observership", true, "91", null, true, 9 },
                    { 13, true, false, "Educational - Postgraduate Resident Elective", true, "91", null, true, 15 },
                    { 22, true, false, "Surgical Assistant", true, "91", null, true, 8 },
                    { 8, true, true, "Conditional - Practice Limitations", true, "91", null, true, 6 },
                    { 9, true, true, "Conditional - Practice Setting", true, "91", null, true, 5 },
                    { 6, true, false, "Provisional - Specialty", true, "91", null, true, 4 },
                    { 5, true, false, "Provisional - Family", true, "91", null, true, 3 },
                    { 2, true, false, "Full - Specialty", true, "91", null, true, 2 },
                    { 29, true, false, "Pharmacy Technician", false, "T9", null, false, 6 },
                    { 10, true, true, "Conditional - Disciplined", true, "91", null, true, 7 },
                    { 14, true, false, "Educational - Postgraduate Fellow", true, "91", null, true, 16 },
                    { 1, true, false, "Full - Family", true, "91", null, true, 1 },
                    { 11, true, false, "Educational - Medical Student", false, "91", null, true, 18 },
                    { 27, true, false, "Temporary Pharmacist", true, "P1", null, true, 4 },
                    { 28, true, false, "Student Pharmacist", false, "P1", null, false, 3 },
                    { 26, true, false, "Limited Pharmacist", true, "P1", null, true, 2 },
                    { 67, true, true, "Conditional - Podiatric Surgeon Disciplined", false, "93", null, true, 28 },
                    { 66, true, true, "Educational - Podiatric Surgeon Resident (Elective)", false, "93", null, true, 27 },
                    { 65, true, true, "Educational - Podiatric Surgeon Student (Elective)", false, "93", null, true, 26 },
                    { 25, true, false, "Full Pharmacist", true, "P1", null, true, 1 },
                    { 21, false, true, "Temporarily Inactive", false, "91", null, true, 24 },
                    { 19, true, false, "Emergency - Specialty", true, "91", null, true, 23 },
                    { 18, true, false, "Emergency - Family", true, "91", null, true, 22 },
                    { 24, true, true, "Assessment", true, "91", null, true, 21 },
                    { 20, false, true, "Retired - Life ", false, "91", null, true, 20 },
                    { 23, false, true, "Administrative", false, "91", null, true, 19 },
                    { 59, true, true, "Podiatric Surgeon", false, "93", null, true, 25 },
                    { 15, true, false, "Educational - Postgraduate Trainee", true, "91", null, true, 17 }
                });

            migrationBuilder.InsertData(
                table: "PracticeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 1, "Remote Practice" },
                    { 2, "Reproductive Health - Sexually Transmitted Infections" },
                    { 3, "Reproductive Health - Contraceptive Management" },
                    { 4, "First Call" }
                });

            migrationBuilder.InsertData(
                table: "PrivilegeTypeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 1, "Allowable Role" },
                    { 2, "Allowable Transaction" }
                });

            migrationBuilder.InsertData(
                table: "SelfDeclarationTypeLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 3, "Has Disciplinary Action" },
                    { 4, "Has PharmaNet Suspended" },
                    { 2, "Has Registration Suspended" },
                    { 1, "Has Conviction" }
                });

            migrationBuilder.InsertData(
                table: "StatusLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 1, "Editable" },
                    { 2, "Under Review" },
                    { 3, "Requires TOA" },
                    { 4, "Locked" },
                    { 5, "Declined" }
                });

            migrationBuilder.InsertData(
                table: "StatusReasonLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 16, "Terms of Access to be determined by an Adjudicator" },
                    { 15, "User has Requested Remote Access" },
                    { 14, "User authenticated with a method other than BC Services Card" },
                    { 13, "User does not have high enough identity assurance level" },
                    { 12, "Admin has flagged the applicant for manual adjudication" },
                    { 11, "Contact Address or Identity Address not in British Columbia" },
                    { 10, "Answered one or more Self Declaration questions \"Yes\"" },
                    { 9, "Licence Class requires manual adjudication" },
                    { 5, "Name discrepancy in PharmaNet practitioner table" },
                    { 7, "Listed as Non-Practicing in PharmaNet practitioner table" },
                    { 6, "Birthdate discrepancy in PharmaNet practitioner table" },
                    { 4, "College License or Practitioner ID not in PharmaNet table" },
                    { 3, "PharmaNet Error, License could not be validated" },
                    { 2, "Manually Adjudicated" },
                    { 1, "Automatically Adjudicated" },
                    { 8, "Insulin Pump Provider" },
                    { 17, "No address from BCSC. Enrollee entered address." }
                });

            migrationBuilder.InsertData(
                table: "CollegeLicense",
                columns: new[] { "CollegeCode", "LicenseCode" },
                values: new object[,]
                {
                    { 1, 6 },
                    { 1, 65 },
                    { 1, 66 },
                    { 1, 67 },
                    { 2, 25 },
                    { 2, 26 },
                    { 2, 28 },
                    { 2, 27 },
                    { 2, 30 },
                    { 1, 59 },
                    { 2, 29 },
                    { 2, 68 },
                    { 3, 47 },
                    { 3, 48 },
                    { 3, 50 },
                    { 3, 51 },
                    { 3, 49 },
                    { 3, 32 },
                    { 3, 33 },
                    { 2, 31 },
                    { 3, 38 },
                    { 1, 21 },
                    { 1, 18 },
                    { 1, 5 },
                    { 1, 2 },
                    { 1, 1 },
                    { 1, 10 },
                    { 1, 22 },
                    { 1, 16 },
                    { 1, 7 },
                    { 1, 4 },
                    { 1, 19 },
                    { 1, 3 },
                    { 1, 12 },
                    { 1, 13 },
                    { 1, 14 },
                    { 1, 15 },
                    { 1, 11 },
                    { 1, 23 },
                    { 1, 20 },
                    { 1, 24 },
                    { 1, 17 },
                    { 3, 39 },
                    { 3, 34 },
                    { 3, 40 },
                    { 1, 8 },
                    { 5, 64 },
                    { 6, 64 },
                    { 7, 64 },
                    { 8, 64 },
                    { 9, 64 },
                    { 10, 64 },
                    { 11, 64 },
                    { 12, 64 },
                    { 13, 64 },
                    { 14, 64 },
                    { 15, 64 },
                    { 16, 64 },
                    { 17, 64 },
                    { 18, 64 },
                    { 3, 63 },
                    { 3, 62 },
                    { 4, 64 },
                    { 3, 60 },
                    { 3, 35 },
                    { 3, 36 },
                    { 3, 37 },
                    { 3, 41 },
                    { 3, 61 },
                    { 3, 44 },
                    { 3, 45 },
                    { 3, 43 },
                    { 3, 42 },
                    { 3, 52 },
                    { 3, 53 },
                    { 3, 55 },
                    { 3, 56 },
                    { 3, 54 },
                    { 3, 57 },
                    { 3, 58 },
                    { 3, 46 },
                    { 1, 9 }
                });

            migrationBuilder.InsertData(
                table: "CollegePractice",
                columns: new[] { "CollegeCode", "PracticeCode" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "PrivilegeGroupLookup",
                columns: new[] { "Code", "Name", "PrivilegeTypeCode" },
                values: new object[,]
                {
                    { 5, "RU That Can Have OBOs", 1 },
                    { 1, "Submit and Access Claims", 2 },
                    { 3, "Access Medical History", 2 },
                    { 2, "Record Medical History", 2 }
                });

            migrationBuilder.InsertData(
                table: "ProvinceLookup",
                columns: new[] { "Code", "CountryCode", "Name" },
                values: new object[,]
                {
                    { "WY", "US", "Wyoming" },
                    { "CA", "US", "California" },
                    { "CO", "US", "Colorado" },
                    { "CT", "US", "Connecticut" },
                    { "DE", "US", "Delaware" },
                    { "DC", "US", "District of Columbia" },
                    { "FL", "US", "Florida" },
                    { "GA", "US", "Georgia" },
                    { "GU", "US", "Guam" },
                    { "HI", "US", "Hawaii" },
                    { "ID", "US", "Idaho" },
                    { "IN", "US", "Indiana" },
                    { "IA", "US", "Iowa" },
                    { "KS", "US", "Kansas" },
                    { "AR", "US", "Arkansas" },
                    { "KY", "US", "Kentucky" },
                    { "AZ", "US", "Arizona" },
                    { "AK", "US", "Alaska" },
                    { "AB", "CA", "Alberta" },
                    { "BC", "CA", "British Columbia" },
                    { "MB", "CA", "Manitoba" },
                    { "NB", "CA", "New Brunswick" },
                    { "NL", "CA", "Newfoundland and Labrador" },
                    { "NS", "CA", "Nova Scotia" },
                    { "AS", "US", "American Samoa" },
                    { "ON", "CA", "Ontario" },
                    { "QC", "CA", "Quebec" },
                    { "SK", "CA", "Saskatchewan" },
                    { "NT", "CA", "Northwest Territories" },
                    { "NU", "CA", "Nunavut" },
                    { "YT", "CA", "Yukon" },
                    { "AL", "US", "Alabama" },
                    { "PE", "CA", "Prince Edward Island" },
                    { "LA", "US", "Louisiana" },
                    { "IL", "US", "Illinois" },
                    { "MD", "US", "Maryland" },
                    { "PR", "US", "Puerto Rico" },
                    { "RI", "US", "Rhode Island" },
                    { "SC", "US", "South Carolina" },
                    { "ME", "US", "Maine" },
                    { "TN", "US", "Tennessee" },
                    { "TX", "US", "Texas" },
                    { "PA", "US", "Pennsylvania" },
                    { "UM", "US", "United States Minor Outlying Islands" },
                    { "VT", "US", "Vermont" },
                    { "VI", "US", "Virgin Islands, U.S." },
                    { "VA", "US", "Virginia" },
                    { "WA", "US", "Washington" },
                    { "WV", "US", "West Virginia" },
                    { "WI", "US", "Wisconsin" },
                    { "UT", "US", "Utah" },
                    { "OR", "US", "Oregon" },
                    { "SD", "US", "South Dakota" },
                    { "OH", "US", "Ohio" },
                    { "MA", "US", "Massachusetts" },
                    { "OK", "US", "Oklahoma" },
                    { "MN", "US", "Minnesota" },
                    { "MS", "US", "Mississippi" },
                    { "MO", "US", "Missouri" },
                    { "MT", "US", "Montana" },
                    { "NE", "US", "Nebraska" },
                    { "MI", "US", "Michigan" },
                    { "NH", "US", "New Hampshire" },
                    { "NJ", "US", "New Jersey" },
                    { "NM", "US", "New Mexico" },
                    { "NY", "US", "New York" },
                    { "NC", "US", "North Carolina" },
                    { "ND", "US", "North Dakota" },
                    { "MP", "US", "Northern Mariana Islands" },
                    { "NV", "US", "Nevada" }
                });

            migrationBuilder.InsertData(
                table: "VendorLookup",
                columns: new[] { "Code", "CareSettingCode", "Email", "Name" },
                values: new object[,]
                {
                    { 2, 2, "support@excelleris.com", "Excelleris" },
                    { 3, 2, "help@iclinicemr.com", "iClinic" },
                    { 4, 2, "prime@medinet.ca", "Medinet" },
                    { 5, 2, "service@plexia.ca", "Plexia" },
                    { 6, 3, "", "PharmaClik" },
                    { 10, 3, "", "WinRx" },
                    { 8, 3, "", "Kroll" },
                    { 9, 3, "", "Assyst Rx-A" },
                    { 11, 3, "", "Shoppers Drug Mart HealthWatch NG" },
                    { 12, 3, "", "Commander Group" },
                    { 13, 3, "", "BDM" },
                    { 7, 3, "", "Nexxsys" },
                    { 1, 2, "CareConnect@phsa.ca", "CareConnect" }
                });

            migrationBuilder.InsertData(
                table: "Privilege",
                columns: new[] { "Id", "Description", "PrivilegeGroupCode", "TransactionType" },
                values: new object[,]
                {
                    { 19, "Regulated User that can have OBOs", 5, "RU with OBOs" },
                    { 14, "Most Recent Profile", 3, "TBR" },
                    { 13, "Pt Profile Request", 3, "TRP" },
                    { 12, "Name Search", 3, "TPN" },
                    { 11, "Prescriber Details", 3, "TIP" },
                    { 10, "Location Details", 3, "TIL" },
                    { 9, "Patient Details", 3, "TID" },
                    { 15, "Filled Elsewhere Profile", 3, "TRS" },
                    { 8, "Drug Monograph", 3, "TDR" },
                    { 6, "Address Update", 2, "TPA" },
                    { 5, "New PHN", 2, "TPH" },
                    { 4, "Maintain Pt Keyword", 1, "TCP" },
                    { 3, "Pt Profile Mail Request", 1, "TPM" },
                    { 2, "Query Claims History", 1, "TDT" },
                    { 1, "Update Claims History", 1, "TAC" },
                    { 7, "Medication Update", 2, "TMU" },
                    { 16, "DUE Inquiry", 3, "TDU" }
                });

            migrationBuilder.InsertData(
                table: "DefaultPrivilege",
                columns: new[] { "PrivilegeId", "LicenseCode" },
                values: new object[,]
                {
                    { 19, 25 },
                    { 12, 22 },
                    { 12, 17 },
                    { 12, 24 },
                    { 12, 19 },
                    { 12, 18 },
                    { 12, 15 },
                    { 12, 14 },
                    { 12, 13 },
                    { 12, 47 },
                    { 12, 12 },
                    { 12, 9 },
                    { 12, 8 },
                    { 12, 7 },
                    { 12, 6 },
                    { 12, 5 },
                    { 12, 4 },
                    { 12, 3 },
                    { 12, 2 },
                    { 12, 10 },
                    { 12, 48 },
                    { 12, 50 },
                    { 12, 51 },
                    { 13, 15 },
                    { 13, 14 },
                    { 13, 13 },
                    { 13, 12 },
                    { 13, 10 },
                    { 13, 9 },
                    { 13, 8 },
                    { 13, 7 },
                    { 13, 6 },
                    { 13, 5 },
                    { 13, 4 },
                    { 13, 3 },
                    { 13, 2 },
                    { 13, 1 },
                    { 13, 28 },
                    { 13, 27 },
                    { 13, 26 },
                    { 13, 25 },
                    { 12, 58 },
                    { 12, 1 },
                    { 12, 28 },
                    { 12, 27 },
                    { 12, 26 },
                    { 11, 2 },
                    { 11, 1 },
                    { 11, 28 },
                    { 11, 27 },
                    { 11, 26 },
                    { 11, 25 },
                    { 10, 58 },
                    { 10, 51 },
                    { 10, 50 },
                    { 10, 48 },
                    { 10, 47 },
                    { 10, 22 },
                    { 10, 17 },
                    { 10, 24 },
                    { 10, 19 },
                    { 10, 18 },
                    { 10, 15 },
                    { 10, 14 },
                    { 10, 13 },
                    { 11, 3 },
                    { 13, 18 },
                    { 11, 4 },
                    { 11, 6 },
                    { 12, 25 },
                    { 11, 58 },
                    { 11, 51 },
                    { 11, 50 },
                    { 11, 48 },
                    { 11, 47 },
                    { 11, 22 },
                    { 11, 17 },
                    { 11, 24 },
                    { 11, 19 },
                    { 11, 18 },
                    { 11, 15 },
                    { 11, 14 },
                    { 11, 13 },
                    { 11, 12 },
                    { 11, 10 },
                    { 11, 9 },
                    { 11, 8 },
                    { 11, 7 },
                    { 11, 5 },
                    { 13, 19 },
                    { 13, 24 },
                    { 13, 17 },
                    { 16, 27 },
                    { 16, 26 },
                    { 16, 25 },
                    { 15, 58 },
                    { 15, 51 },
                    { 15, 50 },
                    { 15, 48 },
                    { 15, 47 },
                    { 15, 22 },
                    { 15, 17 },
                    { 15, 24 },
                    { 15, 19 },
                    { 15, 18 },
                    { 15, 15 },
                    { 15, 14 },
                    { 15, 13 },
                    { 15, 12 },
                    { 15, 10 },
                    { 15, 9 },
                    { 16, 28 },
                    { 15, 8 },
                    { 16, 1 },
                    { 16, 3 },
                    { 16, 50 },
                    { 16, 48 },
                    { 16, 47 },
                    { 16, 22 },
                    { 16, 17 },
                    { 16, 24 },
                    { 16, 19 },
                    { 16, 18 },
                    { 16, 15 },
                    { 16, 14 },
                    { 16, 13 },
                    { 16, 12 },
                    { 16, 10 },
                    { 16, 9 },
                    { 16, 8 },
                    { 16, 7 },
                    { 16, 6 },
                    { 16, 5 },
                    { 16, 4 },
                    { 16, 2 },
                    { 10, 12 },
                    { 15, 7 },
                    { 15, 5 },
                    { 14, 9 },
                    { 14, 8 },
                    { 14, 7 },
                    { 14, 6 },
                    { 14, 5 },
                    { 14, 4 },
                    { 14, 3 },
                    { 14, 2 },
                    { 14, 1 },
                    { 14, 28 },
                    { 14, 27 },
                    { 14, 26 },
                    { 14, 25 },
                    { 13, 58 },
                    { 13, 51 },
                    { 13, 50 },
                    { 13, 48 },
                    { 13, 47 },
                    { 13, 22 },
                    { 14, 10 },
                    { 15, 6 },
                    { 14, 12 },
                    { 14, 14 },
                    { 15, 4 },
                    { 15, 3 },
                    { 15, 2 },
                    { 15, 1 },
                    { 15, 28 },
                    { 15, 27 },
                    { 15, 26 },
                    { 15, 25 },
                    { 14, 58 },
                    { 14, 51 },
                    { 14, 50 },
                    { 14, 48 },
                    { 14, 47 },
                    { 14, 22 },
                    { 14, 17 },
                    { 14, 24 },
                    { 14, 19 },
                    { 14, 18 },
                    { 14, 15 },
                    { 14, 13 },
                    { 16, 51 },
                    { 10, 10 },
                    { 10, 8 },
                    { 6, 28 },
                    { 6, 27 },
                    { 6, 26 },
                    { 6, 25 },
                    { 5, 58 },
                    { 5, 51 },
                    { 5, 50 },
                    { 5, 48 },
                    { 6, 1 },
                    { 5, 47 },
                    { 5, 19 },
                    { 5, 18 },
                    { 5, 15 },
                    { 5, 14 },
                    { 5, 13 },
                    { 5, 12 },
                    { 5, 10 },
                    { 5, 9 },
                    { 5, 24 },
                    { 6, 2 },
                    { 6, 3 },
                    { 6, 4 },
                    { 7, 25 },
                    { 6, 58 },
                    { 6, 51 },
                    { 6, 50 },
                    { 6, 48 },
                    { 6, 47 },
                    { 6, 24 },
                    { 6, 19 },
                    { 6, 18 },
                    { 6, 15 },
                    { 6, 14 },
                    { 6, 13 },
                    { 6, 12 },
                    { 6, 10 },
                    { 6, 9 },
                    { 6, 8 },
                    { 6, 7 },
                    { 6, 6 },
                    { 6, 5 },
                    { 5, 8 },
                    { 5, 7 },
                    { 5, 6 },
                    { 5, 5 },
                    { 1, 25 },
                    { 19, 51 },
                    { 19, 48 },
                    { 19, 47 },
                    { 19, 17 },
                    { 19, 19 },
                    { 19, 18 },
                    { 19, 14 },
                    { 19, 13 },
                    { 19, 12 },
                    { 19, 9 },
                    { 19, 6 },
                    { 19, 5 },
                    { 19, 4 },
                    { 19, 3 },
                    { 19, 2 },
                    { 19, 1 },
                    { 19, 27 },
                    { 19, 26 },
                    { 1, 26 },
                    { 7, 26 },
                    { 1, 27 },
                    { 2, 25 },
                    { 5, 4 },
                    { 5, 3 },
                    { 5, 2 },
                    { 5, 1 },
                    { 5, 28 },
                    { 5, 27 },
                    { 5, 26 },
                    { 5, 25 },
                    { 4, 28 },
                    { 4, 27 },
                    { 4, 26 },
                    { 4, 25 },
                    { 3, 28 },
                    { 3, 27 },
                    { 3, 26 },
                    { 3, 25 },
                    { 2, 28 },
                    { 2, 27 },
                    { 2, 26 },
                    { 1, 28 },
                    { 7, 27 },
                    { 7, 28 },
                    { 7, 1 },
                    { 9, 13 },
                    { 9, 12 },
                    { 9, 10 },
                    { 9, 9 },
                    { 9, 8 },
                    { 9, 7 },
                    { 9, 6 },
                    { 9, 5 },
                    { 9, 4 },
                    { 9, 3 },
                    { 9, 2 },
                    { 9, 1 },
                    { 9, 28 },
                    { 9, 27 },
                    { 9, 26 },
                    { 9, 25 },
                    { 8, 58 },
                    { 8, 51 },
                    { 8, 50 },
                    { 9, 14 },
                    { 8, 48 },
                    { 9, 15 },
                    { 9, 19 },
                    { 10, 7 },
                    { 10, 6 },
                    { 10, 5 },
                    { 10, 4 },
                    { 10, 3 },
                    { 10, 2 },
                    { 10, 1 },
                    { 10, 28 },
                    { 10, 27 },
                    { 10, 26 },
                    { 10, 25 },
                    { 9, 58 },
                    { 9, 51 },
                    { 9, 50 },
                    { 9, 48 },
                    { 9, 47 },
                    { 9, 22 },
                    { 9, 17 },
                    { 9, 24 },
                    { 9, 18 },
                    { 10, 9 },
                    { 8, 47 },
                    { 8, 17 },
                    { 7, 50 },
                    { 7, 48 },
                    { 7, 47 },
                    { 7, 24 },
                    { 7, 19 },
                    { 7, 18 },
                    { 7, 15 },
                    { 7, 14 },
                    { 7, 13 },
                    { 7, 12 },
                    { 7, 10 },
                    { 7, 9 },
                    { 7, 8 },
                    { 7, 7 },
                    { 7, 6 },
                    { 7, 5 },
                    { 7, 4 },
                    { 7, 3 },
                    { 7, 2 },
                    { 7, 51 },
                    { 8, 22 },
                    { 7, 58 },
                    { 8, 26 },
                    { 8, 24 },
                    { 8, 19 },
                    { 8, 18 },
                    { 8, 15 },
                    { 8, 14 },
                    { 8, 13 },
                    { 8, 12 },
                    { 8, 10 },
                    { 8, 9 },
                    { 8, 8 },
                    { 8, 7 },
                    { 8, 6 },
                    { 8, 5 },
                    { 8, 4 },
                    { 8, 3 },
                    { 8, 2 },
                    { 8, 1 },
                    { 8, 28 },
                    { 8, 27 },
                    { 8, 25 },
                    { 16, 58 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessAgreementNote_AdjudicatorId",
                table: "AccessAgreementNote",
                column: "AdjudicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessAgreementNote_EnrolleeId",
                table: "AccessAgreementNote",
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
                name: "IX_Admin_UserId",
                table: "Admin",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_AgreementVersionId",
                table: "Agreement",
                column: "AgreementVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_EnrolleeId",
                table: "Agreement",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_LimitsConditionsClauseId",
                table: "Agreement",
                column: "LimitsConditionsClauseId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_OrganizationId",
                table: "Agreement",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_PartyId",
                table: "Agreement",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedPrivilege_EnrolleeId",
                table: "AssignedPrivilege",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedUsers_HealthAuthorityCode",
                table: "AuthorizedUsers",
                column: "HealthAuthorityCode");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizedUsers_PartyId",
                table: "AuthorizedUsers",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessDay_SiteId",
                table: "BusinessDay",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEvent_AdminId",
                table: "BusinessEvent",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEvent_BusinessEventTypeCode",
                table: "BusinessEvent",
                column: "BusinessEventTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEvent_EnrolleeId",
                table: "BusinessEvent",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEvent_OrganizationId",
                table: "BusinessEvent",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEvent_PartyId",
                table: "BusinessEvent",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessEvent_SiteId",
                table: "BusinessEvent",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLicence_SiteId",
                table: "BusinessLicence",
                column: "SiteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLicenceDocument_BusinessLicenceId",
                table: "BusinessLicenceDocument",
                column: "BusinessLicenceId",
                unique: true);

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
                name: "IX_Contact_PhysicalAddressId",
                table: "Contact",
                column: "PhysicalAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultPrivilege_LicenseCode",
                table: "DefaultPrivilege",
                column: "LicenseCode");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_EmailType",
                table: "EmailTemplate",
                column: "EmailType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_AdjudicatorId",
                table: "Enrollee",
                column: "AdjudicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_GPID",
                table: "Enrollee",
                column: "GPID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_HPDID",
                table: "Enrollee",
                column: "HPDID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollee_UserId",
                table: "Enrollee",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeAddress_AddressId",
                table: "EnrolleeAddress",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeAddress_EnrolleeId_AddressId",
                table: "EnrolleeAddress",
                columns: new[] { "EnrolleeId", "AddressId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeAdjudicationDocument_AdjudicatorId",
                table: "EnrolleeAdjudicationDocument",
                column: "AdjudicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeAdjudicationDocument_EnrolleeId",
                table: "EnrolleeAdjudicationDocument",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeCareSetting_CareSettingCode",
                table: "EnrolleeCareSetting",
                column: "CareSettingCode");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeCareSetting_EnrolleeId",
                table: "EnrolleeCareSetting",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeCredential_CredentialId",
                table: "EnrolleeCredential",
                column: "CredentialId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeCredential_EnrolleeId",
                table: "EnrolleeCredential",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeHealthAuthority_EnrolleeId",
                table: "EnrolleeHealthAuthority",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeHealthAuthority_HealthAuthorityCode",
                table: "EnrolleeHealthAuthority",
                column: "HealthAuthorityCode");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeNote_AdjudicatorId",
                table: "EnrolleeNote",
                column: "AdjudicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeNote_EnrolleeId",
                table: "EnrolleeNote",
                column: "EnrolleeId");

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
                name: "IX_EnrolleeRemoteUser_EnrolleeId",
                table: "EnrolleeRemoteUser",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolleeRemoteUser_RemoteUserId",
                table: "EnrolleeRemoteUser",
                column: "RemoteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentCertificateAccessToken_EnrolleeId",
                table: "EnrolmentCertificateAccessToken",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatus_EnrolleeId",
                table: "EnrolmentStatus",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatus_StatusCode",
                table: "EnrolmentStatus",
                column: "StatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatusReason_EnrolmentStatusId",
                table: "EnrolmentStatusReason",
                column: "EnrolmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatusReason_StatusReasonCode",
                table: "EnrolmentStatusReason",
                column: "StatusReasonCode");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatusReference_AdjudicatorNoteId",
                table: "EnrolmentStatusReference",
                column: "AdjudicatorNoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatusReference_AdminId",
                table: "EnrolmentStatusReference",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolmentStatusReference_EnrolmentStatusId",
                table: "EnrolmentStatusReference",
                column: "EnrolmentStatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GisEnrolment_PartyId",
                table: "GisEnrolment",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationDocument_EnrolleeId",
                table: "IdentificationDocument",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_EnrolleeId",
                table: "Job",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_OboSite_CareSettingCode",
                table: "OboSite",
                column: "CareSettingCode");

            migrationBuilder.CreateIndex(
                name: "IX_OboSite_EnrolleeId",
                table: "OboSite",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_OboSite_HealthAuthorityCode",
                table: "OboSite",
                column: "HealthAuthorityCode");

            migrationBuilder.CreateIndex(
                name: "IX_OboSite_PhysicalAddressId",
                table: "OboSite",
                column: "PhysicalAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_SigningAuthorityId",
                table: "Organization",
                column: "SigningAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_UserId",
                table: "Party",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddress_AddressId",
                table: "PartyAddress",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddress_PartyId_AddressId",
                table: "PartyAddress",
                columns: new[] { "PartyId", "AddressId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartyEnrolment_PartyId",
                table: "PartyEnrolment",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PlrProvider_Ipc",
                table: "PlrProvider",
                column: "Ipc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Privilege_PrivilegeGroupCode",
                table: "Privilege",
                column: "PrivilegeGroupCode");

            migrationBuilder.CreateIndex(
                name: "IX_PrivilegeGroupLookup_PrivilegeTypeCode",
                table: "PrivilegeGroupLookup",
                column: "PrivilegeTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceLookup_CountryCode",
                table: "ProvinceLookup",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteAccessLocation_EnrolleeId",
                table: "RemoteAccessLocation",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteAccessLocation_PhysicalAddressId",
                table: "RemoteAccessLocation",
                column: "PhysicalAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteAccessSite_EnrolleeId",
                table: "RemoteAccessSite",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteAccessSite_SiteId",
                table: "RemoteAccessSite",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteUser_SiteId",
                table: "RemoteUser",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteUserCertification_CollegeCode",
                table: "RemoteUserCertification",
                column: "CollegeCode");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteUserCertification_LicenseCode",
                table: "RemoteUserCertification",
                column: "LicenseCode");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteUserCertification_RemoteUserId",
                table: "RemoteUserCertification",
                column: "RemoteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclaration_EnrolleeId",
                table: "SelfDeclaration",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclaration_SelfDeclarationTypeCode",
                table: "SelfDeclaration",
                column: "SelfDeclarationTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclarationDocument_EnrolleeId",
                table: "SelfDeclarationDocument",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_SelfDeclarationDocument_SelfDeclarationTypeCode",
                table: "SelfDeclarationDocument",
                column: "SelfDeclarationTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_SignedAgreementDocument_AgreementId",
                table: "SignedAgreementDocument",
                column: "AgreementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Site_AdjudicatorId",
                table: "Site",
                column: "AdjudicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_AdministratorPharmaNetId",
                table: "Site",
                column: "AdministratorPharmaNetId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_CareSettingCode",
                table: "Site",
                column: "CareSettingCode");

            migrationBuilder.CreateIndex(
                name: "IX_Site_OrganizationId",
                table: "Site",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_PhysicalAddressId",
                table: "Site",
                column: "PhysicalAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_PrivacyOfficerId",
                table: "Site",
                column: "PrivacyOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_ProvisionerId",
                table: "Site",
                column: "ProvisionerId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_TechnicalSupportId",
                table: "Site",
                column: "TechnicalSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAdjudicationDocument_AdjudicatorId",
                table: "SiteAdjudicationDocument",
                column: "AdjudicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAdjudicationDocument_SiteId",
                table: "SiteAdjudicationDocument",
                column: "SiteId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SiteRegistrationNote_AdjudicatorId",
                table: "SiteRegistrationNote",
                column: "AdjudicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteRegistrationNote_SiteId",
                table: "SiteRegistrationNote",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteRegistrationReviewDocument_SiteId",
                table: "SiteRegistrationReviewDocument",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteVendor_SiteId",
                table: "SiteVendor",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteVendor_VendorCode",
                table: "SiteVendor",
                column: "VendorCode");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_EnrolleeId",
                table: "Submission",
                column: "EnrolleeId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorLookup_CareSettingCode",
                table: "VendorLookup",
                column: "CareSettingCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessAgreementNote");

            migrationBuilder.DropTable(
                name: "AssignedPrivilege");

            migrationBuilder.DropTable(
                name: "AuthorizedUsers");

            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "BusinessDay");

            migrationBuilder.DropTable(
                name: "BusinessEvent");

            migrationBuilder.DropTable(
                name: "BusinessLicenceDocument");

            migrationBuilder.DropTable(
                name: "Certification");

            migrationBuilder.DropTable(
                name: "CollegeLicense");

            migrationBuilder.DropTable(
                name: "CollegePractice");

            migrationBuilder.DropTable(
                name: "DefaultPrivilege");

            migrationBuilder.DropTable(
                name: "DocumentAccessToken");

            migrationBuilder.DropTable(
                name: "EmailLog");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "EnrolleeAddress");

            migrationBuilder.DropTable(
                name: "EnrolleeAdjudicationDocument");

            migrationBuilder.DropTable(
                name: "EnrolleeCareSetting");

            migrationBuilder.DropTable(
                name: "EnrolleeCredential");

            migrationBuilder.DropTable(
                name: "EnrolleeHealthAuthority");

            migrationBuilder.DropTable(
                name: "EnrolleeNotification");

            migrationBuilder.DropTable(
                name: "EnrolleeRemoteUser");

            migrationBuilder.DropTable(
                name: "EnrolmentCertificateAccessToken");

            migrationBuilder.DropTable(
                name: "EnrolmentStatusReason");

            migrationBuilder.DropTable(
                name: "EnrolmentStatusReference");

            migrationBuilder.DropTable(
                name: "FacilityLookup");

            migrationBuilder.DropTable(
                name: "GisEnrolment");

            migrationBuilder.DropTable(
                name: "IdentificationDocument");

            migrationBuilder.DropTable(
                name: "IdentifierTypeLookup");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "JobNameLookup");

            migrationBuilder.DropTable(
                name: "OboSite");

            migrationBuilder.DropTable(
                name: "PartyAddress");

            migrationBuilder.DropTable(
                name: "PartyEnrolment");

            migrationBuilder.DropTable(
                name: "PlrProvider");

            migrationBuilder.DropTable(
                name: "PreApprovedRegistration");

            migrationBuilder.DropTable(
                name: "RemoteAccessLocation");

            migrationBuilder.DropTable(
                name: "RemoteAccessSite");

            migrationBuilder.DropTable(
                name: "RemoteUserCertification");

            migrationBuilder.DropTable(
                name: "SelfDeclaration");

            migrationBuilder.DropTable(
                name: "SelfDeclarationDocument");

            migrationBuilder.DropTable(
                name: "SignedAgreementDocument");

            migrationBuilder.DropTable(
                name: "SiteAdjudicationDocument");

            migrationBuilder.DropTable(
                name: "SiteNotification");

            migrationBuilder.DropTable(
                name: "SiteRegistrationReviewDocument");

            migrationBuilder.DropTable(
                name: "SiteVendor");

            migrationBuilder.DropTable(
                name: "Submission");

            migrationBuilder.DropTable(
                name: "BusinessEventTypeLookup");

            migrationBuilder.DropTable(
                name: "BusinessLicence");

            migrationBuilder.DropTable(
                name: "PracticeLookup");

            migrationBuilder.DropTable(
                name: "Privilege");

            migrationBuilder.DropTable(
                name: "Credential");

            migrationBuilder.DropTable(
                name: "StatusReasonLookup");

            migrationBuilder.DropTable(
                name: "EnrolleeNote");

            migrationBuilder.DropTable(
                name: "EnrolmentStatus");

            migrationBuilder.DropTable(
                name: "HealthAuthorityLookup");

            migrationBuilder.DropTable(
                name: "CollegeLookup");

            migrationBuilder.DropTable(
                name: "LicenseLookup");

            migrationBuilder.DropTable(
                name: "RemoteUser");

            migrationBuilder.DropTable(
                name: "SelfDeclarationTypeLookup");

            migrationBuilder.DropTable(
                name: "Agreement");

            migrationBuilder.DropTable(
                name: "SiteRegistrationNote");

            migrationBuilder.DropTable(
                name: "VendorLookup");

            migrationBuilder.DropTable(
                name: "PrivilegeGroupLookup");

            migrationBuilder.DropTable(
                name: "StatusLookup");

            migrationBuilder.DropTable(
                name: "AgreementVersion");

            migrationBuilder.DropTable(
                name: "Enrollee");

            migrationBuilder.DropTable(
                name: "LimitsConditionsClause");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "PrivilegeTypeLookup");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "CareSettingLookup");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Party");

            migrationBuilder.DropTable(
                name: "ProvinceLookup");

            migrationBuilder.DropTable(
                name: "CountryLookup");
        }
    }
}
