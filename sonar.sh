#!/bin/sh
export TEMPLATE_DIRECTORY='openshift'
export SOURCE_CONTEXT_DIR='sonar-scanner'
export BUILD_CONFIG_TEMPLATE='sonar-scanner.bc.yaml'
export DEPLOY_CONFIG_TEMPLATE='sonar-scanner.dc.yaml'
export APP_NAME='sonar-scanner'
export BUILD_REQUIRED='true'