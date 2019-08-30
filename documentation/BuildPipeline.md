#Overview
This is to outline and explain the technologies, methodology and logic behind the project's pipeline selection and design.

## Workflow
Developer > GitLab > GitLab CI/CD > Azure Docker Repository > Azure VM > Docker Containers > Applications

## Code Repository
We chose to use GitLab as our repository for a few reasons:
* Free, which reduces or eliminates approval processes for many organizations
* Familiar technology, leveraging existing, popular Git
* Integrated, vendor-neutral CI/CD pipeline which can be directly integrated in codebase

## CI/CD Pipeline
As mentioned before, we selected GitLab CI/CD to handle the code. 
* Having this right next to the code eliminated the need for extra overhead with another deployment mechanism like Jenkins
* Simple YAML configuration that runs container in a container that allows for public images
* Run remote docker update from the CI/CD pipleine to all hosts

Azure Container Registry was our choice for the Docker image repo because it was in the same sphere as the application hosting.

## Application Hosting
Because it wasn't a coding requirement to have all technology in containers _and_ because databases should persist, we chose to use an Azure VM to host the Docker containers.  This simplified our hosting topography and gave us more control over the hosting environment.

## TODO
* Mount database volume from an Azure storage volume for static data across multiple Azure VMs _or_
* Mount database volume from an Azure storage volume for static data across multiple Azure Kubernetes database containers 