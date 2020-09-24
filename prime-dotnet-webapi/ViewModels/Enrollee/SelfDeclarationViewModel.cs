using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels
{
    public class SelfDeclarationViewModel
    {
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        public Enrollee Enrollee { get; set; }

        public int SelfDeclarationTypeCode { get; set; }

        public SelfDeclarationType SelfDeclarationType { get; set; }

        public string SelfDeclarationDetails { get; set; }

        public IEnumerable<string> DocumentGuids { get; set; }
    }
}
