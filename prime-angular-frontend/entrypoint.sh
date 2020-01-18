#!/bin/sh
envsubst '$SUFFIX' < /etc/nginx/nginx.template.conf > /etc/nginx/conf.d/default.conf 
echo "$TLS_PRIVATE" > /etc/nginx/private.key
echo "$TLS_CHAIN" >  /etc/nginx/chained.crt
nginx 
tail -f /dev/null
#nginx -g "daemon-off;"
