using static System.Environment;

namespace TestPrimeE2E
{
    public static class TestParameters
    {
        public static readonly string EnrollmentUrl = GetEnvironmentVariable("ENROLLMENT_URL") ?? "http://localhost:4200/info";

        public static readonly string SiteRegistrationUrl = GetEnvironmentVariable("SITEREGISTRATION_URL") ?? "http://localhost:4200/site";

        public static readonly string AdminUrl = GetEnvironmentVariable("ADMIN_URL") ?? "http://localhost:4200/admin";

        public static readonly string BcscId = GetEnvironmentVariable("BCSC_ID");

        public static readonly string BcscPassword = GetEnvironmentVariable("BCSC_PASSWORD");

        public static readonly string IdirId = GetEnvironmentVariable("IDIR_ID");

        public static readonly string IdirPassword = GetEnvironmentVariable("IDIR_PASSWORD");


        // Path to the business licence file to be uploaded
        public static readonly string BusinessLicencePath = GetEnvironmentVariable("BUSINESSLICENCE_PATH");

        // Path to the signed organization agreement file to be uploaded
        public static readonly string SignedOrganizationAgreementPath = GetEnvironmentVariable("SIGNED_ORGANIZATION_AGREEMENT_PATH");

        // Path to where screenshots will be archived.  If not specified, the working directory should be the location used
        public static readonly string ScreenshotsArchivePath = GetEnvironmentVariable("SCREENSHOTS_ARCHIVE_PATH");
    }
}
