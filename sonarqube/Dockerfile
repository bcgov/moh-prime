#FROM docker-registry.default.svc:5000/dqszvc-tools/dotnet-22-rhel7 
FROM mcr.microsoft.com/dotnet/core/sdk:2.2
WORKDIR /opt/app-root/app
ENV PATH $PATH:/root/.dotnet/tools
ENV ASPNETCORE_ENVIRONMENT Development
RUN mkdir -p /opt/app-root/app 
COPY ./prime-dotnet-webapi/ /opt/app-root/app/prime-dotnet-webapi
COPY ./prime-dotnet-webapi-tests/ /opt/app-root/app/prime-dotnet-webapi-tests
RUN dotnet tool install --global coverlet.console && \
    dotnet tool install --global dotnet-sonarscanner --version 4.7.1

CMD ["/opt/app-root/app/prime-dotnet-webapi-tests/sonarQube.cmd"]