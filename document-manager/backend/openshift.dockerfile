FROM registry.redhat.io/rhel8/python-36
ARG SVC_NAME
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
    dnf install -y https://download.postgresql.org/pub/repos/yum/reporpms/EL-8-x86_64/pgdg-redhat-repo-latest.noarch.rpm && \
    dnf install -y postgresql10 && \
    cd /opt/app-root/src && \ 
    pip3 install -r requirements.txt

# Create working directory
WORKDIR ${APP_ROOT}/src
ENV FLASK_APP app.py
ENV SVC_NAME ${SVC_NAME}}
ENV DB_HOST prime-postgresql-db-${SVC_NAME}

# Run the server
EXPOSE 5001 9191
ENTRYPOINT /opt/app-root/src/app.sh backend
