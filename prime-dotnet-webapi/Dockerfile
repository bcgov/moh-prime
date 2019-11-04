FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build
WORKDIR /opt/app-root/app


ENV ASPNETCORE_ENVIRONMENT Development
RUN mkdir -p /opt/app-root/app
COPY *.csproj /opt/app-root/app
RUN dotnet restore 
COPY . .
RUN dotnet publish -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime

WORKDIR /opt/app-root/app

COPY --from=build /opt/app-root/app/out /opt/app-root/app
EXPOSE 8080 5001

ENTRYPOINT ["dotnet", "Prime.dll"]
