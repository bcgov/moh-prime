#FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
#FROM registry.redhat.io/rhel8/dotnet-30 as build
FROM registry.redhat.io/dotnet/dotnet-31-rhel7  
WORKDIR /opt/app-root/app
USER 0
ENV DB_CONNECTION_STRING "host=postgres;port=5432;database=postgres;username=postgres;password=postgres"
ENV ASPNETCORE_ENVIRONMENT Development
ENV PATH="$PATH:/opt/rh/rh-dotnet31/root/usr/bin/"
RUN mkdir -p /opt/app-root/app
COPY *.csproj /opt/app-root/app
RUN /opt/rh/rh-dotnet31/root/usr/bin/dotnet restore
COPY . /opt/app-root/app

# Begin database migration setup
ENV PATH="$PATH:/opt/app-root/.dotnet/tools"
#RUN curl https://api.nuget.org/v3/index.json
RUN dotnet publish -c Release -o /opt/app-root/app/ /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
RUN dotnet tool install --global dotnet-ef
#RUN dotnet ef migrations script --idempotent --output "${WORKDIR}/databaseMigrations.sql"

#FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
#FROM registry.redhat.io/dotnet/dotnet-30-runtime-rhel7 as runtime
#WORKDIR /opt/app-root/app
#COPY --from=build /opt/app-root/app/out /opt/app-root/app
#RUN mv out/* .
EXPOSE 8080 5001 1025
COPY entrypoint.sh .

USER 0
RUN chmod +x entrypoint.sh && \
    chmod 777 entrypoint.sh

ENV API_PORT 8080
ENTRYPOINT [ "./entrypoint.sh" ]
