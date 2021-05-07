rem https://docs.sonarqube.org/latest/analysis/scan/sonarscanner-for-msbuild/
rem https://medium.com/agilix/collecting-test-coverage-using-coverlet-and-sonarqube-for-a-net-core-project-ef4a507d4b28
rem https://github.com/tonerdo/coverlet
rem docker run -d --name sonarqube -p 9000:9000 -p 9092:9092 sonarqube
rem dotnet tool install --global coverlet.console --version 1.6.0
rem dotnet tool install --global dotnet-sonarscanner --version 4.3.1
dotnet build
coverlet .\prime-dotnet-webapi-tests\bin\Debug\netcoreapp3.1\PrimeTests.dll --target "dotnet" --targetargs "test .\prime-dotnet-webapi-tests --no-build --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory ./BuildReports/UnitTests" -f opencover -o ./BuildReports/Coverage/coverage
dotnet build-server shutdown
dotnet sonarscanner begin /k:"prime-web-api" /d:sonar.host.url=http://localhost:9000 /d:sonar.cs.opencover.reportsPaths="./BuildReports/Coverage/coverage.opencover.xml" /d:sonar.exclusions="**/Migrations/*" /d:sonar.coverage.exclusions="**Tests*.cs","**/Migrations/*","**/Program.cs" /d:sonar.cpd.exclusions="**/Migrations/*" /d:sonar.cs.vstest.reportsPaths="./BuildReports/UnitTests/TestResults.trx" /d:sonar.cs.nunit.reportsPaths="./BuildReports/UnitTests/TestResults.xml"
dotnet build
dotnet sonarscanner end

dotnet build
coverlet ./prime-dotnet-webapi-tests/bin/Debug/netcoreapp3.1/PrimeTests.dll --target "dotnet" --targetargs "test ./prime-dotnet-webapi-tests --no-build --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory ./BuildReports/UnitTests" -f opencover -o ./BuildReports/Coverage/coverage


test ./prime-dotnet-webapi-tests --no-build --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory ./BuildReports/UnitTests


coverlet ./prime-dotnet-webapi-tests/bin/Debug/netcoreapp3.1/PrimeTests.dll -f opencover -o ./BuildReports/Coverage/coverage --target "dotnet" --targetargs 'test . --no-build --logger "trx;LogFileName=TestResults.trx" --logger "xunit;LogFileName=TestResults.xml" --results-directory ./BuildReports/UnitTests' 

dotnet build-server shutdown
dotnet sonarscanner begin /k:"prime-web-api" /d:sonar.host.url=http://sonarqube:9000 /d:sonar.cs.opencover.reportsPaths="./BuildReports/Coverage/coverage.opencover.xml" /d:sonar.exclusions="**/Migrations/*" /d:sonar.coverage.exclusions="**Tests*.cs","**/Migrations/*","**/Program.cs" /d:sonar.cpd.exclusions="**/Migrations/*" /d:sonar.cs.vstest.reportsPaths="./BuildReports/UnitTests/TestResults.trx" /d:sonar.cs.nunit.reportsPaths="./BuildReports/UnitTests/TestResults.xml"
dotnet build
dotnet sonarscanner end