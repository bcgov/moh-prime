# 1. `docker build . -t python-curl-psql`
# 2. e.g. `docker tag python-curl-psql image-registry.apps.silver.devops.gov.bc.ca/9c33a9-prod/python-curl-psql:latest`
# 3. If necessary, in Bash shell:  `docker login -u `oc whoami` -p `oc whoami --show-token` image-registry.apps.silver.devops.gov.bc.ca`
# 4. e.g. `docker push image-registry.apps.silver.devops.gov.bc.ca/9c33a9-prod/python-curl-psql:latest`
#
FROM python:3-slim

RUN apt-get -y update
RUN apt-get -y install curl
RUN apt-get -y install postgresql-client

