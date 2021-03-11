###################################
### Stage 1 - Build environment ###
###################################
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
ENV PATH="$PATH:/opt/rh/rh-dotnet31/root/usr/bin/:/app/.dotnet/tools:/root/.dotnet/tools"

# Copy everything and build
COPY . .
RUN dotnet restore "prime.csproj"
RUN dotnet build "prime.csproj" -c Release -o /app
RUN dotnet publish "prime.csproj" -c Release -o /app

# Begin database migration setup
RUN dotnet tool install --global dotnet-ef --version 3.1.1
RUN dotnet ef migrations script --idempotent --output /app/out/databaseMigrations.sql


########################################
### Stage 2 - Production environment ###
########################################
FROM registry.redhat.io/dotnet/dotnet-31-rhel7 AS base

ENV API_PORT 8080

WORKDIR /app
COPY --from=build /app .

RUN apt-get update
RUN apt-get install -yqq gpgv gnupg2 wget
RUN echo 'deb http://apt.postgresql.org/pub/repos/apt/ stretch-pgdg main' >  /etc/apt/sources.list.d/pgdg.list
RUN wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | apt-key add -
RUN apt-get install -yqq --no-install-recommends postgresql-client-10 net-tools moreutils
RUN apt-get install -yf libfontconfig1 libxrender1 libgdiplus xvfb
RUN chmod +x /app/Resources/wkhtmltopdf/Linux/wkhtmltopdf
RUN /app/Resources/wkhtmltopdf/Linux/wkhtmltopdf --version

RUN chmod +x entrypoint.sh
RUN chmod 777 entrypoint.sh
# RUN chmod -R 777 /var/run/
RUN chmod -R 777 /app
# RUN chmod -R 777 /app/.*

EXPOSE 8080 5001 1025
ENTRYPOINT [ "./entrypoint.sh" ]
