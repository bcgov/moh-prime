# PharmaNet Revisions of Information Managment Enhancements (PRIME) WEBAPI

## Table of Contents

[TOC]

## .NET API

### Installation

If you are developing on Linux, to avoid this error during the Site Registration user story:
```
System.ComponentModel.Win32Exception (13): Permission denied
   at Wkhtmltopdf.NetCore.WkhtmlDriver.Convert(String wkhtmlPath, String switches, String html)
   at Wkhtmltopdf.NetCore.GeneratePdf.GetPDF(String html)
   at Prime.Services.PdfService.Generate(String htmlContent) in /moh-prime/prime-dotnet-webapi/Services/PdfService.cs:line 16
```

make sure you enable execution permissions for `wkhtmltopdf`, i.e. `/moh-prime/prime-dotnet-webapi/Resources/wkhtmltopdf/Linux$ chmod +x wkhtmltopdf`.
If you have already built the binaries, you will need to re-build to include `wkhtmltopdf` with the correct permissions.


## Database (PostgreSQL + .NET Core EF)

### Installation

### Creating Migration

[.Net Core EF Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

When you have completed updates to the models and want to create a migration for the database run:

```bash
dotnet ef migrations add <MigrationName>
```

Note: you will have to bring down your api if you are running it in debug mode to run migrations.

### Database Update (Running Migrations)

[.Net Core EF Updates](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli#update-the-database)

To run the migrations in your migration use the update command:

```bash
dotnet ef database update
```

Using the same command, but passing in a target migration you can rollback to a specific migration:

```bash
dotnet ef database update <MigrationName>
```

Note: you will have to bring down your api if you are running it in debug mode to run updates.

### Database Seeding

To seed the database for local development use Postman's runner

#### Example: Seeding New Enrollees 
 
For this example a custom endpoint was not created as only 1500+ enrollees were required, and the status of their enrolment was irrelevant so the `CreateEnrollee` endpoint was used.  For this to work the `CreateEnrollee` endpoint was temporarily updated where the authentication and all the checks were commented out in the controller. 

1. Import the latest JSON from Swagger into Postman by either:
   * Copying it from Swagger at [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html), or 
   * Using this link [http://localhost:5000/swagger/v1/swagger.json](http://localhost:5000/swagger/v1/swagger.json) 
1. Create a new Runner in Postman, and pull over the `Enrollees` collection from within the `PRIME Web API` Collection that was created from the import
1. Deselect everything but `CreateEnrollee` endpoint
1. View the `CreateEnrollee` endpoint in Postman where the request body will already be prefilled, and make adjustments as needed. For example, `countryCode` and `provinceCode` will not be correct. See the example request body provided.
1. Added environment variables to the body for `userId` and `hpdid`. See the example request body provided.
1. View the `Pre-req` tab in Postman, and add a script to populate the environment variables.  See the example script provided.
1. View the Runner and set the number of iterations, and click `Start Run`

**Example JSON Body**

```json
{
  "enrollee": {
    "userId": "{{userId}}",
    "hpdid": "{{hpdid}}",
    "firstName": "consequat labore",
    "lastName": "dolore in",
    "givenNames": "eu",
    "dateOfBirth": "2008-03-28T20:59:34.677Z",
    "preferredFirstName": "voluptate",
    "preferredMiddleName": "aliqua cillum quis",
    "preferredLastName": "dolor Excepteur cupidatat commodo",
    "physicalAddress": {
      "countryCode": "CA",
      "provinceCode": "BC",
      "street": "consequat ea laboris",
      "street2": "id ut consequat",
      "city": "aliqua Ut",
      "postal": "M4E2B6"
    },
    "mailingAddress": {
      "countryCode": "CA",
      "provinceCode": "BC",
      "street": "adipisicing elit velit pariatur",
      "street2": "dolor",
      "city": "ut culpa",
      "postal": "M4E2B6"
    },
    "verifiedAddress": {
      "countryCode": "CA",
      "provinceCode": "BC",
      "street": "consequat quis fugiat i",
      "street2": "est",
      "city": "enim esse",
      "postal": "M4E2B6"
    },
    "email": "example@example.com",
    "phone": "5234555432",
    "phoneExtension": "123",
    "smsPhone": "5234555432"
  },
  "identificationDocumentGuid": "46d30232-8152-b1aa-c997-34c8b4b05739"
}
```

**Example Pre-req Script**

```js
var uuid = require('uuid');
pm.environment.set('userId', uuid.v4());
pm.environment.set('hpdid', `${Date.now()}555555${Date.now()}`);
```


