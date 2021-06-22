using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels.PaperEnrollees
{
    public class PaperEnrolleeAgreementViewModel
    {
        public ICollection<EnrolleeAdjudicationDocument> EnrolleeAdjudicationDocuments { get; set; }

        // Not 100% sure how our TOA's work and if we will need another way to identify what tos is signed.
        public AgreementType? AssignedTOAType { get; set; }
    }
}
