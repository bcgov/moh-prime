#!/bin/sh
envsubst '$branchName' < /etc/nginx/nginx.template.conf > /etc/nginx/conf.d/default.conf 
nginx
#nginx -g "daemon-off;"
