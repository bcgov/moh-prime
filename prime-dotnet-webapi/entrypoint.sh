#!/bin/bash
echo "Running the migrations..."
#psql -d postgres -f databaseMigration.sql
psql -h $DB_HOST -U ${POSTGRESQL_USER} -d ${POSTGRESQL_DATABASE} -a -f databaseMigrations.sql
echo "Resting 5 seconds to let things settle down..."

echo "Running .NET..."
dotnet prime.dll -v #&disown

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
