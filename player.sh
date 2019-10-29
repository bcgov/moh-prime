#!/bin/sh
export PROJECT_PREFIX="dqszvc"
export GIT_URL='https://github.com/bcgov/moh-prime.git'
export BRANCH_LOWER=`echo "${BRANCH_NAME}" | awk '{print tolower($0)}'`
export ACTION=$1
export COMPONENT=$2
export OC_APP=$3
# source ./project.sh
# source ./functions.sh
function variablePopulation() {
    if [ "${BRANCH_LOWER}" == "develop" ] || [ "${BRANCH_LOWER}" == "master" ];
    then 
        export SUFFIX=""
        export CHANGE_BRANCH="$BRANCH_NAME"
    else 
        export SUFFIX="-${BRANCH_LOWER}";
    fi
}

variablePopulation

function build() {
    source ./"$COMPONENT.sh"
    echo "Building $COMPONENT (${APP_NAME}) to $PROJECT_PREFIX-$OC_APP..."
    buildPresent=$(oc get bc/"$APP_NAME-$BRANCH_LOWER" --ignore-not-found=true)
    if [ -z "${buildPresent}" ];
    then 
        MODE="apply"
    else 
        MODE="create"
    fi;
    if [ "${BRANCH_LOWER}" == "develop" ] || [ "${BRANCH_LOWER}" == "master" ];
    then 
        echo "oc process -f ./${TEMPLATE_DIRECTORY}/${BUILD_CONFIG_TEMPLATE} -p NAME=${APP_NAME} -p VERSION=${BUILD_NUMBER} -p SOURCE_CONTEXT_DIR=${SOURCE_CONTEXT_DIR} -p SOURCE_REPOSITORY_URL=${GIT_URL} -p SOURCE_REPOSITORY_REF=${BRANCH_NAME} -p OC_NAMESPACE=${PROJECT_PREFIX} -p OC_APP=${OC_APP} | oc ${MODE} -f - --namespace=${PROJECT_PREFIX}-${OC_APP}"  
        oc process -f ./"${TEMPLATE_DIRECTORY}/${BUILD_CONFIG_TEMPLATE}" \
        -p NAME="${APP_NAME}" \
        -p VERSION="${BUILD_NUMBER}" \
        -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
        -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${BRANCH_NAME}" \
        -p OC_NAMESPACE="${PROJECT_PREFIX}" \
        -p OC_APP="${OC_APP}" | oc "${MODE}" -f - --namespace="${PROJECT_PREFIX}-${OC_APP}"  
    else 
        echo "oc process -f ./${TEMPLATE_DIRECTORY}/${BUILD_CONFIG_TEMPLATE} -p NAME=${APP_NAME} -p VERSION=${BUILD_NUMBER} -p SUFFIX=-${BRANCH_LOWER} -p SOURCE_CONTEXT_DIR=${SOURCE_CONTEXT_DIR} -p SOURCE_REPOSITORY_URL=${GIT_URL} -p SOURCE_REPOSITORY_REF=${BRANCH_NAME} -p OC_NAMESPACE=${PROJECT_PREFIX} -p OC_APP=${OC_APP} | oc ${MODE} -f - --namespace=${PROJECT_PREFIX}-${OC_APP}"  
        oc process -f ./"${TEMPLATE_DIRECTORY}/${BUILD_CONFIG_TEMPLATE}" \
        -p NAME="${APP_NAME}" \
        -p VERSION="${BUILD_NUMBER}" \
        -p SUFFIX="-${BRANCH_LOWER}" \
        -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
        -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
        -p OC_NAMESPACE="${PROJECT_PREFIX}" \
        -p OC_APP="${OC_APP}" | oc "${MODE}" -f - --namespace="${PROJECT_PREFIX}-${OC_APP}"
    fi;
    if [ "$BUILD_REQUIRED" == true ];
    then
        echo "Building oc start-build $APP_NAME$SUFFIX -n $PROJECT_PREFIX-$OC_APP --wait --follow ..."
        oc start-build "$APP_NAME$SUFFIX" -n "$PROJECT_PREFIX-$OC_APP" --wait --follow --pod-running-timeout=600
    else
        echo "Deployment should be automatic..."
    fi
}

