using Newtonsoft.Json;

namespace Prime.Models
{
    public class CollegeLicense : BaseAuditable
    {
        public short CollegeCode { get; set; }

        [JsonIgnore]
        public College College { get; set; }

        public short LicenseCode { get; set; }
        
        [JsonIgnore]
        public License License { get; set; }
    }
}