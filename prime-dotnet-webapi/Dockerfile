
#FROM docker-registry.default.svc:5000/dqszvc-dev/sdk:2.2 AS build
#FROM docker-registry.default.svc:5000/dqszvc-dev/dotnet-30-rhel7 AS build
FROM docker-registry.default.svc:5000/dqszvc-dev/dotnet-22-rhel7 AS build
WORKDIR /opt/app-root/app
SHELL [ "/bin/bash" , "-c" ]
ENV ASPNETCORE_ENVIRONMENT Development
COPY *.csproj /opt/app-root/app
RUN dotnet restore 
COPY . /opt/app-root/app/
RUN dotnet publish -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
#FROM docker-registry.default.svc:5000/dqszvc-dev/aspnet:2.2 AS runtime
#FROM docker-registry.default.svc:5000/dqszvc-dev/dotnet-30-runtime-rhel7 AS runtime
FROM docker-registry.default.svc:5000/dqszvc-dev/dotnet-22-runtime-rhel7 AS runtime
WORKDIR /opt/app-root/app
COPY --from=build /opt/app-root/app/out /opt/app-root/app
EXPOSE 8080 5001
ENTRYPOINT ["dotnet", "prime.dll"]
