# base image
FROM node:10.16 as build-deps
#SHELL [ "/bin/bash","-c"]
# set working directory
ENV NODE_ROOT /usr/src/app
ENV REDIRECT_URL $REDIRECT_URL
RUN mkdir -p /usr/src/app && \
    pwd && \
    echo $REDIRECT_URL && \
    echo "Step 1 environment..." && \
    printenv
WORKDIR /usr/src/app

COPY . .

RUN sed s/'$REDIRECT_URL'/$REDIRECT_URL/g /usr/src/app/src/environments/environment.prod.template.ts > /usr/src/app/src/environments/environment.prod.ts && \
    cat /usr/src/app/src/environments/environment.prod.ts && \
    npm install @angular/cli -g --silent && \ 
    npm install && \
    chmod +x /usr/src/app/midpoint.sh && \ 
    /usr/src/app/midpoint.sh && \
    cat /usr/src/app/src/environments/environment.prod.ts && \
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