FROM postgres:12.20
USER 0
ARG PGPASSWORD
ARG PGDATABASE
ARG PGHOST
ARG PGUSERNAME
ARG MONGO_HOST
ARG MONGO_DATABASE

ENV PGPASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV PGDATABASE "${POSTGRESQL_DATABASE}"
ENV PGHOST "${POSTGRESQL_HOST}"
ENV PGUSERNAME "${POSTGRESQL_USER}"

RUN mkdir -p /opt/backup

WORKDIR /opt/backup

COPY . /opt
COPY . /opt/backup
COPY backup.cron /etc/cron.d


RUN echo "Checking workdir..." && \
    apt-get update -yqq

RUN apt-get -y install nano net-tools gnupg wget ca-certificates apt-transport-https cron inetutils-ping libcurl4-openssl-dev 

RUN chmod -R 755 /opt/backup && \
    touch /var/log/cron.log && \
    chmod 777 /var/log/cron.log && \
    chmod +x /opt/backup.sh && \
    chmod +x /opt/entrypoint.sh && \
    chmod 0644 /opt/backup/backup.cron && \
    cp /opt/backup/backup.cron /etc/cron.d/ && \
    cp /opt/backup.sh /etc/cron.daily && \
    cp /opt/backup.sh /etc/cron.hourly && \
    crontab /etc/cron.d/backup.cron && \
    crontab -l && \
    chmod 777 /var/run

#CMD tail -F /dev/null
CMD /opt/entrypoint.sh
