# Stage 1:  Build an Angular Docker Image
FROM node:14 as build


## Everything should be proxied through nginx now, no separate url
# ARG DOCUMENT_MANAGER_URL 
ARG JWT_WELL_KNOWN_CONFIG
ARG KEYCLOAK_CLIENT_ID
ARG KEYCLOAK_REALM
ARG KEYCLOAK_URL
ARG OC_APP
ARG REDIRECT_URL
ARG VANITY_URL

# Set working directory
# ENV DOCUMENT_MANAGER_URL ${DOCUMENT_MANAGER_URL}
ENV JWT_WELL_KNOWN_CONFIG ${JWT_WELL_KNOWN_CONFIG}
ENV KEYCLOAK_URL ${KEYCLOAK_URL}
ENV KEYCLOAK_REALM ${KEYCLOAK_REALM}
ENV KEYCLOAK_CLIENT_ID ${KEYCLOAK_CLIENT_ID}
ENV OC_APP ${OC_APP}
ENV REDIRECT_URL ${REDIRECT_URL}
ENV VANITY_URL ${VANITY_URL}
ENV NODE_ROOT /usr/src/app

RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

# Set environment variables
COPY package.json package-lock.json ./

COPY . .

# Fill template with environment variables
RUN (eval "echo \"$(cat /usr/src/app/src/environments/environment.prod.template.ts )\"" ) > /usr/src/app/src/environments/environment.prod.ts
# Install Angular CLI
RUN npm install -g @angular/cli
# Install dependencies
RUN npm ci
# Add application
RUN ng build --prod

########################################
### Stage 2 - Production environment ###
########################################
FROM nginx:1.21.0-alpine
RUN apt-get update && \
    apt-get install -y gettext-base && \
    mkdir -p /var/cache/nginx && \
    mkdir -p /var/lib/nginx && \
    mkdir -p /var/log/nginx && \
    mkdir -p /var/cache/nginx/client_temp && \
    touch /etc/nginx/conf.d/default.conf && \
    chmod -R 777 /etc/nginx && \
    chmod -R 777 /var/cache/nginx && \
    chmod -R 777 /var/lib/nginx && \
    chmod -R 777 /var/run && \
    chmod -R 777 /var/lib && \
    chmod -R 777 /var/log

COPY --from=build /usr/src/app/dist/angular-frontend /usr/share/nginx/html

COPY nginx.conf /etc/nginx/
COPY nginx.template.conf /etc/nginx/nginx.template.conf
COPY entrypoint.sh /

RUN chmod +x /entrypoint.sh && \
    chmod 777 /entrypoint.sh && \
    echo "Build completed."

COPY ./entrypoint.sh /
RUN chmod +x /entrypoint.sh

EXPOSE 80 8080 4200:8080

CMD /entrypoint.sh
