#!/bin/bash
cd prime-dotnet-webapi-tests
./sonarQube.cmd
cd ../prime-angular-frontend
sonar-scanner -Dsonar.host.url=http://sonar-backend-dqszvc-tools.pathfinder.gov.bc.ca
