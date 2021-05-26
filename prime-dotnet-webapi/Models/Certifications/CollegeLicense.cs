using Newtonsoft.Json;

namespace Prime.Models
{
    public class CollegeLicense
    {
        public int CollegeCode { get; set; }

        [JsonIgnore]
        public College College { get; set; }

        public int LicenseCode { get; set; }

        [JsonIgnore]
        public License License { get; set; }
    }
}
