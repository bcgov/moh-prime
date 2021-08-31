#!/usr/bin/env bash

# Exit immediately if a command exits with a non-zero status.
set -e

main() {

  echo -e "-------- STARTING CRON --------\n"

#  LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -W ${PGPASSWORD} -c 'select max(ptl."TransactionId") from "PharmanetTransactionLog" ptl')
  LAST_TX_ID=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -W ${PGPASSWORD} -c 'select count(*) from "Enrollee" e')
  echo -e ${LAST_TX_ID}

  ls -l /opt/certs

  curl --cert /opt/certs/prime-odr-api-cert.crt:pwd4primetst --key /opt/certs/prime-odr-api-cert.key --header 'Authorization: Basic cHJpbWVkYXRhdXNlcjpWaWM5I1RzdDE=' -X GET https://t1primedatasvc.maximusbc.ca/odr/prime/pnetdata/transactionLog

}

main  # Ensure the whole file is downloaded before executing
