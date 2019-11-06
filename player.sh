#!/bin/bash
source project.conf
export ACTION=$1
export COMPONENT=$2
export OC_APP=$3
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
    source ./"$1.conf"
    echo "Building $1 (${APP_NAME}) to $PROJECT_PREFIX-$2..."
    buildPresent=$(oc get bc/"$APP_NAME-$BRANCH_LOWER" --ignore-not-found=true)
    if [ -z "${buildPresent}" ];
    then 
        MODE="apply"
    else 
        MODE="create"
    fi;
    if [ "${BRANCH_LOWER}" == "develop" ] || [ "${BRANCH_LOWER}" == "master" ];
    then 
        echo "oc process -f ./${TEMPLATE_DIRECTORY}/${BUILD_CONFIG_TEMPLATE} -p NAME=${APP_NAME} -p VERSION=${BUILD_NUMBER} -p SOURCE_CONTEXT_DIR=${SOURCE_CONTEXT_DIR} -p SOURCE_REPOSITORY_URL=${GIT_URL} -p SOURCE_REPOSITORY_REF=${BRANCH_NAME} -p OC_NAMESPACE=${PROJECT_PREFIX} -p OC_APP=$2 | oc ${MODE} -f - --namespace=${PROJECT_PREFIX}-$2"  
        oc process -f ./"${TEMPLATE_DIRECTORY}/${BUILD_CONFIG_TEMPLATE}" \
        -p NAME="${APP_NAME}" \
        -p VERSION="${BUILD_NUMBER}" \
        -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
        -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${BRANCH_NAME}" \
        -p OC_NAMESPACE="${PROJECT_PREFIX}" \
        -p OC_APP="$2" | oc "${MODE}" -f - --namespace="${PROJECT_PREFIX}-$2"  
    else 
        echo "oc process -f ./${TEMPLATE_DIRECTORY}/${BUILD_CONFIG_TEMPLATE} -p NAME=${APP_NAME} -p VERSION=${BUILD_NUMBER} -p SUFFIX=-${BRANCH_LOWER} -p SOURCE_CONTEXT_DIR=${SOURCE_CONTEXT_DIR} -p SOURCE_REPOSITORY_URL=${GIT_URL} -p SOURCE_REPOSITORY_REF=${BRANCH_NAME} -p OC_NAMESPACE=${PROJECT_PREFIX} -p OC_APP=$2 | oc ${MODE} -f - --namespace=${PROJECT_PREFIX}-$2"  
        oc process -f ./"${TEMPLATE_DIRECTORY}/${BUILD_CONFIG_TEMPLATE}" \
        -p NAME="${APP_NAME}" \
        -p VERSION="${BUILD_NUMBER}" \
        -p SUFFIX="-${BRANCH_LOWER}" \
        -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
        -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
        -p OC_NAMESPACE="${PROJECT_PREFIX}" \
        -p OC_APP="$2" | oc "${MODE}" -f - --namespace="${PROJECT_PREFIX}-$2"
    fi;
    if [ "$BUILD_REQUIRED" == true ];
    then
        echo "Building oc start-build $APP_NAME$SUFFIX -n $PROJECT_PREFIX-$2 --wait --follow ..."
        oc start-build "$APP_NAME$SUFFIX" -n "$PROJECT_PREFIX-$2" --wait --follow 
    else
        echo "Deployment should be automatic..."
    fi
}

function deploy() {
    source ./"$1.conf"
    echo "Deploying $1 (${APP_NAME}) to $2 ..."
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
        -p OC_APP="$2" | oc "${MODE}" -f - --namespace="${PROJECT_PREFIX}-$2"  
    else 
        oc process -f ./"${TEMPLATE_DIRECTORY}/${DEPLOY_CONFIG_TEMPLATE}" \
        -p NAME="${APP_NAME}" \
        -p VERSION="${BUILD_NUMBER}" \
        -p SUFFIX='-'"${BRANCH_LOWER}" \
        -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
        -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
        -p OC_NAMESPACE="${PROJECT_PREFIX}" \
        -p OC_APP="$2" | oc "${MODE}" -f - --namespace="${PROJECT_PREFIX}-$2"
    fi;
}

function deleteBc() {
    source ./"$1.conf"
    deployPresent=$(oc get bc/"${APP_NAME}-${BRANCH_LOWER}" --ignore-not-found=true)
    if [ -z "${deployPresent}" ];
    then 
        MODE="apply"
    else 
        MODE="create"
    fi;
    echo "Deleting $1 from $2 ..."
    echo "${PROJECT_PREFIX}-$2"
    oc process -f ./"${TEMPLATE_DIRECTORY}/${DEPLOY_CONFIG_TEMPLATE}" \
    -p NAME="${APP_NAME}" \
    -p VERSION="${BUILD_NUMBER}" \
    -p SUFFIX="${SUFFIX}" \
    -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
    -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
    -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
    -p OC_NAMESPACE="${PROJECT_PREFIX}" \
    -p OC_APP="$2" | oc delete -f - --namespace="${PROJECT_PREFIX}-$2"
}

