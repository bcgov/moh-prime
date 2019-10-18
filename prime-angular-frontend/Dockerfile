# base image
FROM node:10.16 as build-deps
SHELL [ "/bin/bash","-c"]
# set working directory
ENV NODE_ROOT /usr/src/app
RUN mkdir -p /usr/src/app && \
    echo "Branch name: ${branchName}" && \
    echo "Suffix ${SUFFIX}"
WORKDIR /usr/src/app
COPY . .

RUN npm install @angular/cli -g --silent && \ 
    npm install && \
    ng build --prod && \
    echo "NPM packages installed..."

FROM nginx:1.15-alpine
COPY --from=build-deps /usr/src/app/dist/angular-frontend /usr/share/nginx/html
RUN rm -f /etc/nginx/conf.d/default.conf
# COPY --from=build-deps /usr/src/app/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build-deps /usr/src/app/nginx.template.conf /etc/nginx/conf.d/nginx.template.conf
RUN envsubst '$branchName' < /etc/nginx/conf.d/nginx.template.conf > /etc/nginx/conf.d/default.conf && \
    grep proxy /etc/nginx/conf.d/default.conf
# Due to OpenShift not allowing root user at run, 
# We need to enforce user 0 to get these following changes done:
USER 0
EXPOSE 8080
#RUN envsubst branchName < nginx.template.conf > nginx.conf

RUN mkdir -p /var/cache/nginx && \ 
    mkdir -p /var/cache/nginx/client_temp && \ 
    chown -R 1000:1000 /var/cache/nginx && \ 
    chmod -R 777 /var/cache/nginx && \ 
    chmod -R 777 /var/run && \
    echo "Build completed."
# Also, we are not allowed to bind to port 80 in NGINX as a non-root user

#CMD envsubst branchName < nginx.template.conf > nginx.conf && exec nginx -g daemon off;
CMD ["nginx", "-g", "daemon off;"]
