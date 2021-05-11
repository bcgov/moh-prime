###################################
### Stage 1 - Build environment ###
###################################
FROM public.ecr.aws/bitnami/node:14.15.5-prod AS build-deps

ARG DOCUMENT_MANAGER_URL 
ARG JWT_WELL_KNOWN_CONFIG
ARG KEYCLOAK_CLIENT_ID
ARG KEYCLOAK_REALM
ARG KEYCLOAK_URL
ARG OC_APP
ARG REDIRECT_URL
ARG VANITY_URL
ARG NAME

# Set working directory
ENV DOCUMENT_MANAGER_URL ${DOCUMENT_MANAGER_URL}
ENV JWT_WELL_KNOWN_CONFIG ${JWT_WELL_KNOWN_CONFIG}
ENV KEYCLOAK_URL ${KEYCLOAK_URL}
ENV KEYCLOAK_REALM ${KEYCLOAK_REALM}
ENV KEYCLOAK_CLIENT_ID ${KEYCLOAK_CLIENT_ID}
ENV OC_APP ${OC_APP}
ENV REDIRECT_URL ${REDIRECT_URL}
ENV VANITY_URL ${VANITY_URL}
ENV NAME ${NAME}
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
FROM registry.redhat.io/rhel8/nginx-118
USER 0
COPY --from=build-deps /usr/src/app /opt/app-root/
COPY --from=build-deps /usr/src/app/nginx.conf /etc/nginx/nginx.conf
COPY --from=build-deps /usr/src/app/openshift.nginx.conf /tmp/openshift.nginx.conf 
RUN (eval "echo \"$(cat /usr/src/app/openshift.nginx.conf )\"" ) > /etc/nginx/conf.d/prime.conf && \
    chown -R 1001200000:1001200000 /etc/nginx
USER 1001200000
EXPOSE 80 8080 4200:8080
CMD ["sh","-c","nginx -g 'daemon off;'"]