#FROM docker-registry.default.svc:5000/dqszvc-tools/dotnet-22-rhel7 
#FROM mcr.microsoft.com/dotnet/core/sdk:2.2
#FROM registry.redhat.io/dotnet/dotnet-22-rhel7
#FROM docker-registry.default.svc:5000/dqszvc-tools/centos:7
FROM centos:7
SHELL ["/bin/bash", "-c"]
WORKDIR /opt/app-root/app
ENV HOME /opt/app-root/app
RUN mkdir -p /opt/app-root/
COPY . . 
USER 0
ENV PATH $PATH:/root/.dotnet/tools:/opt/app-root/app/prime-dotnet-webapi-tests:/opt/app-root/app/.dotnet/tools/:/usr/share/dotnet
ENV ASPNETCORE_ENVIRONMENT Development
ENV JAVA_HOME /opt/app-root/app/jdk-11.0.2/bin
ENV PATH $PATH:$JAVA_HOME
RUN chmod +x *.bash && \
    useradd jenkins-prod && \
    chmod 777 /opt/app-root/app && \
    rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm && \
    curl -sL https://rpm.nodesource.com/setup_10.x | bash - && \
    yum -y install epel-release && \
    yum install -y -q which dotnet-sdk-2.2 gcc-c++ make nodejs nano xterm envsubst git wget && \
    wget https://download.java.net/java/GA/jdk11/9/GPL/openjdk-11.0.2_linux-x64_bin.tar.gz && \
    tar -zxf openjdk-11.0.2_linux-x64_bin.tar.gz && \
    npm install -g @angular/cli sonarqube-scanner && \
    dotnet tool install --global coverlet.console && \
    dotnet tool install --global dotnet-sonarscanner --version 4.7.1 && \
    wget https://jenkins-prod-dqszvc-tools.pathfinder.gov.bc.ca/jnlpJars/agent.jar && \
    mkdir -p /opt/app-root/app/jenkins && \
    mkdir -p /.dotnet && \
    chown -R 1001:1001 /.dotnet && \
    mkdir -p /.local && \
    chown -R 1001:1001 /.local && \
    mkdir -p /.nuget && \
    chown -R 1001:1001 /.nuget && \
    mkdir -p /tmp/NuGetScratch/ && \
    chown -R 1001:1001 /tmp/NuGetScratch/ && \
    chown -R 1001:1001 /opt/app-root/

USER jenkins-prod
#CMD [ "tail","-f","/dev/null" ]
CMD [ "./entrypoint.bash" ]
