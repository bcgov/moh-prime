#!/bin/bash
cd /opt/app-root/src/app
flask run &disown 
uwsgi uwsgi.ini