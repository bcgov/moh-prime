###################################
### Stage 1 - Build environment ###
###################################
# FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
FROM registry.redhat.io/rhel8/dotnet-31 AS build
WORKDIR /opt/app-root/app
ARG API_PORT 
ARG ASPNETCORE_ENVIRONMENT
ARG POSTGRESQL_PASSWORD
ARG POSTGRESQL_DATABASE
ARG POSTGRESQL_ADMIN_PASSWORD
ARG POSTGRESQL_USER
ARG SVC_NAME
ARG DB_HOST

ENV PATH="$PATH:/opt/rh/rh-dotnet31/root/usr/bin/:/opt/app-root/app/.dotnet/tools:/root/.dotnet/tools:/opt/app-root/.dotnet/tools"

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
RUN dotnet tool install --global dotnet-ef --version 3.1.1
RUN dotnet ef migrations script --idempotent --output /opt/app-root/app/out/databaseMigrations.sql

########################################
###   Stage 2 - Runtime environment  ###
########################################
# FROM registry.access.redhat.com/ubi8/dotnet-31-runtime AS runtime
# ENV KEYCLOAK_REALM_URL $KEYCLOAK_REALM_URL
# ENV MOH_KEYCLOAK_REALM_URL $MOH_KEYCLOAK_REALM_URL
# ENV API_PORT 8080

# WORKDIR /opt/app-root/app
# COPY --from=build /opt/app-root/app /opt/app-root/app

# # Install packages necessary for PRIME (incl. PostgreSQL client for waiting on DB, and wkhtmltopdf to render HTML into PDF)
# USER 0
# RUN dnf install -y https://download.postgresql.org/pub/repos/yum/reporpms/EL-8-x86_64/pgdg-redhat-repo-latest.noarch.rpm && \
#     dnf install -y postgresql13 

# RUN curl -L -o ./wkhtmltox-0.12.6.centos8.x86_64.rpm https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6-1/wkhtmltox-0.12.6-1.centos8.x86_64.rpm && \
#     dnf install -y http://mirror.centos.org/centos/8/AppStream/x86_64/os/Packages/xorg-x11-fonts-75dpi-7.5-19.el8.noarch.rpm && \
#     dnf install -y xorg-x11-fonts-75dpi && \
#     dnf install -y ./wkhtmltox-0.12.6.centos8.x86_64.rpm && \ 
#     dnf install -y wkhtmltox openssl-devel.x86_64 openssl-libs.x86_64

# RUN chmod +x entrypoint.sh && \
#     chmod 777 entrypoint.sh && \
#     chmod -R 777 /opt/app-root/app
#     # chown -R default:default /opt/app-root/app
# # RUN chmod -R 777 /app/.*
# # USER default

# EXPOSE 8080 5001 1025
# ENTRYPOINT [ "./entrypoint.sh" ]
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
USER 0
ENV PATH="$PATH:/opt/rh/rh-dotnet31/root/usr/bin/:/opt/app-root/.dotnet/tools:/root/.dotnet/tools"
ENV ASPNETCORE_ENVIRONMENT "${ASPNETCORE_ENVIRONMENT}"
ENV POSTGRESQL_PASSWORD "${POSTGRESQL_PASSWORD}"
ENV POSTGRESQL_DATABASE "${POSTGRESQL_DATABASE}"
ENV POSTGRESQL_ADMIN_PASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV POSTGRESQL_USER "${POSTGRESQL_USER}"
ENV PGPASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV SUFFIX "${SUFFIX}"
ENV DB_HOST "$DB_HOST"
#ENV DB_CONNECTION_STRING "host=postgresql$SUFFIX;port=5432;database=${POSTGRESQL_DATABASE};username=${POSTGRESQL_USER};password=${POSTGRESQL_ADMIN_PASSWORD}"

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
