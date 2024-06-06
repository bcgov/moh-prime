###################################
### Stage 1 - Build environment ###
###################################
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /opt/app-root/app
ARG API_PORT
ARG ASPNETCORE_ENVIRONMENT
ARG POSTGRESQL_PASSWORD
ARG POSTGRESQL_DATABASE
ARG POSTGRESQL_ADMIN_PASSWORD
ARG POSTGRESQL_USER
ARG SVC_NAME
ARG DB_HOST

ENV PATH="$PATH:/opt/rh/rh-dotnet80/root/usr/bin/:/opt/app-root/app/.dotnet/tools:/root/.dotnet/tools:/opt/app-root/.dotnet/tools"

ENV API_PORT 8080
ENV ASPNETCORE_ENVIRONMENT "${ASPNETCORE_ENVIRONMENT}"
ENV POSTGRESQL_PASSWORD "${POSTGRESQL_PASSWORD}"
ENV POSTGRESQL_DATABASE "${POSTGRESQL_DATABASE}"
ENV POSTGRESQL_ADMIN_PASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV POSTGRESQL_USER "${POSTGRESQL_USER}"
ENV SVC_NAME "${SVC_NAME}"
ENV DB_HOST "$DB_HOST"

ENV KEYCLOAK_REALM_URL $KEYCLOAK_REALM_URL
ENV MOH_KEYCLOAK_REALM_URL $MOH_KEYCLOAK_REALM_URL
ENV API_PORT 8080

COPY . /opt/app-root/app
RUN dotnet restore "prime.csproj"
RUN dotnet build "prime.csproj" -c Release -o /opt/app-root/app/out
RUN dotnet publish "prime.csproj" -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App

# Begin database migration setup
RUN dotnet tool install --global dotnet-ef --version 8.0.3
RUN dotnet ef migrations script --idempotent --output /opt/app-root/app/out/databaseMigrations.sql

########################################
###   Stage 2 - Runtime environment  ###
########################################
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
USER 0
ENV PATH="$PATH:/opt/rh/rh-dotnet80/root/usr/bin/:/opt/app-root/.dotnet/tools:/root/.dotnet/tools"
ENV ASPNETCORE_ENVIRONMENT "${ASPNETCORE_ENVIRONMENT}"
ENV POSTGRESQL_PASSWORD "${POSTGRESQL_PASSWORD}"
ENV POSTGRESQL_DATABASE "${POSTGRESQL_DATABASE}"
ENV POSTGRESQL_ADMIN_PASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV POSTGRESQL_USER "${POSTGRESQL_USER}"
ENV SUFFIX "${SUFFIX}"
ENV DB_HOST "$DB_HOST"

ENV KEYCLOAK_REALM_URL $KEYCLOAK_REALM_URL
ENV MOH_KEYCLOAK_REALM_URL $MOH_KEYCLOAK_REALM_URL
ENV API_PORT 8080

WORKDIR /opt/app-root/app
COPY --from=build /opt/app-root/app/out/ /opt/app-root/app
COPY --from=build /opt/app-root/app/Configuration/ /opt/app-root/app/Configuration/
COPY --from=build /opt/app-root/app/entrypoint.sh /opt/app-root/app

# psql needed to run Database Migrations
RUN apt-get update && \
    apt-get -y install postgresql-client
RUN apt-get update && \
    apt-get install -yf libfontconfig1 libxrender1 libgdiplus xvfb && \
    chmod +x /opt/app-root/app/Resources/wkhtmltopdf/Linux/wkhtmltopdf && \
    /opt/app-root/app/Resources/wkhtmltopdf/Linux/wkhtmltopdf --version
# Permissions on /opt/app-root necessary for Wkhtmltopdf operation and writing of application logs
RUN chmod 755 entrypoint.sh && \
    chmod -R 770 /var/run/ && \
    chmod -R 770 /opt/app-root && \
    chmod -R 770 /opt/app-root/.*

EXPOSE 8080 5001 1025
ENTRYPOINT [ "./entrypoint.sh" ]
