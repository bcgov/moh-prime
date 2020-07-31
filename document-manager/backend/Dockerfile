FROM python:3.6
USER 0
ENV APP_ROOT /opt/app-root
SHELL ["/bin/bash","-c"]
# Update installation utility
#RUN apt-get update
# Install project dependencies
COPY . /opt/app-root/src

# Install the requirements

COPY . .

RUN set -x && \
    pip3 install --upgrade -U pip setuptools wheel && \
    pip3 install psycopg2 && \
    apt-get update -yqq && \
    apt-get install -yqq postgresql-client && \
    cd /opt/app-root/src && \ 
    pip3 install -r requirements.txt

# Create working directory
WORKDIR ${APP_ROOT}/src
ENV FLASK_APP app.py
ENV DB_HOST postgresql${SUFFIX}
ENV PGPASSWORD postgres
# Run the server
EXPOSE 5001 9191
#ENTRYPOINT /opt/app-root/src/app.sh dev
# Entry remarked out, using command in compose instead