using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    public class CollegeLicense : IDefinable
    {
        public short CollegeCode ,

        [JsonIgnore]
        public College College ,

        public short LicenseCode ,

        [JsonIgnore]
        public License License ,
    }
}
