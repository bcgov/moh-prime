using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Prime.Migrations
{
    public partial class AddPlrProviderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    CollegeId = table.Column<string>(nullable: true),
                    ProviderType = table.Column<string>(nullable: true),
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
                    Address1_Line1 = table.Column<string>(nullable: true),
                    Address1_Line2 = table.Column<string>(nullable: true),
                    Address1_Line3 = table.Column<string>(nullable: true),
                    City1 = table.Column<string>(nullable: true),
                    Province1 = table.Column<string>(nullable: true),
                    Country1 = table.Column<string>(nullable: true),
                    PostalCode1 = table.Column<string>(nullable: true),
                    Address1StartDate = table.Column<DateTime>(nullable: false),
                    Address2_Line1 = table.Column<string>(nullable: true),
                    Address2_Line2 = table.Column<string>(nullable: true),
                    Address2_Line3 = table.Column<string>(nullable: true),
                    City2 = table.Column<string>(nullable: true),
                    Province2 = table.Column<string>(nullable: true),
                    Country2 = table.Column<string>(nullable: true),
                    PostalCode2 = table.Column<string>(nullable: true),
                    Address2StartDate = table.Column<DateTime>(nullable: false),
                    Credentials = table.Column<string>(nullable: true),
                    TelephoneAreaCode = table.Column<string>(nullable: true),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    FaxAreaCode = table.Column<string>(nullable: true),
                    FaxNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlrProvider", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlrProvider");
        }
    }
}
