FROM docker-registry.default.svc:5000/dqszvc-dev/sdk:2.2 AS build
SHELL [ "/bin/bash" , "-c" ]
ENV ASPNETCORE_ENVIRONMENT Development
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out
FROM docker-registry.default.svc:5000/dqszvc-dev/aspnet:2.2 AS runtime
COPY --from=build out .
EXPOSE 8080 5001
ENTRYPOINT ["dotnet", "prime.dll"]