export PROJECT_PREFIX='dqszvc'
export GIT_URL='https://github.com/bcgov/moh-prime.git'
export BRANCH_LOWER=$(echo "$BRANCH_NAME" | tr '[:upper:]' '[:lower:]') 
function variablePopulation() {
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
}
variablePopulation