#!/bin/bash
source project.bash

$OVERRIDES

function determineMode() {
    buildPresent=`oc get $2/$1-$BRANCH_LOWER --ignore-not-found=true`
    if [ -z ${buildPresent} ];
    then MODE="apply"
    else MODE="create"
    fi;
}

# Scrubs all PR assets from the environment
function ocCleanPR(){
    artifactQueue=`oc get all -n $PROJECT_PREFIX-$1 | grep -i "-$BRANCH_NAME"  | column -t | awk '{print $1}' | sort`
    for i in ${artifactQueue};
    do
    oc delete -n dqszvc-dev $i
    done
}
# Build an deploy are very alike, require similar logic for config injestion.
# This takes in Git, Jenkins and system variables to the template that will be processed.
function build()
    source $1.bash
    echo "Building $1 to $2 ..."
    echo "$PROJECT_PREFIX-$2"
    oc process -f $TEMPLATE_DIRECTORY/$BUILD_CONFIG_TEMPLATE \
    -p NAME="$2" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="$SUFFIX" \
    -p SOURCE_CONTEXT_DIR="$SOURCE_CONTEXT_DIR" \
    -p SOURCE_REPOSITORY_URL="$GIT_URL" \
    -p SOURCE_REPOSITORY_REF="$CHANGE_BRANCH"  \
    -p OC_NAMESPACE="$PROJECT_PREFIX" \
    -p OC_APP="$2" | oc apply -f - --namespace="$PROJECT_PREFIX-$2" 
    echo "Building..."
    oc start-build $2$SUFFIX -n $PROJECT_PREFIX-$2 --wait --follow
}

function deploy()
    source $1.bash
    echo "Deploying $1 to $2 ..."
    echo "$PROJECT_PREFIX-$2"
    oc process -f $TEMPLATE_DIRECTORY/$DEPLOY_CONFIG_TEMPLATE \
    -p NAME="$2" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="$SUFFIX" \
    -p SOURCE_CONTEXT_DIR="$SOURCE_CONTEXT_DIR" \
    -p SOURCE_REPOSITORY_URL="$GIT_URL" \
    -p SOURCE_REPOSITORY_REF="$CHANGE_BRANCH"  \
    -p OC_NAMESPACE="$PROJECT_PREFIX" \
    -p OC_APP="$2" | oc apply -f - --namespace="$PROJECT_PREFIX-$2" 
}

function ocApply() {
    source $1.bash
    echo "ocApply..."
    echo "$PROJECT_PREFIX-$2"
    if [ "$1" == "build" ];
    then 
    configType="bc"
    elif [ "$1" == "deploy" ];
    then 
    configType="dc"
    fi
    if [ "${BRANCH_LOWER}" == "develop" ] || [ "${BRANCH_LOWER}" == "master" ];
    then 
    SUFFIX=""
    CHANGE_BRANCH="$BRANCH_NAME"
    else 
    SUFFIX="-${BRANCH_LOWER}";
    fi
    oc process -f openshift/$2.$configType.yaml \
    -p NAME="$2" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="$SUFFIX" \
    -p SOURCE_CONTEXT_DIR="prime-$2" \
    -p SOURCE_REPOSITORY_URL="$GIT_URL" \
    -p SOURCE_REPOSITORY_REF="$CHANGE_BRANCH"  \
    -p OC_NAMESPACE="$PROJECT_PREFIX" \
    -p OC_APP="$3" | oc apply -f - --namespace="$PROJECT_PREFIX-$3" 
    if [[ "$1" == "build" &&  "$2" != "postgresql" ]];
    then
    echo "Building..."
    oc start-build $2$SUFFIX -n $PROJECT_PREFIX-$3 --wait --follow
    else
    echo "Deployment should be automatic..."
    fi
}

function sonar(){
    OC_APP=$2
    deployPresent=`oc get bc/$1-$BRANCH_LOWER --ignore-not-found=true`
    if [ -z ${deployPresent} ];
    then MODE="apply"
    else MODE="create"
    fi;
    oc process -f openshift/sonar.pod.yaml \
    -p NAME="sonar-runner" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="$BRANCH_LOWER" \
    -p SOURCE_CONTEXT_DIR="." \
    -p SOURCE_REPOSITORY_URL="$GIT_URL" \
    -p SOURCE_REPOSITORY_REF="$BRANCH_NAME" \
    -p OC_NAMESPACE="$PROJECT_PREFIX" \
    -p OC_APP="$OC_APP" | oc $MODE -f - --namespace=$PROJECT_PREFIX-$OC_APP
    echo "Scanning..."
    sonar-scanner -X
}
#
case "$1" in
    build)
        build $2 $3
        ;;
    deploy)
        deploy $2 $3
        ;;
    ocApply)
        ocApply $2 $3 $4
        ;;
    sonar)
        sonar
        ;;
    zap)
        zap $2 $3
        ;;
    cleanup)
        cleanup
        ;;
    *)
    echo "Usage: $0 {ocApply|sonar|zap|promote} [build|depoly] <app> <dev|test|prod>"
esac