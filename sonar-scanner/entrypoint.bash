#!/bin/bash
java -jar agent.jar -jnlpUrl https://jenkins-prod-dqszvc-tools.pathfinder.gov.bc.ca/computer/code-tests/slave-agent.jnlp -secret c598ca95983a9f6df4d06cc7f770b0d1ea404b806851f1a7f1066d89515c2c12 -workDir "/opt/app-root/app/jenkins"
tail -f /dev/null
#git clone git://github.com/bcgov/moh-prime.git
#cd moh-prime
#git checkout ${BRANCH_NAME}
#sed -i 's/\r$//' sonar-scanner/sonar-runner.bash
#source sonar-scanner/sonar-runner.bash
