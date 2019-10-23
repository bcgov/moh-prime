#!/bin/sh
wall "SUFFIX=$SUFFIX"
echo "SUFFIX=$SUFFIX" > /tmp/suffix.out
envsubst '$SUFFIX' < /etc/nginx/nginx.template.conf > /etc/nginx/conf.d/default.conf 
nginx
tail -f /dev/null
#nginx -g "daemon-off;"
