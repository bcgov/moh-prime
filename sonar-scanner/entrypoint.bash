#!/bin/bash
java -jar agent.jar -jnlpUrl https://jenkins-jnlp/computer/code-tests/slave-agent.jnlp -secret c598ca95983a9f6df4d06cc7f770b0d1ea404b806851f1a7f1066d89515c2c12 -workDir "/opt/app-root/app/"
tail -f /dev/null