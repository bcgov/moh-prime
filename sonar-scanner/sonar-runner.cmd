dotnet sonarscanner begin /k:"prime-dotnet-webapi" /d:sonar.host.url=http://sonar-backend-dqszvc-tools.pathfinder.gov.bc.ca /d:sonar.cs.opencover.reportsPaths="./BuildReports/Coverage/coverage.opencover.xml" /d:sonar.coverage.exclusions="**Tests*.cs","**/Migrations/*","**/Program.cs" d:sonar.cpd.exclusions="**/Migrations/*" /d:sonar.cs.vstest.reportsPaths="./BuildReports/UnitTests/TestResults.trx" /d:sonar.cs.nunit.reportsPaths="./BuildReports/UnitTests/TestResults.xml"
dotnet build
dotnet sonarscanner end
cd ../prime-angular-frontend
