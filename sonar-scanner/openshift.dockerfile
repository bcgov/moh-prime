#FROM docker-registry.default.svc:5000/dqszvc-tools/jenkins-slave-nodejs-rhel7
FROM docker-registry.default.svc:5000/dqszvc-tools/jenkins-slave-base-centos7
#FROM openshift/jenkins-slave-base-centos7
#FROM openshift/jenkins-slave-nodejs-centos7
SHELL ["/bin/bash", "-c"]
COPY . /var/lib/origin 
USER 0
#ENV PATH $PATH:/root/.dotnet/tools:/opt/app-root/app/prime-dotnet-webapi-tests:/opt/app-root/app/.dotnet/tools/:/usr/share/dotnet
ENV ASPNETCORE_ENVIRONMENT Development
ENV JAVA_HOME=/usr/lib/jvm/java-1.8.0-openjdk-1.8.0.191.b12-1.el7_6.x86_64/jre/bin
ENV PATH $PATH:$JAVA_HOME:/var/lib/jenkins/tools/hudson.plugins.sonar.SonarRunnerInstallation/SonarQubeScanner/bin:/home/default/.dotnet/tools
RUN chmod +x *.bash && \
    useradd default && \
    rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm && \
    yum -y install epel-release && \
    yum install -y -q dotnet-sdk-2.2 java-1.8.0-openjdk-1.8.0.232 gcc-c++ make nodejs nano xterm envsubst git find which && \
    npm install -g @angular/cli sonarqube-scanner && \
    dotnet tool install --global coverlet.console && \
    dotnet tool install --global dotnet-sonarscanner --version 4.7.1 && \
    wget https://jenkins-prod-dqszvc-tools.pathfinder.gov.bc.ca/jnlpJars/agent.jar && \
    mkdir -p /.dotnet && \
    chown -R default:0 /.dotnet && \
    mkdir -p /.local && \
    chown -R default:0 /.local && \
    mkdir -p /.nuget && \
    chown -R default:0 /.nuget && \
    mkdir -p /tmp/NuGetScratch/ && \
    chown -R default:0 /tmp/NuGetScratch/ && \
    mkdir -p /opt/app-root/app && \
    mkdir -p /opt/app-root/out && \
    chown -R default:0 /opt/app-root && \
    chmod 777 /etc/passwd

#USER jenkins
#CMD [ "tail","-f","/dev/null" ]
#CMD [ "./entrypoint.bash" ]
#ENTRYPOINT [ "./entrypoint.bash" ]
CMD ["/usr/lib/jvm/java-1.8.0-openjdk-1.8.0.191.b12-1.el7_6.x86_64/jre/bin/java","-jar agent.jar","-jnlpUrl http://jenkins-prod/computer/code-tests/slave-agent.jnlp","-secret c598ca95983a9f6df4d06cc7f770b0d1ea404b806851f1a7f1066d89515c2c12","-workDir $HOME"]