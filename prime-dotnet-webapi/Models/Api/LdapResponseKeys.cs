namespace Prime.Models.Api
{
    public class LdapResponseKeys
    {
        public int? RemainingAttempts { get; set; }

        public int? LockoutTimeInHours { get; set; }

        public string GisUserRole { get; set; }

        // public LdapResponseKeys(int remainingAttempts, int lockoutTimeInHours, string gisUserRole)
        // {
        //     RemainingAttempts = remainingAttempts;
        //     LockoutTimeInHours = lockoutTimeInHours;
        //     GisUserRole = gisUserRole;
        // }
    }
}
