using System;

namespace Prime.Models.AccessAgreement
{
    public interface IAccessClause
    {
        int Id { get; set; }

        string Clause { get; set; }

        DateTime EffectiveDate { get; set; }
    }
}
