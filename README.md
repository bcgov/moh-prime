## DESCRIPTION
PharmaNet Revisions fo Information Management Enhancements (PRIME)

## HOW TO DEPLOY

Clone a copy of the code from this repository, then deploy the code using the 
following Docker command in the optimize-prime folder:

	docker-compose up --build

## HOW TO DEVELOP

To get the project up and running, install Docker and run the following
Docker command:

	docker-compose up --build	

### LOCAL DEVELOPMENT

For local development where the web client and server are run directly off the host system some of the containers should be stopped to avoid port collisions and to reduce resources on containers that aren't required.

**Required Containers**

* primedb
* mailhog
* document-manager-backend
* prime-redis
 
To run the backend open `prime-dotnet-webapi` in VSCode and launch the API using the debugger (or F5).  For the web client run the web client locally type:

```
cd prime-angular-frontend
ng serve -o
```

To update the database schema, first update the model file in the
[Models](prime-dotnet-webapi/Models) folder, and rebuild using:

	docker-compose up --build

To generate a new migration file, run this command:

	dotnet ef migrations add InitialCreate

Then, to migrate the new model schema over to the database, run the
following command:

	dotnet ef database update
	
Your changes will be deployed automatically next time the app starts.

## DOCUMENTATION

We have further [Documentation](documentation/Readme.md) available.

## APACHE 2.0 LICENSE

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
