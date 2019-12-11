export BRANCH_LOWER=`echo "${BRANCH_NAME}" | awk '{print tolower($0)}'`
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

function toolbelt() {
    source $1.conf
    OC_APP=tools
    buildPresent=$(oc get bc/"$APP_NAME" --ignore-not-found=true)
    if [ -z "${buildPresent}" ];
    then
        MODE="apply"
    else
        MODE="create"
    fi;
    oc process -f ./"${TEMPLATE_DIRECTORY}/$BUILD_CONFIG_TEMPLATE" \
        -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" | oc $MODE -f - --namespace="${PROJECT_PREFIX}-$2"
    oc process -f "${TEMPLATE_DIRECTORY}/$DEPLOY_CONFIG_TEMPLATE" \
        -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
        -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" | oc $MODE -f - --namespace="${PROJECT_PREFIX}-$2"
    if [ "$BUILD_REQUIRED" == true ];
    then
        echo "Building oc start-build $APP_NAME -n ${PROJECT_PREFIX}-${OC_APP} --wait --follow ..."
        oc start-build $APP_NAME -n ${PROJECT_PREFIX}-$2 --wait --follow
    else
        echo "Deployment should be automatic..."
    fi
}

function determineMode() {
    buildPresent=$(oc get "$2"/"$1-${BRANCH_LOWER}" --ignore-not-found=true)
    if [ -z "${buildPresent}" ];
    then MODE="apply"
    else MODE="create"
    fi;
}

function occleanup() {
    OPEN_PR_ARRAY=()
    LIVE_BRANCH_ARRAY=()
    ORPHANS=()
    curl -o openPRs.txt "https://api.github.com/repos/${PROJECT_OWNER}/${PROJECT_NAME}/pulls?status=open&sort=number"
    declare -p OPEN_PR_ARRAY=( $(grep '"number"' openPRs.txt | column -t | sed 's|[:,]||g' | awk '{print $2}') )
    declare -p LIVE_BRANCH_ARRAY=( $(oc get route -n ${PROJECT_PREFIX}-dev | awk '{print $1}' | grep -P "(\-pr\-\d+)" | sed 's/[^0-9]*//g' | sort -un) )
    ORPHANS=$(echo ${OPEN_PR_ARRAY[@]} ${LIVE_BRANCH_ARRAY[@]} | tr ' ' '\n' | sort | uniq -u)
    for i in $ORPHANS
    do
        cleanOcArtifacts $i
    done
}

function cleanOcArtifacts() {
    declare -p ALL_BRANCH_ARTIFACTS=( $(oc get all,pvc,secrets,route -n ${PROJECT_PREFIX}-dev | grep -i "\-$1" | awk '{print $1}' | grep -P "(\-pr\-\d+)") )
    for a in "${ALL_BRANCH_ARTIFACTS[@]}"
    do
        oc delete -n ${PROJECT_PREFIX}-dev $a
    done
}

function nukenpave() {
    declare -p TARGET_ARTIFACTS=($(oc get all,pvc,route -n ${PROJECT_PREFIX}-$2 | grep -i "$1" | awk '{print $1}' | grep -Ev "(\-pr\-)") )
    for target in "${TARGET_ARTIFACTS[@]}"
    do
        oc delete -n ${PROJECT_PREFIX}-$2 $target
    done
        build $1 $2
        deploy $1 $2
}
