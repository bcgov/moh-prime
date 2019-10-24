#!/bin/sh
envsubst '$REDIRECT_URL' < /usr/src/prime-angular-frontend/src/environments/environment.prod.template.ts > /usr/src/prime-angular-frontend/src/environments/environment.prod.ts 
# Substitutes the environmental URL
