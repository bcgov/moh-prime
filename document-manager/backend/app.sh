#!/bin/bash
flask run &disown 
uwsgi uwsgi.ini