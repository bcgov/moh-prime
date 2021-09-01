#!/usr/bin/env bash

# TODO: Exit immediately if a command exits with a non-zero status.
# set -e

function get_last_tx_id() {
  local LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -t -c 'select max(ptl."TransactionId") from "PharmanetTransactionLog" ptl')
  # Trim whitespace
  LAST_TX_ID=`echo ${LAST_TX_ID} | sed 's/^ *//g'`
  # Handle initial empty table condition
  if [ "${LAST_TX_ID}" = '' ]; then LAST_TX_ID='0'; fi
  # "Return" result to caller
  echo "${LAST_TX_ID}"
}


function main() {
  echo -e "-------- STARTING CRON --------\n"

  echo -e "Connecting to database host:  _${PGHOST}_\n"

  LAST_TX_ID="$(get_last_tx_id)"
  echo -e "Last transaction id:  _${LAST_TX_ID}_\n"

  UUID=$(cat /proc/sys/kernel/random/uuid)
  echo -e "Generated request id:  _${UUID}_\n"

  echo -e "API client name:  _${PRIME_ODR_API_CLIENT_NAME}_\n"
  echo -e "Fetch size:  _${PRIME_ODR_API_FETCH_SIZE}_\n"


  # CA certs need to be in place:  https://stackoverflow.com/questions/3160909/how-do-i-deal-with-certificates-using-curl-while-trying-to-access-an-https-url
  # ls -l /etc/ssl/certs
  curl --cert /opt/certs/prime-odr-api-cert.crt:${PRIME_ODR_API_SSL_CERT_PASSWORD} --key /opt/certs/prime-odr-api-cert.key --header "Authorization: Basic ${PRIME_ODR_API_ENCODED_CREDENTIALS}" \
    "https://t1primedatasvc.maximusbc.ca/odr/prime/pnetdata/transactionLog?requestUUID=${UUID}&clientName=${PRIME_ODR_API_CLIENT_NAME}&lastTxnId=${LAST_TX_ID}&fetchSize=${PRIME_ODR_API_FETCH_SIZE}" | \
    python3 /opt/scripts/parse_api_response.py | \
    psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -c "\copy \"PharmanetTransactionLog\"(\"TransactionId\", \"TxDateTime\", \"UserId\", \"SourceIpAddress\", \"LocationIpAddress\", \"PharmacyId\", \"TransactionType\", \"TransactionSubType\", \"PractitionerId\", \"CollegePrefix\", \"TransactionOutcome\", \"ProviderSoftwareId\", \"ProviderSoftwareVersion\") FROM STDIN (FORMAT CSV)"

  echo -e "\n-------- Read file? --------\n"
  cat /tmp/isThereMoreData.txt

  LAST_TX_ID="$(get_last_tx_id)"
  echo -e "Last transaction id:  _${LAST_TX_ID}_\n"
}

main  # Ensure the whole file is downloaded before executing
