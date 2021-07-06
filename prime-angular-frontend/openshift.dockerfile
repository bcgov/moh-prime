###################################
### Stage 1 - Build environment ###
###################################
FROM public.ecr.aws/bitnami/node:14.17.0-prod AS build-deps


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
FROM registry.redhat.io/rhel8/nginx-118
ARG SVC_NAME
ENV SVC_NAME ${SVC_NAME}
USER 0
# COPY --from=build-deps /usr/src/app /opt/app-root/

COPY --from=build-deps /usr/src/app/nginx.conf /etc/nginx/nginx.conf
# COPY --from=build-deps /usr/src/app/dist/angular-frontend /usr/share/nginx/html
COPY --from=build-deps /usr/src/app/dist/angular-frontend /opt/app-root/src
COPY --from=build-deps /usr/src/app/openshift.nginx.conf /tmp/openshift.nginx.conf 
RUN sed s/\$SVC_NAME/$SVC_NAME/g /tmp/openshift.nginx.conf > /etc/nginx/conf.d/prime.conf && \
    chown -R 1001200000:1001200000 /etc/nginx /opt/app-root/ 

# Create symlinks to redirect nginx logs to stdout and stderr
RUN bash -xeu -c 'mkdir -p /var/log/nginx'
RUN  ln -sf /dev/stdout /var/log/nginx/access.log \
  && ln -sf /dev/stderr /var/log/nginx/error.log

USER 1001200000
EXPOSE 80 8080 4200:8080
CMD ["sh","-c","nginx -g 'daemon off;'"]
