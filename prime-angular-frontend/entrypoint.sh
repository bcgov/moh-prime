#!/bin/sh
envsubst '$branchName' < /etc/nginx/nginx.template.conf > /etc/nginx/conf.d/default.conf; 
nginx -g daemon-off;
