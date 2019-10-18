# base image
FROM node:10.16 as build-deps
ARG branchName="$branchName"
SHELL [ "/bin/bash","-c"]
# set working directory
ENV NODE_ROOT /usr/src/app
ENV branchName $branchName
RUN mkdir -p /usr/src/app && \
    find -type f -name branchName.env
WORKDIR /usr/src/app
COPY . .
RUN cat branchName.env && \
    source branchName.env && \
    echo "branchName=$branchName"
RUN npm install @angular/cli -g --silent && \ 
    npm install && \
    ng build --prod && \
    echo "NPM packages installed..."

FROM nginx:1.15-alpine
COPY --from=build-deps /usr/src/app/dist/angular-frontend /usr/share/nginx/html
RUN rm -f /etc/nginx/conf.d/default.conf 
#COPY --from=build-deps /usr/src/app/nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build-deps /usr/src/app/nginx.template.conf /etc/nginx/conf.d/nginx.template.conf
RUN envsubst '$branchName' < /etc/nginx/conf.d/nginx.template.conf > /etc/nginx/conf.d/default.conf && \
    grep proxy /etc/nginx/conf.d/default.conf
EXPOSE 8080
RUN mkdir -p /var/cache/nginx && \ 
    mkdir -p /var/cache/nginx/client_temp && \ 
    chown -R 1000:1000 /var/cache/nginx && \ 
    chmod -R 777 /var/cache/nginx && \ 
    chmod -R 777 /var/run && \
    echo "Build completed."

CMD ["nginx", "-g", "daemon off;"]
