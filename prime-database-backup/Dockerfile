FROM postgres:10.6
USER 0

ENV PG_PASSWORD "postgres"
ENV PG_DATABASE "postgres"
ENV PG_PASSWORD "postgres"
ENV PG_USERNAME "postgres"

COPY . /opt/backup
RUN chmod -R 777 /opt/backup && \
    chmod +x /opt/backup/backup.sh && \
    chmod +x /opt/backup/entrypoint.sh && \
    cp /opt/backup/backup.sh /etc/cron.d/

CMD ./entrypoint.sh