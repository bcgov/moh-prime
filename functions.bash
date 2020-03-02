oc project ${PROJECT_PREFIX}-$3

export BRANCH_LOWER=`echo "${BRANCH_NAME}" | awk '{print tolower($0)}'`
function variablePopulation() {
    if [ "${APP_NAME}" == "test" ] || [ "${APP_NAME}" == "prod" ];
    then
        export DOTNET_PHASE="Release"
    else
        export DOTNET_PHASE="Development"
    fi
}

variablePopulation

function pipeline_args() {
    export PIPELINE_ARGS="$*"
}

function determineMode() {
    buildPresent=$(oc get bc/"${APP_NAME}${SUFFIX}" --ignore-not-found=true | wc -l)
    if [ -z "${buildPresent}" ];
    then 
        MODE="apply"
        #OC_ARGS=""
    else 
        MODE="apply"
        #OC_ARGS="--overwrite=false --all"
    fi;
}

function build() {
    source ./"$2.conf"
    echo "Building $2 (${APP_NAME}) to $PROJECT_PREFIX-$3..."
    buildPresent=$(oc get bc/"${APP_NAME}${SUFFIX}" --ignore-not-found=true | wc -l)
    determineMode
    echo "oc process -f ./${TEMPLATE_DIRECTORY}/${BUILD_CONFIG_TEMPLATE} -p NAME=${APP_NAME} -p VERSION=${BUILD_NUMBER} -p SOURCE_CONTEXT_DIR=${SOURCE_CONTEXT_DIR} -p SOURCE_REPOSITORY_URL=${GIT_URL} -p SOURCE_REPOSITORY_REF=${BRANCH_NAME} -p OC_NAMESPACE=$PROJECT_PREFIX -p OC_APP=$3 ${@:4} | oc ${MODE} -f - --namespace=$PROJECT_PREFIX-$3"
    oc process -f ./"${TEMPLATE_DIRECTORY}/${BUILD_CONFIG_TEMPLATE}" \
    -p NAME="${APP_NAME}" \
    -p VERSION="${BUILD_NUMBER}" \
    -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
    -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
    -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
    -p OC_NAMESPACE="$PROJECT_PREFIX" \
    -p OC_APP="$3" ${@:4} --output="yaml" | oc "${MODE}" -f - --namespace="$PROJECT_PREFIX-$3" ${OC_ARGS} #--output="yaml"
    if [ "$BUILD_REQUIRED" == true ];
    then
        echo "Building oc start-build ${APP_NAME}${SUFFIX} -n $PROJECT_PREFIX-$3 --wait --follow ..."
        oc start-build "${APP_NAME}${SUFFIX}" -n "$PROJECT_PREFIX-$3" --wait --follow
    else
        echo "Deployment should be automatic..."
    fi
}

function deploy() {
    source ./"$2.conf"
    echo "Deploying $2 (${APP_NAME}) to $3 ..."
    export deployPresent=$(oc get dc/${APP_NAME}${SUFFIX} --ignore-not-found=true | wc -l)
    export pvcPresent=$(oc get pvc/${APP_NAME}${SUFFIX} --ignore-not-found=true | wc -l)
    export routePresent=$(oc get route/${APP_NAME}${SUFFIX} --ignore-not-found=true | wc -l)
    export servicePresent=$(oc get service/${APP_NAME}${SUFFIX} --ignore-not-found=true | wc -l)
    determineMode
    oc process -f ./"${TEMPLATE_DIRECTORY}/${DEPLOY_CONFIG_TEMPLATE}" \
    -p NAME="${APP_NAME}" \
    -p VERSION="${BUILD_NUMBER}" \
    -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
    -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
    -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
    -p OC_NAMESPACE="$PROJECT_PREFIX" \
    -p OC_APP="$3" ${@:4} | oc "${MODE}" -f - --namespace="$PROJECT_PREFIX-$3" ${OC_ARGS}
}

