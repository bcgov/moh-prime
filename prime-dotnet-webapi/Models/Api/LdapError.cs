namespace Prime.Models.Api
{
    public class LdapThrottlingParameters
    {
        public int RemainingAttempts { get; set; }

        public int LockoutTimeInHours { get; set; }
    }
}
