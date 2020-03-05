using System;

namespace Prime.Models.AccessAgreement
{
    public interface IAccessClause
    {
        int Id { get; set; }

        string Clause { get; set; }

        DateTimeOffset EffectiveDate { get; set; }
    }
}
