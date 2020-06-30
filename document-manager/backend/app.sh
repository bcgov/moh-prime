#!/bin/bash
cd /opt/app-root/src
flask run ${FLASK_RUN_PARAMS} &disown 
uwsgi uwsgi.ini