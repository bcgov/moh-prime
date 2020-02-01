#FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
FROM  mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
#FROM registry.redhat.io/rhel8/dotnet-30 as build
#FROM registry.redhat.io/dotnet/dotnet-31-rhel7  
USER 0
ENV DB_CONNECTION_STRING "host=postgres;port=5432;database=postgres;username=postgres;password=postgres"
WORKDIR /opt/app-root/app
ENV ASPNETCORE_ENVIRONMENT Development
ENV PATH="$PATH:/opt/app-root/.dotnet/tools"
RUN mkdir -p /opt/app-root/app
COPY *.csproj /opt/app-root/app
RUN dotnet restore
COPY . /opt/app-root/app

RUN dotnet publish -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
ENV DB_CONNECTION_STRING "host=postgres;port=5432;database=postgres;username=postgres;password=postgres"
ENV API_PORT 8080
WORKDIR /opt/app-root/app
COPY --from=build /opt/app-root/app/out /opt/app-root/app
EXPOSE 8080 5001 1025
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh && \
    chmod 777 entrypoint.sh

ENTRYPOINT [ "./entrypoint.sh" ]