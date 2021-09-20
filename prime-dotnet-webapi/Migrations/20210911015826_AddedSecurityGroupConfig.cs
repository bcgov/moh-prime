using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class AddedSecurityGroupConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityGroup",
                table: "HealthAuthoritySite");

            migrationBuilder.AddColumn<int>(
                name: "SecurityGroupCode",
                table: "HealthAuthoritySite",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SecurityGroupLookup",
                columns: table => new
                {
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityGroupLookup", x => x.Code);
                });

            migrationBuilder.InsertData(
                table: "SecurityGroupLookup",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { 1, "EMRMD (EMR - Community-based Clinics)" },
                    { 2, "HAD (Hospital Admitting)" },
                    { 3, "HAI (HA Viewer)" },
                    { 4, "HAP (Hospital Access)" },
                    { 5, "HNF (Emergency Department Access (EDAP))" },
                    { 6, "IP (In-patient Pharmacies - Hospital)" },
                    { 7, "MD (COMPAP)" },
                    { 8, "OP (Hospital Outpatient Pharmacy)" },
                    { 9, "VHA (Cerner Integration Site)" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecurityGroupLookup");

            migrationBuilder.DropColumn(
                name: "SecurityGroupCode",
                table: "HealthAuthoritySite");

            migrationBuilder.AddColumn<string>(
                name: "SecurityGroup",
                table: "HealthAuthoritySite",
                type: "text",
                nullable: true);
        }
    }
}
