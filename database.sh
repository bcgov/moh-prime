#!/bin/sh
export TEMPLATE_DIRECTORY="openshift"
export SOURCE_CONTEXT_DIR="prime-postgres"
export BUILD_CONFIG_TEMPLATE="postgresql.bc.yaml"
export DEPLOY_CONFIG_TEMPLATE="postgresql.dc.yaml"
export OC_APP_NAME="postgresql"
export BUILD_REQUIRED="false"