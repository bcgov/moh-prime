FROM docker-registry.default.svc:5000/dqszvc-tools/postgresql:10.6
USER 0

ENV PG_PASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV PG_DATABASE "${POSTGRESQL_DATABASE}"
ENV PG_PASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV PG_USERNAME "${POSTGRESQL_USER}"

COPY . /opt/backup
RUN chmod -R 777 /opt/backup && \
    chmod +x /opt/backup/backup.sh && \
    chmod +x /opt/backup/entrypoint.sh && \
    cp /opt/backup/backup.sh /etc/cron.d/
CMD /opt/backup/entrypoint.sh