function deploy() {
    source ./"${COMPONENT}.sh"
    echo "Deploying ${COMPONENT} (${APP_NAME}) to ${OC_APP} ..."
    deployPresent=$(oc get dc/"${APP_NAME}-${BRANCH_LOWER}" --ignore-not-found=true)
    if [ -z "${deployPresent}" ];
    then 
        MODE="apply"
    else 
        MODE="create"
    fi;
    if [ "${BRANCH_LOWER}" == "develop" ] || [ "${BRANCH_LOWER}" == "master" ];
    then 
        oc process -f ./"${TEMPLATE_DIRECTORY}/${DEPLOY_CONFIG_TEMPLATE}" \
        -p NAME="${APP_NAME}" \
        -p VERSION="${BUILD_NUMBER}" \
        -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
        -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${BRANCH_NAME}" \
        -p OC_NAMESPACE="${PROJECT_PREFIX}" \
        -p OC_APP="${OC_APP}" | oc "${MODE}" -f - --namespace="${PROJECT_PREFIX}-${OC_APP}"  
    else 
        oc process -f ./"${TEMPLATE_DIRECTORY}/${DEPLOY_CONFIG_TEMPLATE}" \
        -p NAME="${APP_NAME}" \
        -p VERSION="${BUILD_NUMBER}" \
        -p SUFFIX='-'"${BRANCH_LOWER}" \
        -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
        -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
        -p OC_NAMESPACE="${PROJECT_PREFIX}" \
        -p OC_APP="${OC_APP}" | oc "${MODE}" -f - --namespace="${PROJECT_PREFIX}-${OC_APP}"
    fi;
}

function deleteBc() {
    source ./"$COMPONENT.sh"
    deployPresent=$(oc get bc/"${APP_NAME}-${BRANCH_LOWER}" --ignore-not-found=true)
    if [ -z "${deployPresent}" ];
    then 
        MODE="apply"
    else 
        MODE="create"
    fi;
    echo "Deleting ${COMPONENT} from $OC_APP ..."
    echo "${PROJECT_PREFIX}-${OC_APP}"
    oc process -f ./"${TEMPLATE_DIRECTORY}/${DEPLOY_CONFIG_TEMPLATE}" \
    -p NAME="${APP_NAME}" \
    -p VERSION="${BUILD_NUMBER}" \
    -p SUFFIX="${SUFFIX}" \
    -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
    -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
    -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
    -p OC_NAMESPACE="${PROJECT_PREFIX}" \
    -p OC_APP="${OC_APP}" | oc delete -f - --namespace="${PROJECT_PREFIX}-${OC_APP}"
}

function deleteDc() {
    source ./"$COMPONENT.sh"
    deployPresent=$(oc get dc/"${APP_NAME}-${BRANCH_LOWER}" --ignore-not-found=true)
    if [ -z "${deployPresent}" ];
    then 
        MODE="apply"
    else 
        MODE="create"
    fi;
    echo "Deleting ${COMPONENT} from ${OC_APP} ..."
    echo "${PROJECT_PREFIX}-${OC_APP}"
    oc process -f ./"${TEMPLATE_DIRECTORY}/${DEPLOY_CONFIG_TEMPLATE}" \
    -p NAME="${APP_NAME}" \
    -p VERSION="${BUILD_NUMBER}" \
    -p SUFFIX="${SUFFIX}" \
    -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
    -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
    -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
    -p OC_NAMESPACE="${PROJECT_PREFIX}" \
    -p OC_APP="${OC_APP}" | oc delete -f - --namespace="${PROJECT_PREFIX}-${OC_APP}"
}

