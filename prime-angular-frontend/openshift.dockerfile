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
COPY package.json yarn.lock ./

COPY . .

# Install Angular CLI
RUN npm set unsafe-perm true
RUN npm install -g @angular/cli
# Install dependencies
RUN npm ci
# Add application
RUN ng build --configuration=production

########################################
### Stage 2 - Production environment ###
########################################
FROM public.ecr.aws/bitnami/nginx:1.20
ARG SVC_NAME
ENV SVC_NAME ${SVC_NAME}

COPY --from=build-deps /usr/src/app/dist/angular-frontend /opt/app-root/src

# USER 1001200000
EXPOSE 80 8080 4200:8080
CMD ["sh","-c","nginx -g 'daemon off;'"]
