#!/bin/bash
git clone git://github.com/bcgov/moh-prime.git
cd moh-prime
git checkout ${BRANCH_NAME}
source sonar-scanner/.bash_profile