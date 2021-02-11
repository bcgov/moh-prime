### Stage 1: Create build environment ###
FROM node:10.16
RUN mkdir /app
WORKDIR /app

# Set environment variables
ENV REDIRECT_URL $REDIRECT_URL
ENV VANITY_URL $VANITY_URL
ENV OC_APP $OC_APP
ENV KEYCLOAK_URL $KEYCLOAK_URL
ENV KEYCLOAK_REALM $KEYCLOAK_REALM
ENV KEYCLOAK_CLIENT_ID $KEYCLOAK_CLIENT_ID
ENV JWT_WELL_KNOWN_CONFIG $JWT_WELL_KNOWN_CONFIG
ENV DOCUMENT_MANAGER_URL $DOCUMENT_MANAGER_URL

# Copy package definition, install them, then copy everything into working directory and build it
# COPY package.json package-lock.json ./
COPY . ./
RUN echo "Populating environment..."
RUN (eval "echo \"$(cat /app/src/environments/environment.prod.template.ts )\"" ) > /app/src/environments/environment.prod.ts
RUN npm install
RUN npm run build


### Stage 2: Run Angular & Nginx ###
FROM nginx:stable

RUN apt-get update
RUN touch /etc/nginx/conf.d/default.conf
RUN chmod -R 777 /etc/nginx

COPY nginx.conf /etc/nginx/nginx.conf
COPY nginx.template.conf /etc/nginx/nginx.template.conf
COPY entrypoint.sh /

RUN chmod -R 777 /app/src
RUN ln /app/dist/angular-frontend /usr/share/nginx/html

RUN chmod +x /entrypoint.sh
RUN chmod 777 /entrypoint.sh
RUN echo "Build completed."

COPY ./entrypoint.sh /
RUN chmod +x /entrypoint.sh

EXPOSE 80 8080 4200:8080

CMD /entrypoint.sh
