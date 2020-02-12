FROM postgres:10.6
USER 0

ENV PGPASSWORD "postgres"
ENV PGDATABASE "postgres"
ENV PGHOST "primedb"
ENV PGUSERNAME "postgres"

COPY . /opt/backup/

RUN apt-get update -yqq && \ 
    apt-get install -yqq inetutils-ping cron && \ 
    chmod -R 777 /opt/backup && \
    chmod +x /opt/backup/*.sh && \
    chmod 0644 /opt/backup/backup.cron && \
    cp /opt/backup/backup.cron /etc/cron.d/ && \
    crontab /etc/cron.d/backup.cron

CMD /opt/backup/entrypoint.sh
