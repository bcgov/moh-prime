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

To update the database schema, first update the model file in the
[Models](prime-dotnet-webapi/Models) folder, and rebuild using:

	docker-compose up --build

To generate a new migration file, run this command:

	dotnet ef database update

Then, to migrate the new model schema over to the database, run the
following command:

	dotnet ef migrations add InitialCreate
	
Your changes will be deployed automatically next time the app starts.

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