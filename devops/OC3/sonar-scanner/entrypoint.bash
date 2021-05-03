#!/bin/bash
sleep 30
npm install @angular/core
/zap/zap.sh -daemon -host 0.0.0.0 -port 8080 -config api.addrs.addr.name=.* -config api.addrs.addr.regex=true -config spider.maxDuration=1 -addonupdate -addoninstall pscanrulesBeta > /dev/null 2>&1 

wget https://jenkins-moh-prime.apps.silver.devops.gov.bc.ca/jnlpJars/agent.jar
/usr/local/openjdk-8/bin/java -jar agent.jar -jnlpUrl http://jenkins-prod/computer/code-tests/slave-agent.jnlp -secret $NODE_SECRET -workDir $HOME
tail -f /dev/null
#Jenkins sometimes takes a while to drop connections. This order mitigates the issue.
