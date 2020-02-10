FROM postgres:10.6
USER 0

ENV PGPASSWORD "postgres"
ENV PGDATABASE "postgres"
ENV PGHOST "primedb"
ENV PGUSERNAME "postgres"

COPY . /opt/backup/

RUN apt-get update -yqq && \ 
    apt-get install -yqq inetutils-ping && \ 
    chmod -R 777 /opt/backup && \
    chmod +x /opt/backup/*.sh && \
    cp /opt/backup/backup.cron /etc/cron.d/

CMD /opt/backup/entrypoint.sh