function deleteDc() {
    source ./"$1.conf"
    deployPresent=$(oc get dc/"${APP_NAME}-${BRANCH_LOWER}" --ignore-not-found=true)
    if [ -z "${deployPresent}" ];
    then 
        MODE="apply"
    else 
        MODE="create"
    fi;
    echo "Deleting $1 from $2 ..."
    echo "${PROJECT_PREFIX}-$2"
    oc process -f ./"${TEMPLATE_DIRECTORY}/${DEPLOY_CONFIG_TEMPLATE}" \
    -p NAME="${APP_NAME}" \
    -p VERSION="${BUILD_NUMBER}" \
    -p SUFFIX="${SUFFIX}" \
    -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
    -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
    -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
    -p OC_NAMESPACE="${PROJECT_PREFIX}" \
    -p OC_APP="$2" | oc delete -f - --namespace="${PROJECT_PREFIX}-$2"
}

function ocApply() {
    source ./"$1.conf"
    echo "ocApply..."
    echo "${PROJECT_PREFIX}-$2"
    if [ $1 == "build" ];
    then 
        configType="bc"
    elif [ $1 == "deploy" ];
    then 
        configType="dc"
    fi
    if [ "${BRANCH_LOWER}" == "develop" ] || [ "${BRANCH_LOWER}" == "master" ];
    then 

        CHANGE_BRANCH="${BRANCH_NAME}"
    else 
        SUFFIX="\-${BRANCH_LOWER}";
    fi
    oc process -f openshift/"$2"."${configType}".yaml \
    -p NAME="$2" \
    -p VERSION="${BUILD_NUMBER}" \
    -p SUFFIX="${SUFFIX}" \
    -p SOURCE_CONTEXT_DIR="prime-$2" \
    -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
    -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}"  \
    -p OC_NAMESPACE="${PROJECT_PREFIX}" \
    -p OC_APP="$3" | oc apply -f - --namespace="${PROJECT_PREFIX}-$3" 
    if [[ $1 == "build" &&  "$2" != "postgresql" ]];
    then
        echo "Building..."
        oc start-build $2${SUFFIX} -n ${PROJECT_PREFIX}-$3 --wait --follow
    else
        echo "Deployment should be automatic..."
    fi
}

function sonar(){
    OC_APP="$2"
    oc process -f openshift/sonar-scanner.bc.yaml \
    | oc apply -f - --namespace="${PROJECT_PREFIX}-$1"
    oc process -f openshift/sonar-scanner.dc.yaml \
    | oc apply -f - --namespace="${PROJECT_PREFIX}-$1"
    oc start-build sonar-runner -n ${PROJECT_PREFIX}-$1 --wait --follow
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
    artifactItems=$(oc get all,secrets,pvc -n ${PROJECT_PREFIX}-dev | grep -i "\-${BRANCH_NAME}"  | column -t | awk '{print $1}' | sort)
    echo "${artifactItems}"
    for i in ${artifactItems};
    do
        oc delete -n ${PROJECT_PREFIX}-dev $i
    done
}

function cleanup() {
    artifactItems=$(oc get all,pvc,secrets -n ${PROJECT_PREFIX}-dev | grep -i "\-$1"  | column -t | awk '{print $1}' | sort)
    echo "${artifactItems}"
    for i in ${artifactItems};
    do
        oc delete -n ${PROJECT_PREFIX}-dev $i
    done
}

function dotnetTests()
{   
    echo "Starting tests..." 
    dotnet build 
    echo "Beginning .NET code coverage scan..."
    $HOME/.dotnet/tools/coverlet "./prime-dotnet-webapi-tests/bin/Debug/netcoreapp2.2/PrimeTests.dll" -f opencover -o ./BuildReports/Coverage/coverage --target "dotnet" --targetargs "test . --no-build --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory ./BuildReports/UnitTests"
    dotnet build-server shutdown 
    echo "Beginning .NET sonar scan..."
    $HOME/.dotnet/tools/dotnet-sonarscanner begin /k:"prime-dotnet-webapi" /d:sonar.host.url=http://sonarqube:9000 /d:sonar.cs.opencover.reportsPaths="./BuildReports/Coverage/coverage.opencover.xml" /d:sonar.exclusions="**/Migrations/*" /d:sonar.coverage.exclusions="**Tests*.cs","**/Migrations/*","**/Program.cs" /d:sonar.cpd.exclusions="**/Migrations/*" /d:sonar.cs.vstest.reportsPaths="./BuildReports/UnitTests/TestResults.trx" /d:sonar.cs.nunit.reportsPaths="./BuildReports/UnitTests/TestResults.xml"
    $HOME/.dotnet/tools/dotnet-sonarscanner end
}

function angularTests()
{ 
    echo "Beginning angular/javascript sonar scan..."
    sonar-scanner -Dsonar.host.url=http://sonarqube:9000 -Dsonar.projectKey=angular-frontend -Dsonar.sources=prime-angular-frontend
}

function scan() 
{
    echo "Beginning tests on .NET ..."
    dotnetTests > /dev/null 2>&1 
    echo "Beginning tests on Angular ..."
    angularTests > /dev/null 2>&1 
}

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
        sonar $2
        ;;
    scan)
        scan
        ;;
    zap)
        zap $2 $3
        ;;
    cleanup)
        cleanup $2
        ;;
    *)
    echo "Usage: $0 {ocApply|sonar|zap|promote} [build|depoly] <app> <dev|test|prod>"
esac
