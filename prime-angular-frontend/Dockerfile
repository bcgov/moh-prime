# base image
FROM node:14.16 as buildDeps
#SHELL [ "/bin/bash","-c"]
# set working directory
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

RUN cat /usr/src/app/src/environments/environment.prod.ts
RUN npm install @angular/cli -g && \
    npm install && \
    ng build --configuration=production && \
    echo "NPM packages installed..."

# RUN npm audit fix && \
# FROM nginx:1.15-alpine
# COPY --from=buildDeps /usr/src/app/dist/angular-frontend /usr/share/nginx/html
# RUN rm -f /etc/nginx/conf.d/default.conf
# COPY --from=buildDeps /usr/src/app/nginx.conf /etc/nginx/
# COPY --from=buildDeps /usr/src/app/nginx.template.conf /etc/nginx/nginx.template.conf
# COPY --from=build-deps /usr/src/app/nginx${OC_APP}.conf /etc/nginx/nginx.template.conf
# COPY --from=buildDeps /usr/src/app/entrypoint.sh /etc/nginx

RUN chmod +x /entrypoint.sh && \
    chmod 777 /entrypoint.sh && \
    echo "Build completed."

#WORKDIR /

COPY ./entrypoint.sh /
RUN chmod +x /entrypoint.sh

EXPOSE 80 8080 4200:8080

#CMD /etc/nginx/entrypoint.sh
CMD /entrypoint.sh
