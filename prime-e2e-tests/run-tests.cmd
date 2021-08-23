SETLOCAL

@REM SET ENROLLMENT_URL=http://localhost:4200/info
@REM SET BCSC_ID=
@REM SET BCSC_PASSWORD=
@REM dotnet test --filter TestPrimeE2E.Enrollment.EnrollmentTests.FullFamilyPhysician_HappyPath

@REM SET ADMIN_URL=http://localhost:4200/admin
@REM SET IDIR_ID=
@REM SET IDIR_PASSWORD=
@REM SET BCSC_ID=
@REM dotnet test --filter TestPrimeE2E.Admin.AdminTests.ApproveEnrollment

@REM SET ENROLLMENT_URL=http://localhost:4200/info
@REM SET BCSC_ID=
@REM SET BCSC_PASSWORD=
@REM dotnet test --filter TestPrimeE2E.Enrollment.EnrollmentTests.MedicalOfficeAssistant_HappyPath

@REM SET SITEREGISTRATION_URL=http://localhost:4200/site
@REM SET BCSC_ID=
@REM SET BCSC_PASSWORD=
@REM SET BUSINESSLICENCE_PATH=C:\Users\177092\Downloads\543b1v.jpg
@REM SET SIGNED_ORGANIZATION_AGREEMENT_PATH=C:\Users\177092\Downloads\543b1v.jpg
@REM dotnet test --filter TestPrimeE2E.SiteRegistration.SiteRegistrationTests.SiteRegInitial

@REM SET ADMIN_URL=http://localhost:4200/admin
@REM SET IDIR_ID=
@REM SET IDIR_PASSWORD=
@REM dotnet test --filter TestPrimeE2E.Admin.AdminTests.ApproveRejectUnrejectSiteRegistration

SET ADMIN_URL=http://localhost:4200/admin
SET IDIR_ID=
SET IDIR_PASSWORD=
SET HEALTH_AUTHORITY=Island Health
dotnet test --filter TestPrimeE2E.Admin.AdminTests.EnterHealthAuthorityOrgInfo
