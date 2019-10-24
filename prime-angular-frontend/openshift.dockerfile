# base image
FROM node:10.16 as build-deps
#SHELL [ "/bin/bash","-c"]
# set working directory
ENV NODE_ROOT /usr/src/app
RUN mkdir -p /usr/src/app 
WORKDIR /usr/src/app

COPY . .

RUN npm install @angular/cli -g --silent && \ 
    npm install && \
    ls -alh && \
    /usr/src/prime-angular-frontend/midpoint.sh && \
    ng build --prod && \
    echo "NPM packages installed..."

FROM nginx:1.15-alpine
COPY --from=build-deps /usr/src/app/dist/angular-frontend /usr/share/nginx/html
RUN rm -f /etc/nginx/conf.d/default.conf 
#COPY --from=build-deps /usr/src/app/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build-deps /usr/src/app/nginx.template.conf /etc/nginx/nginx.template.conf
USER 0
RUN echo "SUFFIX=$SUFFIX"
#RUN envsubst '$SUFFIX' < /etc/nginx/nginx.template.conf > /etc/nginx/conf.d/default.conf
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