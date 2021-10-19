FROM mcr.microsoft.com/dotnet/sdk:6.0.100-rc.2-alpine3.14 as build

# ENV DB_CONNECTION_STRING "host=localhost;port=5433;database=postgres;username=postgres;password=postgres"

WORKDIR /app

RUN apk add postgresql-client
ENV DOTNET_CLI_HOME="/DOTNET_CLI_HOME"
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"

RUN dotnet tool install --global dotnet-ef --version 6.0.0-rc.2.21480.5
ENV PATH="$PATH:/DOTNET_CLI_HOME/.dotnet/tools"

COPY *.csproj /app
RUN dotnet restore
COPY . /app

# RUN dotnet build "pip.csproj" -c Release -o /app/out
RUN dotnet publish "pip.csproj" -c Release -o /app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App

RUN dotnet ef migrations script --idempotent --output /app/out/databaseMigrations.sql

EXPOSE 5000

# FROM mcr.microsoft.com/dotnet/runtime:6.0
# WORKDIR /app
# # RUN apt-get install postgresql-client
# COPY --from=build /root/.dotnet/ /root/.dotnet/.
# COPY --from=build /app/Configuration/ /app/Configuration/
# COPY --from=build /app/out /app/out