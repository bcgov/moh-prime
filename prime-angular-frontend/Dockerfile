# base image
FROM node:10.16 as build-deps
SHELL [ "/bin/bash","-c"]
# set working directory
ENV NODE_ROOT /usr/src/app
RUN mkdir -p /usr/src/app 
WORKDIR /usr/src/app

COPY . .

RUN npm install @angular/cli -g --silent && \ 
    npm install && \
    ng build --prod && \
    echo "NPM packages installed..."

FROM nginx:1.15-alpine
COPY --from=build-deps /usr/src/app/dist/angular-frontend /usr/share/nginx/html
RUN rm -f /etc/nginx/conf.d/default.conf 
#COPY --from=build-deps /usr/src/app/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build-deps /usr/src/app/nginx.template.conf /etc/nginx/conf.d
COPY --from=build-deps /usr/src/app/entrypoint.sh /home

#RUN envsubst '$branchName' < /etc/nginx/conf.d/nginx.template.conf > /etc/nginx/conf.d/default.conf

EXPOSE 8080
RUN mkdir -p /var/cache/nginx && \ 
    mkdir -p /var/cache/nginx/client_temp && \ 
    chown -R 1000:1000 /var/cache/nginx && \ 
    chmod -R 777 /var/cache/nginx && \ 
    chmod -R 777 /var/run && \
    chmod +x /home/entrypoint.sh && \
    chmod 777 /home/entrypoint.sh && \
    echo "Build completed."

#CMD ["nginx", "-g", "daemon off;"]
CMD /home/entrypoint.sh
