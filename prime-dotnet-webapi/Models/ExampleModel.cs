using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Prime.Models
{
    [DataContract]
    public class ExampleModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}
