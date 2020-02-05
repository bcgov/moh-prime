# base image
FROM node:10.16 as build-deps
#SHELL [ "/bin/bash","-c"]
# set working directory
USER 0
ENV NODE_ROOT /usr/src/app
ENV REDIRECT_URL $REDIRECT_URL
ENV OC_APP $OC_APP
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

COPY . .
ENV KEYCLOAK_URL $KEYCLOAK_URL
ENV KEYCLOAK_REALM $KEYCLOAK_REALM
ENV KEYCLOAK_CLIENT_ID $KEYCLOAK_CLIENT_ID
ENV JWT_WELL_KNOWN_CONFIG $JWT_WELL_KNOWN_CONFIG

RUN echo "Populating environment..." && \
    (eval "echo \"$(cat /usr/src/app/src/environments/environment.prod.template.ts )\"" ) > /usr/src/app/src/environments/environment.prod.ts
RUN cat /usr/src/app/src/environments/environment.prod.ts && \
    npm install @angular/cli -g --silent && \ 
    npm install && \
    npm audit fix && \
    ng build --prod && \
    echo "NPM packages installed..." 

FROM nginx:1.15-alpine

ENV KEYCLOAK_URL $KEYCLOAK_URL
ENV KEYCLOAK_REALM $KEYCLOAK_REALM
ENV KEYCLOAK_CLIENT_ID $KEYCLOAK_CLIENT_ID
ENV JWT_WELL_KNOWN_CONFIG $JWT_WELL_KNOWN_CONFIG
RUN rm -f /etc/nginx/conf.d/default.conf 
COPY --from=build-deps /usr/src/app/dist/angular-frontend /usr/share/nginx/html
COPY --from=build-deps /usr/src/app/nginx.conf /etc/nginx/
#COPY --from=build-deps /usr/src/app/nginx.template.conf /etc/nginx/nginx.template.conf
COPY --from=build-deps /usr/src/app/nginx${OC_APP}.conf /etc/nginx/nginx.template.conf
COPY --from=build-deps /usr/src/app/entrypoint.sh /etc/nginx

EXPOSE 8080
RUN mkdir -p /var/cache/nginx && \ 
    mkdir -p /var/cache/nginx/client_temp && \ 
    touch /etc/nginx/conf.d/default.conf && \
    chmod -R 777 /etc/nginx && \
    chmod -R 777 /var/cache/nginx && \ 
    chmod -R 777 /var/run && \
    chmod +x /etc/nginx/entrypoint.sh && \
    chmod 777 /etc/nginx/entrypoint.sh && \
    echo "Build completed."

WORKDIR /

COPY ./entrypoint.sh /
RUN chmod a+x /entrypoint.sh

EXPOSE 80 8080 4200:8080

CMD /etc/nginx/entrypoint.sh`