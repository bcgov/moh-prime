# optimize-prime

## TABLE OF CONTENTS


## DESCRIPTION
In this repository is a web application for submitting, reviewing, 
and approving pharmacist application information.

## HOW TO USE

The client web front end can be accessed through the root URL it is 
deployed at, http://www.optimizeprime.live. The client page
requires authentication through Google to access. 

The administrator interface for applicant viewing can be accessed at 
http://www.optimizeprime.live/dashboard/admin/applicants. 

Accessing the database can be done by creating a connection to port 
5432 using a database tool such as DBeaver.

## HOW TO DEPLOY

prime-dotnet-webapi:

- dotnet ef database update


## HOW TO DEVELOP

To get the project up and running, install Docker and run the following
Docker command:

	docker-compose up --build
	
Here are the environment variables for the docker-compose and their uses:

	DB_CONNECTION_STRING - Contains the information needed for connecting to the database.
	JWT_SIGNING_KEY - Private key for signing Json web token.
	ASPNETCORE_HTTPS_PORT - Port for redirecting insecure requests to HTTPS.
	
For full development, developer dependencies are the following:

	.NET Core SDK
	Visual Studio Code
	Gitlab
	Postman

The following technologies are used in this project:
	
	Node.js
	Angular.js
	PostgreSQL
	
To update the database schema, first update the model file in the
[Models](prime-dotnet-webapi/Models) folder, and rebuild with a docker-compose up --build
command. Then, to migrate the new model schema over to the database, run the
following command:

	dotnet ef migrations add InitialCreate
	
Your changes will be deployed automatically next time the app starts.

## PROJECT STRUCTURE OVERVIEW

[Link to architecture](documentation/Architecture.md)

[Link to test plan](documentation/TestPlan.md)

[Link to build pipeline](documentation/BuildPipeline.md)

## APACHE 2.0 LICENSE

Copyright 2019 Sierra Systems Group Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.