#FROM docker-registry.default.svc:5000/dqszvc-tools/dotnet-22-rhel7 
FROM mcr.microsoft.com/dotnet/core/sdk:2.2
WORKDIR /opt/app-root/app
ENV PATH $PATH:/root/.dotnet/tools:/opt/app-root/app/prime-dotnet-webapi-tests
ENV ASPNETCORE_ENVIRONMENT Development
RUN mkdir -p /opt/app-root/app && \
    apt-get update -yqq && \
    apt-get install -yqq default-jre
COPY ./prime-dotnet-webapi/ /opt/app-root/app/prime-dotnet-webapi
COPY ./prime-dotnet-webapi-tests/ /opt/app-root/app/prime-dotnet-webapi-tests
COPY ./sonar-scanner/sonar-runner.cmd /opt/app-root/app
RUN dotnet tool install --global coverlet.console && \
    dotnet tool install --global dotnet-sonarscanner --version 4.7.1 && \
    chmod +x /opt/app-root/app/sonar-runner.cmd 
CMD [ "/bin/sh","./sonar-runner.cmd" ]