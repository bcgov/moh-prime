FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
USER 0
SHELL [ "/bin/bash" , "-c" ]
WORKDIR /app
ENV WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Development
RUN mkdir -p /app
COPY *.csproj /app
RUN dotnet restore
# Copy everything else and build
COPY . /app
RUN dotnet publish -c Release -o out
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
COPY --from=build /app/out .
EXPOSE 8080 5001
ENTRYPOINT ["dotnet", "prime.dll"]