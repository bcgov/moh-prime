#!/bin/sh
envsubst '$branchName' < /etc/nginx/conf.d/nginx.template.conf > /etc/nginx/conf.d/default.conf; 
nginx -g daemon-off;
