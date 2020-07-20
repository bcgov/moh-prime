FROM gitlab-runner:latest
USER 0
RUN mkdir -p /opt/backup

WORKDIR /opt/backup

COPY . /opt
COPY . /opt/backup
COPY backup.cron /etc/cron.d

RUN echo "Checking workdir..." && \
    apt-get update -yqq && \ 
    apt-get install -y inetutils-ping nano net-tools cron gnupg wget apt-transport-https ca-certificates libcurl4-openssl-dev

CMD /opt/entrypoint.sh
