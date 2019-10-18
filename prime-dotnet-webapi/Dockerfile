
#FROM docker-registry.default.svc:5000/dqszvc-dev/sdk:2.2 AS build
FROM docker-registry.default.svc:5000/dqszvc-dev/dotnet-30-rhel7 AS build
#FROM docker-registry.default.svc:5000/dqszvc-dev/dotnet-22-rhel7 AS build
WORKDIR /opt/app-root/app
SHELL [ "/bin/bash" , "-c" ]
ENV ASPNETCORE_ENVIRONMENT Development
#ENV PATH "$PATH:/opt/rh/rh-dotnet22/root/usr/lib64/dotnet"
ENV PATH "$PATH:/opt/rh/rh-dotnet30/root/usr/lib64/dotnet"
COPY *.csproj /opt/app-root/app
RUN find /opt -type f -name dotnet
RUN dotnet restore 
COPY . /opt/app-root/app/
RUN dotnet publish -c Release -o /opt/app-root/app/out /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App
#FROM docker-registry.default.svc:5000/dqszvc-dev/aspnet:2.2 AS runtime
FROM docker-registry.default.svc:5000/dqszvc-dev/dotnet-30-runtime-rhel7 AS runtime
#FROM docker-registry.default.svc:5000/dqszvc-dev/dotnet-22-runtime-rhel7 AS runtime
WORKDIR /opt/app-root/app
COPY --from=build /opt/app-root/app/out /opt/app-root/app
EXPOSE 8080 5001
ENTRYPOINT ["dotnet", "prime.dll"]
