#!/bin/bash

function backend()
{
    cd /opt/app-root/src
    flask run ${FLASK_RUN_PARAMS} &disown 
    uwsgi uwsgi.ini
}

function migrate()
{
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
