FROM docker-registry.default.svc:5000/dqszvc-tools/python:3.6
USER 0
SHELL ["/bin/bash","-c"]
# Update installation utility
#RUN apt-get update
# Install project dependencies
COPY . ${APP_ROOT}/src

# Install the requirements

COPY . .

RUN set -x && \
    pip3 install --upgrade -U pip setuptools wheel && \
    pip3 install psycopg2 && \
    apt-get update -yqq && \
    apt-get install -yqq postgresql-client && \
    source /opt/app-root/etc/scl_enable && \
    cd ${APP_ROOT}/src && \ 
    pip3 install -r requirements.txt

# Create working directory
WORKDIR ${APP_ROOT}/src
ENV FLASK_APP app.py
ENV DB_HOST postgresql${SUFFIX}
# Run the server
EXPOSE 5001 9191
ENTRYPOINT /opt/app-root/src/app.sh backend
