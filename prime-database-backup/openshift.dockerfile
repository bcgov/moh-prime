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

RUN echo "Checking opt dir..." && \
    ls -alh /opt && \
    echo "Checking workdir..." && \
    ls -alh /opt/backup && \
    apt-get update -yqq && \ 
    apt-get install -yqq inetutils-ping vim nano net-tools && \ 
    ls -alh /opt

RUN chmod -R 777 /opt/backup && \
    chmod +x /opt/backup.sh && \
    chmod +x /opt/entrypoint.sh && \
    cp /opt/backup/backup.cron /etc/cron.d/ && \
    cp /opt/* /opt/backup

#CMD tail -F /dev/null
CMD /opt/entrypoint.sh
