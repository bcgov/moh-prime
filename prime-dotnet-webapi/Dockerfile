#FROM docker-registry.default.svc:5000/dqszvc-dev/sdk:2.2 AS build
FROM docker-registry.default.svc:5000/dqszvc-dev/dotnet-30-rhel7 AS build
SHELL [ "/bin/bash" , "-c" ]
ENV ASPNETCORE_ENVIRONMENT Development
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out
#FROM docker-registry.default.svc:5000/dqszvc-dev/aspnet:2.2 AS runtime
FROM docker-registry.default.svc:5000/dqszvc-dev/dotnet-30-runtime-rhel7 AS build
COPY --from=build out .
EXPOSE 8080 5001
ENTRYPOINT ["dotnet", "prime.dll"]