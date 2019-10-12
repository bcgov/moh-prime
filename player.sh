#!/bin/bash
# Because I don't know Groovy well enough.
###
export licensePlate="dqszvc"
export yamlLocation="openshift/compositions"
export gitUrl="https://github.com/bcgov/moh-prime.git"

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
    oc process -f openshift/$1.bc.json \
    -p NAME="dotnet-webapi" \
    -p VERSION={$BUILD_NUMBER} \
    -p SUFFIX="-PR-$pr" \
	-p SOURCE_CONTEXT_DIR="prime-dotnet-webapi" \
    -p SOURCE_REPOSITORY_URL="${gitUrl}" \
    -p SOURCE_REPOSITORY_REF="${BRANCH_NAME}" | oc apply -f - --namespace=$licensePlate-dev
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
