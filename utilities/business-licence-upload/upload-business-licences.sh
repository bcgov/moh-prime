#!/bin/bash

echo -e "-------- Getting access_token --------\n"
TOKEN=$(curl -X POST "${KEYCLOAK_URL}/realms/${KEYCLOAK_REALM}/protocol/openid-connect/token" \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "grant_type=client_credentials" \
  -d "client_id=${KEYCLOAK_CLIENT_ID}" \
  -d "client_secret=${KEYCLOAK_CLIENT_SECRET}" | python -c "import sys, json; print(json.load(sys.stdin)['access_token'])")
echo -e ${TOKEN}

FILE=$1

echo -e "-------- Uploading ${FILE} --------\n"
DOC_GUID=$(curl -X POST --data-binary "@${FILE}" -H "Authorization: Bearer ${TOKEN}" "${DOCMAN_DOCUMENTS_URL}?folder=licences&filename=${FILE}" | python -c "import sys, json; print(json.load(sys.stdin)['document_guid'])")
echo -e ${DOC_GUID}
