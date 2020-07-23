#!/bin/bash
source project.conf
source functions.bash
source tests.bash
find . -type d ! -name openshift -delete

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
    fsparsify)
        sparsify $@
        ;;
    notifyGitHub)
        notifyGitHub $@
        ;;
    *)
    echo "You\'re doing it wrong..."
esac
