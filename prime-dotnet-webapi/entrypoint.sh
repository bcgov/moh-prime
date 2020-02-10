#!/bin/bash
echo "Running the migrations..."
#psql -d postgres -f databaseMigration.sql

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
dotnet prime.dll -v &disown

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

waitForIt localhost:${API_PORT}/api/enrollees 401
waitForIt localhost:${API_PORT}/api/lookups 401

echo -e "\nThe system is up."
tail -f /dev/null
