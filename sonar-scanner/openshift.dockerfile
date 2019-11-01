#FROM docker-registry.default.svc:5000/dqszvc-tools/dotnet-22-rhel7 
#FROM mcr.microsoft.com/dotnet/core/sdk:2.2
#FROM registry.redhat.io/dotnet/dotnet-22-rhel7
#FROM docker-registry.default.svc:5000/dqszvc-dev/centos:7
FROM centos:7
WORKDIR /opt/app-root/app
ENV HOME /opt/app-root/app
RUN mkdir -p /opt/app-root/app
#USER 0
ENV PATH $PATH:/root/.dotnet/tools:/opt/app-root/app/prime-dotnet-webapi-tests:/opt/app-root/app/.dotnet/tools/:/usr/share/dotnet
ENV ASPNETCORE_ENVIRONMENT Development
RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm && \
    curl -sL https://rpm.nodesource.com/setup_10.x | bash - && \
    yum install -y -q dotnet-sdk-2.2 java-1.8.0-openjdk gcc-c++ make nodejs nano xterm envsubst && \
    npm install -g @angular/cli sonarqube-scanner && \
    dotnet tool install --global coverlet.console && \
    dotnet tool install --global dotnet-sonarscanner --version 4.7.1 && \
    mkdir -p /.dotnet && \
    chown -R 1001:1001 /.dotnet && \
    mkdir -p /.local && \
    chown -R 1001:1001 /.local && \
    mkdir -p /.nuget && \
    chown -R 1001:1001 /.nuget && \
    mkdir -p /tmp/NuGetScratch/ && \
    chown -R 1001:1001 /tmp/NuGetScratch/ && \
    chown -R 1001:1001 /opt/app-root/
#COPY ./prime-dotnet-webapi/ /opt/app-root/app/prime-dotnet-webapi/
#COPY ./prime-dotnet-webapi-tests/ /opt/app-root/app/prime-dotnet-webapi-tests/
#COPY ./sonar-scanner/ /opt/app-root/app/sonar-scanner/
#COPY ./prime-angular-frontend/ /opt/app-root/app/prime-angular-frontend
#COPY ./sonar-scanner/prime-dotnet-webapi.sln /opt/app-root/app
#COPY ./sonar-scanner/.bash_profile /opt/app-root/app
#COPY ./sonar-scanner/sonar-runner.cmd /opt/app-root/app/
COPY ./ /opt/app-root/app/
RUN localedef -i en_US -f UTF-8 en_US.UTF-8 && \
    chmod +x /opt/app-root/app/sonar-scanner/sonar-runner.cmd && \
    chmod +x /opt/app-root/app/sonar-scanner/*.bash

USER 1001
CMD [ "/bin/sh","/opt/app-root/app/sonar-scanner/sonar-runner.cmd"]
#SHELL ["/bin/bash", "-c"]
#CMD ["/bin/bash","/opt/app-root/app/sonar-scanner/sonarQube.cmd"]
#ENTRYPOINT source entrypoint.bash
#ENTRYPOINT /opt/app-root/app/sonar-runner.cmd