#!/bin/bash
#export MAIL_SERVER_URL=`route -n|grep "UG"|grep -v "UGH"|cut -f 10 -d " "`
#export MAIL_SERVER_PORT=1025
#dotnet prime.dll
if [ -z "$DB_HOST" ]
then
    export DB_CONNECTION_STRING="${DB_CONNECTION_STRING}"
else
    host=postgresql;port=5432;database=postgres;username=postgres;password=0p1miz3Pr1m3
    export DB_CONNECTION_STRING="host=${DB_HOST};port=5432;database=${POSTGRESQL_DATABASE};username=${POSTGRESQL_USER};password=${POSTGRESQL_ADMIN_PASSWORD}"
fi

echo "Running database migrations..."
dotnet ef database update

echo "Running .NET..."
dotnet prime.dll &disown

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
