using System.Collections.Generic;

namespace Prime.Models
{
    public class FakeAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public ICollection<FakeXref> Occupants { get; set; }
    }
}
