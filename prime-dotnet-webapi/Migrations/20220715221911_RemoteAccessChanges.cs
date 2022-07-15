using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class RemoteAccessChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowRequestRemoteAccess",
                table: "LicenseDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 3,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 4,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AllowRequestRemoteAccess", "Manual" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AllowRequestRemoteAccess", "Manual" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 8,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 9,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 10,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AllowRequestRemoteAccess", "Manual" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AllowRequestRemoteAccess", "Manual" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AllowRequestRemoteAccess", "Manual" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 18,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 19,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 47,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 51,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 106,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 107,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "AllowRequestRemoteAccess", "Manual" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 110,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 111,
                column: "AllowRequestRemoteAccess",
                value: true);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "AllowRequestRemoteAccess", "Manual" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "AllowRequestRemoteAccess", "Manual" },
                values: new object[] { true, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowRequestRemoteAccess",
                table: "LicenseDetail");

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 5,
                column: "Manual",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 6,
                column: "Manual",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 12,
                column: "Manual",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 13,
                column: "Manual",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 14,
                column: "Manual",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 109,
                column: "Manual",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 113,
                column: "Manual",
                value: false);

            migrationBuilder.UpdateData(
                table: "LicenseDetail",
                keyColumn: "Id",
                keyValue: 114,
                column: "Manual",
                value: false);
        }
    }
}
