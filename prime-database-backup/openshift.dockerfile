FROM docker-registry.default.svc:5000/dqszvc-tools/ubuntu:18.04
USER 0
ENV PGPASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV POSTGRESQL_PASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV POSTGRESQL_DATABASE "${POSTGRESQL_DATABASE}"
ENV POSTGRESQL_ADMIN_PASSWORD "${POSTGRESQL_ADMIN_PASSWORD}"
ENV POSTGRESQL_USER "${POSTGRESQL_USER}"
RUN apt-get update && \
    apt-get install -yqq wget curl nano net-utils nmap && \
    wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | apt-key add - && \
    sh -c 'echo "deb http://apt.postgresql.org/pub/repos/apt/ $(lsb_release -sc)-pgdg main" > /etc/apt/sources.list.d/PostgreSQL.list' && \
    apt-get update && \
    apt-get install -yqq postgresql-client-10 postgresql-10 && \
    systemctl enable postgresql.service