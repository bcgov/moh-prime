# Step 1 #

Set test parameters by:  `setx ENROLLMENT_URL https://pr-1227.pharmanetenrolment.gov.bc.ca/info` 

See `TestParameters.cs` for parameters.

Check which environment variables are set by `ls env:` on Windows.


# Step 2 #

Run tests:  `dotnet test`

Run a subset of tests, e.g. only those related to enrollment:  `dotnet test --filter TestPrimeE2E.Enrollment.EnrollmentTests` 
or even more specifically, e.g. `dotnet test --filter TestPrimeE2E.Enrollment.EnrollmentTests.MedicalOfficeAssistant_HappyPath`
