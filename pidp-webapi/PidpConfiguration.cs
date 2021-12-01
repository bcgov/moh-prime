namespace Pidp
{
    public class PidpConfiguration
    {
        public static bool IsDevelopment() => EnvironmentName == Environments.Development;
        private static readonly string? EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public ConnectionStringConfiguration ConnectionStrings { get; set; } = new();


        // ------- Configuration Objects -------
        public class ConnectionStringConfiguration
        {
            public string PidpDatabase { get; set; } = string.Empty;
        }
    }
}
