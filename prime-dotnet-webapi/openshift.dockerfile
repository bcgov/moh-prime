# Create build environment
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy .csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else now and build
COPY . ./
RUN dotnet publish -c Release -o /app/out/ -p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App

# Begin database migration setup
RUN dotnet tool install --global dotnet-ef --version 3.1.1
RUN dotnet ef migrations script --idempotent --output /opt/app-root/app/out/databaseMigrations.sql

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
USER 0

ENV API_PORT 8080

WORKDIR /app
COPY --from=build /app/out/ .
COPY --from=build /app/Configuration/ .
COPY --from=build /app/entrypoint.sh .

RUN apt-get update
RUN apt-get install -yqq gpgv gnupg2 wget
RUN echo 'deb http://apt.postgresql.org/pub/repos/apt/ stretch-pgdg main' >  /etc/apt/sources.list.d/pgdg.list
RUN wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | apt-key add -
RUN apt-get update
RUN apt-get install -yqq --no-install-recommends postgresql-client-10 net-tools moreutils
RUN apt-get install -yf libfontconfig1 libxrender1 libgdiplus xvfb
RUN chmod +x /app/Resources/wkhtmltopdf/Linux/wkhtmltopdf
RUN /app/Resources/wkhtmltopdf/Linux/wkhtmltopdf --version
# RUN chmod +x entrypoint.sh
# RUN chmod 777 entrypoint.sh
RUN chmod +x entrypoint.sh
RUN chmod 777 entrypoint.sh
# RUN chmod -R 777 /var/run/
RUN chmod -R 777 /app
RUN chmod -R 777 /app/.*

EXPOSE 8080 5001 1025
ENTRYPOINT [ "./entrypoint.sh" ]
