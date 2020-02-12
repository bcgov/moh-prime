FROM postgres:10.6
USER 0
ENV PGPASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV PGDATABASE "${POSTGRESQL_DATABASE}"
ENV PGHOST "${POSTGRESQL_HOST}"
ENV PGUSERNAME "${POSTGRESQL_USER}"

RUN mkdir -p /opt/backup

WORKDIR /opt/backup

COPY . /opt
COPY . /opt/backup
COPY backup.cron /etc/cron.d

RUN echo "Checking opt dir..." && \
    ls -alh /opt && \
    echo "Checking workdir..." && \
    ls -alh /opt/backup && \
    apt-get update -yqq && \ 
    apt-get install -yqq inetutils-ping vim nano net-tools cron && \ 
    ls -alh /opt

RUN chmod -R 777 /opt/backup && \
    touch /var/log/cron.log && \
    chmod 777 /var/log/cron.log && \
    chmod +x /opt/backup.sh && \
    chmod +x /opt/entrypoint.sh && \
    chmod 0644 /opt/backup/backup.cron && \
    cp /opt/backup/backup.cron /etc/cron.d/ && \
    cp /opt/backup.sh /etc/cron.daily && \
    crontab /etc/cron.d/backup.cron && \
    crontab -l && \
    chmod 777 /var/run
    
#CMD tail -F /dev/null
CMD /opt/entrypoint.sh
