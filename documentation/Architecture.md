## Architectural Summary
PRIME is a traditional N-Tier application architecture. It's composed of a frontend Angular application that communicates to a RESTful .Net Core API which persists it's data in a Postgres Database. We use Keycloak to authenticate users against BCSC and SiteMinder IDIR. There is one external integration with a HIBC REST API that validates College Licences against our users.

## Technologies in use
- .Net Core 3.1 C# 
  - REST API
- Angular 7
- NGINX
- Postgresql
- Metabase
- Jenkins
- SonarQube
- SchemaSpy

## Architecture Diagram
The following is a high level application component diagram of the PRIME application
![alt text](images/architecture.png "PRIME Architecture")