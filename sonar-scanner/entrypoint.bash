#!/bin/bash
sleep 30
npm install @angular/core
/usr/local/openjdk-8/bin/java -jar /usr/share/jenkins/agent.jar -jnlpUrl http://jenkins-prod/computer/code-tests/slave-agent.jnlp -secret c598ca95983a9f6df4d06cc7f770b0d1ea404b806851f1a7f1066d89515c2c12 -workDir $HOME
tail -f /dev/null
#Jenkins sometimes takes a while to drop connections. This order mitigates the issue.