### Refactored from https://github.com/bcgov/SchemaSpy
FROM openjdk:jre-alpine
USER root
RUN apk update && \
    apk upgrade && \
    apk --no-cache add \
        tini \
        git \
        openssh-client && \
    apk --no-cache add --virtual \
        devs \
        tar \
        curl

RUN curl -L "https://github.com/mholt/caddy/releases/download/v0.10.10/caddy_v0.10.10_linux_amd64.tar.gz" \
    | tar --no-same-owner -C /usr/bin/ -xz caddy

RUN apk del devs

COPY Caddyfile /etc/Caddyfile

ENTRYPOINT ["/sbin/tini"]

RUN mkdir -p /var/www/html && \
    chmod g+w /var/www/html && \
    chmod g+w /etc/Caddyfile

EXPOSE 8080
ENV LC_ALL C
ENV OUTPUT_PATH=/var/www/html
ENV SCHEMA_SPY_VERSION=6.1.0
ENV POSTGRESQL_VERSION=42.2.1

RUN mkdir -p /app
WORKDIR /app/

RUN apk update && \
    apk add --no-cache \
        wget \
        ca-certificates \
        librsvg \
        graphviz \
        ttf-ubuntu-font-family && \
    mkdir -p lib && \
    wget -nv -O lib/schemaspy-$SCHEMA_SPY_VERSION.jar https://github.com/schemaspy/schemaspy/releases/download/v$SCHEMA_SPY_VERSION/schemaspy-$SCHEMA_SPY_VERSION.jar && \
    cp lib/schemaspy-$SCHEMA_SPY_VERSION.jar lib/schemaspy.jar && \
    wget -nv -O lib/pgsql-jdbc.jar http://central.maven.org/maven2/org/postgresql/postgresql/$POSTGRESQL_VERSION/postgresql-$POSTGRESQL_VERSION.jar && \
    apk del \
        wget \
        ca-certificates

COPY start.sh conf ./

RUN chown -R 1001:0 /app && \
    chmod -R ug+rwx /app

USER 1001

CMD [ "sh", "start.sh" ]
