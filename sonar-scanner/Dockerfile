#FROM docker-registry.default.svc:5000/dqszvc-tools/jenkins-slave-nodejs-rhel7
FROM openshift/jenkins-slave-nodejs-centos7
SHELL ["/bin/bash", "-c"]
COPY . /var/lib/origin 
USER 0
#ENV PATH $PATH:/root/.dotnet/tools:/opt/app-root/app/prime-dotnet-webapi-tests:/opt/app-root/app/.dotnet/tools/:/usr/share/dotnet
ENV ASPNETCORE_ENVIRONMENT Development
#ENV JAVA_HOME /opt/app-root/app/jdk-11.0.2/bin
#ENV PATH $PATH:$JAVA_HOME
RUN chmod +x *.bash 
RUN useradd jenkins 
RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm 
RUN yum -y install epel-release 
RUN yum install -y -q which dotnet-sdk-2.2 gcc-c++ make nano xterm envsubst git wget 
RUN npm install -g @angular/cli sonarqube-scanner 
RUN dotnet tool install --global coverlet.console 
RUN dotnet tool install --global dotnet-sonarscanner --version 4.7.1 
RUN wget https://jenkins-prod-dqszvc-tools.pathfinder.gov.bc.ca/jnlpJars/agent.jar 
RUN mkdir -p /.dotnet && \
    chown -R jenkins:jenkins /.dotnet 
RUN mkdir -p /.local && \
    chown -R jenkins:jenkins /.local 
RUN mkdir -p /.nuget && \
    chown -R jenkins:jenkins /.nuget 
RUN mkdir -p /tmp/NuGetScratch/ && \
    chown -R jenkins:jenkins /tmp/NuGetScratch/ 
RUN chmod 777 /etc/passwd
#USER jenkins
#CMD [ "tail","-f","/dev/null" ]
CMD [ "./entrypoint.bash" ]
