#!/usr/bin/env bash

# Exit immediately if a command exits with a non-zero status.
set -e

main() {

  echo -e "-------- STARTING CRON --------\n"

  LAST_TX_ID=psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -W ${PGPASSWORD} -c 'select max(ptl."TransactionId") from "PharmanetTransactionLog" ptl'
  echo -e ${LAST_TX_ID}

  ls -l /opt/certs
}

main  # Ensure the whole file is downloaded before executing
