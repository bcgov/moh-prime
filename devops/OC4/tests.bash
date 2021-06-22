export uuid=$(cat /proc/sys/kernel/random/uuid)
export SONAR_URL=http://sonar-backend-9c33a9-tools.apps.silver.devops.gov.bc.ca

function headless(){
    export DISPLAY=:1.0
    if [ ! -f /tmp/.X1-lock ]
    then
        Xvfb :1 -screen 0 1024x768x16 -ac &
    fi
}

function dotnetTests()
{
    # Clean out previous test runs before continuing
    echo "Cleaning out .sonarqube directory..."
    rm -rf ".sonarqube"
    echo "Cleaning out BuildReports directory..."
    rm -rf "BuildReports"

    curl "http://sonarqube:9000/api/qualitygates/show"

    source api.conf
    echo "Building .NET application..."
    dotnet build
    echo "Start .NET code coverage scan..."
    coverlet "./prime-dotnet-webapi-tests/bin/Debug/netcoreapp3.1/PrimeTests.dll" --target "dotnet" --targetargs 'test . --no-build --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory ./BuildReports/UnitTests' -f opencover -o ./BuildReports/Coverage/coverage
    dotnet build-server shutdown
    echo "Start .NET sonar scan..."
    dotnet sonarscanner begin /k:${APP_NAME} /n:${APP_NAME} /d:sonar.host.url=${SONAR_URL} /d:sonar.cs.opencover.reportsPaths="BuildReports/Coverage/coverage.opencover.xml" /d:sonar.exclusions="**/Migrations/*" /d:sonar.coverage.exclusions="**Tests*.cs","**/Migrations/*","**/Program.cs" /d:sonar.cpd.exclusions="**/Migrations/*" /d:sonar.cs.vstest.reportsPaths="BuildReports/UnitTests/TestResults.trx" /d:sonar.cs.nunit.reportsPaths="BuildReports/UnitTests/TestResults.xml"
    dotnet build -v n
    dotnet sonarscanner end
    dotnet build-server shutdown

    curl "http://sonarqube:9000/api/qualitygates/show"
}

function angularTests()
{
    headless
    source frontend.conf
    cd prime-angular-frontend
    echo "Pull NPM dependencies..."
    npm install @angular/core
    echo "Starting Angular tests..."
    npm run sonar
    cd ..
}

function scan()
{
    echo "Running .NET tests..."
    dotnetTests
    echo "Running Angular tests..."
    angularTests
}

function zap()
{
    mkdir -p zap
    source frontend.conf
    /zap/zap.sh -cmd -quickurl ${SCHEMA}://${VANITY_URL} -quickout /tmp/${APP_NAME}.${uuid}.xml -config api.addrs.addr.name=.* -config api.addrs.addr.regex=true -config spider.maxDuration=5 -addonupdate -addoninstall pscanrulesBeta -config connection.timeoutInSecs=600 -port 8080 -host 127.0.0.1
    /sonarscanner/bin/sonar-scanner -Dsonar.projectName=${APP_NAME}.zap -Dsonar.projectKey=${APP_NAME}.zap -Dsonar.sources=${SOURCE_CONTEXT_DIR} -Dsonar.host.url=${SONAR_URL} -Dsonar.zaproxy.reportPath=/tmp/${APP_NAME}.${uuid}.xml
    rm -f /tmp/${APP_NAME}.${uuid}.xml
}
