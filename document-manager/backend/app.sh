#!/bin/bash
cd /opt/app-root/src
flask run &disown 
uwsgi uwsgi.ini