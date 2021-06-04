#!/bin/bash
source ./devops/OC4/project.conf
source ./devops/OC4/functions.bash
# source ./devops/OC4/tests.bash

case "$1" in
    build)
        build $@ # > /dev/null 2>&1
        #echo "Extra params= ${@:4}"
        ;;
    deploy)
        deploy $@ # > /dev/null 2>&1
        #echo "Extra params= ${@:4}"
        ;;
    toolbelt)
        toolbelt $@
        echo "Extra params= ${@:4}"
        ;;
    scan)
        scan
        ;;
    zap)
        zap $@
        ;;
    occleanup)
        occleanup
        ;;
    pipeline_args)
        pipeline_args $@
        ;;
    functionTest)
        functionTest $@
        ;;
    sparsify)
        sparsify $@
        ;;
    notifyGitHub)
        notifyGitHub $@
        ;;
    *)
    echo "You are doing it wrong..."
    ;;
esac
