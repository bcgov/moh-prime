#!/usr/bin/env bash

# TODO: Exit immediately if a command exits with a non-zero status.
# set -e

main() {

  echo -e "-------- STARTING CRON --------\n"

#  LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -W ${PGPASSWORD} -c 'select max(ptl."TransactionId") from "PharmanetTransactionLog" ptl')
  LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -t -c 'select count(*) from "HealthAuthorityLookup" h')
  echo -e "Last transaction id:  ${LAST_TX_ID}\n"

  UUID=$(cat /proc/sys/kernel/random/uuid)
  # Trim whitespace
  UUID=`echo ${UUID} | sed 's/ *$//g'`
  echo -e "Generated request id:  ${UUID}\n"

  echo -e "API client name:  ${PRIME_ODR_API_CLIENT_NAME}\n"

  # ls -l /etc/ssl/certs
  curl -v --cert /opt/certs/prime-odr-api-cert.crt:${PRIME_ODR_API_SSL_CERT_PASSWORD} --key /opt/certs/prime-odr-api-cert.key --header "Authorization: Basic ${PRIME_ODR_API_ENCODED_CREDENTIALS}" \
    -X GET https://t1primedatasvc.maximusbc.ca/odr/prime/pnetdata/transactionLog?requestUUID=${UUID}&clientName=${PRIME_ODR_API_CLIENT_NAME}&lastTxnId=${LAST_TX_ID}&fetchSize=${PRIME_ODR_API_FETCH_SIZE}
}

main  # Ensure the whole file is downloaded before executing
