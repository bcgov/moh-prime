using Newtonsoft.Json;

namespace Prime.Models
{
    public class CollegePractice
    {
        public int CollegeCode { get; set; }

        [JsonIgnore]
        public College College { get; set; }

        public int PracticeCode { get; set; }

        [JsonIgnore]
        public Practice Practice { get; set; }
    }
}
