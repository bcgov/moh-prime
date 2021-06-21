using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels
{
    public class PaperEnrolleeSelfDeclarationViewModel
    {
        public ICollection<SelfDeclaration> SelfDeclarations { get; set; }

        public ICollection<SelfDeclarationDocument> SelfDeclarationDocuments { get; set; }
    }
}
