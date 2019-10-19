#!/bin/bash
export licensePlate='dqszvc'
export yamlLocation='openshift/compositions'
export gitUrl='https://github.com/bcgov/moh-prime.git'

function build(){
    buildPresent=`oc get bc/$1-$branchName --ignore-not-found=true`
    if [ -z ${buildPresent} ];
    then MODE="apply"
    else MODE="create"
    fi;
    oc process -f openshift/$1.bc.json \
    -p NAME="$1" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="$branchName" \
    -p SOURCE_CONTEXT_DIR="prime-$1" \
    -p SOURCE_REPOSITORY_URL="$gitUrl" \
    -p SOURCE_REPOSITORY_REF="$CHANGE_BRANCH" \
    -p OC_NAMESPACE="$licensePlate" \
    -p OC_APP="$OC_APP" | oc $MODE -f - --namespace=$licensePlate-$OC_APP
    if [ "$1" != "postgresql" ];
    then
    echo "Building..."
    oc start-build $1-$branchName -n $licensePlate-$OC_APP --wait --follow
    sleep 2
    else 
    echo "Component $1 does not need to be built, only deployed"
    fi
}

function deploy(){
    deployPresent=`oc get bc/$1-$branchName --ignore-not-found=true`
    if [ -z ${deployPresent} ];
    then MODE="apply"
    else MODE="create"
    fi;
    oc process -f openshift/$1.dc.json \
    -p NAME="$1" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="$branchName" \
    -p SOURCE_CONTEXT_DIR="prime-$1" \
    -p SOURCE_REPOSITORY_URL="$gitUrl" \
    -p SOURCE_REPOSITORY_REF="$CHANGE_BRANCH" \
    -p OC_NAMESPACE="$licensePlate" \
    -p OC_APP="$OC_APP" | oc $MODE -f - --namespace=$licensePlate-$OC_APP
    echo "Building..."
}

case "$1" in
    build)
        build $2
        ;;
    deploy)
        deploy $2
        ;;
    sonar)
        sonar $2
        ;;
    zap)
        zap $2
        ;;
    *)
    echo "Usage: $0 {build|deploy|sonar|zap|promote} <app> "
esac