FROM postgres:10.6
USER 0

ENV PGPASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV PGDATABASE "${POSTGRESQL_DATABASE}"
ENV PGHOST "${POSTGRESQL_HOST}"
ENV PGUSERNAME "${POSTGRESQL_USER}"
RUN ls -alh && \
    mkdir -p /opt/backup && \
    ls -alh /opt && \
    ls -alh /opt/backup
COPY . /opt/backup
RUN apt-get update -yqq && \ 
    apt-get install -yqq inetutils-ping && \ 
    ls -alh /opt && \
    chmod -R 777 /opt/backup && \
    chmod +x /opt/backup/backup.sh && \
    chmod +x /opt/backup/entrypoint.sh && \
    cp /opt/backup/backup.sh /etc/cron.d/
CMD tail -F /dev/null
#CMD /opt/backup/entrypoint.sh