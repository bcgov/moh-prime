dotnet build
coverlet .\prime-dotnet-webapi-tests\bin\Debug\netcoreapp2.2\PrimeTests.dll --target "dotnet" --targetargs "test .\prime-dotnet-webapi-tests --no-build --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory ./BuildReports/UnitTests" -f opencover -o ./BuildReports/Coverage/coverage
dotnet build-server shutdown
dotnet sonarscanner begin /k:"prime-web-api" /d:sonar.host.url=http://localhost:9000 /d:sonar.cs.opencover.reportsPaths="./BuildReports/Coverage/coverage.opencover.xml" /d:sonar.coverage.exclusions="**Tests*.cs","**/Migrations/*","**/Program.cs" /d:sonar.cpd.exclusions="**/Migrations/*" /d:sonar.cs.vstest.reportsPaths="./BuildReports/UnitTests/TestResults.trx" /d:sonar.cs.nunit.reportsPaths="./BuildReports/UnitTests/TestResults.xml"
dotnet build
dotnet sonarscanner end
