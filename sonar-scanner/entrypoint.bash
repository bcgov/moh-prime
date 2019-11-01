#!/bin/bash
git clone git://github.com/bcgov/moh-prime.git
cd moh-prime
git checkout ${BRANCH_NAME}
sed -i 's/\r$//' sonar-scanner/sonar-runner.bash
source sonar-scanner/sonar-runner.bash