FROM registry.redhat.io/dotnet/dotnet-22-rhel7 AS build
SHELL [ "/bin/bash" , "-c" ]
ENV ASPNETCORE_ENVIRONMENT Development
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out
FROM registry.redhat.io/dotnet/dotnet-22-runtime-rhel7 AS runtime
COPY --from=build out .
EXPOSE 8080 5001
ENTRYPOINT ["dotnet", "prime.dll"]