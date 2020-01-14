#!/bin/bash
#export MAIL_SERVER_URL=`route -n|grep "UG"|grep -v "UGH"|cut -f 10 -d " "`
#export MAIL_SERVER_PORT=1025
#dotnet prime.dll

echo "Running database migrations..."
dotnet ef database update

echo "Running .NET..."
dotnet prime.dll

function waitForIt() {
until [[ "$response" -eq "$2" ]]
do
    echo "Waiting for the host ..." ;
    sleep 1 ;
    response=`curl -s -o /dev/null -w "%{http_code}" $1`
done
echo "$1 responded $2"
}

waitForIt localhost:5000/api/enrollees 401
waitForIt localhost:5000/api/lookups 401

echo -e "\nThe system is up."
tail -f /dev/null
