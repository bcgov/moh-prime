FROM python:3.6

# Update installation utility
USER 0
# Update installation utility
#RUN apt-get update
SHELL ["/bin/bash","-c"]
RUN ls -alh 
# Install project dependencies
COPY . ${APP_ROOT}/src

# Install the requirements

COPY . .

RUN ls -alh && \
    source /opt/app-root/etc/scl_enable && \
    set -x && \
    pip3 install --upgrade -U pip setuptools wheel && \
    pip3 install psycopg2 && \
    cd ${APP_ROOT}/src && \ 
    pip3 install -r requirements.txt

# Create working directory
RUN mkdir -p /app
WORKDIR /app



# Run the server
EXPOSE 5001
CMD ["flask","run"]
