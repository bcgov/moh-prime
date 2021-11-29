FROM mcr.microsoft.com/dotnet/sdk:6.0.100-rc.2

ENV ConnectionStrings__PidpDatabase "host=localhost;port=5433;database=postgres;username=postgres;password=postgres"

WORKDIR /vsdbg

RUN dotnet tool install --global dotnet-ef --version 6.0.0-rc.2.21480.5
ENV PATH="${PATH}:/root/.dotnet/tools"

ENV DOTNET_USE_POLLING_FILE_WATCHER 1
ENV API_PORT 8080

WORKDIR /app

EXPOSE 8080 5001 5000

ENTRYPOINT dotnet watch run --urls=http://+:8080
