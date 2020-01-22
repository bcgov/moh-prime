#!/bin/bash
source project.conf
source functions.bash
source tests.bash

case "$1" in
    build)
        build $@
        echo "Extra params= ${@:3}"
        #> /dev/null 2>&1
        ;;
    deploy)
        deploy $@
        echo "Extra params= ${@:3}"
        #> /dev/null 2>&1
        ;;
    toolbelt)
        toolbelt $@
        echo "Extra params= ${@:3}"
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
    nukenpave)
        nukenpave $@
        ;;
    pipeline_args)
        pipeline_args $@
        ;;
    functionTest)
        functionTest $@
        ;;
    *)
    echo "You\'re doing it wrong..."
esac
