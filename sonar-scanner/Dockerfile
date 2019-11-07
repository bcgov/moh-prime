FROM openshift/jenkins-slave-base-centos7
SHELL ["/bin/bash", "-c"]
COPY . /var/lib/origin 
USER 0

ENV JAVA_HOME=/usr/lib/jvm/java-1.8.0-openjdk-1.8.0.191.b12-1.el7_6.x86_64/jre/bin
ENV PATH $PATH:$JAVA_HOME:/var/lib/jenkins/tools/hudson.plugins.sonar.SonarRunnerInstallation/SonarQubeScanner/bin:/opt/sonar/bin
#COMMON
RUN echo "Installing common, Jenkins prerequisites..." && \
    chmod +x *.bash && \
    yum install -y epel-release java-1.8.0-openjdk-1.8.0.232 nano xterm envsubst git gunzip find which unzip && \
    useradd default && \
    chown -R default:0 /home/default && \
    chmod -R a+rwx /home/default && \
    chown -R default:0 /var/lib/origin && \
    chmod -R a+rwx /var/lib/origin && \
    wget https://jenkins-prod-dqszvc-tools.pathfinder.gov.bc.ca/jnlpJars/agent.jar && \
    chmod 777 /etc/passwd

# Node
RUN echo "Installing Node and Sonar Scanner..." && \
    curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.33.11/install.sh | bash && \ 
    yum install -y -q gcc-c++ make yarn nodejs && \    
    npm install -g @angular/cli sonarqube-scanner sonar-scanner chromium

#.NET 2.2
ENV ASPNETCORE_ENVIRONMENT Development
ENV PATH=$PATH:/home/default/.dotnet/tools
RUN echo "Installing .NET, coverlet, scanner..." && \
    rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm && \
    yum install -y -q dotnet-sdk-2.2 && \
    dotnet tool install --global coverlet.console && \
    dotnet tool install --global dotnet-sonarscanner --version 4.7.1 && \
    mkdir -p /.dotnet && \
    chown -R default:0 /.dotnet && \
    chmod -R a+rwx /.dotnet && \
    mkdir -p /.local && \
    chown -R default:0 /.local && \
    chmod -R a+rwx /.local && \
    mkdir -p /.nuget && \
    chown -R default:0 /.nuget && \
    chmod -R a+rwx /.nuget && \
    mkdir -p /tmp/NuGetScratch/ && \
    mkdir -p /tmp/NuGetScratch/lock && \
    chown -R default:0 /tmp/NuGetScratch/ && \
    chmod -R a+rwx /tmp/NuGetScratch/ && \
    chmod -R 777 /tmp/NuGetScratch/ 

USER 1000
ENTRYPOINT [ "./entrypoint.bash" ]
