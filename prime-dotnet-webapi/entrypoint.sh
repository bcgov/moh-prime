#!/bin/bash
#export MAIL_SERVER_URL=`route -n|grep "UG"|grep -v "UGH"|cut -f 10 -d " "`
#export MAIL_SERVER_PORT=1025
#dotnet prime.dll
echo "Running .NET..."
export DB_CONNECTION_STRING="host=${DB_HOST};port=5432;database=${POSTGRESQL_DATABASE};username=${POSTGRESQL_USER};password=${POSTGRESQL_ADMIN_PASSWORD}"
/opt/rh/rh-dotnet22/root/usr/lib64/dotnet/dotnet prime.dll
until [ "$response" -eq "401" ]
    echo "Waiting for the host ..."
    sleep 1
    response=`curl -s -o /dev/null -w "%{http_code}" localhost:8080/api/enrollees`
done
echo -e "\nThe system is up."
tail -f /dev/null