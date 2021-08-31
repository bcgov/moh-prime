#!/usr/bin/env bash

# Exit immediately if a command exits with a non-zero status.
set -e

main() {

  echo -e "-------- STARTING CRON --------\n"

#  LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -W ${PGPASSWORD} -c 'select max(ptl."TransactionId") from "PharmanetTransactionLog" ptl')
  LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -t -c 'select count(*) from "HealthAuthorityLookup" h')
  echo -e ${LAST_TX_ID}

  ls -l /opt/certs

  sha256sum /opt/certs/prime-odr-api-cert.crt
  sha256sum /opt/certs/prime-odr-api-cert.key

  python --version

  curl -V

  # curl --cert /opt/certs/prime-odr-api-cert.crt:${PRIME_ODR_API_ENCODED_CREDENTIALS} --key /opt/certs/prime-odr-api-cert.key --header 'Authorization: Basic ${PRIME_ODR_API_ENCODED_CREDENTIALS}' -X GET https://t1primedatasvc.maximusbc.ca/odr/prime/pnetdata/transactionLog
  curl --header 'Authorization: Basic ${PRIME_ODR_API_ENCODED_CREDENTIALS}' -X GET https://t1primedatasvc.maximusbc.ca/odr/prime/pnetdata/transactionLog
}

main  # Ensure the whole file is downloaded before executing
