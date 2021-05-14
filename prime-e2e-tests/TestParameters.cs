using static System.Environment;

namespace TestPrimeE2E
{
    public static class TestParameters
    {
        public static readonly string EnrollmentUrl = GetEnvironmentVariable("ENROLLMENT_URL") ?? "http://localhost:4200/info";

        public static readonly string SiteRegistrationUrl = GetEnvironmentVariable("SITEREGISTRATION_URL") ?? "http://localhost:4200/site";

        public static readonly string BcscId = GetEnvironmentVariable("BCSC_ID");

        public static readonly string BcscPassword = GetEnvironmentVariable("BCSC_PASSWORD");

        // Path to the business licence file to be uploaded
        public static readonly string BusinessLicencePath = GetEnvironmentVariable("BUSINESSLICENCE_PATH");
    }
}
