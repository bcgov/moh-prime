FROM docker-registry.default.svc:5000/dqszvc-dev/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 5051
FROM docker-registry.default.svc:5000/dqszvc-dev/sdk:2.2 AS build
WORKDIR /src

RUN dotnet restore "prime.csproj"
WORKDIR "/src/prime"
COPY . .
WORKDIR "/src/prime"
RUN dotnet build "prime.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "prime.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENV ASPNETCORE_ENVIRONMENT DevStaging
ENV ASPNETCORE_URLS=http://+:5051

ENTRYPOINT ["dotnet", "prime.dll"]