# A Sidecar for collecting/processing Logs

[Fluent-bit](https://docs.fluentbit.io/manual/about/what-is-fluent-bit) is basically a 'log forwarder' tool that can be run as a 'side-car' container (a docker image) in each pod containing our apps. Although it can also be deployed as a stand-alone service (servicing multiple apps). Fluent-bit can forward logs to lots of different 'outputs' for example, HTTP, Opensearch, Slack, AWS Lamda and a lot more (see: https://docs.fluentbit.io/manual/pipeline/outputs). The steps required for deploying a Fluent-bit sidecar are shown below. Note: These steps are to deploy the sidecar.

You can read about a common-service-showcase in BCGOV which has deployed a Fluent-bit sidecar for a node application on OpenShift [here](https://github.com/bcgov/common-service-showcase/wiki/Logging-to-a-Sidecar).

## Implementing a Fluent-bit for PRIME applicatinons on OpenShift

We deploy a Fluent-bit sidecar container in both backend/webapi and frontend/nginx applications (in deploymentConfigs) to collect/process/monitor Logs inside the applications and alert the PRIME team if certain keywords, regular expressions, etc. are matched in the log stream. Each release of Fluent-bit comes with a debug version (for example: fluent-bit:2.X-debug) that includes some other Linux tools such as busybox, bash, etc. and make testing the installation easier. In this example, we used `fluent-bit:3.0.3`. We forward alerts to a private Slack channel (a webhook). So our overall flow of logs is: 

`frontend and backend app > fluent-bit sidecar > webhook (Slack channel)`.


### Fluent-bit for frontend/nginx applicatinons

Our nginx app output logs to a configurable file path (for example `tmp/error_flentbit.log`) which can be set in nginx.configmap. Our Fluent-bit container will mount the directory `/tmp/` from our nginx application and read the log file `error_flentbit.log`.
Fluent-bit has its own configuration file that we can create using an OpenShift [config map template](./frontend-fluentbit-config.yaml). This config of the Fluent-bit uses `tail` plugin in the `INPUT` section to receive logs from nginx log file, and uses a `parsers` to define the log formats by providing a Path in the SERVICE section that links to a separate file parser.conf `(Parsers_File  parsers.conf)`. It also uses `grep` plugin in the `FILTER` section to filter the logs based on specific keywords (e.g. error). At the end it uses the `SLACK` plugin in the `OUTPUT` section to communicate with our Slack webhook and outputs error notifications to the PRIME Team's Slack channel.


### Fluent-bit for backend/webapi applicatinons

Our webapi app outputs logs to a file path (`/opt/app-root/app/logs/`) and the Fluent-bit container will mount the directory from the webpai app to a log-storage. A [Fluent-bit config map](./webapi-fluentbit-config.yaml) with the same configuration is used to read the log files `*.log` from mounted path, filter the log streams based on `ERR` keyword and output the filtered logs to the PRIME Team's Slack channel. 

