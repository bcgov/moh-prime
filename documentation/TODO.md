# TODO
This is a section to document technical backlog items, that due to our time constraints, were prioritized lower than other business driven value.

## Container Runtime

Most major cloud vendors now have managed Kubernetes as a service. Azure has AKS and Openshift, AWS has EKS and OpenShift, and google has GKE. Any of these would have been preferable to a single Virtual Machine running docker. Our MVP approach was to have the application up and running and our VM Docker image accomplished this, but we recognize the value of Kubernetes with it's scaling, networking, security and other features out of the box.

## Testing

Our team has experience with various functional testing suites that are based on Selenium. Our team follows the Testing Pyramid https://martinfowler.com/bliki/TestPyramid.html when approaching a testing strategy. As such, we valued lower level unit test over higher level functional test. Regardless, there can be value in these tests and they were a piece of our testing strategy that we believe should be included.

## Angular Components

Angular reactive forms is a component that we have identified that could bring value to a data driven application. This Coding Challenge application is light on data input so we did not bring this component into our application. If the application were to include a lot of data input and validation this component would have been prioritized higher on our backlog and brought in.

## Admin Page

The user story for the admin application page excludes any role based access control. This would be the first additional piece of functionality that would be added on future development.

## SSL

SSL encryption using Let's Encrypt or another CA Root authority should be added to any production based application. This was item was at the top of the list and in the remaining minutes was almost completed.

## Swagger

When publishing a REST based API it is a good idea to also generate swagger based specifications for easy consumption of the API.

## Database Normalization

Our database only contains a single table with multiple columns. We do store a status based column and have a task on the backlog to normalize this column into a code based table.

## Schema Spy

Schema spy is a great tool to visualize the database schema. This tool brings immense value to any project with a database and assists in automating the creation of a data dictionary.