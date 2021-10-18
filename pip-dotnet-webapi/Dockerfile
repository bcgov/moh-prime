FROM mcr.microsoft.com/dotnet/sdk:6.0.100-rc.2
# ARG DB_HOST
# ARG PGPASSWORD
# ARG POSTGRESQL_ADMIN_PASSWORD
# ARG POSTGRESQL_USERNAME
# ARG POSTGRESQL_DATABASE
# ARG DB_CONNECTION_STRING
# ARG ASPNETCORE_HTTPS_PORT
# ARG ASPNETCORE_ENVIRONMENT
# ARG ASPNETCORE_URLS

# ENV DB_HOST ${DB_HOST}
# ENV PGPASSWORD ${POSTGRESQL_PASSWORD}
# ENV POSTGRESQL_ADMIN_PASSWORD ${POSTGRESQL_PASSWORD}
# ENV POSTGRESQL_USERNAME ${POSTGRESQL_USERNAME}
# ENV POSTGRESQL_DATABASE ${POSTGRESQL_DATABASE}
ENV DB_CONNECTION_STRING "host=localhost;port=5433;database=postgres;username=postgres;password=postgres"
# ENV ASPNETCORE_HTTPS_PORT ${ASPNETCORE_HTTPS_PORT}
# ENV ASPNETCORE_ENVIRONMENT ${ASPNETCORE_ENVIRONMENT}
# ENV ASPNETCORE_URLS ${ASPNETCORE_URLS}
# ENV DOTNET_CLI_HOME "/tmp/DOTNET_CLI_HOME"


WORKDIR /vsdbg

# RUN apt-get update \
#     && apt-get install -y --no-install-recommends \
#     unzip \
#     && rm -rf /var/lib/apt/lists/* \
#     && curl -sSL https://aka.ms/getvsdbgsh \
#     | bash /dev/stdin -v latest -l /vsdbg \
#     && apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev \
#     && apt install -y procps

RUN dotnet tool install --global dotnet-ef --version 6.0.0-rc.2.21480.5
ENV PATH="${PATH}:/root/.dotnet/tools"

ENV DOTNET_USE_POLLING_FILE_WATCHER 1
ENV API_PORT 8080

WORKDIR /app

EXPOSE 8080 5001 5000

ENTRYPOINT dotnet watch run --urls=http://+:8080
