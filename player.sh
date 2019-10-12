#!/bin/bash

licensePlate='dqszvc'
yamlLocation='openshift/compositions'
gitUrl='https://github.com/bcgov/moh-prime.git'
app=''

function build(){
    oc process -f openshift/$app.bc.json \
    -p NAME="$app" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="-$BRANCH_NAME" \
    -p SOURCE_CONTEXT_DIR="prime-$app" \
    -p SOURCE_REPOSITORY_URL="$gitUrl" \
    -p SOURCE_REPOSITORY_REF="develop" | oc apply -f - --namespace=$licensePlate-dev
}

case "$1" in
    build)
        build
        ;;
    deploy)
        deploy
        ;;
    sonar)
        sonar
        ;;
    zap)
        zap
        ;;
    *)
    echo "Usage: $0 {build|deploy|sonar|zap|promote} <app> "


esac
