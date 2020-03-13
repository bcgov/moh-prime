## Devops Summary
PRIME development is performed in an agile manner and embraces devops practices and culture. We follow a gitflow branch structure with builds and deploys triggered by Pull-Requests. We use github triggers to webhook our Jenkins builds and utilizes shell and OC commands to build and deploy the application to the BC Government's Pathfinder OpenShift instance.

## Branching Strategy
All development feature branches should have an origin of 'Develop'. When the feature is complete it should be pushed to github and a Pull-Request created to merge the Pull-Request into 'Develop'. The Pull-Request will trigger a build in Jenkins and will output and update the Pull-Request on it's status. After the Pull-Request Review the branch should be squashed and merged which will also delete the branch from the remote origin. At our Product Owner's discretion, we will merge 'Develop' into 'Master'. This merge triggers our webhook and calls jenkins to build the application and publish it to our Test openshift instance. Once this build has gained acceptance we will perform a Jenkins action which publish the application to Production and into a Demo environment. A release branch will be created so that any production issues could be remediated here.

## Jenkins
Jenkins is performing a build of all of our application components, running unit test suites, performing a zap scan, publishing our application build outputs to OpenShift as containers (which will run any database migrations), executing static code analysis and publishing all outputs to SonarQube.

## SonarQube
We're posting the results of code coverage, unit tests, zap scans and static code analysis to our SonarQube instance.

## SchemaSpy
We refresh our schemaspy physical model representation on every build. This is a great place to start to explore how we are persisting the data within PRIME.