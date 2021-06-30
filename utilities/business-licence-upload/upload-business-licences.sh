#!/bin/bash

echo -e "-------- Getting access_token --------"
token=$(curl -X POST "${KEYCLOAK_URL}/realms/${KEYCLOAK_REALM}/protocol/openid-connect/token" \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "grant_type=client_credentials" \
  -d "client_id=${KEYCLOAK_CLIENT_ID}" \
  -d "client_secret=${KEYCLOAK_CLIENT_SECRET}" \
  -s | python -c "import sys, json; print(json.load(sys.stdin)['access_token'])")
if [ $? -eq 0 ]; then
  echo -e ${token}
else
  echo -e "Problem obtaining Keycloak token, please check Keycloak parameters."
  exit 1
fi

output="${OUTPUT_FILENAME}"
# Create new file (or replace) in working directory
printf 'PEC,Filename,Document GUID\n' > "${output}"

num_files=0
num_upload_errors=0
num_filename_format_errors=0
data_dir=$1
for file in $data_dir/*
do
  ((num_files++))
  echo -e "-------- Uploading ${file} and obtaining Document GUID --------"

  filename_only="$(basename "${file}")"

  url_encoded_filename=$(python -c "import urllib.parse, sys; print(urllib.parse.quote(sys.argv[1]))" "${filename_only}")

  doc_guid=$(curl -X POST --data-binary "@${file}" -H "Authorization: Bearer ${token}" -s "${DOCMAN_DOCUMENTS_URL}?folder=${DOCMAN_DEST_DIR}&filename=${url_encoded_filename}" | python get_doc_guid.py)
  if [ $? -eq 0 ]; then
    echo -e ${doc_guid}
    # Expect files to be named like "PEC - Business License.pdf"
    pec=$(python -c "import sys, re; match = re.search('^[A-Z]{3}', sys.argv[1]); print(match.group() if match is not None else 'N/A')" "${filename_only}")
    if [ "$pec" = 'N/A' ]; then
      ((num_filename_format_errors++))
    else
      # Append to output file
      printf "${pec},${filename_only},${doc_guid}\n" >> "${output}"
    fi
  else
    echo -e "WARNING:  ${file} failed to upload to Document Manager."
    ((num_upload_errors++))
  fi
done
echo -e "\n\nStatistics:  Number of files: ${num_files}, number of errors uploading: ${num_upload_errors}, number of unexpected filenames: ${num_filename_format_errors}\n"

