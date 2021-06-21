using Prime.Models;

namespace Prime.ViewModels
{
    public class PaperEnrolleeToaViewModel
    {
        // public ICollection<SupportingDocument> SupportingDocuments { get; set; }

        // Not 100% sure how our TOA's work and if we will need another way to identify what tos is signed.
        public AgreementType? AssignedTOAType { get; set; }
    }
}
