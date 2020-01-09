#!/usr/bin/env sh

# Creates the directory the cert will be in
if [ ! -z "$LETSENCRYPT_WEBROOT_PATH" ]; then
    mkdir -p $LETSENCRYPT_WEBROOT_PATH
else
    mkdir /etc/ssl/letsencrypt
fi

# $LETSENCRYPT_FIRST_TIME is an environment variable
if [ "$LETSENCRYPT_FIRST_TIME" = "true" ]
then
    cd /letsencrypt

    # Build the certbot-command depending on environment variables
    CERTONLY_COMMAND="./certbot-auto certonly --webroot"

#    export LETSENCRYPT_AGREE_TOS='true'
    if [ "$LETSENCRYPT_AGREE_TOS" = "true" ]; then
        CERTONLY_COMMAND="$CERTONLY_COMMAND --agree-tos"
    fi

#    export LETSENCRYPT_EMAIL="jwanglof@gmail.com"
    if [ ! -z "$LETSENCRYPT_EMAIL" ]; then
        CERTONLY_COMMAND="$CERTONLY_COMMAND --email $LETSENCRYPT_EMAIL"
    fi

#    export LETSENCRYPT_WEBROOT_PATH="/asd/asd"
    if [ ! -z "$LETSENCRYPT_WEBROOT_PATH" ]; then
        CERTONLY_COMMAND="$CERTONLY_COMMAND --webroot-path $LETSENCRYPT_WEBROOT_PATH"
    fi

    # It's only possible to use multi-domains or one domain
    #export LETSENCRYPT_DOMAINS=botillsammans.nu,www.botillsammans.nu
#    export LETSENCRYPT_DOMAIN=example.com
    if [ ! -z "$LETSENCRYPT_DOMAINS" ]; then
#        CERTONLY_COMMAND="$CERTONLY_COMMAND --domains [$LETSENCRYPT_DOMAINS]"
        CERTONLY_COMMAND="$CERTONLY_COMMAND --domains $LETSENCRYPT_DOMAINS"
    elif [ ! -z "$LETSENCRYPT_DOMAIN" ]; then
        CERTONLY_COMMAND="$CERTONLY_COMMAND --domain $LETSENCRYPT_DOMAIN"
    else
        echo "You must choose at least one domain"
        exit 1
    fi

    # ssl@botillsammans.nu is an alias of jwanglof@botillsammans.nu
    #./certbot-auto certonly --webroot --agree-tos --email ssl@botillsammans.nu -w /etc/ssl/botillsammans -d botillsammans.nu -d www.botillsammans.nu
#    ./certbot-auto certonly --webroot --agree-tos --email ssl@botillsammans.nu -w /etc/ssl/botillsammans -d botillsammans.klumpen.se
    if [ -z "$LETSENCRYPT_DRY_RUN" ]; then
        echo "Will run this certbot-command:", ${CERTONLY_COMMAND}
        ${CERTONLY_COMMAND}
        echo "Certbot-command DONE"
    else
        echo "DRY RUN!"
        echo "Would run this certbot-command:", ${CERTONLY_COMMAND}
    fi
else
    echo "Not first time"
fi

# $LETSENCRYPT_RENEW is an environment variable
if [ "$LETSENCRYPT_RENEW" = "true" ]
then
    echo "Renew the cert!"
else
    echo "No renew"
fi

# Go back to root as default
cd /

echo "Starting Nginx"
#
envsubst '$SUFFIX' < /etc/nginx/nginx.template.conf > /etc/nginx/conf.d/default.conf 
nginx 
tail -f /dev/null
/usr/sbin/nginx -c /etc/nginx/nginx.conf -g "daemon off;"
#
echo "Failed starting Nginx!"