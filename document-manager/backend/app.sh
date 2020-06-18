#!/bin/bash
cd /opt/app-root/src/app
flask run &disown 
cd /opt/app-root/src
uwsgi uwsgi.ini