# base image
FROM node:10.16 as build-deps
#SHELL [ "/bin/bash","-c"]
# set working directory
ENV NODE_ROOT /usr/src/app
ENV REDIRECT_URL ${REDIRECT_URL}
ENV OC_APP ${OC_APP}
RUN mkdir -p /usr/src/app && \
    pwd && \
    echo "RedirectURL = $REDIRECT_URL" && \
    echo "OC APP = $OC_APP" && \
    echo "Step 1 environment..."
WORKDIR /usr/src/app`

COPY . .

RUN set -o allexport ; \
    [ -f /usr/src/app/src/environments/keycloak.${OC_APP}.env ] && \
    . /usr/src/app/src/environments/keycloak.${OC_APP}.env ; \
    set +o allexport ; \
    echo "Method 1" && \
    printenv
RUN '(eval "echo \"$(cat /usr/src/app/src/environments/environment.prod.template.ts )\"" )' | sh && \
    echo "Method 2" && \
    printenv 
#RUN /usr/src/app/src/environments/keycloak.$OC_APP.sh
RUN '(eval "echo \"$(cat /usr/src/app/src/environments/environment.prod.template.ts )\"" )' > /usr/src/app/src/environments/environment.prod.ts
RUN cat /usr/src/app/src/environments/environment.prod.ts && \
    npm install @angular/cli -g --silent && \ 
    npm install && \
    ng build --prod && \
    echo "NPM packages installed..." && \
    printenv

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