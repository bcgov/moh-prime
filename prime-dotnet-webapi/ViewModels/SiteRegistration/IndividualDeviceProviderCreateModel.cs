using System;

namespace Prime.ViewModels
{
    public class IndividualDeviceProviderChangeModel
    {
        public int? CommunitySiteId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
