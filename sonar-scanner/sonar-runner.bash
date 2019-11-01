#!/bin/bash
function dotnetTests()
{   
    echo "Starting tests..." 
    dotnet build 
    coverlet "./prime-dotnet-webapi-tests/bin/Debug/netcoreapp2.2/PrimeTests.dll" \ 
        --target "dotnet" \ 
        --targetargs "test . --no-build --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory ./BuildReports/UnitTests" \ 
        -f opencover -o ./BuildReports/Coverage/coverage 
    dotnet build-server shutdown 
    dotnet sonarscanner begin \ 
        /k:"prime-web-api" \ 
        /d:sonar.host.url=http://sonar-backend-dqszvc-tools.pathfinder.gov.bc.ca \ 
        /d:sonar.cs.opencover.reportsPaths="./BuildReports/Coverage/coverage.opencover.xml" \
        /d:sonar.exclusions="**/Migrations/*" \ 
        /d:sonar.coverage.exclusions="**Tests*.cs","**/Migrations/*","**/Program.cs" \
        /d:sonar.cpd.exclusions="**/Migrations/*" \ 
        /d:sonar.cs.vstest.reportsPaths="./BuildReports/UnitTests/TestResults.trx" \ 
        /d:sonar.cs.nunit.reportsPaths="./BuildReports/UnitTests/TestResults.xml"
    dotnet build
    dotnet sonarscanner end
} 
function angularTests()
{ 
    cd prime-angular-frontend
    sonar-scanner -Dsonar.host.url=http://sonar-backend-dqszvc-tools.pathfinder.gov.bc.ca
}
echo "Beginning tests in container..."
dotnetTests
angularTests
