#!/bin/sh
wall "SUFFIX=$SUFFIX"
envsubst '$SUFFIX' < /etc/nginx/nginx.template.conf > /etc/nginx/conf.d/default.conf 
nginx
tail -f /dev/null
#nginx -g "daemon-off;"
