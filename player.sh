#!/bin/bash
licensePlate='dqszvc'
yamlLocation='openshift/compositions'
gitUrl='https://github.com/bcgov/moh-prime.git'
gitBranch="$CHANGE_BRANCH"
branchName=`echo "$BRANCH_NAME" | awk '{print tolower($0)}'`


function build(){
    oc process -f openshift/$1.bc.json \
    -p NAME="$1" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="$branchName" \
    -p SOURCE_CONTEXT_DIR="prime-$1" \
    -p SOURCE_REPOSITORY_URL="$gitUrl" \
    -p SOURCE_REPOSITORY_REF="$CHANGE_BRANCH" | oc apply -f - --namespace=$licensePlate-dev
    echo "Building..."
    echo "start-build $1-$branchName -n $licensePlate-dev --follow"
    oc start-build $1-$branchName -n $licensePlate-dev --follow
    sleep 2
}

function deploy(){
    oc process -f openshift/$1.dc.json \
    -p NAME="$1" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="$branchName" \
    -p SOURCE_CONTEXT_DIR="prime-$1" \
    -p SOURCE_REPOSITORY_URL="$gitUrl" \
    -p SOURCE_REPOSITORY_REF="$CHANGE_BRANCH" | oc apply -f - --namespace=$licensePlate-dev
    echo "Building..."
    echo "oc rollout latest dc/$1-$branchName -n $licensePlate-dev"
    oc rollout latest dc/$1-$branchName -n $licensePlate-dev
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