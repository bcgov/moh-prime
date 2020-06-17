FROM docker-registry.default.svc:5000/dqszvc-tools/python-36-rhel7:1-36

# Update installation utility
#RUN apt-get update

RUN ls -alh 
# Install project dependencies
COPY requirements.txt ${APP_ROOT}/src

RUN source /opt/app-root/etc/scl_enable && \
    set -x && \
    pip install -U pip setuptools wheel && \
    cd ${APP_ROOT}/src && pip install -r requirements.txt

# Create working directory
RUN mkdir /app
WORKDIR /app

# Install the requirements
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

COPY . .

# Run the server
EXPOSE 5001
CMD ["flask","run"]
