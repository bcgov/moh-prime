#!/bin/sh
function replaceString() {
sed s/$1/$2/g  /usr/src/app/src/environments/environment.prod.template.ts > /usr/src/app/src/environments/environment.prod.ts
}
replaceString '$REDIRECT_URL' "$REDIRECT_URL"
# echo "REDIRECT_URL = $REDIRECT_URL"
# echo "sed "
# envsubst '$REDIRECT_URL' < /usr/src/app/src/environments/environment.prod.template.ts > /usr/src/app/src/environments/environment.prod.ts
# Substitutes the environmental URL
