BRANCH_LOWER=`echo "${BRANCH_NAME}" | awk '{print tolower($0)}'`

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
    if [ "${BUILD_REQUIRED}" !=  'true' ] ;
    then
        echo "Deployment should be automatic..."
    else
        echo "Building $1 (${APP_NAME}) to $PROJECT_PREFIX-$2..."
        buildPresent=$(oc get bc/"$APP_NAME-$BRANCH_LOWER" --ignore-not-found=true)
        if [ -z "${buildPresent}" ] ;
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
            -p SOURCE_REPOSITORY_URL="https://${GIT_URL}" \
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
            -p SOURCE_REPOSITORY_URL="https://${GIT_URL}" \
            -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
            -p OC_NAMESPACE="${PROJECT_PREFIX}" \
            -p OC_APP="$2" | oc "${MODE}" -f - --namespace="${PROJECT_PREFIX}-$2"
        fi;
        if [ "$BUILD_REQUIRED" == true ];
        then
            echo "Building oc start-build $APP_NAME$SUFFIX -n $PROJECT_PREFIX-$2 --wait --follow ..."
            oc start-build "$APP_NAME$SUFFIX" -n "$PROJECT_PREFIX-$2" --wait --follow
        fi
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
        -p SOURCE_REPOSITORY_URL="https://${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${BRANCH_NAME}" \
        -p OC_NAMESPACE="${PROJECT_PREFIX}" \
        -p OC_APP="$2" | oc "${MODE}" -f - --namespace="${PROJECT_PREFIX}-$2"
    else
        oc process -f ./"${TEMPLATE_DIRECTORY}/${DEPLOY_CONFIG_TEMPLATE}" \
        -p NAME="${APP_NAME}" \
        -p VERSION="${BUILD_NUMBER}" \
        -p SUFFIX='-'"${BRANCH_LOWER}" \
        -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
        -p SOURCE_REPOSITORY_URL="https://${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
        -p OC_NAMESPACE="${PROJECT_PREFIX}" \
        -p OC_APP="$2" | oc "${MODE}" -f - --namespace="${PROJECT_PREFIX}-$2"
    fi;
}

function sonar(){
    oc process -f openshift/sonar-scanner.bc.yaml \
        -p SOURCE_REPOSITORY_URL="https://${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" | oc apply -f - --namespace="${PROJECT_PREFIX}-$1"
    oc process -f openshift/sonar-scanner.dc.yaml \
        -p SOURCE_REPOSITORY_URL="https://${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" | oc apply -f - --namespace="${PROJECT_PREFIX}-$1"
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

function gitPromote() {
    # Update branch with latest changes from branch
    git clone ${GIT_URL}
    cd ${PROJECT_NAME}
    git checkout ${CHANGE_BRANCH} && \
    git merge --squash -s ours -m "Merging $1 to ${CHANGE_BRANCH}" $1 ${CHANGE_BRANCH} && \
    git commit -a -m "Merge branch ${CHANGE_BRANCH} into $1" &&\
    git merge --squash -s ours -m "Updating branch with $1" ${CHANGE_BRANCH} origin/$1 &&\
    git push https://${GIT_USERNAME}:${GIT_PASSWORD}@${GIT_URL} $1
}
