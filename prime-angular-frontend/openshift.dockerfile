###################################
### Stage 1 - Build environment ###
###################################
FROM public.ecr.aws/bitnami/node:14.15.5-prod AS build-deps

# Set working directory
ENV NODE_ROOT /usr/src/app
RUN mkdir -p /usr/src/app
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

COPY package.json package-lock.json ./

COPY . .

# Fill template with environment variables
#RUN (eval "echo \"$(cat /usr/src/app/src/environments/environment.prod.template.ts )\"" ) > /usr/src/app/src/environments/environment.prod.ts
# Install Angular CLI
RUN npm install -g @angular/cli
# Install dependencies
# COPY package.json package.json
RUN npm ci

# Add application
RUN ng build --prod
USER 0

# Debugging
# RUN find -type f -name *.js

########################################
### Stage 2 - Production environment ###
########################################
FROM registry.redhat.io/rhel8/nginx-118
ENV REDIRECT_URL $REDIRECT_URL
ENV VANITY_URL $VANITY_URL
ENV OC_APP $OC_APP
ENV KEYCLOAK_URL $KEYCLOAK_URL
ENV KEYCLOAK_REALM $KEYCLOAK_REALM
ENV KEYCLOAK_CLIENT_ID $KEYCLOAK_CLIENT_ID
ENV JWT_WELL_KNOWN_CONFIG $JWT_WELL_KNOWN_CONFIG
ENV DOCUMENT_MANAGER_URL $DOCUMENT_MANAGER_URL


# Edit folder permissions
# RUN chmod 766 -R /etc/nginx
# RUN chmod 666 /var/cache/nginx
# RUN chmod 666 /var/lib/nginx

# RUN touch /etc/nginx/conf.d/default.conf
# COPY nginx.conf /etc/nginx/nginx.conf
# COPY nginx.template.conf /etc/nginx/conf.d/default.conf
# COPY entrypoint.sh /

COPY --from=build-deps /usr/src/app /opt/app-root/
COPY --from=build-deps /usr/src/app/nginx.conf /etc/nginx/nginx.conf
COPY --from=build-deps /usr/src/app/openshift.nginx.conf /etc/nginx/conf.d/prime.conf
COPY --from=build-deps /usr/src/app/nginx.template.conf /etc/nginx/nginx.template.conf
USER 0
RUN chmod 766 ./environments/environment.prod.template.ts && \
    envsubst < ./environments/environment.prod.template.ts > ./environments/environment.ts
# RUN chmod +x /entrypoint.sh
# RUN chmod 777 /entrypoint.sh
# RUN echo "Build completed."

# COPY ./entrypoint.sh /app
# RUN chmod +x /entrypoint.sh

EXPOSE 80 8080 4200:8080

# CMD /entrypoint.sh
# CMD ["sh", "-c", "envsubst '$SUFFIX' < /etc/nginx/nginx.template.conf > /etc/nginx/conf.d/default.conf && exec nginx -g 'daemon off;'"]
CMD ["sh","-c","nginx -g 'daemon off;'"]