###################################
### Stage 1 - Build environment ###
###################################
FROM public.ecr.aws/bitnami/node:14.15.5-prod AS builder

# Set working directory
RUN id -u
WORKDIR /usr/src/app

# Set environment variables
ENV REDIRECT_URL $REDIRECT_URL
ENV VANITY_URL $VANITY_URL
ENV OC_APP $OC_APP
ENV KEYCLOAK_URL $KEYCLOAK_URL
ENV KEYCLOAK_REALM $KEYCLOAK_REALM
ENV KEYCLOAK_CLIENT_ID $KEYCLOAK_CLIENT_ID
ENV JWT_WELL_KNOWN_CONFIG $JWT_WELL_KNOWN_CONFIG
ENV DOCUMENT_MANAGER_URL $DOCUMENT_MANAGER_URL

RUN apt-get update

COPY . .

# Fill template with environment variables
RUN pwd
RUN echo hello
RUN (eval "echo \"$(cat /usr/src/app/src/environments/environment.prod.template.ts )\"" ) > /usr/src/app/src/environments/environment.prod.ts

# Install Angular CLI
RUN npm install -g @angular/cli

# Install dependencies
# COPY package.json package.json
RUN npm install

# Add application
RUN ng build --prod


########################################
### Stage 2 - Production environment ###
########################################
FROM public.ecr.aws/nginx/nginx:1.18

WORKDIR /app

# Edit folder permissions
RUN chmod -R 766 /etc/nginx
RUN chmod -R 666 /var/cache/nginx
RUN chmod -R 666 /var/lib/nginx

RUN touch /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/nginx.conf
COPY nginx.template.conf /etc/nginx/nginx.template.conf
COPY entrypoint.sh /

COPY --from=builder /usr/src/app/dist/angular-frontend /usr/share/nginx/html

# RUN chmod +x /entrypoint.sh
# RUN chmod 777 /entrypoint.sh
# RUN echo "Build completed."

# COPY ./entrypoint.sh /app
RUN chmod +x /entrypoint.sh

EXPOSE 80 8080 4200:8080

CMD /entrypoint.sh
