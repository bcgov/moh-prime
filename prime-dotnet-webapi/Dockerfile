FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
USER 0
WORKDIR /tmp
ENV WORKDIR /tmp
ENV ASPNETCORE_ENVIRONMENT Development
# Copy csproj and restore as distinct layers
RUN echo `pwd`
RUN mkdir -p /tmp
COPY *.csproj /tmp
RUN dotnet restore
# Copy everything else and build
COPY . /tmp
RUN dotnet publish -c Release -o out
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
COPY --from=build /tmp .
EXPOSE 8080 5001
USER 1001
ENV ASPNETCORE_ENVIRONMENT Development
ENTRYPOINT ["/usr/bin/dotnet", "prime.dll"]