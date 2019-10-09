FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
USER 0

ENV WORKDIR /opt/app
WORKDIR /opt/app
ENV ASPNETCORE_ENVIRONMENT Development

# Copy csproj and restore as distinct layers
RUN mkdir -p $WORKDIR
COPY *.csproj $WORKDIR
RUN dotnet restore

# Copy everything else and build
COPY . $WORKDIR
RUN dotnet publish -c Release -o out
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
COPY --from=build $WORKDIR .

EXPOSE 8080 5001

USER 1001
ENV ASPNETCORE_ENVIRONMENT Development

ENTRYPOINT ["/usr/bin/dotnet", "prime.dll"]
