# base image
FROM node:10.16 as build-deps

# set working directory
ENV NODE_ROOT /usr/src/app
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

COPY . .

RUN npm install @angular/cli -g && \
    npm install && \
    ng build --prod --subresource-integrity

FROM nginx

FROM nginx:1.15-alpine
COPY --from=build-deps /usr/src/app/dist/angular-frontend /usr/share/nginx/html

RUN rm /etc/nginx/conf.d/default.conf
COPY --from=build-deps /usr/src/app/nginx.conf /etc/nginx/nginx.conf

RUN export CERTBOT_DEPS="py-pip \
                         build-base \
                         libffi-dev \
                         python-dev \
                         ca-certificates \
                         openssl-dev \
                         linux-headers \
                         dialog \
                         wget" && \
            apk --update add openssl \
                             augeas-libs \
                             ${CERTBOT_DEPS}

RUN pip install --upgrade --no-cache-dir pip virtualenv

#RUN mkdir /letsencrypt
#WORKDIR /letsencrypt

# Get the certbot so we can use Lets Encrypt
RUN wget https://dl.eff.org/certbot-auto
RUN chmod a+x certbot-auto

# Clean up
RUN apk del ${CERTBOT_DEPS}
RUN rm -rf /var/cache/apk/*

WORKDIR /

#COPY ./run.sh /
#RUN chmod a+x /run.sh

EXPOSE 80 8080 4200:8080

#CMD ["sh", "/run.sh"]

CMD ["nginx", "-g", "daemon off;"]
