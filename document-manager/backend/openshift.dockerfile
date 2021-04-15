FROM registry.redhat.io/rhel8/python-36
USER 0
ENV APP_ROOT /opt/app-root
SHELL ["/bin/bash","-c"]

# Install project dependencies
COPY . /opt/app-root/src

# Install the requirements
COPY . .

RUN set -x && \
    pip3 install --upgrade -U pip setuptools wheel && \
    pip3 install psycopg2 && \
    yum install -y postgresql10 && \
    cd /opt/app-root/src && \ 
    pip3 install -r requirements.txt

# Create working directory
WORKDIR ${APP_ROOT}/src
ENV FLASK_APP app.py
ENV DB_HOST prime-postgresql-db

# Run the server
EXPOSE 5001 9191
ENTRYPOINT /opt/app-root/src/app.sh backend
