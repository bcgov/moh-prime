namespace Prime.Models.Api
{
    public class GisUserRepresentation
    {
        public string Gisuserrole { get; set; }
        public string Authenticated { get; set; }
        public string Unlocked { get; set; }
        public string Username { get; set; }
        public int? RemainingAttempts { get; set; }
        public int? LockoutTimeInHours { get; set; }

    }
}
