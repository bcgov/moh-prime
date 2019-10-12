#!/bin/bash
licensePlate='dqszvc'
yamlLocation='openshift/compositions'
gitUrl='https://github.com/bcgov/moh-prime.git'
gitBranch=`git rev-parse --abbrev-ref HEAD`
branchName=`echo "$BRANCH_NAME" | awk '{print tolower($0)}'`

function build(){
    echo "Processing..."
    echo='oc process -f openshift/$1.bc.json -p NAME="$1" -p VERSION="$BUILD_NUMBER" -p SUFFIX="-$BRANCH_NAME" -p SOURCE_CONTEXT_DIR="prime-$1" -p SOURCE_REPOSITORY_URL="$gitUrl" -p SOURCE_REPOSITORY_REF="$gitBranch" | oc replace -f - --namespace=$licensePlate-dev'
    oc process -f openshift/$1.bc.json \
    -p NAME="$1" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="$branchName" \
    -p SOURCE_CONTEXT_DIR="prime-$1" \
    -p SOURCE_REPOSITORY_URL="$gitUrl" \
    -p SOURCE_REPOSITORY_REF="$gitBranch" | oc apply -f - --namespace=$licensePlate-dev
    echo "Building..."
    echo "oc start-build $1-$branchName -n $licensePlate-dev"
    oc start-build $1-$branchName -n $licensePlate-dev
}
function deploy(){
    echo "Processing..."
    echo='oc process -f openshift/$1.dc.json -p NAME="$1" -p VERSION="$BUILD_NUMBER" -p SUFFIX="$BRANCH_NAME" -p SOURCE_CONTEXT_DIR="prime-$1" -p SOURCE_REPOSITORY_URL="$gitUrl" -p SOURCE_REPOSITORY_REF="$gitBranch" | oc replace -f - --namespace=$licensePlate-dev'
    oc process -f openshift/$1.dc.json \
    -p NAME="$1" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="$branchName" \
    -p SOURCE_CONTEXT_DIR="prime-$1" \
    -p SOURCE_REPOSITORY_URL="$gitUrl" \
    -p SOURCE_REPOSITORY_REF="$gitBranch" | oc apply -f - --namespace=$licensePlate-dev
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