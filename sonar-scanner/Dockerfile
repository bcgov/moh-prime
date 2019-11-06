FROM openshift/jenkins-slave-base-centos7
SHELL ["/bin/bash", "-c"]
COPY . /var/lib/origin 
USER 0
ENV ASPNETCORE_ENVIRONMENT Development
ENV JAVA_HOME=/usr/lib/jvm/java-1.8.0-openjdk-1.8.0.191.b12-1.el7_6.x86_64/jre/bin
ENV PATH $PATH:$JAVA_HOME:/var/lib/jenkins/tools/hudson.plugins.sonar.SonarRunnerInstallation/SonarQubeScanner/bin:/home/jenkins/.dotnet/tools:/home/default/.dotnet/tools
RUN chmod +x *.bash && \
    useradd default && \
    useradd jenkins && \
    rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm && \
    yum install -y -q epel-release && \
    yum install -y -q dotnet-sdk-2.2 java-1.8.0-openjdk-1.8.0.232 gcc-c++ make nodejs nano xterm envsubst git find which && \
    npm install -g @angular/cli sonarqube-scanner sonar-scanner chromium && \
    dotnet tool install --global coverlet.console && \
    dotnet tool install --global dotnet-sonarscanner --version 4.7.1 && \
    wget https://jenkins-prod-dqszvc-tools.pathfinder.gov.bc.ca/jnlpJars/agent.jar && \
    chown -R default:0 /home/jenkins && \
    chmod -R a+rwx /home/jenkins && \
    chown -R default:0 /home/default && \
    chmod -R a+rwx /home/default && \
    chown -R default:0 /var/lib/origin && \
    chmod -R a+rwx /var/lib/origin && \
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
    chmod -R 777 /tmp/NuGetScratch/ && \
    mkdir -p /opt/app-root/app && \
    mkdir -p /opt/app-root/out && \
    chown -R default:0 /opt/app-root && \
    chmod -R a+rwx /opt/app-root && \
    chmod 777 /etc/passwd
    
ENTRYPOINT [ "./entrypoint.bash" ]
