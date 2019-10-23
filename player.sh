#!/bin/bash
export licensePlate='dqszvc'
export yamlLocation='openshift/compositions'
export gitUrl='https://github.com/bcgov/moh-prime.git'
#export branchName="$BRANCH_NAME"
export branchName=$(echo "$BRANCH_NAME" | tr '[:upper:]' '[:lower:]') 

$OVERRIDES

function determineMode() {
    `oc get $2/$1-$branchName --ignore-not-found=true`
    if [ -z ${buildPresent} ];
    then MODE="apply"
    else MODE="create"
    fi;
}

# Scrubs all PR assets from the environment
function cleanPR(){
    artifactQueue=`oc get all -n $licensePlate-$1 | grep -i "-$BRANCH_NAME"  | column -t | awk '{print $1}' | sort`
    for i in ${artifactQueue};
    do
    oc delete -n dqszvc-dev $i
    done
}

# Build an deploy are very alike, require similar logic for config injestion.
function ocApply() {
    echo "ocApply..."
    if [ "${process}" == "build" ];
    then configType="bc"
    elif [ "${process}" == "deploy" ];
    then configType="dc"
    fi
    if [ "${branchName}" == "develop" ] || [ "${branchName}" == "master" ];
    then SUFFIX=""
    else SUFFIX="-${branchName}";
    fi
    oc process -f openshift/$1.$configType.json \
    -p NAME="$1" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="$SUFFIX" \
    -p SOURCE_CONTEXT_DIR="prime-$1" \
    -p SOURCE_REPOSITORY_URL="$gitUrl" \
    -p SOURCE_REPOSITORY_REF="$BRANCH_NAME"  \
    -p OC_NAMESPACE="$licensePlate" \
    -p OC_APP="$OC_APP" | oc apply -f - --namespace=$licensePlate-$OC_APP
}

function build(){
    OC_APP=$2
    buildPresent=`oc get bc/$1-$branchName --ignore-not-found=true`
    if [ -z ${buildPresent} ];
    then MODE="apply"
    else MODE="create"
    fi;
    oc process -f openshift/$1.bc.json \
    -p NAME="$1" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="-$branchName" \
    -p SOURCE_CONTEXT_DIR="prime-$1" \
    -p SOURCE_REPOSITORY_URL="$gitUrl" \
    -p SOURCE_REPOSITORY_REF="$BRANCH_NAME" \
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
    OC_APP=$2
    deployPresent=`oc get bc/$1-$branchName --ignore-not-found=true`
    if [ -z ${deployPresent} ];
    then MODE="apply"
    else MODE="create"
    fi;
    oc process -f openshift/$1.dc.json \
    -p NAME="$1" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="-$branchName" \
    -p SOURCE_CONTEXT_DIR="prime-$1" \
    -p SOURCE_REPOSITORY_URL="$gitUrl" \
    -p SOURCE_REPOSITORY_REF="$BRANCH_NAME" \
    -p OC_NAMESPACE="$licensePlate" \
    -p OC_APP="$OC_APP" | oc $MODE -f - --namespace=$licensePlate-$OC_APP
    echo "Building..."
}



#
case "$1" in
    ocApply)
        ocApply $2 $3
        ;;
    build)
        build $2 $3
        ;;
    deploy)
        deploy $2 $3
        ;;
    sonar)
        sonar $2 $3
        ;;
    zap)
        zap $2 $3
        ;;
    cleanup)
        cleanup
        ;;
    *)
    echo "Usage: $0 {build|deploy|sonar|zap|promote} <app> <dev|test|prod>"
esac