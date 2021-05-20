SETLOCAL
SET ENROLLMENT_URL=http://localhost:4200/info
SET BCSC_ID=PRIMET026
SET BCSC_PASSWORD=98926
dotnet test --filter TestPrimeE2E.Enrollment.EnrollmentTests.FullFamilyPhysician_HappyPath
