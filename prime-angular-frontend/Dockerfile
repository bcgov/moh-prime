FROM node:10.16 as buildDeps
USER 0
ENV NODE_ROOT /usr/src/app
ENV REDIRECT_URL $REDIRECT_URL
RUN mkdir -p /usr/src/app

RUN printenv && \
    pwd && \
    ls -alh
WORKDIR /usr/src/app

COPY . .
ENV KEYCLOAK_URL $KEYCLOAK_URL
ENV KEYCLOAK_REALM $KEYCLOAK_REALM
ENV KEYCLOAK_CLIENT_ID $KEYCLOAK_CLIENT_ID
ENV JWT_WELL_KNOWN_CONFIG $JWT_WELL_KNOWN_CONFIG
RUN apt-get update && \
    apt-get install -y nginx gettext-base && \
    mkdir -p /var/cache/nginx && \
    mkdir -p /var/cache/nginx/client_temp && \
    touch /etc/nginx/conf.d/default.conf && \
    chmod -R 777 /etc/nginx && \
    chmod -R 777 /var/cache/nginx && \
    chmod -R 777 /var/run
COPY nginx.conf /etc/nginx/
COPY nginxdev.conf /etc/nginx/nginx.template.conf
COPY entrypoint.sh /
RUN echo "Populating environment..." && \
    (eval "echo \"$(cat /usr/src/app/src/environments/environment.prod.template.ts )\"" ) > /usr/src/app/src/environments/environment.prod.ts
RUN cat /usr/src/app/src/environments/environment.prod.ts
RUN npm install @angular/cli -g && \
    npm install && \
    ng build --prod && \
    echo "NPM packages installed..."

RUN chmod +x /entrypoint.sh && \
    chmod 777 /entrypoint.sh && \
    echo "Build completed."

COPY ./entrypoint.sh /
RUN chmod +x /entrypoint.sh

EXPOSE 80 8080 4200:8080

CMD /entrypoint.sh
