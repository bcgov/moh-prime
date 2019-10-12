#!/bin/bash
# Because I don't know Groovy well enough.
###
export licensePlate="dqszvc"
export yamlLocation="openshift/compositions"
export gitUrl="https://github.com/bcgov/moh-prime.git"
app=""

function ocPush(){
    for file in `find $yamlLocation -type f -name *.yaml`
    do oc apply --namespace="$licensePlate-$1" -f $file
    done
}
function dryRun(){
    for file in `find $yamlLocation -type f -name *.yaml`
    do oc create -f $file --dry-run -o json
    done;
}

function build(){
    oc process -f openshift/dotnet-webapi-bc.json \
    -p NAME="dotnet-webapi" \
    -p VERSION=v1 \
    -p SUFFIX="-PR-$pr" \
	-p SOURCE_CONTEXT_DIR="prime-dotnet-webapi" \
    -p SOURCE_REPOSITORY_URL="${gitUrl}" \
    -p SOURCE_REPOSITORY_REF="${BRANCH_NAME}" | oc apply -f - --namespace=dqszvc-dev


oc process -f openshift/sonar-postgresql.bc.yaml \
    -p NAME=sonarqube-postgresql \
    -p VERSION=v1 | oc create -f -
 
oc process -f openshift/sonar-postgresql.dc.yaml \
    -p NAME=sonarqube-postgresql \
    -p DATABASE_SERVICE_NAME=sonarqube-postgresql \
    -p IMAGE_STREAM_NAME=sonarqube-postgresql \
    -p IMAGE_STREAM_VERSION=v1 \
    -p POSTGRESQL_DATABASE=sonarqube \
    -p VOLUME_CAPACITY=5Gi | oc create -f -
 
oc process -f openshift/sonarqube.dc.yaml \
    -p NAME=sonarqube \
    -p DATABASE_SERVICE_NAME=sonarqube-postgresql \
    -p DATABASE_NAME=sonarqube \
    -p VERSION=v1 | oc create -f -