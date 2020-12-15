# PharmaNet Revisions of Information Managment Enhancements (PRIME) WEBAPI

## Table of Contents

[TOC]

## .NET API

This project can be built and run with the following commands:

```
dotnet publish -c Debug
dotnet bin/Debug/netcoreapp3.1/prime.dll
```

But this requires the .NET Core SDK.

### Installation

The .NET Core SDK can be obtained directly from [Microsoft](https://dotnet.microsoft.com/download)
or third-party package managers such as [Chocolatey](https://chocolatey.org/packages/dotnetcore-sdk).

## Database (PostgreSQL + .NET Core EF)

The database schema and default contents is setup by the command `dotnet ef database update --verbose`
but the expected database server must be running.

### Installation

The database server is provided by the `postgres` service listed in the `docker-compose.yml` file at the root of the repository.

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

- if you have the API running, it will conflict with `dotnet ef database update`
- if while running `dotnet ef database update`, you get the error `"csc.dll" exited with code 137`, it may be because you don't have enough memory (e.g. if running command inside a VM)
