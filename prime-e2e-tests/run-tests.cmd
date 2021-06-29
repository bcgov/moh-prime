SETLOCAL
SET ENROLLMENT_URL=http://localhost:4200/info
SET BCSC_ID=
SET BCSC_PASSWORD=
dotnet test --filter TestPrimeE2E.Enrollment.EnrollmentTests.FullFamilyPhysician_HappyPath

SET ADMIN_URL=http://localhost:4200/admin
SET IDIR_ID=
SET IDIR_PASSWORD=
dotnet test --filter TestPrimeE2E.Admin.AdminTests

SET ENROLLMENT_URL=http://localhost:4200/info
SET BCSC_ID=
SET BCSC_PASSWORD=
dotnet test --filter TestPrimeE2E.Enrollment.EnrollmentTests.MedicalOfficeAssistant_HappyPath

SET SITEREGISTRATION_URL=http://localhost:4200/site
SET BCSC_ID=
SET BCSC_PASSWORD=
SET BUSINESSLICENCE_PATH=C:\Users\177092\Downloads\543b1v.jpg
SET SIGNED_ORGANIZATION_AGREEMENT_PATH=C:\Users\177092\Downloads\543b1v.jpg
dotnet test --filter TestPrimeE2E.SiteRegistration

SET ADMIN_URL=http://localhost:4200/admin
SET IDIR_ID=
SET IDIR_PASSWORD=
dotnet test --filter TestPrimeE2E.Admin.AdminTests.ApproveRejectUnrejectSiteRegistration
