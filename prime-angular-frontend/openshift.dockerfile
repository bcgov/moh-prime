# Stage 1:  Build an Angular Docker Image
FROM docker-registry.default.svc:5000/dqszvc-tools/node:14 as build

USER 0
ENV NODE_ROOT /usr/src/app
ENV REDIRECT_URL $REDIRECT_URL
ENV VANITY_URL $VANITY_URL
ENV OC_APP $OC_APP

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
ENV DOCUMENT_MANAGER_URL $DOCUMENT_MANAGER_URL

RUN echo "Populating environment..." && \
    (eval "echo \"$(cat /usr/src/app/src/environments/environment.prod.template.ts )\"" ) > /usr/src/app/src/environments/environment.prod.ts
RUN cat /usr/src/app/src/environments/environment.prod.ts && \
    npm install @angular/cli  -g --silent && \
    npm install && \
    ng build --prod && \
    echo "NPM packages installed..."


# Stage 2:  Use the compiled app, ready for production with Nginx
FROM docker-registry.default.svc:5000/dqszvc-tools/nginx:1.18.0

RUN apt-get update && \
    apt-get install -y gettext-base && \
    mkdir -p /var/cache/nginx && \
    mkdir -p /var/lib/nginx && \
    mkdir -p /var/log/nginx && \
    mkdir -p /var/cache/nginx/client_temp && \
    touch /etc/nginx/conf.d/default.conf && \
    chmod -R 777 /etc/nginx && \
    chmod -R 777 /var/cache/nginx && \
    chmod -R 777 /var/lib/nginx && \
    chmod -R 777 /var/run && \
    chmod -R 777 /var/lib && \
    chmod -R 777 /var/log

COPY --from=build /usr/src/app/dist/angular-frontend /usr/share/nginx/html

COPY nginx.conf /etc/nginx/
COPY nginx.template.conf /etc/nginx/nginx.template.conf
COPY entrypoint.sh /

RUN chmod +x /entrypoint.sh && \
    chmod 777 /entrypoint.sh && \
    echo "Build completed."

COPY ./entrypoint.sh /
RUN chmod +x /entrypoint.sh

EXPOSE 80 8080 4200:8080

CMD /entrypoint.sh