function toolbelt() {
    source ./$2.conf
    #OC_APP=tools
    buildPresent=$(oc get bc/"$APP_NAME" --ignore-not-found=true)
    MODE="apply"
    OC_ARGS="--overwrite=false --all"
    oc process -f ./"${TEMPLATE_DIRECTORY}/$BUILD_CONFIG_TEMPLATE" \
        -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
        -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
        -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
        -p OC_NAMESPACE="$PROJECT_PREFIX" \
        -p OC_APP="$3" ${@:4} --output="yaml" | oc $MODE -f - --namespace="$PROJECT_PREFIX-$3" ${OC_ARGS}
    if [ "$BUILD_REQUIRED" == true ];
    then
        echo "Building oc start-build $APP_NAME -n $PROJECT_PREFIX-${OC_APP} --wait --follow ..."
        oc start-build $APP_NAME -n $PROJECT_PREFIX-$3 ${OC_ARGS} --wait --follow
    else
        echo "Deployment should be automatic..."
    fi
    oc process -f ./"${TEMPLATE_DIRECTORY}/$DEPLOY_CONFIG_TEMPLATE" \
        -p SOURCE_REPOSITORY_URL="${GIT_URL}" \
        -p SOURCE_CONTEXT_DIR="${SOURCE_CONTEXT_DIR}" \
        -p SOURCE_REPOSITORY_REF="${CHANGE_BRANCH}" \
        -p OC_NAMESPACE="$PROJECT_PREFIX" \
        -p OC_APP="$3" ${@:4} --output="yaml" | oc $MODE -f - --namespace="$PROJECT_PREFIX-$3" ${OC_ARGS}
}


function getAllAssets() {
    oc get all,pvc,secrets -n $PROJECT_PREFIX-dev | column -t | awk '{print $1}' | sort -n
    declare -p ALL_ASSETS=( $( oc get all,pvc,secrets -n $PROJECT_PREFIX-dev | column -t | awk '{print $1}' | sort -n) )
}

function getAllPrRoutes() {
    declare -p ROUTE_ARRAY=( $(oc get route -n $PROJECT_PREFIX-dev | awk '{print $2}' | grep "pr-" | sed 's/.pharmanetenrolment-dqszvc-dev.pathfinder.gov.bc.ca//g' | sed 's/pr-//g') )
}
function getAllOpenPr () {
    curl -o openPRs.txt "https://api.github.com/repos/${PROJECT_OWNER}/${PROJECT_NAME}/pulls?status=open&sort=number"
    declare -p OPEN_PR_ARRAY=( $(grep '"number"' openPRs.txt | column -t | sed 's|[:,]||g' | awk '{print $2}') )
}

function getOldPr () {
    ORPHANS=$(printf '%s\n' "${ROUTE_ARRAY[@]}" "${OPEN_PR_ARRAY[@]}" | sort | uniq -u)
}

function occleanup() {
    OPEN_PR_ARRAY=()
    LIVE_BRANCH_ARRAY=()
    ORPHANS=()
    curl -o openPRs.txt "https://api.github.com/repos/${PROJECT_OWNER}/${PROJECT_NAME}/pulls?status=open&sort=number"
    declare -p OPEN_PR_ARRAY=( $(grep '"number":' openPRs.txt | column -t | sed 's|[:,]||g' | awk '{print $2}') )
    declare -p LIVE_BRANCH_ARRAY=( $(oc get dc -n $PROJECT_PREFIX-dev | awk '{print $1}' | grep -P "(\-pr\-\d+)" | sed 's/[^0-9]*//g' | sort -un) )
    ORPHANS=$(echo ${OPEN_PR_ARRAY[@]} ${LIVE_BRANCH_ARRAY[@]} | tr ' ' '\n' | sort | uniq -u)
    echo "ORPHANS=${ORPHANS}"
    for i in ${ORPHANS}
    do
        cleanOcArtifacts $i 
    done
}

function cleanOcArtifacts() {
    echo "Cleaning PR $1"
    declare -p ALL_BRANCH_ARTIFACTS=( $(oc get all,pvc,secrets,route -n $PROJECT_PREFIX-dev | grep -i "pr\-$1" | awk '{print $1}' ) )
    for a in "${ALL_BRANCH_ARTIFACTS[@]}"
    do
       echo "oc delete -n $PROJECT_PREFIX-dev $a"
       oc delete -n $PROJECT_PREFIX-dev $a
    done
}

function functionTest() {
    echo "1=$2"
    echo "2=$3"
    echo "Trailing = ${@:4}"
    echo "All = $@"
}
