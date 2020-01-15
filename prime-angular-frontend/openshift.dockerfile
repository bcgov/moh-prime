# base image
FROM node:10.16 as build-deps
#SHELL [ "/bin/bash","-c"]
# set working directory
ENV NODE_ROOT /usr/src/app
ENV REDIRECT_URL $REDIRECT_URL
ENV OC_APP $OC_APP
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

COPY . .
RUN KEYCLOAK_URL=$(grep KEYCLOAK_URL /usr/src/app/src/environments/keycloak.env.$OC_APP | cut -d "=" -f2) && \
    KEYCLOAK_REALM=$(grep KEYCLOAK_REALM /usr/src/app/src/environments/keycloak.env.$OC_APP | cut -d "=" -f2) && \
    KEYCLOAK_CLIENT_ID=$(grep KEYCLOAK_CLIENT_ID /usr/src/app/src/environments/keycloak.env.$OC_APP | cut -d "=" -f2) && \
    (eval "echo \"$(cat /usr/src/app/src/environments/environment.prod.template.ts )\"" ) > /usr/src/app/src/environments/environment.prod.ts
RUN cat /usr/src/app/src/environments/environment.prod.ts && \
    npm install @angular/cli -g --silent && \ 
    npm install && \
    npm audit fix && \
    ng build --prod && \
    echo "NPM packages installed..." 

FROM nginx:1.15-alpine
COPY --from=build-deps /usr/src/app/dist/angular-frontend /usr/share/nginx/html
RUN rm -f /etc/nginx/conf.d/default.conf 
#COPY --from=build-deps /usr/src/app/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build-deps /usr/src/app/nginx.template.conf /etc/nginx/nginx.template.conf
COPY --from=build-deps /usr/src/app/entrypoint.sh /home

EXPOSE 8080
RUN mkdir -p /var/cache/nginx && \ 
    mkdir -p /var/cache/nginx/client_temp && \ 
    chown -R 1001:1001 /var/cache/nginx && \ 
    touch /etc/nginx/conf.d/default.conf && \
    chown -R 1001:1001 /etc/nginx/conf.d/ && \
    chmod 777 /etc/nginx/conf.d/default.conf && \
    chmod -R 777 /var/cache/nginx && \ 
    chmod -R 777 /var/run && \
    chmod +x /home/entrypoint.sh && \
    chmod 777 /home/entrypoint.sh && \
    echo "Build completed."
RUN export CERTBOT_DEPS="py-pip \
                         build-base \
                         libffi-dev \
                         python-dev \
                         ca-certificates \
                         openssl-dev \
                         linux-headers \
                         dialog \
                         wget" && \
            apk --update add openssl \
                             augeas-libs \
                             ${CERTBOT_DEPS}

RUN pip install --upgrade --no-cache-dir pip virtualenv

#RUN mkdir /letsencrypt
#WORKDIR /letsencrypt

# Get the certbot so we can use Lets Encrypt
RUN wget https://dl.eff.org/certbot-auto
RUN chmod a+x certbot-auto

# Clean up
RUN apk del ${CERTBOT_DEPS}
RUN rm -rf /var/cache/apk/*

WORKDIR /

COPY ./run.sh /
RUN chmod a+x /run.sh

EXPOSE 80 8080 4200:8080

CMD ["sh", "/run.sh"]

#CMD ["nginx", "-g", "daemon off;"]

#CMD ["/home/entrypoint.sh"]