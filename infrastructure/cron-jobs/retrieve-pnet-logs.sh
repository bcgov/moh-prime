#!/usr/bin/env bash


function get_now_timestamp() {
  # Return formatted timestamp (UTC time)
  local  __resultvar=$1
  local  now_ts="$(date +'%T')" 
  eval $__resultvar="${now_ts}"
}


function exec_sql_no_resultset() {
  # $1 parameter - SQL to execute 
  # No result set will be returned 
  
  # Quote parameter to handle spaces in SQL string 
  psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -c "$1"
}


function drop_db_indices() {
  # Drop most indices on PharmanetTransactionLog table for better performance
  # during importing of data 

  echo -e "-------- Dropping indices --------"
  exec_sql_no_resultset 'DROP INDEX public."IX_PharmanetTransactionLog_PharmacyId";'
  exec_sql_no_resultset 'DROP INDEX public."IX_PharmanetTransactionLog_TxDateTime";'
  exec_sql_no_resultset 'DROP INDEX public."IX_PharmanetTransactionLog_UserId";'
  echo
}


function get_last_tx_id() {
  # See https://www.linuxjournal.com/content/return-values-bash-functions
  local  __resultvar=$1
  local  tx_id=''

  # Set to enter loop
  local db_status=-1
  while [ $db_status -ne 0 ]
  do
    echo -e "-------- get_last_tx_id calling psql --------"
    tx_id=$(psql -h ${PGHOST} -d ${PGDATABASE} -U ${PGUSER} -t -c 'select max(ptl."TransactionId") from "PharmanetTransactionLog" ptl')   
    db_status=$?
    echo -e "Last psql status:  _${db_status}_"
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


function create_db_indices() {
  # Re-create indices on PharmanetTransactionLog table for better performance
  # for queries/reports

  get_now_timestamp NOW_TIMESTAMP; echo -e "-------- Creating indices at ${NOW_TIMESTAMP} --------"
  exec_sql_no_resultset 'CREATE INDEX "IX_PharmanetTransactionLog_PharmacyId" ON public."PharmanetTransactionLog" USING btree ("PharmacyId");'
  get_now_timestamp NOW_TIMESTAMP; echo -e "Created IX_PharmanetTransactionLog_PharmacyId at ${NOW_TIMESTAMP}"
  exec_sql_no_resultset 'CREATE INDEX "IX_PharmanetTransactionLog_TxDateTime" ON public."PharmanetTransactionLog" USING btree ("TxDateTime");'
  get_now_timestamp NOW_TIMESTAMP; echo -e "Created IX_PharmanetTransactionLog_TxDateTime at ${NOW_TIMESTAMP}"
  exec_sql_no_resultset 'CREATE INDEX "IX_PharmanetTransactionLog_UserId" ON public."PharmanetTransactionLog" USING btree ("UserId");'
  get_now_timestamp NOW_TIMESTAMP; echo -e "Created IX_PharmanetTransactionLog_UserId at ${NOW_TIMESTAMP}"
}


function main() {
  echo -e "-------- STARTING CRON at $(date +"%B %d, %Y %T") UTC --------\n"

  echo -e "Connecting to database host:  _${PGHOST}_"
  echo -e "API client name:  _${PRIME_ODR_API_CLIENT_NAME}_"
  echo -e "Fetch size:  _${PRIME_ODR_API_FETCH_SIZE}_"
  echo

  drop_db_indices
  HAS_MORE='Y'
  while [ "${HAS_MORE}" = 'Y' ]
  do
    get_last_tx_id LAST_TX_ID
    echo -e "Last transaction id:  _${LAST_TX_ID}_\n"

    UUID=$(cat /proc/sys/kernel/random/uuid)
    echo -e "Generated request id:  _${UUID}_"

    echo -e "-------- Calling PRIME-ODR API then Postgres COPY --------\n"
    # CA certs need to be in place:  https://stackoverflow.com/questions/3160909/how-do-i-deal-with-certificates-using-curl-while-trying-to-access-an-https-url
    # ls -l /etc/ssl/certs
    curl -sS --cert /opt/certs/prime-odr-api-cert.crt:${PRIME_ODR_API_SSL_CERT_PASSWORD} --key /opt/certs/prime-odr-api-cert.key --header "Authorization: Basic ${PRIME_ODR_API_ENCODED_CREDENTIALS}" \
      "${PRIME_ODR_API_URL}?requestUUID=${UUID}&clientName=${PRIME_ODR_API_CLIENT_NAME}&lastTxnId=${LAST_TX_ID}&fetchSize=${PRIME_ODR_API_FETCH_SIZE}" | \
      python3 /opt/scripts/parse_api_response.py | \
      exec_sql_no_resultset "\copy \"PharmanetTransactionLog\"(\"TransactionId\", \"TxDateTime\", \"UserId\", \"SourceIpAddress\", \"LocationIpAddress\", \"PharmacyId\", \"ProviderSoftwareId\", \"ProviderSoftwareVersion\", \"PractitionerId\", \"CollegePrefix\", \"TransactionType\", \"TransactionSubType\", \"TransactionOutcome\") FROM STDIN (FORMAT CSV)"

    HAS_MORE=$(cat /tmp/isThereMoreData.txt)
    echo
  done
  create_db_indices
}

main  # Ensure the whole file is downloaded before executing
