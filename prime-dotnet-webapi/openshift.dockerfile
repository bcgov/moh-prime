#FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
FROM registry.redhat.io/rhel8/dotnet-30 as build
WORKDIR /opt/app-root/app
USER 0
ENV PATH "$PATH:/opt/rh/rh-dotnet22/root/usr/lib64/dotnet"
ENV ASPNETCORE_ENVIRONMENT "${ASPNETCORE_ENVIRONMENT}"
ENV POSTGRESQL_PASSWORD "${POSTGRESQL_PASSWORD}"
ENV POSTGRESQL_DATABASE "${POSTGRESQL_DATABASE}"
ENV POSTGRESQL_ADMIN_PASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV POSTGRESQL_USER "${POSTGRESQL_USER}"
ENV SUFFIX "${SUFFIX}"
ENV DB_HOST "$DB_HOST"
ARG DB_CONNECTION_STRING="host=postgresql${SUFFIX};port=5432;database=${POSTGRESQL_DATABASE};username=${POSTGRESQL_USER};password=${POSTGRESQL_PASSWORD}"

RUN mkdir -p /opt/app-root/app
COPY *.csproj /opt/app-root/app
RUN dotnet restore
COPY . /opt/app-root/app

# Begin database migration setup
ENV PATH="$PATH:/opt/app-root/.dotnet/tools"
RUN curl https://api.nuget.org/v3/index.json
RUN dotnet publish -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
RUN dotnet tool install --global dotnet-ef --version 3.0.0
RUN dotnet ef migrations script --idempotent --output "${WORKDIR}/databaseMigrations.sql"

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
FROM registry.redhat.io/dotnet/dotnet-30-runtime-rhel7 as runtime

WORKDIR /opt/app-root/app
COPY --from=build /opt/app-root/app/out /opt/app-root/app
EXPOSE 8080 5001 1025
COPY entrypoint.sh .
#RUN apt update && \
#    apt install -yqq net-tools \
#    inetutils-ping \
#    telnet && \
USER 0
RUN chmod +x entrypoint.sh && \
    chmod 777 entrypoint.sh
ENV DB_CONNECTION_STRING="host=postgresql${SUFFIX};port=5432;database=${POSTGRESQL_DATABASE};username=${POSTGRESQL_USER};password=${POSTGRESQL_PASSWORD}"
ENV API_PORT 8080
ENTRYPOINT [ "./entrypoint.sh" ]
