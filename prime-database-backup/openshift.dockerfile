FROM postgres:10.6
USER 0

ENV PGPASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV PGDATABASE "${POSTGRESQL_DATABASE}"
ENV PGHOST "${POSTGRESQL_HOST}"
ENV PGUSERNAME "${POSTGRESQL_USER}"

COPY . /opt/backup
RUN apt-get update -yqq && \ 
    apt-get install -yqq inetutils-ping && \ 
    chmod -R 777 /opt/backup && \
    chmod +x /opt/backup/backup.sh && \
    chmod +x /opt/backup/entrypoint.sh && \
    cp /opt/backup/backup.sh /etc/cron.d/

CMD /opt/backup/entrypoint.sh