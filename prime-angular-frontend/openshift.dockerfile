FROM node:10.16 as build-deps

ENV NODE_ROOT /usr/src/app
ENV REDIRECT_URL ${REDIRECT_URL}
ENV OC_APP ${OC_APP}
RUN mkdir -p /usr/src/app

WORKDIR /usr/src/app

COPY . .


RUN KEYCLOAK_URL=$(grep KEYCLOAK_URL /usr/src/app/src/environments/keycloak.env.$OC_APP) && \
    KEYCLOAK_REALM=$(grep KEYCLOAK_REALM /usr/src/app/src/environments/keycloak.env.$OC_APP) && \
    KEYCLOAK_CLIENT_ID=$(grep KEYCLOAK_CLIENT_ID /usr/src/app/src/environments/keycloak.env.$OC_APP) 

RUN (eval "echo \"$(cat /usr/src/app/src/environments/environment.prod.template.ts )\"" ) > /usr/src/app/src/environments/environment.prod.ts && \
    cat /usr/src/app/src/environments/environment.prod.ts

RUN npm install @angular/cli -g --silent && \ 
    npm install && \
    ng build --prod && \
    echo "NPM packages installed..." && \

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

#CMD ["nginx", "-g", "daemon off;"]

ENTRYPOINT /home/entrypoint.sh