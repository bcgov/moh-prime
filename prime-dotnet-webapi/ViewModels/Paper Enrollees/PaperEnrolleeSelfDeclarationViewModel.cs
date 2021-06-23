using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels.PaperEnrollees
{
    public class PaperEnrolleeSelfDeclarationViewModel
    {
        public int EnrolleeId { get; set; }

        public int SelfDeclarationTypeCode { get; set; }

        public string SelfDeclarationDetails { get; set; }
    }
}
