function dotnetTests()
{   
    source api.conf
    echo "Starting tests..." 
    dotnet build 
    echo "Beginning .NET code coverage scan..."
    coverlet "./prime-dotnet-webapi-tests/bin/Debug/netcoreapp2.2/PrimeTests.dll" --target "dotnet" --targetargs 'test . --no-build --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory ../BuildReports/UnitTests' -f opencover -o ./BuildReports/Coverage/coverage 
    dotnet build-server shutdown
    echo "Beginning .NET sonar scan..."
    dotnet sonarscanner begin /k:${APP_NAME} /d:sonar.projectName=${APP_NAME} /d:sonar.host.url=http://sonarqube:9000 /d:sonar.cs.opencover.reportsPaths="./BuildReports/Coverage/coverage.opencover.xml" /d:sonar.exclusions="**/Migrations/*" /d:sonar.coverage.exclusions="**Tests*.cs","**/Migrations/*","**/Program.cs" /d:sonar.cpd.exclusions="**/Migrations/*" /d:sonar.cs.vstest.reportsPaths="./BuildReports/UnitTests/TestResults.trx" /d:sonar.cs.nunit.reportsPaths="./BuildReports/UnitTests/TestResults.xml"
    dotnet build
    dotnet-sonarscanner end
}

function angularTests()
{ 
    cd prime-angular-frontend
    npm run sonar
    cd ..
}

function scan() 
{
    echo "Beginning tests on .NET ..."
    dotnetTests > /dev/null 2>&1 
    echo "Beginning tests on Angular ..."
    angularTests > /dev/null 2>&1 
}

function zap()
{
    source $1.conf
    zap-$2.py -x ./${APP_NAME}.xml -t http://$APP_NAME-$PROJECT_PREFIX-dev.pathfinder.gov.bc.ca
    sonar-scanner -Dsonar.projectName=${APP_NAME} -Dsonar.projectKey=${APP_NAME} -Dsonar.sources=. -Dsonar.host.url=http://sonarqube:9000 -Dsonar.zaproxy.reportPath=./${APP_NAME}.xml
}
