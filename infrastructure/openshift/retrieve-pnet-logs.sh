#!/usr/bin/env bash

# TODO: Exit immediately if a command exits with a non-zero status.
# set -e

main() {

  echo -e "-------- STARTING CRON --------\n"

#  LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -W ${PGPASSWORD} -c 'select max(ptl."TransactionId") from "PharmanetTransactionLog" ptl')
  LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -t -c 'select count(*) from "HealthAuthorityLookup" h')
  # Trim whitespace
  LAST_TX_ID=`echo ${LAST_TX_ID} | sed 's/^ *//g'`
  echo -e "Last transaction id:  ${LAST_TX_ID}\n"

  UUID=$(cat /proc/sys/kernel/random/uuid)
  echo -e "Generated request id:  ${UUID}\n"

  echo -e "API client name:  ${PRIME_ODR_API_CLIENT_NAME}\n"

  # CA certs need to be in place:  https://stackoverflow.com/questions/3160909/how-do-i-deal-with-certificates-using-curl-while-trying-to-access-an-https-url
  # ls -l /etc/ssl/certs
  curl --cert /opt/certs/prime-odr-api-cert.crt:${PRIME_ODR_API_SSL_CERT_PASSWORD} --key /opt/certs/prime-odr-api-cert.key --header "Authorization: Basic ${PRIME_ODR_API_ENCODED_CREDENTIALS}" \
    https://t1primedatasvc.maximusbc.ca/odr/prime/pnetdata/transactionLog?requestUUID=${UUID}&lastTxnId=${LAST_TX_ID}
    # ?requestUUID=${UUID}&clientName=${PRIME_ODR_API_CLIENT_NAME}&lastTxnId=${LAST_TX_ID}&fetchSize=${PRIME_ODR_API_FETCH_SIZE}
}

main  # Ensure the whole file is downloaded before executing
