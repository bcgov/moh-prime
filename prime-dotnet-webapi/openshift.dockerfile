FROM registry.access.redhat.com/ubi8/dotnet-50 AS build
WORKDIR /opt/app-root/app
ARG API_PORT
ARG ASPNETCORE_ENVIRONMENT
ARG POSTGRESQL_PASSWORD
ARG POSTGRESQL_DATABASE
ARG POSTGRESQL_ADMIN_PASSWORD
ARG POSTGRESQL_USER
ARG SVC_NAME
ARG DB_HOST

ENV PATH="$PATH:/opt/rh/rh-dotnet50/root/usr/bin/:/opt/app-root/.dotnet/tools:/root/.dotnet/tools"
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
RUN dotnet publish -c Release -o /opt/app-root/app/out/ /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
RUN dotnet tool install --global dotnet-ef --version 5.0.6
RUN dotnet ef migrations script --idempotent --output /opt/app-root/app/out/databaseMigrations.sql
########################################
###   Stage 2 - Runtime environment  ###
########################################
FROM registry.access.redhat.com/ubi8/dotnet-50-runtime AS runtime
USER 0
ENV PATH="$PATH:/opt/rh/rh-dotnet50/root/usr/bin/:/opt/app-root/.dotnet/tools:/root/.dotnet/tools"
ENV ASPNETCORE_ENVIRONMENT "${ASPNETCORE_ENVIRONMENT}"
ENV POSTGRESQL_PASSWORD "${POSTGRESQL_PASSWORD}"
ENV POSTGRESQL_DATABASE "${POSTGRESQL_DATABASE}"
ENV POSTGRESQL_ADMIN_PASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV POSTGRESQL_USER "${POSTGRESQL_USER}"
ENV PGPASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV SUFFIX "${SUFFIX}"
ENV DB_HOST "$DB_HOST"
ENV KEYCLOAK_REALM_URL $KEYCLOAK_REALM_URL
ENV MOH_KEYCLOAK_REALM_URL $MOH_KEYCLOAK_REALM_URL
ENV API_PORT 8080
USER 0 
WORKDIR /opt/app-root/app
COPY --from=build /opt/app-root/app /opt/app-root/app

RUN yum install -yqq https://download.postgresql.org/pub/repos/yum/reporpms/EL-8-x86_64/pgdg-redhat-repo-latest.noarch.rpm &&\
    yum install -yqq postgresql10 
RUN yum install -yqq http://mirror.centos.org/centos/8-stream/AppStream/x86_64/os/Packages/xorg-x11-fonts-75dpi-7.5-19.el8.noarch.rpm && \
    yum install -yqq https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6-1/wkhtmltox-0.12.6-1.centos8.x86_64.rpm
# RUN yum install -y https://download.postgresql.org/pub/repos/yum/reporpms/EL-8-x86_64/pgdg-redhat-repo-latest.noarch.rpm &&\
#     yum install -yqq http://mirror.centos.org/centos/8-stream/AppStream/x86_64/os/Packages/xorg-x11-fonts-75dpi-7.5-19.el8.noarch.rpm && \
#     yum install -yqq https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6-1/wkhtmltox-0.12.6-1.centos8.x86_64.rpm
    
# RUN yum update -yqq && \
#     yum search postgresql psql && \
#     yum install -y postgresql10 gpg gnupg2 wget && \
#     yum install -yqq gpg gnupg2 wget
    
# RUN yum install -yqq libfontconfig1 libxrender1 libgdiplus xvfb

# RUN chmod +x /opt/app-root/app/Resources/wkhtmltopdf/Linux/wkhtmltopdf && \
#     /opt/app-root/app/Resources/wkhtmltopdf/Linux/wkhtmltopdf --version
RUN chmod +x entrypoint.sh && \
    chmod 777 entrypoint.sh && \
    chmod -R 777 /var/run/ && \
    chmod -R 777 /opt/app-root && \
    chmod -R 777 /opt/app-root/.*

RUN chmod +x entrypoint.sh
RUN chmod 777 entrypoint.sh
# RUN chmod -R 777 /var/run/
RUN chmod -R 777 /opt/app-root/app
# RUN chmod -R 777 /app/.*

EXPOSE 8080 5001 1025
ENTRYPOINT [ "./entrypoint.sh" ]
