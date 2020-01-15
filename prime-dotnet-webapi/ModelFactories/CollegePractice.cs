using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    public class CollegePractice : IDefinable
    {
        public short CollegeCode ,

        [JsonIgnore]
        public College College ,

        public short PracticeCode ,

        [JsonIgnore]
        public Practice Practice ,
    }
}
