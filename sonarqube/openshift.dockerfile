#FROM docker-registry.default.svc:5000/dqszvc-tools/dotnet-22-rhel7 
#FROM mcr.microsoft.com/dotnet/core/sdk:2.2
#FROM registry.redhat.io/dotnet/dotnet-22-rhel7
#FROM docker-registry.default.svc:5000/dqszvc-dev/centos:7
FROM centos:7
WORKDIR /opt/app-root/app
ENV HOME /opt/app-root/app
RUN mkdir -p /opt/app-root/app
#USER 0
ENV PATH $PATH:/root/.dotnet/tools:/opt/app-root/app/prime-dotnet-webapi-tests:/opt/app-root/app/.dotnet/tools/
ENV ASPNETCORE_ENVIRONMENT Development
RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm && \
    curl -sL https://rpm.nodesource.com/setup_10.x | bash - && \
    yum install -y -q dotnet-sdk-2.2 java-1.8.0-openjdk nodejs gcc-c++ make nodejs && \
    npm install -g @angular/cli sonarqube-scanner 
COPY ./prime-dotnet-webapi/ /opt/app-root/app/prime-dotnet-webapi
COPY ./prime-dotnet-webapi-tests/ /opt/app-root/app/prime-dotnet-webapi-tests
COPY ./prime-angular-frontend/ /opt/app-root/app/prime-angular-frontend
COPY ./sonarqube/sonarQube.cmd /opt/app-root/app/
RUN dotnet tool install --global coverlet.console && \
    dotnet tool install --global dotnet-sonarscanner --version 4.7.1 && \
    chmod +x /opt/app-root/app/sonarQube.cmd && \
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
CMD [ "/bin/bash","/opt/app-root/app/sonarQube.cmd" ]