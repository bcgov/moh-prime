#!/bin/bash
cd ${WORKDIR}/src/app
flask run &disown 
uwsgi uwsgi.ini