using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels.PaperEnrollees
{
    public class PaperEnrolleeSelfDeclarationViewModel
    {
        public ICollection<SelfDeclaration> SelfDeclarations { get; set; }

        public ICollection<SelfDeclarationDocument> SelfDeclarationDocuments { get; set; }
    }
}
