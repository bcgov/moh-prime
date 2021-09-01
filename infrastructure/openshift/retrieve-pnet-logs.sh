#!/usr/bin/env bash

# TODO: Exit immediately if a command exits with a non-zero status.
# set -e

main() {

  echo -e "-------- STARTING CRON --------\n"

#  LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -W ${PGPASSWORD} -c 'select max(ptl."TransactionId") from "PharmanetTransactionLog" ptl')
  LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -t -c 'select count(*) from "HealthAuthorityLookup" h')
  # Trim whitespace
  LAST_TX_ID=`echo ${LAST_TX_ID} | sed 's/^ *//g'`
  echo -e "Last transaction id:  _${LAST_TX_ID}_\n"

  UUID=$(cat /proc/sys/kernel/random/uuid)
  echo -e "Generated request id:  _${UUID}_\n"

  echo -e "API client name:  _${PRIME_ODR_API_CLIENT_NAME}_\n"
  echo -e "Fetch size:  _${PRIME_ODR_API_FETCH_SIZE}_\n"

  PROCESS_JSON_SCRIPT=(curl -s "https://raw.githubusercontent.com/bcgov/moh-prime/8553e5cec508e8454d7661f4b8a6d255afc7cea5/infrastructure/openshift/parse_api_response.py")

  # CA certs need to be in place:  https://stackoverflow.com/questions/3160909/how-do-i-deal-with-certificates-using-curl-while-trying-to-access-an-https-url
  # ls -l /etc/ssl/certs
  curl --cert /opt/certs/prime-odr-api-cert.crt:${PRIME_ODR_API_SSL_CERT_PASSWORD} --key /opt/certs/prime-odr-api-cert.key --header "Authorization: Basic ${PRIME_ODR_API_ENCODED_CREDENTIALS}" \
    "https://t1primedatasvc.maximusbc.ca/odr/prime/pnetdata/transactionLog?requestUUID=${UUID}&clientName=${PRIME_ODR_API_CLIENT_NAME}&lastTxnId=${LAST_TX_ID}&fetchSize=${PRIME_ODR_API_FETCH_SIZE}" | \
    python3 -c ${PROCESS_JSON_SCRIPT}


  echo -e "\n-------- Write to file? --------\n"
  pwd
  touch /tmp/more.txt
  ls -l /tmp
}

main  # Ensure the whole file is downloaded before executing
