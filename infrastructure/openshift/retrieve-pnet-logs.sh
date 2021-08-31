#!/usr/bin/env bash

# TODO: Exit immediately if a command exits with a non-zero status.
# set -e

main() {

  echo -e "-------- STARTING CRON --------\n"

#  LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -W ${PGPASSWORD} -c 'select max(ptl."TransactionId") from "PharmanetTransactionLog" ptl')
  LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -t -c 'select count(*) from "HealthAuthorityLookup" h')
  echo -e ${LAST_TX_ID}

  # ls -l /etc/ssl/certs

  sha256sum /opt/certs/prime-odr-api-cert.crt
  sha256sum /opt/certs/prime-odr-api-cert.key

  echo ${PRIME_ODR_API_ENCODED_CREDENTIALS}

  curl -v --cert /opt/certs/prime-odr-api-cert.crt:${PRIME_ODR_API_SSL_CERT_PASSWORD} --key /opt/certs/prime-odr-api-cert.key --header "Authorization: Basic ${PRIME_ODR_API_ENCODED_CREDENTIALS}" -X GET https://t1primedatasvc.maximusbc.ca/odr/prime/pnetdata/transactionLog

  python3 --version
}

main  # Ensure the whole file is downloaded before executing
