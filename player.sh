#!/bin/bash
# Because I don't know Groovy well enough.
###
export licensePlate="dqszvc"
export yamlLocation="openshift/compositions"

function ocPush(){
    for file in `find $yamlLocation -type f -name *.yaml`
    do oc apply --namespace="$licensePlate-$1" -f $file
    done
}
function dryRun()
    for file in `find $yamlLocation -type f -name *.yaml`
    do oc create -f $file --dry-run -o json
    