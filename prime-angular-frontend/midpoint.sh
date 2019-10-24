#!/bin/sh
echo "REDIRECT_URL = $REDIRECT_URL"
envsubst '$REDIRECT_URL' < /usr/src/app/src/environments/environment.prod.template.ts > //usr/src/app/src/environments/environment.prod.ts
# Substitutes the environmental URL
