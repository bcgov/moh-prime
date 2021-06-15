# Step 0 #

The easist way to run is to execute `run-tests.cmd`, and modify the contents of the batch script as needed.  No new shell session is required.  For more information, reference the steps below.


# Step 1 #

See `TestParameters.cs` for parameters.



# Step 2 #

Run all tests:  `dotnet test`

Run a subset of tests, e.g. only those related to enrollment:  `dotnet test --filter TestPrimeE2E.Enrollment.EnrollmentTests`
or even more specifically, e.g. `dotnet test --filter TestPrimeE2E.Enrollment.EnrollmentTests.MedicalOfficeAssistant_HappyPath`
