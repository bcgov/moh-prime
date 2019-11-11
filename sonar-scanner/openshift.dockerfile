#FROM docker-registry.default.svc:5000/dqszvc-tools/jenkins-slave-base-centos7
FROM docker-registry.default.svc:5000/dqszvc-tools/openjdk:8-jdk
ARG VERSION=3.35
ARG user=jenkins
ARG group=jenkins
ARG uid=1000
ARG gid=1000
RUN groupadd -g ${gid} ${group} && \
    useradd -c "${user} user" -d /home/${user} -u ${uid} -g ${gid} -m ${user}
LABEL Description="This is a base image, which provides the ${user} agent executable (agent.jar)" Vendor="${user} project" Version="${VERSION}"

ARG AGENT_WORKDIR=/home/${user}/agent
ENV AGENT_WORKDIR=${AGENT_WORKDIR}
WORKDIR /home/${user}

RUN echo 'deb http://deb.debian.org/debian stretch-backports main' > /etc/apt/sources.list.d/stretch-backports.list
RUN mkdir /home/${user}/.${user} && \
    mkdir -p ${AGENT_WORKDIR} && \
    apt-get -yq update && \
    apt-get -yq install -t stretch-backports \
        git-lfs \
        curl \
        unzip \
        xvfb \
        libxi6 \
        libgconf-2-4 \
        maven \
        apt-transport-https && \
    curl --create-dirs -fsSLo /usr/share/${user}/agent.jar https://jenkins-prod:9000/jnlpJars/agent.jar && \
    chmod 755 /usr/share/${user} && \
    chmod 644 /usr/share/${user}/agent.jar && \
    ln -sf /usr/share/${user}/agent.jar /usr/share/${user}/slave.jar

VOLUME /home/${user}/.${user}
VOLUME ${AGENT_WORKDIR}

SHELL ["/bin/bash", "-c"]
COPY . ${AGENT_WORKDIR}

# ENV JAVA_HOME=/usr/lib/jvm/java-1.8.0-openjdk-1.8.0.191.b12-1.el7_6.x86_64/jre/bin
ENV PATH $PATH:$JAVA_HOME:/var/lib/${user}/tools/hudson.plugins.sonar.SonarRunnerInstallation/SonarQubeScanner/bin:/opt/sonar/bin
#COMMON
RUN echo "Installing common, ${user} and Sonar Scanner prerequisites..." && \
    apt-get -yq install openjdk-8-jre && \
    wget -q http://sourceforge.net/projects/sonar-pkg/files/deb/binary/sonar_6.7.4_all.deb && \
    dpkg -i sonar_6.7.4_all.deb && \
    chown -R ${user}:${group} /home/${user} && \
    chmod -R a+rwx /home/${user} && \
    chown -R ${user}:${group} ${AGENT_WORKDIR} && \
    chmod -R a+rwx ${AGENT_WORKDIR} && \
    chmod 777 /etc/passwd

# Headless Chrome
RUN curl -sS -o - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - && \
    echo "deb [arch=amd64]  http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google-chrome.list && \
    apt-get -yq update && \
    apt-get -yq install google-chrome-stable && \
    wget -q https://chromedriver.storage.googleapis.com/2.41/chromedriver_linux64.zip && \
    unzip chromedriver_linux64.zip && \
    mv chromedriver /usr/bin/chromedriver && \
    chown root:root /usr/bin/chromedriver && \
    chmod +x /usr/bin/chromedriver && \
    chmod 777 /usr/bin/chromedriver

# Node
RUN echo "Installing Node..." && \
    curl -sL https://deb.nodesource.com/setup_12.x | bash - && \
    apt-get -yq install nodejs && \
    echo 'kernel.unprivileged_userns_clone=1' > /etc/sysctl.d/userns.conf && \
    export PUPPETEER_SKIP_CHROMIUM_DOWNLOAD=1 && \
    npm install -g --silent @angular/cli \
        @angular/core \ 
        @angular-devkit/build-angular \
        @angular/compiler \ 
        @angular/compiler-cli \ 
        typescript \ 
        puppeteer \ 
        jasmine \ 
        karma \ 
        karma-chrome-launcher \ 
        karma-mocha \ 
        karma-chai \ 
        karma-jasmine \
        karma-jasmine-html-reporter \
        karma-coverage-istanbul-reporter 

#.NET 2.2
ENV ASPNETCORE_ENVIRONMENT Development
ENV PATH=$PATH:/home/${user}/.dotnet/tools
RUN echo "Installing .NET, coverlet, scanner..." && \
    wget -q -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg && \
    mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/ && \
    wget -q -q https://packages.microsoft.com/config/debian/9/prod.list && \
    mv prod.list /etc/apt/sources.list.d/microsoft-prod.list && \
    chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg && \
    chown root:root /etc/apt/sources.list.d/microsoft-prod.list && \
    apt-get -yq update && \
    apt-get -yq install dotnet-sdk-2.2 && \
    dotnet tool install --global coverlet.console && \
    dotnet tool install --global dotnet-sonarscanner --version 4.7.1 && \
    mkdir -p /.dotnet && \
    chown -R ${user}:${group} /.dotnet && \
    chmod -R a+rwx /.dotnet && \
    mkdir -p /.local && \
    chown -R ${user}:${group} /.local && \
    chmod -R a+rwx /.local && \
    mkdir -p /.nuget && \
    chown -R ${user}:${group} /.nuget && \
    chmod -R a+rwx /.nuget && \
    mkdir -p /tmp/NuGetScratch/ && \
    mkdir -p /tmp/NuGetScratch/lock && \
    chown -R ${user}:${group} /tmp/NuGetScratch/ && \
    chmod -R a+rwx /tmp/NuGetScratch/ && \
    chmod -R 777 /tmp/NuGetScratch/ 

# All files in ${user} home need to be writable 
RUN chown -R ${user}:${group} /home/${user} && \
    chmod -R a+rwx /home/${user} && \
    chown -R ${user}:${group} ${AGENT_WORKDIR} && \
    chmod -R a+rwx ${AGENT_WORKDIR} && \
    chown -R ${user}:${group} ${AGENT_WORKDIR} && \
    chmod -R a+rwx ${AGENT_WORKDIR}

# For local testing
#COPY ../ ./moh-prime
USER ${user}
ENTRYPOINT [ "${AGENT_WORKDIR}/entrypoint.bash" ]