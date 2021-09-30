using System;
using Prime.ViewModels;

namespace Prime.Models.Api
{
    public class EnrolleeCreatePayload
    {
        public EnrolleeCreateModel Enrollee { get; set; }

        public Guid? IdentificationDocumentGuid { get; set; }
    }
}
