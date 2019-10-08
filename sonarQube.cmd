rem https://medium.com/agilix/collecting-test-coverage-using-coverlet-and-sonarqube-for-a-net-core-project-ef4a507d4b28
rem docker run -d --name sonarqube -p 9000:9000 -p 9092:9092 sonarqube

rem dotnet test prime-dotnet-webapi-tests/PrimeTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
dotnet build
coverlet .\prime-dotnet-webapi-tests\bin\Debug\netcoreapp2.2\PrimeTests.dll --target "dotnet" --targetargs "test .\prime-dotnet-webapi-tests --no-build" -f opencover -o prime-dotnet-webapi-tests\coverage
dotnet build-server shutdown
dotnet sonarscanner begin /k:"prime-web-api" /d:sonar.host.url=http://localhost:9000 /d:sonar.cs.opencover.reportsPaths="prime-dotnet-webapi-tests\coverage.opencover.xml" /d:sonar.coverage.exclusions="**Tests*.cs"
dotnet build
dotnet sonarscanner end