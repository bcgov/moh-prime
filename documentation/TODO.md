# TODO
This is a section to document technical backlog items, that due to our time constraints, were prioritized lower than other business driven value.

## Container Runtime

Most major cloud vendors now have managed Kubernetes as a service. Azure has AKS and Openshift, AWS has EKS and OpenShift, and google has GKE. Any of these would have been preferable to a single Virtual Machine running docker. Our MVP approach was to have the application up and running and our VM Docker image accomplished this, but we recognize the value of kubernetes with it's scaling, networking, security and other features out of the box.

## Testing

Our team has experience with various functional testing suites that are based on Selenium. Our team follows the Testing Pyrmiad https://martinfowler.com/bliki/TestPyramid.html when approaching a testing strategy. As such, we valued lower level unit test over higher level functional test. Regardless, there can be value in these tests and they were a piece of our testing strategy that we beleive should be included.

## Angular Components

Angular reactive forms is a component that we have identified that could bring value to a data driven application. This Coding Challenge application is light on data input so we did not bring this component into our application. If the application were to include a lot of data input and validation this component would have been prioritized higher on our backlog and brought in.
