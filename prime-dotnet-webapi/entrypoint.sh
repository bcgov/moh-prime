#!/bin/bash
echo "Running the migrations..."
#psql -d postgres -f databaseMigration.sql
if [ -z "${DB_CONNECTION_STRING}" ]
then
export MONGO_CONNECTION_STRING="mongodb://$MONGO_HOST;port=27017;database=${MONGODB_DATABASE};username=${MONGODB_USER};password=${MONGODB_PASSWORD}/?authSource="${MONGODB_USER}";
export DB_CONNECTION_STRING="host=$DB_HOST;port=5432;database=${POSTGRESQL_DATABASE};username=${POSTGRESQL_USER};password=${POSTGRESQL_ADMIN_PASSWORD}";
fi
export AUTH=$(printf $PHARMANET_API_USERNAME:$PHARMANET_API_PASSWORD|base64)
export logfile=prime.logfile.out
# Wait for database connection
PG_IS_READY=$(pg_isready -h $DB_HOST -U ${POSTGRESQL_USER} -d ${POSTGRESQL_DATABASE})
n=0
until [[ $n -ge 5 || ($PG_IS_READY == *"$DB_HOST"* && $PG_IS_READY == *"accepting connections"*) ]]
do
    echo "Waiting for the database ..." ;
    sleep 3 ;
    PG_IS_READY=$(pg_isready -h $DB_HOST -U ${POSTGRESQL_USER} -d ${POSTGRESQL_DATABASE})
    n=$[$n+1]
done
if [[ $n -ge 5 ]]
then
    echo "Failed to connect to database."
else
    echo "Connected to database."
fi


psql -h $DB_HOST -U ${POSTGRESQL_USER} -d ${POSTGRESQL_DATABASE} -a -f databaseMigrations.sql

echo "Resting 5 seconds to let things settle down..."
echo "Running .NET..."
dotnet prime.dll -v 2>&1 | ts > $logfile &
echo "Launched, waiting for connection to API internally..."

function waitForIt() {
until [[ "$response" -eq "$2" ]]
do
    echo "Waiting for the host ..." ;
    sleep 1 ;
    response=`curl -s -o /dev/null -w "%{http_code}" $1`
done
echo "$1 responded $2"
}

function pharmanetCheck() {
    UUID=$(cat /proc/sys/kernel/random/uuid)
    AUTH=$(printf $PHARMANET_API_USERNAME:$PHARMANET_API_PASSWORD|base64)
    printf {\"applicationUUID\":\"${UUID}\"\,\"programArea\":\"PRIME\"\,\"licenceNumber\":\"20361\"\,\"collegeReferenceId\":\"P1\"} > /tmp/data.out
    openssl pkcs12 -in $PHARMANET_SSL_CERT_FILENAME -out /tmp/pharmanet-api-cert.pem -nodes -passin pass:$PHARMANET_SSL_CERT_PASSWORD
    curl -s -o /dev/null -w "%{http_code}" --cert /tmp/pharmanet-api-cert.pem \
    -H "Authorization: Basic $AUTH" \
    -H "Content-Type: application/json" $PHARMANET_API_URL \
    -H "Accept: application/json" \
    --data "@/tmp/data.out"
}

function pharmanetVerboseCheck() {
    UUID=$(cat /proc/sys/kernel/random/uuid)
    AUTH=$(printf $PHARMANET_API_USERNAME:$PHARMANET_API_PASSWORD|base64)
    printf {\"applicationUUID\":\"${UUID}\"\,\"programArea\":\"PRIME\"\,\"licenceNumber\":\"20361\"\,\"collegeReferenceId\":\"P1\"} > /tmp/data.out
    openssl pkcs12 -in $PHARMANET_SSL_CERT_FILENAME -out /tmp/pharmanet-api-cert.pem -nodes -passin pass:$PHARMANET_SSL_CERT_PASSWORD
    curl -v -s -o /dev/null --cert /tmp/pharmanet-api-cert.pem \
    -H "Authorization: Basic $AUTH" \
    -H "Content-Type: application/json" $PHARMANET_API_URL \
    -H "Accept: application/json" \
    --data "@/tmp/data.out"
}

waitForIt localhost:${API_PORT}/api/enrollees 401 2>&1 | logger &
waitForIt localhost:${API_PORT}/api/lookups 401 2>&1 | logger

echo -e "\nThe system is up."

tail -f $logfile
