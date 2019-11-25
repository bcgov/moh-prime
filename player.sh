#!/bin/bash
source project.conf
source tests.bash
source functions.bash

case "$1" in
    build)
        build $2 $3 
        #> /dev/null 2>&1 
        ;;
    deploy)
        deploy $2 $3 
        #> /dev/null 2>&1 
        ;;
    toolbelt)
        toolbelt $2
        ;;
    scan)
        scan
        ;;
    zap)
        zap $2 $3
        ;;
    cleanup)
        cleanup $2
        ;;
    *)
    echo "Usage: $0 [application (build|deploy) (dev|test|prod|tools) | scan]"
esac
