#FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
FROM  mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
#FROM registry.redhat.io/rhel8/dotnet-30 as build
#FROM registry.redhat.io/dotnet/dotnet-31-rhel7  
USER 0
ENV DB_CONNECTION_STRING "host=postgres;port=5432;database=postgres;username=postgres;password=postgres"
WORKDIR /opt/app-root/app
ENV ASPNETCORE_ENVIRONMENT Development
ENV PATH="$PATH:/opt/app-root/.dotnet/tools:/root/.dotnet/tools"

RUN mkdir -p /opt/app-root/app
COPY *.csproj /opt/app-root/app
RUN dotnet restore
COPY . /opt/app-root/app

#RUN dotnet publish -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
RUN dotnet publish -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
RUN dotnet tool install --global dotnet-ef --version 3.1.1 && \
    dotnet ef migrations script --idempotent --output /opt/app-root/app/out/databaseMigrations.sql

RUN apt update -y && \
    echo "deb http://apt.postgresql.org/pub/repos/apt/ $(lsb_release -sc)-pgdg main" > /etc/apt/sources.list.d/PostgreSQL.list  && \
    wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | apt-key add - && \
    apt-get update && \
    apt-get install -yqq --no-install-recommends postgresql-client-10

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
ENV DB_CONNECTION_STRING "host=postgres;port=5432;database=postgres;username=postgres;password=postgres"
ENV API_PORT 8080
WORKDIR /opt/app-root/app
COPY --from=build /opt/app-root/app/out /opt/app-root/app
EXPOSE 8080 5001 1025
COPY entrypoint.sh .

RUN chmod +x entrypoint.sh && \
    chmod 777 entrypoint.sh

USER 10000

ENTRYPOINT [ "./entrypoint.sh" ]
