using System;
using Prime.Models;

namespace Prime.ViewModels
{
    public class AgreementVersionViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public AgreementType AgreementType { get; set; }
        public string Text { get; set; }
    }
}
