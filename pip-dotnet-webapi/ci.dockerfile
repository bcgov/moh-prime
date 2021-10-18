FROM mcr.microsoft.com/dotnet/sdk:6.0.100-rc.2-alpine3.14 as build

# ENV DB_CONNECTION_STRING "host=localhost;port=5433;database=postgres;username=postgres;password=postgres"

# WORKDIR /vsdbg

RUN apk add postgresql-client
ENV DOTNET_CLI_HOME="/tmp/DOTNET_CLI_HOME"

RUN dotnet tool install --global dotnet-ef --version 6.0.0-rc.2.21480.5
ENV PATH="$PATH:/tmp/DOTNET_CLI_HOME/.dotnet/tools"

ENV API_PORT 8080

WORKDIR /app

COPY *.csproj /app
RUN dotnet restore
COPY . /app

# RUN dotnet build "pip.csproj" -c Release -o /app/out
RUN dotnet publish "pip.csproj" -c Release -o /app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App

# RUN dotnet ef migrations script --idempotent --output /app/out/databaseMigrations.sql


# FROM mcr.microsoft.com/dotnet/runtime:6.0
# WORKDIR /app
# # RUN apt-get install postgresql-client
# COPY --from=build /root/.dotnet/ /root/.dotnet/.
# COPY --from=build /app/Configuration/ /app/Configuration/
# COPY --from=build /app/out /app/out