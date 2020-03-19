export uuid=$(cat /proc/sys/kernel/random/uuid)
export SONAR_URL=http://sonar-backend-dqszvc-tools.pathfinder.gov.bc.ca
function headless(){
    export DISPLAY=:1.0
    if [ ! -f /tmp/.X1-lock ]
    then
        Xvfb :1 -screen 0 1024x768x16 -ac &
    fi
}

function dotnetTests()
{
    source api.conf
    echo "Starting tests..."
    dotnet build
    echo "Beginning .NET code coverage scan..."
    ~/.dotnet/tools/coverlet "./bin/Debug/netcoreapp3.1/PrimeTests.dll" --target "dotnet" --targetargs 'test . --no-build --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory ../BuildReports/UnitTests' -f opencover -o ./BuildReports/Coverage/coverage
    dotnet build-server shutdown
    echo "Beginning .NET sonar scan..."
    ~/.dotnet/tools/dotnet-sonarscanner begin /k:${APP_NAME} /n:${APP_NAME} /d:sonar.host.url=${SONAR_URL} /d:sonar.cs.opencover.reportsPaths="./BuildReports/Coverage/coverage.opencover.xml" /d:sonar.exclusions="**/Migrations/*" /d:sonar.coverage.exclusions="**Tests*.cs","**/Migrations/*","**/Program.cs" /d:sonar.cpd.exclusions="**/Migrations/*" /d:sonar.cs.vstest.reportsPaths="./BuildReports/UnitTests/TestResults.trx" /d:sonar.cs.nunit.reportsPaths="./BuildReports/UnitTests/TestResults.xml"
    dotnet build -v n
    ~/.dotnet/tools/dotnet-sonarscanner end
    dotnet build-server shutdown
}

function angularTests()
{
    headless
    source frontend.conf
    cd prime-angular-frontend
    npm install @angular/core
    npm run sonar
    cd ..
}
function scan()
{
    echo "Beginning tests on Angular ..."
    angularTests # > /dev/null 2>&1 
    echo "Beginning tests on .NET ..."
    dotnetTests # > /dev/null 2>&1
}

function zap()
{
    mkdir -p zap
    source frontend.conf
    /zap/zap.sh -cmd -quickurl ${SCHEMA}://${VANITY_URL} -quickout /tmp/${APP_NAME}.${uuid}.xml -config api.addrs.addr.name=.* -config api.addrs.addr.regex=true -config spider.maxDuration=5 -addonupdate -addoninstall pscanrulesBeta -config connection.timeoutInSecs=600 -port 8080 -host 127.0.0.1
    /sonarscanner/bin/sonar-scanner -Dsonar.projectName=${APP_NAME}.zap -Dsonar.projectKey=${APP_NAME}.zap -Dsonar.sources=${SOURCE_CONTEXT_DIR} -Dsonar.host.url=${SONAR_URL} -Dsonar.zaproxy.reportPath=/tmp/${APP_NAME}.${uuid}.xml
    rm -f /tmp/${APP_NAME}.${uuid}.xml
}
