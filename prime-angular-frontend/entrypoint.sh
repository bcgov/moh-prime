#!/bin/sh
whoami
echo "Substituting environment..."
envsubst '$SUFFIX' < /etc/nginx/nginx.template.conf > /etc/nginx/conf.d/default.conf 
echo "Running nginx..."
runuser -u nginx -- nginx
echo "Keeping container alive..."
tail -f /dev/null

#nginx -g "daemon-off;"
