FROM mcr.microsoft.com/dotnet/sdk:6.0.100-rc.2-alpine3.14 as build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet add package Microsoft.EntityFrameworkCore.Tools --version 5.0.11
RUN dotnet publish -c Release -o out
ENV PATH="$PATH:/root/.dotnet/tools:/app/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef --version 5.0.11

# RUN ls -al /root/.dotnet/tools
RUN dotnet ef migrations script --idempotent --output out/databaseMigrations.sql


# ##
FROM mcr.microsoft.com/dotnet/runtime:6.0.0-rc.2-alpine3.14
WORKDIR /app
RUN apk add postgresql-client
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "pip.dll"]