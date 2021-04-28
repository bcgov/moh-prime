using static System.Environment;

namespace TestPrimeE2E
{
    public static class TestParameters
    {
        public static readonly string EnrollmentUrl = GetEnvironmentVariable("ENROLLMENT_URL") ?? "http://localhost:4200/info";

        public static readonly string BcscId = GetEnvironmentVariable("BCSC_ID");

        public static readonly string BcscPassword = GetEnvironmentVariable("BCSC_PASSWORD");
    }
}