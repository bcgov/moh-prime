###################################
### Stage 1 - Build environment ###
###################################
FROM registry.redhat.io/rhel8/dotnet-50 AS build
WORKDIR /opt/app-root/app
ARG API_PORT
ARG ASPNETCORE_ENVIRONMENT
ARG POSTGRESQL_PASSWORD
ARG POSTGRESQL_DATABASE
ARG POSTGRESQL_ADMIN_PASSWORD
ARG POSTGRESQL_USER
ARG SVC_NAME
ARG DB_HOST

ENV PATH="$PATH:/opt/rh/rh-dotnet50/root/usr/bin/:/opt/app-root/app/.dotnet/tools:/root/.dotnet/tools:/opt/app-root/.dotnet/tools"

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
COPY *.csproj /opt/app-root/app
RUN dotnet restore
COPY . /opt/app-root/app

RUN dotnet restore "prime.csproj"
RUN dotnet build "prime.csproj" -c Release -o /opt/app-root/app/out
RUN dotnet publish "prime.csproj" -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App

# Begin database migration setup
RUN dotnet tool install --global dotnet-ef --version 5.0.6
RUN dotnet ef migrations script --idempotent --output /opt/app-root/app/out/databaseMigrations.sql

########################################
###   Stage 2 - Runtime environment  ###
########################################
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
USER 0
ENV PATH="$PATH:/opt/rh/rh-dotnet50/root/usr/bin/:/opt/app-root/.dotnet/tools:/root/.dotnet/tools"
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

RUN apt-get update && \
    apt-get install -yqq gpgv gnupg2 wget && \
    echo 'deb http://apt.postgresql.org/pub/repos/apt/ stretch-pgdg main' >  /etc/apt/sources.list.d/pgdg.list && \
    wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | apt-key add - && \
    apt-get update && \
    apt-get install -yqq --no-install-recommends postgresql-client-10 net-tools moreutils && \
    apt-get install -yf libfontconfig1 libxrender1 libgdiplus xvfb && \
    chmod +x /opt/app-root/app/Resources/wkhtmltopdf/Linux/wkhtmltopdf && \
    /opt/app-root/app/Resources/wkhtmltopdf/Linux/wkhtmltopdf --version && \
    chmod +x entrypoint.sh && \
    chmod 777 entrypoint.sh && \
    chmod -R 777 /var/run/ && \
    chmod -R 777 /opt/app-root && \
    chmod -R 777 /opt/app-root/.*

EXPOSE 8080 5001 1025
ENTRYPOINT [ "./entrypoint.sh" ]
