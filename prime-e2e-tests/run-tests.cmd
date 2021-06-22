SETLOCAL
SET ENROLLMENT_URL=https://dev-9c33a9-dev.apps.silver.devops.gov.bc.ca/info
SET BCSC_ID=
SET BCSC_PASSWORD=
dotnet test --filter TestPrimeE2E.Enrollment.EnrollmentTests.FullFamilyPhysician_HappyPath

SET ENROLLMENT_URL=https://dev-9c33a9-dev.apps.silver.devops.gov.bc.ca/info
SET BCSC_ID=
SET BCSC_PASSWORD=
dotnet test --filter TestPrimeE2E.Enrollment.EnrollmentTests.MedicalOfficeAssistant_HappyPath

SET SITEREGISTRATION_URL=https://pr-1332-9c33a9-dev.apps.silver.devops.gov.bc.ca/site
SET BCSC_ID=
SET BCSC_PASSWORD=
SET BUSINESSLICENCE_PATH=C:\Users\177092\Downloads\543b1v.jpg
SET SIGNED_ORGANIZATION_AGREEMENT_PATH=C:\Users\177092\Downloads\543b1v.jpg
dotnet test --filter TestPrimeE2E.SiteRegistration
