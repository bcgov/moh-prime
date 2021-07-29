using System;
using System.Collections.Generic;

namespace Prime.ViewModels.Plr
{
    public class PlrViewModel
    {
        public int Id { get; set; }

        public string IdentifierType { get; set; }

        public string CollegeId { get; set; }

        public string ProviderRoleType { get; set; }

        public string NamePrefix { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string LastName { get; set; }

        public string StatusCode { get; set; }

        public string StatusReasonCode { get; set; }

        public ICollection<string> Expertise { get; set; }

        public DateTimeOffset UpdatedTimeStamp { get; set; }

        public DateTimeOffset StatusStartDate { get; set; }
    }
}
