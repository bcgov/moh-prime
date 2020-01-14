using System.Collections.Generic;

namespace Prime.Models
{
    public class FakeEnrollee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<FakeXref> Addresses { get; set; }
    }
}
