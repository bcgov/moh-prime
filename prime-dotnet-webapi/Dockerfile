FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /opt/app-root/app
ENV ASPNETCORE_ENVIRONMENT Development
RUN mkdir -p /opt/app-root/app
COPY *.csproj /opt/app-root/app
RUN dotnet restore
COPY . /opt/app-root/app
RUN dotnet publish -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /opt/app-root/app
COPY --from=build /opt/app-root/app/out /opt/app-root/app
EXPOSE 8080 5001 1025
COPY entrypoint.sh .
RUN apt update && \
    apt install -yqq net-tools \
    inetutils-ping \
    telnet && \
    chmod +x entrypoint.sh && \
    chmod 777 entrypoint.sh
ENV DB_CONNECTION_STRING "host=postgres;port=5432;database=postgres;username=postgres;password=postgres"
ENTRYPOINT [ "./entrypoint.sh" ]
