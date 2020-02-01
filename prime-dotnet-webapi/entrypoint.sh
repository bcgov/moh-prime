#!/bin/bash
#dotnet prime.dll

echo "Resting 5 seconds to let things settle down..."
sleep 5

#echo "Running database update..."
#dotnet ef database update -v

#echo "Generating upgrade scripts..."
#dotnet ef migrations script --idempotent --output "${WORKDIR}/databaseMigrations.sql"

echo "Resting 5 seconds to let things settle down..."
sleep 5

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
