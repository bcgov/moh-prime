using System;
using System.Collections.Generic;

namespace Prime.ViewModels
{
    public class SelfDeclarationViewModel
    {
        public int Id { get; set; }
        public int EnrolleeId { get; set; }
        public bool Answered { get; set; }
        public int SelfDeclarationTypeCode { get; set; }
        public string SelfDeclarationDetails { get; set; }
        public IEnumerable<Guid> DocumentGuids { get; set; }
    }
}
