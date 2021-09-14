#!/usr/bin/env bash

function get_last_tx_id() {
  # See https://www.linuxjournal.com/content/return-values-bash-functions
  local  __resultvar=$1
  local  tx_id=''

  # Set to enter loop
  local db_status=-1
  while [ $db_status -ne 0 ]
  do
    echo -e "-------- get_last_tx_id calling psql --------\n"
    tx_id=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -t -c 'select max(ptl."TransactionId") from "PharmanetTransactionLog" ptl')   
    db_status=$?
    echo -e "Last psql status:  _${db_status}_\n"
    # If error encountered, wait, let database server recover before trying again 
    if [ $db_status -ne 0 ]; then sleep 10; fi
  done

  # Trim whitespace
  tx_id=`echo ${tx_id} | sed 's/^ *//g'`
  # Handle initial empty table condition
  if [ "${tx_id}" = '' ]; then tx_id='0'; fi
  # "Return" result to caller
  eval $__resultvar="${tx_id}"
}


function main() {
  echo -e "-------- STARTING CRON at $(date +"%B %d, %Y %T") UTC --------\n"

  echo -e "Connecting to database host:  _${PGHOST}_\n"
  echo -e "API client name:  _${PRIME_ODR_API_CLIENT_NAME}_\n"
  echo -e "Fetch size:  _${PRIME_ODR_API_FETCH_SIZE}_\n"

  HAS_MORE='Y'
  while [ "${HAS_MORE}" = 'Y' ]
  do
    get_last_tx_id LAST_TX_ID
    echo -e "Last transaction id:  _${LAST_TX_ID}_\n"

    UUID=$(cat /proc/sys/kernel/random/uuid)
    echo -e "Generated request id:  _${UUID}_\n"

    # CA certs need to be in place:  https://stackoverflow.com/questions/3160909/how-do-i-deal-with-certificates-using-curl-while-trying-to-access-an-https-url
    # ls -l /etc/ssl/certs
    curl --cert /opt/certs/prime-odr-api-cert.crt:${PRIME_ODR_API_SSL_CERT_PASSWORD} --key /opt/certs/prime-odr-api-cert.key --header "Authorization: Basic ${PRIME_ODR_API_ENCODED_CREDENTIALS}" \
      "${PRIME_ODR_API_URL}?requestUUID=${UUID}&clientName=${PRIME_ODR_API_CLIENT_NAME}&lastTxnId=${LAST_TX_ID}&fetchSize=${PRIME_ODR_API_FETCH_SIZE}" | \
      python3 /opt/scripts/parse_api_response.py | \
      psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -c "\copy \"PharmanetTransactionLog\"(\"TransactionId\", \"TxDateTime\", \"UserId\", \"SourceIpAddress\", \"LocationIpAddress\", \"PharmacyId\", \"ProviderSoftwareId\", \"ProviderSoftwareVersion\", \"PractitionerId\", \"CollegePrefix\", \"TransactionType\", \"TransactionSubType\", \"TransactionOutcome\") FROM STDIN (FORMAT CSV)"

    HAS_MORE=$(cat /tmp/isThereMoreData.txt)
  done
}

main  # Ensure the whole file is downloaded before executing
