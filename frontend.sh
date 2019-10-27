#!/bin/sh
export TEMPLATE_DIRECTORY="openshift"
export SOURCE_CONTEXT_DIR="prime-angular-frontend"
export BUILD_CONFIG_TEMPLATE="angular-frontend.bc.yaml"
export DEPLOY_CONFIG_TEMPLATE="angular-frontend.dc.yaml"
export OC_APP_NAME="angular-frontend"
export BUILD_REQUIRED="true"