function ocApply() {
    source ./"${COMPONENT}.sh"
    echo "ocApply..."
    echo "${PROJECT_PREFIX}-${OC_APP}"
    if [ ${COMPONENT} == "build" ];
    then 
        configType="bc"
    elif [ ${COMPONENT} == "deploy" ];
    then 
        configType="dc"
    fi
    if [ "${BRANCH_LOWER}" == "develop" ] || [ "${BRANCH_LOWER}" == "master" ];
    then 

        CHANGE_BRANCH="${BRANCH_NAME}"
    else 
        SUFFIX="\-${BRANCH_LOWER}";
    fi
    oc process -f openshift/"${OC_APP}"."${configType}".yaml \
    -p NAME="${OC_APP}" \
    -p VERSION="${BUILD_NUMBER}" \
    -p SUFFIX="${SUFFIX}" \
    -p SOURCE_CONTEXT_DIR="prime-${OC_APP}" \
    -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
    -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}"  \
    -p OC_NAMESPACE="${PROJECT_PREFIX}" \
    -p OC_APP="$3" | oc apply -f - --namespace="${PROJECT_PREFIX}-$3" 
    if [[ ${COMPONENT} == "build" &&  "${OC_APP}" != "postgresql" ]];
    then
        echo "Building..."
        oc start-build ${OC_APP}${SUFFIX} -n ${PROJECT_PREFIX}-$3 --wait --follow
    else
        echo "Deployment should be automatic..."
    fi
}

function sonar(){
    OC_APP="$OC_APP"
    deployPresent=$(oc get bc/${COMPONENT}-${BRANCH_LOWER} --ignore-not-found=true)
    if [ -z ${deployPresent} ];
    then MODE="apply"
    else MODE="create"
    fi;
    oc process -f openshift/sonar.pod.yaml \
    -p NAME="sonar-runner" \
    -p VERSION="${BUILD_NUMBER}" \
    -p SUFFIX='-'"${BRANCH_LOWER}" \
    -p SOURCE_CONTEXT_DIR="." \
    -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
    -p SOURCE_REPOSITORY_REF="${BRANCH_NAME}" \
    -p OC_NAMESPACE="${PROJECT_PREFIX}" \
    -p OC_APP="${OC_APP}" | oc ${MODE} -f - --namespace="${PROJECT_PREFIX}-${OC_APP}"
    echo "Scanning..."
    sonar-scanner -X
}

function determineMode() {
    buildPresent=$(oc get "$2"/"$1-${BRANCH_LOWER}" --ignore-not-found=true)
    if [ -z "${buildPresent}" ];
    then MODE="apply"
    else MODE="create"
    fi;
}

# Scrubs all PR assets from the environment
function cleanOcArtifacts() {
    artifactItems=$(oc get all -n ${PROJECT_PREFIX}-dev | grep -i "-${BRANCH_NAME}"  | column -t | awk '{print $1}' | sort)
    echo "${artifactItems}"
    for i in ${artifactItems};
    do
        oc delete -n ${PROJECT_PREFIX}-dev $i
    done
    artifactSecrets=$(oc get secrets -n ${PROJECT_PREFIX}-dev | grep -i "-${BRANCH_NAME}"  | column -t | awk '{print $1}' | sort)
    echo "${artifactSecrets}"
    for i in ${artifactSecrets};
    do
        oc delete -n ${PROJECT_PREFIX}-dev secret/"$i"
    done
    artifactStorage=$(oc get all -n ${PROJECT_PREFIX}-dev | grep -i "-${BRANCH_NAME}"  | column -t | awk '{print $1}' | sort)
    echo "${artifactSorage}"
    for i in ${artifactStorage};
    do
        oc delete -n ${PROJECT_PREFIX}-dev $i
    done
}


# Build an deploy are very alike, require similar logic for config injestion.
# This takes in Git, Jenkins and system variables to the template that will be processed.

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