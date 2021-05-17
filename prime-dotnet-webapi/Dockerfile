FROM mcr.microsoft.com/dotnet/sdk:5.0

WORKDIR /vsdbg

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    unzip \
    && rm -rf /var/lib/apt/lists/* \
    && curl -sSL https://aka.ms/getvsdbgsh \
    | bash /dev/stdin -v latest -l /vsdbg \
    && apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev \
    && apt install -y procps

RUN dotnet tool install --global dotnet-ef --version 3.1.14
ENV PATH="${PATH}:/root/.dotnet/tools"

ENV DOTNET_USE_POLLING_FILE_WATCHER 1
ENV API_PORT 8080

WORKDIR /app

EXPOSE 8080 5001 5000

ENTRYPOINT dotnet watch run --urls=http://+:8080
