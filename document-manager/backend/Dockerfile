FROM python:3.6
ENV APP_ROOT /opt/app-root

# Update installation utility
USER 0
# Update installation utility
#RUN apt-get update
SHELL ["/bin/bash","-c"]
# Install project dependencies
COPY . ${APP_ROOT}/src

# Install the requirements

COPY . .

RUN set -x && \
    pip3 install --upgrade -U pip setuptools wheel && \
    pip3 install psycopg2 && \
    cd ${APP_ROOT}/src && \ 
    pip3 install -r requirements.txt
#    source /opt/app-root/etc/scl_enable && \

# Create working directory
WORKDIR ${APP_ROOT}/src
ENV FLASK_APP app.py
# Run the server
EXPOSE 5001 9191
ENTRYPOINT /opt/app-root/src/app.sh backend
#CMD ["flask","run"]
