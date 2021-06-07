#!/bin/bash
function waitForPsql() 
{
until  psql -h $DB_HOST -U ${DB_USER} -d ${DB_USER} -v QueryTimeout=1 -v ON_ERROR_STOP=1 -c "select version()" > /dev/null;
do
    echo "waiting for postgres container..."
    sleep 2
done
}

function backend()
{
    waitForPsql
    cd /opt/app-root/src
    flask run ${FLASK_RUN_PARAMS} &disown 
    uwsgi uwsgi.ini
}

function migrate()
{
    waitForPsql
    flask db upgrade ${FLASK_RUN_PARAMS}
}
case "$1" in
    backend)
        backend
        ;;
    migrate)
        migrate
        ;;
    *)
    echo "You\'re doing it wrong..."
esac
