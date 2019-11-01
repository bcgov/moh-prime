#FROM docker-registry.default.svc:5000/dqszvc-tools/dotnet-22-rhel7 
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
USER 0
WORKDIR /opt/app-root/app
ENV HOME /opt/app-root/app
ENV PATH $PATH:./usr/share/dotnet:/opt/app-root/app/.dotnet/tools
ENV ASPNETCORE_ENVIRONMENT Development
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get update -yqq 
RUN apt-get install -yqq default-jre nodejs gcc g++ make && \
    mkdir -p /opt/app-root/app && \
    npm install -g @angular/cli sonarqube-scanner && \
    dotnet tool install --global coverlet.console && \
    dotnet tool install --global dotnet-sonarscanner --version 4.7.1
#COPY ./ /opt/app-root/app/
COPY ./prime-dotnet-webapi-tests/ /opt/app-root/app/prime-dotnet-webapi-tests/
COPY ./sonar-scanner/entrypoint.* /opt/app-root/app/
COPY ./prime-angular-frontend/ /opt/app-root/app/prime-angular-frontend
COPY ./prime-dotnet-webapi.sln /opt/app-root/app/prime-dotnet-webapi.sln
COPY ./sonar-scanner/.bash_profile /opt/app-root/app
COPY ./sonar-scanner/sonar-runner.cmd /opt/app-root/app/
RUN chmod +x /opt/app-root/app/sonar-runner.cmd && \
    chmod +x /opt/app-root/app/entrypoint.bash && \
    mkdir -p /.dotnet && \
    chown -R 1001:1001 /.dotnet && \
    mkdir -p /.local && \
    chown -R 1001:1001 /.local && \
    mkdir -p /.nuget && \
    chown -R 1001:1001 /.nuget && \
    mkdir -p /tmp/NuGetScratch/ && \
    chown -R 1001:1001 /tmp/NuGetScratch/ && \
    chown -R 1001:1001 /opt/app-root/
USER 1001
CMD [ "/bin/sh","./sonar-runner.cmd" ]
