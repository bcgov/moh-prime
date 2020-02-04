#!/bin/bash
source project.conf
source functions.bash
source tests.bash

case "$1" in
    build)
        build $@ #> /dev/null 2>&1
        echo "Extra params= ${@:4}"
        ;;
    deploy)
        deploy $@ #>  /dev/null 2>&1
        echo "Extra params= ${@:4}"
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
