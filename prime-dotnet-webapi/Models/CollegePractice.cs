using Newtonsoft.Json;

namespace Prime.Models
{
    public class CollegePractice : BaseAuditable
    {
        public short CollegeCode { get; set; }

        [JsonIgnore]
        public College College { get; set; }

        public short PracticeCode { get; set; }
        
        [JsonIgnore]
        public Practice Practice { get; set; }
    }
}