FROM postgres:10.6
USER 0
ENV PGPASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV PGDATABASE "${POSTGRESQL_DATABASE}"
ENV PGHOST "${POSTGRESQL_HOST}"
ENV PGUSERNAME "${POSTGRESQL_USER}"
ENV MONGO_HOST "${MONGO_HOST}"
ENV MONGO_DATABASE "${MONGO_DATABASE}"
ENV MONGO_CONNECTION_STRING "host=${MONGO_HOST};port=27017;database=db;username=root;password=${POSTGRESQL_ADMIN_PASSWORD}"
RUN mkdir -p /opt/backup

WORKDIR /opt/backup

COPY . /opt
COPY . /opt/backup
COPY backup.cron /etc/cron.d

RUN echo "Checking workdir..." && \
    apt-get update -yqq && \ 
    apt-get install -y inetutils-ping nano net-tools cron gnupg wget apt-transport-https ca-certificates libcurl4-openssl-dev

RUN wget -qO - http://www.mongodb.org/static/pgp/server-4.2.asc | apt-key add - && \
    echo "deb [ arch=amd64 ] https://repo.mongodb.org/apt/ubuntu bionic/mongodb-org/4.2 multiverse" | tee /etc/apt/sources.list.d/mongodb-org-4.2.list && \
    apt-get update && \
    apt-get install -y mongodb-tools mongodb-clients && \
    ls -alh /opt

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
