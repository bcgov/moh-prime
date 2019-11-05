#!/bin/bash
wget https://jenkins-prod-dqszvc-tools.pathfinder.gov.bc.ca/jnlpJars/agent.jar
java -jar agent.jar -jnlpUrl http://jenkins-prod/computer/code-tests/slave-agent.jnlp -secret c598ca95983a9f6df4d06cc7f770b0d1ea404b806851f1a7f1066d89515c2c12 -workDir "$HOME"
tail -f /dev/null
