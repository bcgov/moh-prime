###################################
### Stage 1 - Build environment ###
###################################
# FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
FROM registry.redhat.io/rhel8/dotnet-31 AS build
WORKDIR /opt/app-root/app
ENV PATH="$PATH:/opt/rh/rh-dotnet31/root/usr/bin/:/opt/app-root/app/.dotnet/tools:/root/.dotnet/tools:/opt/app-root/.dotnet/tools"

ENV API_PORT 8080
ENV ASPNETCORE_ENVIRONMENT "${ASPNETCORE_ENVIRONMENT}"
ENV POSTGRESQL_PASSWORD "${POSTGRESQL_PASSWORD}"
ENV POSTGRESQL_DATABASE "${POSTGRESQL_DATABASE}"
ENV POSTGRESQL_ADMIN_PASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV POSTGRESQL_USER "${POSTGRESQL_USER}"
ENV SUFFIX "${SUFFIX}"
ENV DB_HOST "$DB_HOST"

# Copy everything and build
COPY . .

RUN dotnet restore "prime.csproj"
RUN dotnet build "prime.csproj" -c Release -o /opt/app-root/app/out
RUN dotnet publish "prime.csproj" -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App

# Begin database migration setup
RUN dotnet tool install --global dotnet-ef --version 3.1.1
RUN dotnet ef migrations script --idempotent --output /opt/app-root/app/out/databaseMigrations.sql

########################################
### Stage 2 - Production environment ###
########################################
# FROM registry.redhat.io/dotnet/dotnet-31-rhel7 AS runtime
FROM registry.access.redhat.com/ubi8/dotnet-31-runtime AS runtime

ENV API_PORT 8080

WORKDIR /opt/app-root/app
COPY --from=build /opt/app-root/app /opt/app-root/app

# Install packages necessary for PRIME (incl. PostgreSQL client for waiting on DB, and wkhtmltopdf to render HTML into PDF)
USER 0
RUN yum install -y https://download.postgresql.org/pub/repos/yum/reporpms/EL-8-x86_64/pgdg-redhat-repo-latest.noarch.rpm && \
    yum install -y postgresql10

RUN chmod +x entrypoint.sh
RUN chmod 777 entrypoint.sh
# RUN chmod -R 777 /var/run/
RUN chmod -R 777 /opt/app-root/app 
# RUN chmod -R 777 /app/.*

EXPOSE 8080 5001 1025
ENTRYPOINT [ "./entrypoint.sh" ]
