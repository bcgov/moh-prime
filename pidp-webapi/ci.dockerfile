FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /app

RUN apk add postgresql-client
ENV DOTNET_CLI_HOME="/DOTNET_CLI_HOME"
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"

RUN dotnet tool install --global dotnet-ef --version 6.0.0-rc.2.21480.5
ENV PATH="$PATH:/DOTNET_CLI_HOME/.dotnet/tools"

COPY *.csproj /app
RUN dotnet restore
COPY . /app

RUN dotnet publish "pidp.csproj" -c Release -o /app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App

RUN dotnet ef migrations script --idempotent --output /app/out/databaseMigrations.sql

EXPOSE 5000
