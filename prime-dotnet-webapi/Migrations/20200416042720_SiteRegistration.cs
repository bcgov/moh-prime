using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class SiteRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    LastName = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    JobRoleTitle = table.Column<string>(nullable: true),
                    AddressId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    SMSPhone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Party_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
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
                    Name = table.Column<string>(nullable: true),
                    DoingBusinessAs = table.Column<string>(nullable: true),
                    AcceptedAgreementDate = table.Column<DateTimeOffset>(nullable: true),
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
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    CreatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedUserId = table.Column<Guid>(nullable: false),
                    UpdatedTimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    HoursWeekend = table.Column<bool>(nullable: false),
                    Hours24 = table.Column<bool>(nullable: false),
                    HoursSpecial = table.Column<string>(nullable: true),
                    DoingBusinessAs = table.Column<string>(nullable: true),
                    AddressId = table.Column<int>(nullable: false),
                    AdministratorPharmaNetId = table.Column<int>(nullable: false),
                    PrivacyOfficerId = table.Column<int>(nullable: false),
                    TechnicalSupportId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Location_Party_AdministratorPharmaNetId",
                        column: x => x.AdministratorPharmaNetId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Location_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Location_Party_PrivacyOfficerId",
                        column: x => x.PrivacyOfficerId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Location_Party_TechnicalSupportId",
                        column: x => x.TechnicalSupportId,
                        principalTable: "Party",
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
                    PEC = table.Column<string>(nullable: true),
                    Completed = table.Column<bool>(nullable: false),
                    ApprovedDate = table.Column<DateTimeOffset>(nullable: true),
                    ProvisionerId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    VendorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Site_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Site_Party_ProvisionerId",
                        column: x => x.ProvisionerId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Site_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_AddressId",
                table: "Location",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_AdministratorPharmaNetId",
                table: "Location",
                column: "AdministratorPharmaNetId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_OrganizationId",
                table: "Location",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_PrivacyOfficerId",
                table: "Location",
                column: "PrivacyOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_TechnicalSupportId",
                table: "Location",
                column: "TechnicalSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_SigningAuthorityId",
                table: "Organization",
                column: "SigningAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_AddressId",
                table: "Party",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_LocationId",
                table: "Site",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_ProvisionerId",
                table: "Site",
                column: "ProvisionerId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_VendorId",
                table: "Site",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Party");
        }
    }
}
