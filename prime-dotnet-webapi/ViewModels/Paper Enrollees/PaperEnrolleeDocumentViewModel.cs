using System;
using Prime.Models.Documents;

namespace Prime.ViewModels.PaperEnrollees
{
    public class PaperEnrolleeDocumentViewModel
    {
        public Guid DocumentGuid { get; set; }

        public EnrolleeAdjudicationDocumentType DocumentType { get; set; }
    }
}
