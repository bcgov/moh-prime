#!/bin/sh
envsubst '$SUFFIX' < /etc/nginx/nginx.template.conf > /etc/nginx/conf.d/default.conf 
nginx -g "pid /tmp/nginx.pid; worker_processes 2; daemon-off;"
tail -f /dev/null

#nginx -g "daemon-off;"
