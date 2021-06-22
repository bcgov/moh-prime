using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels.PaperEnrollee
{
    public class PaperEnrolleeSelfDeclarationViewModel
    {
        public ICollection<SelfDeclaration> SelfDeclarations { get; set; }

        public ICollection<SelfDeclarationDocument> SelfDeclarationDocuments { get; set; }
    }
}
