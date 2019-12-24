#!/bin/bash
source project.conf
source functions.bash
source tests.bash

case "$1" in
    build)
        build $2 $3 $@
        #> /dev/null 2>&1
        ;;
    deploy)
        deploy $2 $3 $@
        #> /dev/null 2>&1
        ;;
    toolbelt)
        toolbelt $2 $3 $@
        ;;
    scan)
        scan
        ;;
    zap)
        zap $2 $3 $@
        ;;
    occleanup)
        occleanup
        ;;
    nukenpave)
        nukenpave $2 $3 $@
        ;;
    *)
    echo "You\'re doing it wrong..."
esac
