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

Make sure you enable execution permissions for `wkhtmltopdf`, i.e. `/moh-prime/prime-dotnet-webapi/Resources/wkhtmltopdf/Linux$ chmod +x wkhtmltopdf` 


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

### TIPS + TRICKS

