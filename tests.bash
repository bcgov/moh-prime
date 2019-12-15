export DISPLAY=:1.0
if [ ! -f /tmp/.X1-lock ]
then
    Xvfb :1 -screen 0 1024x768x16 -ac &
fi

uuid=$(cat /proc/sys/kernel/random/uuid)

function dotnetTests()
{
    source api.conf
    echo "Starting tests..."
    dotnet build
    echo "Beginning .NET code coverage scan..."
    coverlet "./prime-dotnet-webapi-tests/bin/Debug/netcoreapp2.2/PrimeTests.dll" --target "dotnet" --targetargs 'test . --no-build --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory ../BuildReports/UnitTests' -f opencover -o ./BuildReports/Coverage/coverage
    dotnet build-server shutdown
    echo "Beginning .NET sonar scan..."
    dotnet sonarscanner begin /k:${APP_NAME} /n:${APP_NAME} /d:sonar.host.url=http://sonarqube:9000 /d:sonar.cs.opencover.reportsPaths="./BuildReports/Coverage/coverage.opencover.xml" /d:sonar.exclusions="**/Migrations/*" /d:sonar.coverage.exclusions="**Tests*.cs","**/Migrations/*","**/Program.cs" /d:sonar.cpd.exclusions="**/Migrations/*" /d:sonar.cs.vstest.reportsPaths="./BuildReports/UnitTests/TestResults.trx" /d:sonar.cs.nunit.reportsPaths="./BuildReports/UnitTests/TestResults.xml"
    dotnet build
    dotnet-sonarscanner end
}

function angularTests()
{
    cd prime-angular-frontend
    npm install @angular/core
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
    /zap/zap.sh -cmd -quickurl http://$APP_NAME-$PROJECT_PREFIX-dev.pathfinder.gov.bc.ca -quickout /tmp/${APP_NAME}.${uuid}.xml -config api.addrs.addr.name=.* -config api.addrs.addr.regex=true -config spider.maxDuration=5 -addonupdate -addoninstall pscanrulesBeta -config connection.timeoutInSecs=600
    /sonarscanner/bin/sonar-scanner -Dsonar.projectName=${APP_NAME}.zap -Dsonar.projectKey=${APP_NAME}.zap -Dsonar.sources=${SOURCE_CONTEXT_DIR} -Dsonar.host.url=http://sonarqube:9000 -Dsonar.zaproxy.reportPath=/tmp/${APP_NAME}.${uuid}.xml
    rm -f /tmp/${APP_NAME}.${uuid}.xml
}

