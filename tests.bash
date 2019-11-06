function dotnetTests()
{   
    echo "Starting tests..." 
    dotnet build 
    echo "Beginning .NET code coverage scan..."
    coverlet "./bin/Debug/netcoreapp2.2/PrimeTests.dll" \ 
        -f opencover \
        -o ./BuildReports/Coverage/coverage \ 
        --target "dotnet" \ 
        --targetargs "test . --no-build --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory ./BuildReports/UnitTests"
    dotnet build-server shutdown 
    echo "Beginning .NET sonar scan..."
    dotnet sonarscanner begin \ 
        /k:"prime-dotnet-webapi" \ 
        /d:sonar.host.url=http://sonarqube:9000 \ 
        /d:sonar.cs.opencover.reportsPaths="./BuildReports/Coverage/coverage.opencover.xml" \ 
        /d:sonar.exclusions="**/Migrations/*" \ 
        /d:sonar.coverage.exclusions="**Tests*.cs","**/Migrations/*","**/Program.cs" \ 
        /d:sonar.cpd.exclusions="**/Migrations/*" \ 
        /d:sonar.cs.vstest.reportsPaths="./BuildReports/UnitTests/TestResults.trx" \ 
        /d:sonar.cs.nunit.reportsPaths="./BuildReports/UnitTests/TestResults.xml"
    dotnet-sonarscanner end
}

function angularTests()
{ 
    echo "Beginning angular/javascript sonar scan..."
    cd prime-angular-frontend
    sonar-scanner \ 
        -Dsonar.host.url=http://sonarqube:9000 \ 
        -Dsonar.projectKey=angular-frontend -Dsonar.sources=prime-angular-frontend
    cd ..
}

function scan() 
{
    echo "Beginning tests on .NET ..."
    dotnetTests > /dev/null 2>&1 
    echo "Beginning tests on Angular ..."
    angularTests > /dev/null 2>&1 
}
