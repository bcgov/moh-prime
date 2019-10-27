#!/bin/sh
export PROJECT_PREFIX="dqszvc"
export GIT_URL='https://github.com/bcgov/moh-prime.git'
export BRANCH_LOWER=$(echo "$BRANCH_NAME" | tr '[:upper:]' '[:lower:]') 
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