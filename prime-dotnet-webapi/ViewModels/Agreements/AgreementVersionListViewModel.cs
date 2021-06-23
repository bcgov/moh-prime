using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class AgreementVersionListViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset EffectiveDate { get; set; }
        public AgreementType AgreementType { get; set; }
    }
}
