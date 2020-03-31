using System;

namespace Prime
{
    public static class PrimeConstants
    {
        public readonly static string ENVIRONMENT_NAME = Environment.GetEnvironmentVariable("OC_APP") ?? "local";
        public readonly static string FRONTEND_URL = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "localhost:4200";

        // Enrollee Classifications
        public const string PRIME_OBO = "OBO";
        public const string PRIME_RU = "RU";

        // Mail server
        public readonly static string MAIL_SERVER_URL = Environment.GetEnvironmentVariable("MAIL_SERVER_URL") ?? "localhost";
        public readonly static int MAIL_SERVER_PORT = int.Parse(Environment.GetEnvironmentVariable("MAIL_SERVER_PORT") ?? "1025");

        // Pharmanet college validation API
        public readonly static string PHARMANET_API_URL = Environment.GetEnvironmentVariable("PHARMANET_API_URL") ?? "";
        public readonly static string PHARMANET_API_USERNAME = Environment.GetEnvironmentVariable("PHARMANET_API_USERNAME") ?? "";
        public readonly static string PHARMANET_API_PASSWORD = Environment.GetEnvironmentVariable("PHARMANET_API_PASSWORD") ?? "";
        public readonly static string PHARMANET_SSL_CERT_FILENAME = Environment.GetEnvironmentVariable("PHARMANET_SSL_CERT_FILENAME") ?? "";
        public readonly static string PHARMANET_SSL_CERT_PASSWORD = Environment.GetEnvironmentVariable("PHARMANET_SSL_CERT_PASSWORD") ?? "";

        // Logging
        public readonly static string LOG_FILE_PATH = Environment.GetEnvironmentVariable("LOG_FILE_PATH") ?? "./logs";
    }
}
