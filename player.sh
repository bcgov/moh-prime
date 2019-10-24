#!/bin/bash
export licensePlate='dqszvc'
export yamlLocation='openshift/compositions'
export gitUrl='https://github.com/bcgov/moh-prime.git'
export branchName=$(echo "$BRANCH_NAME" | tr '[:upper:]' '[:lower:]') 

$OVERRIDES

function determineMode() {
    buildPresent=`oc get $2/$1-$branchName --ignore-not-found=true`
    if [ -z ${buildPresent} ];
    then MODE="apply"
    else MODE="create"
    fi;
}

# Scrubs all PR assets from the environment
function ocCleanPR(){
    artifactQueue=`oc get all -n $licensePlate-$1 | grep -i "-$BRANCH_NAME"  | column -t | awk '{print $1}' | sort`
    for i in ${artifactQueue};
    do
    oc delete -n dqszvc-dev $i
    done
}
# Build an deploy are very alike, require similar logic for config injestion.
# This takes in Git, Jenkins and system variables to the template that will be processed.
function ocApply() {
    echo "ocApply..."
    echo "$licensePlate-$3"
    if [ "$1" == "build" ];
    then 
    configType="bc"
    elif [ "$1" == "deploy" ];
    then 
    configType="dc"
    fi
    if [ "${branchName}" == "develop" ] || [ "${branchName}" == "master" ];
    then 
    SUFFIX=""
    CHANGE_BRANCH="$BRANCH_NAME"
    else 
    SUFFIX="-${branchName}";
    fi
    oc process -f openshift/$2.$configType.yaml \
    -p NAME="$2" \
    -p VERSION="$BUILD_NUMBER" \
    -p SUFFIX="$SUFFIX" \
    -p SOURCE_CONTEXT_DIR="prime-$2" \
    -p SOURCE_REPOSITORY_URL="$gitUrl" \
    -p SOURCE_REPOSITORY_REF="$CHANGE_BRANCH"  \
    -p OC_NAMESPACE="$licensePlate" \
    -p OC_APP="$3" | oc apply -f - --namespace="$licensePlate-$3" 
    if [[ "$1" == "build" &&  "$2" != "postgresql" ]];
    then
    echo "Building..."
    oc start-build $2$SUFFIX -n $licensePlate-$3 --wait --follow
    else
    echo "Deployment should be automatic..."
    fi
}
#
case "$1" in
    ocApply)
        ocApply $2 $3 $4
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
    echo "Usage: $0 {ocApply|sonar|zap|promote} [build|depoly] <app> <dev|test|prod>"
esac