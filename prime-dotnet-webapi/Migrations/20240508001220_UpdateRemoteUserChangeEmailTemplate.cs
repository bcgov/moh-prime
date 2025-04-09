using Microsoft.EntityFrameworkCore.Migrations;

namespace Prime.Migrations
{
    public partial class UpdateRemoteUserChangeEmailTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Template" },
                values: new object[] { "The email will be triggered when the remote user has been updated and the site is re-submitted.", "Hello, <p><p>@{ var pecText = string.IsNullOrWhiteSpace(Model.SitePec) ? \"Not Assigned\" : Model.SitePec; } <p> Notification: The list of Remote Practitioners at @Model.SiteStreetAddress of @Model.OrganizationName (PEC: @pecText) has been updated.</p> <h2 class=\"mb-2\">Remote Users</h2> @foreach (var remoteUser in Model.RemoteUsers) { <p> <span class=\"ml-2\">@remoteUser</span> </p> } <h2 class=\"mb-2\">Site Information</h2> <p> See the attached registration and organization agreement for more information.</p> <br/>Thank you, <br/><br/>PRIME Support team <br/>1-844-397-7463<br/> PRIMESupport@gov.bc.ca" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplate",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Template" },
                values: new object[] { "The email will be triggered when the remote user has been updated and the site is completed.", "Hello, <p><p>@{ var pecText = string.IsNullOrWhiteSpace(Model.SitePec) ? \"Not Assigned\" : Model.SitePec; } <p> Notification: The list of Remote Practitioners at @Model.SiteStreetAddress of @Model.OrganizationName (PEC: @pecText) has been updated. <br/><br/>The remote practitioners at this site are: </p> <h2 class=\"mb-2\">Remote Users</h2> @foreach (var name in Model.RemoteUserNames) { <div class=\"ml-2 mb-2\"> <h5>Name</h5> <span class=\"ml-2\">@name</span> </div> } <h2 class=\"mb-2\">Site Information</h2> <p> See the attached registration and organization agreement for more information. @if (!string.IsNullOrWhiteSpace(Model.DocumentUrl)) { @(\"To access the Business Licence, click this\") <a href=\"@Model.DocumentUrl\" target=\"_blank\">link</a>@(\".\") } </p> <br/>Thank you, <br/><br/>PRIME Support team <br/>1-844-397-7463<br/> PRIMESupport@gov.bc.ca" });
        }
    }
}
