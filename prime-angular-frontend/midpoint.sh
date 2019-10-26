#!/bin/sh
sed s/'$REDIRECT_URL'/$REDIRECT_URL/g /usr/src/app/src/environments/environment.prod.template.ts > /usr/src/app/src/environments/environment.prod.ts
# echo "REDIRECT_URL = $REDIRECT_URL"
# echo "sed "
# envsubst '$REDIRECT_URL' < /usr/src/app/src/environments/environment.prod.template.ts > /usr/src/app/src/environments/environment.prod.ts
# Substitutes the environmental URL
