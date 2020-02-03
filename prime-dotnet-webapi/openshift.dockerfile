#FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
FROM docker-registry.default.svc:5000/dqszvc-tools/dotnet-31-rhel7 
WORKDIR /opt/app-root/app
USER 0
ENV PATH="$PATH:/opt/rh/rh-dotnet31/root/usr/bin//opt/app-root/.dotnet/tools:/root/.dotnet/tools"
ENV ASPNETCORE_ENVIRONMENT "${ASPNETCORE_ENVIRONMENT}"
ENV POSTGRESQL_PASSWORD "${POSTGRESQL_PASSWORD}"
ENV POSTGRESQL_DATABASE "${POSTGRESQL_DATABASE}"
ENV POSTGRESQL_ADMIN_PASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV POSTGRESQL_USER "${POSTGRESQL_USER}"
ENV SUFFIX "${SUFFIX}"
ENV DB_HOST "$DB_HOST"
ENV DB_CONNECTION_STRING "host=postgresql${SUFFIX};port=5432;database=${POSTGRESQL_DATABASE};username=${POSTGRESQL_USER};password=${POSTGRESQL_PASSWORD}"
ENV KEYCLOAK_URL $KEYCLOAK_URL
ENV KEYCLOAK_REALM $KEYCLOAK_REALM
ENV KEYCLOAK_CLIENT_ID $KEYCLOAK_CLIENT_ID
ENV JWT_WELL_KNOWN_CONFIG $JWT_WELL_KNOWN_CONFIG
ENV API_PORT 8080
COPY *.csproj /opt/app-root/app
RUN /opt/rh/rh-dotnet31/root/usr/bin/dotnet restore
COPY . /opt/app-root/app

# Begin database migration setup
RUN dotnet publish -c Release -o /opt/app-root/app/ /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
RUN dotnet tool install --global dotnet-ef --version 3.1.1
RUN dotnet ef migrations script --idempotent --output /opt/app-root/app/databaseMigrations.sql

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
#FROM registry.redhat.io/dotnet/dotnet-30-runtime-rhel7 as runtime
#WORKDIR /opt/app-root/app
#COPY --from=build /opt/app-root/app/out /opt/app-root/app

EXPOSE 8080 5001 1025
COPY entrypoint.sh .

RUN chmod +x entrypoint.sh && \
    chmod 777 entrypoint.sh && \
    chmod -R 777 /opt/app-root && \
    chmod -R 777 /opt/app-root/.*


ENTRYPOINT [ "./entrypoint.sh" ]
